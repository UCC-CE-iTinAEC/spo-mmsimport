using CommandLine;
using DataAccess;
using Microsoft.SharePoint.Client;
using SPOMMSImport.Extensions;
using System;
using System.Collections.Generic;
using System.Net;

namespace SPOMMSImport
{
    class Program
    {
        static int Main(string[] args) {
            return CommandLine.Parser.Default.ParseArguments<ImportOptions, ClearOptions>(args)
                .MapResult(
                    (ImportOptions opts) => importTerms(opts),
                    (ClearOptions opts) => clearTerms(opts),
                    errs => 1);
        }

        static int importTerms(ImportOptions opts) {
            try {
                // parse csv
                var data = DataTable.New.ReadCsv(opts.FilePath);
                var terms = new List<TermData>();
                foreach (var row in data.Rows) {
                    var term = new TermData {
                        Term = row.GetValueOrEmpty("Term")
                    };
                    foreach (var col in data.ColumnNames) {
                        if (col.StartsWith("Prop_")) {
                            var prop = col.Substring(5);
                            term.Properties[prop] = row.GetValueOrEmpty(col);
                        }
                    }
                    terms.Add(term);
                }

                // initialize context
                using (var ctx = new ClientContext(opts.Url)) {
                    ctx.AuthenticationMode = ClientAuthenticationMode.Default;
                    ctx.Credentials = getCredentials(opts.Username, opts.Password);

                    // import terms
                    var mgr = new MetadataManager(ctx, opts.TermStore);
                    mgr.ImportTerms(opts.TermGroup, opts.TermSet, terms);
                }
            } catch (Exception ex) {
                Console.WriteLine("An error occurred");
                Console.WriteLine(ex.Message);
                return 1;
            }

            return 0;
        }

        static int clearTerms(ClearOptions opts) {
            try {
                // initialize context
                using (var ctx = new ClientContext(opts.Url)) {
                    ctx.AuthenticationMode = ClientAuthenticationMode.Default;
                    ctx.Credentials = getCredentials(opts.Username, opts.Password);

                    // import terms
                    var mgr = new MetadataManager(ctx, opts.TermStore);
                    mgr.ClearTerms(opts.TermGroup, opts.TermSet);
                }
            } catch (Exception ex) {
                Console.WriteLine("An error occurred");
                Console.WriteLine(ex.Message);
                return 1;
            }

            return 0;
        }

        static ICredentials getCredentials(string username, string password) {
            return new SharePointOnlineCredentials(username, password.ToSecureString());
        }
    }
}

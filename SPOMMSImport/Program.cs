using CommandLine.Text;
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
        static void Main(string[] args) {
            var opts = new Options();
            var result = CommandLine.Parser.Default.ParseArguments(args, opts);
            if (!result) {
                Console.Write(HelpText.AutoBuild(opts).ToString());
                return;
            }

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
                var mgr = new MetadataImporter(ctx, opts.TermStore);
                mgr.ImportTerms(opts.TermGroup, opts.TermSet, terms);
            }
        }

        static ICredentials getCredentials(string username, string password) {
            return new SharePointOnlineCredentials(username, password.ToSecureString());
        }
    }
}

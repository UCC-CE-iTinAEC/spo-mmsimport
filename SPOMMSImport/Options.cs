using CommandLine;

namespace SPOMMSImport
{
    [Verb("import", HelpText = "Import terms from a csv file into a term set in the term store")]
    public class ImportOptions
    {
        [Option('f', "file", Required = true, HelpText = "Data file containing data to import.")]
        public string FilePath { get; set; }
        
        [Option('l', "url", Required = true, HelpText = "Url to the site or tenant")]
        public string Url { get; set; }

        [Option('u', "username", Required = true, HelpText = "Username to authenticate")]
        public string Username { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password to authenticate")]
        public string Password { get; set; }

        [Option('t', "termStore", Required = false, HelpText = "Term store to import to.  If empty the default site collection term store will be used" )]
        public string TermStore { get; set; }

        [Option('g', "termGroup", Required = true, HelpText = "Term group containing the term set to import to.")]
        public string TermGroup { get; set; }

        [Option('s', "termSet", Required = true, HelpText = "Term set to import terms into.")]
        public string TermSet { get; set; }
    }

    [Verb("clear", HelpText = "Remove all terms from a term set in the term store.")]
    public class ClearOptions
    {
        [Option('l', "url", Required = true, HelpText = "Url to the site or tenant")]
        public string Url { get; set; }

        [Option('u', "username", Required = true, HelpText = "Username to authenticate")]
        public string Username { get; set; }

        [Option('p', "password", Required = true, HelpText = "Password to authenticate")]
        public string Password { get; set; }

        [Option('t', "termStore", Required = false, HelpText = "Term store to modify.  If empty the default site collection term store will be used")]
        public string TermStore { get; set; }

        [Option('g', "termGroup", Required = true, HelpText = "Term group containing the term set to clear.")]
        public string TermGroup { get; set; }

        [Option('s', "termSet", Required = true, HelpText = "Term set to clear.")]
        public string TermSet { get; set; }
    }
}

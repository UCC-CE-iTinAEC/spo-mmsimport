using CommandLine;

namespace SPOMMSImport
{
    public class Options
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
}

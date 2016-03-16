using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPOMMSImport
{
    public class TermData
    {
        public string Term { get; set; }
        public IDictionary<string, string> Properties { get; set; }

        public TermData() {
            Properties = new Dictionary<string, string>();
        }
    }
}

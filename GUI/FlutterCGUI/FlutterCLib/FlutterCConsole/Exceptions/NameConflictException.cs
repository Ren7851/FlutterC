using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    class NameConflictException : ParseException
    {
        public string s;
        public NameConflictException(string s, string function)
        {
            this.s = s;
            tokens = new List<string>();
            tokens.Add(s);
            this.function = function;
        }

        public override string what()
        {
            return "name conflict " + s + " ";
        }
    }
}

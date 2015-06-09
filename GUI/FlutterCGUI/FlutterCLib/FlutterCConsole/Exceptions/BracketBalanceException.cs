using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    public class BracketBalanceException : ParseException
    {
        public string kindOfBracket;

        public BracketBalanceException(string kind, int i, List<string> tokens) {
            this.kindOfBracket = kind;
            this.tokens = tokens;
        }

        public override string what()
        {
            return kindOfBracket+" balance violation: ";
        }
    }
}

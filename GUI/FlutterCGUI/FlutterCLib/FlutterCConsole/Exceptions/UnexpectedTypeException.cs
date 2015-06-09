using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    public class UnexpectedTypeException : ParseException
    {
        int i;
        public UnexpectedTypeException(int i, List<string> tokens)
        {
            this.i = i;
            this.tokens = tokens;
        }

        public override string what()
        {
            return "unexpected type " + tokens[i];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    public class SemicolonException : ParseException{
        public SemicolonException(int i, List<string> tokens)
        {
            this.tokens = tokens;
        }


        public override string what()
        {
            return "semicolon issue " ;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    class FunctionRecognisionException : ParseException
    {
        public FunctionRecognisionException(int i, List<string> tokens) {
            this.tokens = tokens;
        }


        public override string what()
        {
            return "can't recognise function ";
        }
    }
}

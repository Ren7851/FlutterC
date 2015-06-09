using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    public class BlockException : ParseException
    {
        public BlockException(int i, List<string> tokens, string function)
        {
            this.tokens = tokens;
            this.function = function;
        }

        public override string what()
        {
            return "wrong block: ";
        }
    }
}

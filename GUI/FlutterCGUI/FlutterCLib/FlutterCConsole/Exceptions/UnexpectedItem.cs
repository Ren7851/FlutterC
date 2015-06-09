using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    class UnexpectedItem : ParseException
    {
        string que;
        public UnexpectedItem(List<string> s, string que, string function){
            tokens = s;
            this.que = que;
            this.function = function;
        }

        public override string what()
        {
            return "unexpected token "+que+" ";
        }
    }
}

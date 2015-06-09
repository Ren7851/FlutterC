using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    public class AccessViolationException : RuntimeFlutterCException
    {
        public int val;

        public AccessViolationException(int address){
            this.val = address;
        }
    }
}

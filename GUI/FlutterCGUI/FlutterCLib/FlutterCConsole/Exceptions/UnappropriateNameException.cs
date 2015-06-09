using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    public class UnappropriateNameException : ParseException
    {
        public string name;
        public UnappropriateNameException(int i, List<string> tokens, string name)
        {
            this.tokens = tokens;
            this.name = name;
        }

        public override string what()
        {
            return "unappropriate name "+name+" ";
        }
    }
}

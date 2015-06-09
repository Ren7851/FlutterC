using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    public abstract class ParseException : FlutterCException
    {
        public List<string> tokens;
        public string function;

        public abstract string what();

        public override string Message
        {
            get
            {
                string res = what() + "in <";
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (i != tokens.Count-1)
                    {
                        res += tokens[i]+" ";
                    }
                    else
                    {
                        res += tokens[i];
                    }
                }
                res += ">\n";
                if(function!=null){
                    res += "in function " + function;
                }
                return res;
            }
        }
    }
}

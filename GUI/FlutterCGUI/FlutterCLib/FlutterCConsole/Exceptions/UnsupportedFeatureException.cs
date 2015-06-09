using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Exceptions
{
    class UnsupportedFeatureException : FlutterCException
    {
        string s;
        public UnsupportedFeatureException(string s)
        {
            this.s = s;
        }

        public override string Message
        {
            get
            {
                return "Feature " + s + " is unsupported";
            }
        }

    }
}

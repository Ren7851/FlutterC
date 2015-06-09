using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class CastInfo : Value
    {
        public CastInfo(string typetoken)
        {
            this.typetoken = typetoken;
        }

        public override string ToString()
        {
            return "cast type = "+this.typetoken;
        }
    }
}

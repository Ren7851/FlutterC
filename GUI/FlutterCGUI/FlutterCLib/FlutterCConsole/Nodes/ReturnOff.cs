using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class ReturnOff : Off
    {
        public override string print(int o)
        {
            return new string(' ', o) + "returnoff ";
        }

        public override void execute()
        {
        }
    }
}

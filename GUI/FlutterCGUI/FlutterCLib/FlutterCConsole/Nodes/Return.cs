using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class Return : Node
    {
        public override string print(int o)
        {
            return new string(' ', o) + "return ";
        }

        public override void execute()
        {
            Memory.getInstance().functionReturnFlag = true;
        }
    }
}

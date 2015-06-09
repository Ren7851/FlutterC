using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class Continue : Node
    {
        public int index;

        public Continue(int index)
        {
            this.index = index;
        }

        public override string print(int o)
        {
            return new string(' ', o) + "continue "+index;
        }

        public override void execute()
        {
            Memory.getInstance().continueFlag = true;
            Memory.getInstance().continueIndex = index;
        }
    }
}

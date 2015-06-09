using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class Break : Node
    {
        public int index = 0;

        public Break(int index)
        {
            this.index = index;
        }

        public override string print(int o)
        {
            return new string(' ', o) + "break "+index;
        }

        public override void execute()
        {
            Memory.getInstance().breakFlag = true;
            Memory.getInstance().breakIndex = index;
        }
    }
}

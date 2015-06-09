using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Nodes
{
    class Pop : Node
    {
        public Pop()
        {
            this.name = "pop";
        }

        public override string print(int o)
        {
            return new string(' ', o) + "pop ";
        }

        public override void execute()
        {
            Memory.getInstance().nodeExecuted(this);
            Memory.getInstance().pop();
        }
    }
}

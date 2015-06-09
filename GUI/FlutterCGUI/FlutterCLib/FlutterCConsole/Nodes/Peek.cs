using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class Peek : Operator
    {
        public override void execute()
        {
            calculateRes();
        }

        public override string ToString()
        {
            return print(0);
        }

        public override string print(int o)
        {
            return new string(' ', o) + "peek";
        }

        public override void calculateRes()
        {
            Memory.getInstance().nodeExecuted(this);
            base.calculateRes();
            this.res = Memory.getInstance().peek();
        }
    }
}

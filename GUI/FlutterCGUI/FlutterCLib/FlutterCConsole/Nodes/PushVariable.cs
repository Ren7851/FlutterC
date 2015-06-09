using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class PushVariable : Command
    {
        public Variable var;



        public PushVariable(Variable val) {
            var = val;
        }

        public override string print(int o)
        {
            return new string(' ', o) + "push var "+var.ToString();
        }

        public override void execute()
        {
            Memory.getInstance().push(var.values.Peek());
        }
    }
}

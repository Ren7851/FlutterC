using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole
{
    public class Push : Node
    {
        public Value value;

        public Push(Value val) {
            value = val;
        }

        public override string print(int o)
        {
            return new string(' ', o) + "push "+value.ToString();
        }

        public override void execute()
        {
            Memory.getInstance().push(value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public abstract class Command : Node
    {
        public override string print(int o)
        {
            return new string(' ', o) + name;
        }
    }
}

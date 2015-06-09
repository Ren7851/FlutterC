using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class ContinueOff : Off
    {

        public int index;
        public ContinueOff(int index)
        {
            this.index = index;
        }

        public override string print(int o)
        {
            return new string(' ', o) + "continueoff "+index;
        }

        public override void execute()
        {
        }
    }
}

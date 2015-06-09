using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class ClearStack : Node
    {
        public override string print(int o)
        {
            return "clear stack";
        }

        public override void execute()
        {
            while(!(Memory.instance.peek() is FlutterCConsole.CallMarker)){
                Memory.instance.pop();
            }
            Memory.instance.pop();
        }
    }


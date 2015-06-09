using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class Ampersand : ArithmeticsCommand
    {
        public Ampersand()
        {
            this.name = "&&&";
        }

        public override void setOperation()
        {
        }

        public override void execute()
        {
            Memory.getInstance().nodeExecuted(this);
            Value b = Memory.getInstance().peek();
            Memory.getInstance().pop();
            Value res = b.operatorAmpersand();
            Memory.getInstance().push(res);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class LogicalNot : ComprasionCommand
    {
        public LogicalNot()
        {
            this.name = "!";
        }

        public override void setOperation()
        {
        }

        public override void execute()
        {
            Memory.getInstance().nodeExecuted(this);
            Value b = Memory.getInstance().peek();
            Memory.getInstance().pop();

            Value res = b.makeCopy();

            res.operatorLogicalNot();

            Memory.getInstance().push(res);
        }
    }
}

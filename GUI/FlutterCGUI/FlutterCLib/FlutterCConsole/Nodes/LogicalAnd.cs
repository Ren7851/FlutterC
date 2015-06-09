using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class LogicalAnd : ComprasionCommand
    {
        public LogicalAnd()
        {
            this.name = "&&";
        }

        public override void setOperation()
        {
            comprasionOperation = res.operatorLogicalAnd;
        }
    }
}

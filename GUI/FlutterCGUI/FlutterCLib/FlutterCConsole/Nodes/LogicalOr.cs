using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class LogicalOr : ComprasionCommand
    {
        public LogicalOr()
        {
            this.name = "||";
        }

        public override void setOperation()
        {
            comprasionOperation = res.operatorLogicalOr;
        }
    }
}

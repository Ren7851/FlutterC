using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Nodes
{
    class LessEqual : FlutterCConsole.ComprasionCommand
    {
        public LessEqual()
        {
            this.name = "<=";
        }

        public override void setOperation()
        {
            comprasionOperation = res.operatorLessEqual;
        }

    }
}

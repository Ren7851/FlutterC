using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Nodes
{
    class MoreEqual : FlutterCConsole.ComprasionCommand
    {

        public MoreEqual()
        {
            this.name = ">=";
        }

        public override void setOperation()
        {
            comprasionOperation = res.operatorMoreEqual;
        }

    }
}

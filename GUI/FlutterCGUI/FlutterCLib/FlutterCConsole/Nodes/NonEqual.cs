using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Nodes
{
    class NonEqual : FlutterCConsole.ComprasionCommand
    {
        public NonEqual()
        {
            this.name = "!=";
        }

        public override void setOperation()
        {
            comprasionOperation = res.operatorNonEqual;
        }
    }
}

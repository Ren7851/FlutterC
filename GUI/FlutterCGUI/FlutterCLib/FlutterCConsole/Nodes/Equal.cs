using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Nodes
{
    class Equal : FlutterCConsole.ComprasionCommand
    {
        public Equal()
        {
            this.name = "==";
        }

        public override void setOperation()
        {
            comprasionOperation = res.operatorEqual;
        }

    }
}

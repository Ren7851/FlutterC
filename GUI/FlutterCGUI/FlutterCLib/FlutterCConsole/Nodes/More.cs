using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Nodes
{
    class More : FlutterCConsole.ComprasionCommand
    {

        public More()
        {
            this.name = ">";
        }

        public override void setOperation()
        {
            comprasionOperation = res.operatorMore;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole.Nodes
{
    class Less : FlutterCConsole.ComprasionCommand
    {

        public Less()
        {
            this.name = "<";
        }

        public override void setOperation()
        {
            comprasionOperation = res.operatorLess;
        }


    }
}

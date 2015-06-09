using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class RightShift : BitwiseCommand
    {
        public override void setOperation()
        {
            operation = res.operatorRightShift;
        }
    }
}

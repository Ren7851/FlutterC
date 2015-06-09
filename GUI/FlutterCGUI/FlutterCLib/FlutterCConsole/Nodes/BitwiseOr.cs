using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class BitwiseOr : BitwiseCommand
    {
        public override void setOperation()
        {
            operation = res.operatorBitwiseOr;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class Mod : ArithmeticsCommand
    {
        public override void setOperation()
        {
            operation = res.operatorMod;
        }
    }
}

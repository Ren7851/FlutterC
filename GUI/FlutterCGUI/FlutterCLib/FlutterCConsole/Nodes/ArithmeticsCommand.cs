using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public delegate void PerformDelegate(Value x);
    public delegate Value ComprasionDelegate(Value x);

    public abstract class ArithmeticsCommand : PrimitiveValueCommand
    {
    }
}

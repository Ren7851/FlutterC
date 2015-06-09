using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlutterCConsole.Exceptions;

namespace FlutterCConsole.Nodes
{
    public class InversedAssignment : Command
    {
        public InversedAssignment()
        {
            name = "\\=";
        }

        public override void execute()
        {
            Memory.getInstance().nodeExecuted(this);
            Value b = Memory.getInstance().peek();
            Memory.getInstance().pop();

            Value a = Memory.getInstance().peek();
            Memory.getInstance().pop();

            if (!b.isPointer())
            {
                a = a.implicitCast(b.typetoken);
            }
            else
            {
                if (a.typetoken != b.typetoken)
                {
                    throw new TypeException();
                }
            }
            b.copyFrom(a);
            Memory.getInstance().push(b);
        }
    }
}

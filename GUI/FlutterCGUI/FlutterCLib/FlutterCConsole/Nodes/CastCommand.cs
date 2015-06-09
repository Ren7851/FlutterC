using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;

namespace FlutterCConsole
{
    public class CastCommand : Command
    {
        public CastCommand()
        {
            this.name = "cast";
        }

        public override void execute()
        {
            Memory.getInstance().nodeExecuted(this);
            Value a = Memory.getInstance().peek();
            Memory.getInstance().pop();

            Value b = Memory.getInstance().peek();
            Memory.getInstance().pop();

            if(b is CastInfo){
                if (a.isPointer())
                {
                    a = a.explicitCast(b.typetoken);
                }
                else
                {
                    a = a.explicitCast(b.typetoken);
                }
            }
            else
            {
                throw new TypeException();
            }

            Memory.getInstance().push(a);
        }
    }
}

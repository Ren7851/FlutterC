using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public abstract class ComprasionCommand : PrimitiveValueCommand
    {
        public ComprasionDelegate comprasionOperation;

        public override void execute()
        {
            Memory.getInstance().nodeExecuted(this);
            Value a = Memory.instance.peek();
            Memory.instance.pop();

            Value b = Memory.instance.peek();
            Memory.instance.pop();


            copyA = a.makeCopy();

            res = b.makeCopy();


            if (!copyA.isPointer() && !res.isPointer())
            {
                char p = copyA.typetoken[0];
                char q = copyA.typetoken[0];
                if (p == 'd' || q == 'd')
                {
                    copyA.implicitCast("double");
                    res.implicitCast("double");
                }
                else
                {
                    if (p == 'f' || q == 'f')
                    {
                        copyA.implicitCast("float");
                        res.implicitCast("float");
                    }
                    else
                    {
                        if (p == 'c')
                        {
                            copyA.implicitCast("int");
                        }

                        if (q == 'c')
                        {
                            copyA.implicitCast("int");
                        }

                        if (p == 'l' || q == 'l')
                        {
                            copyA.implicitCast("long");
                            res.implicitCast("long");
                        }
                    }
                }

            }
            else
            {
                if (copyA.isPointer() && res.isPointer())
                {
                    //throw new FlutterCConsole.Exceptions.TypeException();
                }
                else
                {
                    if (!copyA.isPointer())
                    {
                        copyA.implicitCast("int");
                    }
                    else
                    {
                        throw new FlutterCConsole.Exceptions.TypeException();
                    }
                }
            }

            setOperation();

            res = comprasionOperation(copyA);


            Memory.getInstance().push(res);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;

namespace FlutterCConsole
{
    public class Asterix : PointerCommand
    {
        public Asterix()
        {
            this.name =  "***";
        }

        public override void execute()
        {
            base.execute();
            Value a = Memory.getInstance().peek();
            
            Memory.getInstance().pop();

            if(a.isPointer()){
                Value asterix = a.operatorAsterix();
                Memory.getInstance().push(asterix);
            }
            else
            {
                throw new TypeException();
            }
        }
    }
}

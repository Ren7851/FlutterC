using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class PostIncrement : ArithmeticsCommand
    {
        public PostIncrement()
        {
            this.name = "v++";
        }

        public override void setOperation()
        {
        }

        public override void execute()
        {
            Memory.getInstance().nodeExecuted(this);
            Value b = Memory.getInstance().peek();
            Memory.getInstance().pop();

            Value res = b.makeCopy();
            b.value++;
            Memory.getInstance().push(res);
        }
    }
}

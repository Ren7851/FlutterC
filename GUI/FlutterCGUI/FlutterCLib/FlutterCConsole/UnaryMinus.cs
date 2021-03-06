﻿namespace FlutterCConsole
{
    public class UnaryMinus : ArithmeticsCommand
    {
        public UnaryMinus()
        {
            this.name = "u-";
        }

        public override void setOperation()
        {
        }

        public override void execute()
        {
            Memory.getInstance().nodeExecuted(this);
            Value b = Memory.getInstance().peek();
            Memory.getInstance().pop();

            Value res = (b.makeCopy());

            res.value *= -1;

            Memory.getInstance().push(res);
        }
    }
}

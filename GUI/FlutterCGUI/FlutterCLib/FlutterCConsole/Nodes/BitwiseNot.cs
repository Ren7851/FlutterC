namespace FlutterCConsole
{
    public class BitwiseNot : BitwiseCommand
    {
        public BitwiseNot()
        {
            this.name = "~";
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

            res.operatorTilda();

            Memory.getInstance().push(res);
        }
    }
}


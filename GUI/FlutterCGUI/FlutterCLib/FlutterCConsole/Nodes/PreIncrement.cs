namespace FlutterCConsole
{
    public class PreIncrement : ArithmeticsCommand
    {
        public PreIncrement()
        {
            this.name = "++v";
        }

        public override void setOperation()
        {
        }

        public override void execute()
        {
            Memory.getInstance().nodeExecuted(this);
            Value b = Memory.getInstance().peek();
            Memory.getInstance().pop();
            b.value++;
            Memory.getInstance().push(b.makeCopy());
        }
    }
}

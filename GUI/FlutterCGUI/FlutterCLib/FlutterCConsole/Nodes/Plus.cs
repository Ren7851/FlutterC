namespace FlutterCConsole
{
    public class Plus : ArithmeticsCommand
    {
        public Plus()
        {
            this.name = "+";
        }

        public override void setOperation()
        {
            operation = res.operatorPlus;
        }
    }
}

namespace FlutterCConsole
{
    public class Mult : ArithmeticsCommand
    {
        public Mult()
        {
            this.name = "*";
        }

        public override void setOperation()
        {
            operation = res.operatorMult;
        }
    }
}

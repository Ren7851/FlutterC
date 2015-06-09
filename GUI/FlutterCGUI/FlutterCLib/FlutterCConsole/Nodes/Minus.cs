namespace FlutterCConsole
{
    public class Minus : ArithmeticsCommand
    {
        public Minus()
        {
            this.name = "-";
        }


        public override void setOperation()
        {
            operation = res.operatorMinus;
        }
    }
}

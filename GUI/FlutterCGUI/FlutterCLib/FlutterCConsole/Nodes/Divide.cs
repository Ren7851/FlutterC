namespace FlutterCConsole
{
    public class Divide : ArithmeticsCommand
    {
        public Divide()
        {
            this.name = "/";
        }

        public override void setOperation()
        {
            operation = res.operatorDivide;
        }
    }
}

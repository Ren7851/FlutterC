namespace FlutterCConsole
{
    public class BreakOff : Off
    {
        public int index;
        public BreakOff(int index)
        {
            this.index = index;
        }

        public override string print(int o)
        {
            return new string(' ', o) + "breakoff "+index;
        }

        public override void execute()
        {
        }
    }
}

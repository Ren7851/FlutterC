using FlutterCConsole.Exceptions;

namespace FlutterCConsole
{
    public class Assignment : Command
    {
        public Assignment() {
            name = "=";
        }

        public static void assignTo(Value var, Value value)
        {
            if (!var.isPointer())
            {
                value = value.implicitCast(var.typetoken);
            }
            else
            {
                if (value.typetoken != var.typetoken)
                {
                    if (var.value == 0)
                    {
                        value.value = 0;
                    }
                    else
                    {
                        throw new TypeException();
                    }
                }
            }
            var.copyFrom(value);
        }

        public override void execute()
        {
            base.execute();
            Value a = Memory.getInstance().peek();
            Memory.getInstance().pop();

            Value b = Memory.getInstance().peek();
            Memory.getInstance().pop();

            assignTo(b, a);
            Memory.getInstance().push(b);
        }
    }
}

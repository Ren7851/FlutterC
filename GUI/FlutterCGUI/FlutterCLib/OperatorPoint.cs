
public class OperatorPoint : Node
{
    public OperatorPoint()
    {
        this.name = "get field";
    }

    public override void execute()
    {
        base.execute();
        Memory.getInstance().nodeExecuted(this);
        Value a = Memory.getInstance().peek();
        Memory.getInstance().pop();

        Value b = Memory.getInstance().peek();
        Memory.getInstance().pop();

        if (a is FieldName)
        {
            Memory.getInstance().push(b.getField(((FieldName)a).fieldName));
        }
        else
        {
            throw new FlutterCConsole.Exceptions.TypeException();
        }
    }
}


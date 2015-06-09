using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Malloc : NativeFunctionBody
{
    public Malloc()
    {
        FunctionBodyStringNames fbsn = new FunctionBodyStringNames();
        int argNumber = 1;

        List<string> l = new List<string>();
        l.Add("int");
        fbsn.argsTypes.Add(l);
        fbsn.argNames.Add("size");
        fbsn.setReturnType(l);

        string functionName = "malloc";
        init(argNumber, functionName, fbsn);
    }

    public override void execute()
    {
        Value arg = Memory.getInstance().peek();
        Memory.getInstance().pop();
        Value pointer = new Value("char*", true);
        Memory.getInstance().cellMemory.malloc(pointer, (int)arg.value);
        Memory.getInstance().push(pointer);
    }
}

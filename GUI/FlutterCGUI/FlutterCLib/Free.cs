using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Free : NativeFunctionBody
{
    public Free()
    {
        FunctionBodyStringNames fbsn = new FunctionBodyStringNames();
        int argNumber = 1;

        List<string> l = new List<string>();
        l.Add("int");
        fbsn.argsTypes.Add(l);
        fbsn.argNames.Add("size");
        fbsn.setReturnType(l);

        string functionName = "free";
        init(argNumber, functionName, fbsn);
    }

    public override void execute()
    {
        Value arg = Memory.getInstance().peek();
        Memory.getInstance().pop();
        Memory.getInstance().cellMemory.free((int)arg.value);
    }
}

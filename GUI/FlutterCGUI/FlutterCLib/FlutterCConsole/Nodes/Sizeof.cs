using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Sizeof : NativeFunctionBody
{

    public Sizeof()
    {
        FunctionBodyStringNames fbsn = new FunctionBodyStringNames();
        int argNumber = 1;

        List<string> l = new List<string>();
        l.Add("int");
        fbsn.argsTypes.Add(l);
        fbsn.argNames.Add("size");
        fbsn.setReturnType(l);

        string functionName = "sizeof";
        init(argNumber, functionName, fbsn);
    }

    public override void execute()
    {
        Value q = Memory.instance.peek();
        Memory.instance.pop();

        int size = 0;
        if(q.isPointer()){
            size = Memory.instance.cellMemory.sizes[(int)q.value];
        }
        else
        {
            size = 1;
        }

        Value g = new Value("int", false);
        g.value = size;

        Memory.instance.push(g);
    }
}

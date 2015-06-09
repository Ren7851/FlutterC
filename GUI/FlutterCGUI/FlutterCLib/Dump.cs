using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Dump : NativeFunctionBody{
    public Dump()
    {
        FunctionBodyStringNames fbsn = new FunctionBodyStringNames();
        int argNumber = 0;

        List<string> l = new List<string>();
        l.Add("char");
        fbsn.setReturnType(l);

        string functionName = "dump";
        init(argNumber, functionName, fbsn);
    }

    public override void execute()
    {
        if (Memory.instance.isAlgoRegime)
        {
            return;
        }
        string outp = Memory.instance.memoryDumpFun(true);
        Memory.instance.output += outp;
    }
}
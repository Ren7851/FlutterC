using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Srand : NativeFunctionBody
{
    public Srand()
    {
        FunctionBodyStringNames fbsn = new FunctionBodyStringNames();
        int argNumber = 1;

        List<string> l = new List<string>();
        l.Add("int");
        fbsn.argsTypes.Add(l);
        fbsn.argNames.Add("seed");
        fbsn.setReturnType(l);

        string functionName = "srand";
        init(argNumber, functionName, fbsn);
    }

    public override void execute()
    {
        base.execute();
        Value arg = args[0].get();
        Rand.random = new Random((int)arg.value);
    }
}


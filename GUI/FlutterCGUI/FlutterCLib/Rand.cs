using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Rand : NativeFunctionBody
{
    private static Value rand = new Value("int", false);
    public static Random random = new Random(1);

    public Rand()
    {
        FunctionBodyStringNames fbsn = new FunctionBodyStringNames();
        int argNumber = 0;
        List<string> l = new List<string>();
        l.Add("int");
        fbsn.setReturnType(l);
        string functionName = "rand";

        init(argNumber, functionName, fbsn);

        this.addNode(new FlutterCConsole.PushVariable(returnVar));
        this.addNode(new FlutterCConsole.Push(rand));
        this.addNode(new FlutterCConsole.Assignment());
    }

    public override void execute()
    {
        rand.value = random.Next();
        base.execute();
    }
}


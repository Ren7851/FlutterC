using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Scanf : NativeFunctionBody
{
    public Scanf()
    {
        this.functionName = "scanf";
        this.name = functionName;
        this.fbsn = new FunctionBodyStringNames();
    }

    public override void execute()
    {
        List<decimal> vs = new List<decimal>();

        while (true)
        {
            decimal pk = Memory.instance.peek().value;

            if (Memory.instance.peek().typetoken == "char*")
            {
                char q = (char)Memory.instance.cellMemory.getValue((int)pk).value;
                if (q == 'ё')
                {
                    break;
                }
            }

            vs.Add(Memory.instance.peek().value);
            Memory.instance.pop();
        }

        string formatString = FlutterCConsole.Utils.getStringByAddress((int)Memory.instance.peek().value);
        formatString = formatString.Substring(1);
        Memory.instance.pop();

        decimal[] obs = vs.ToArray();
        Array.Reverse(obs);

        
        string[] tokens = new string[vs.Count];
        for (int i = 0; i < tokens.Length; i++ )
        {
            tokens[i] = Memory.instance.nextToken();
        }

        FlutterCConsole.Utils.scanf(formatString, tokens, obs);
    }
}


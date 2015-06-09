using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Printf : NativeFunctionBody
{
    public Printf()
    {
        this.functionName = "printf";
        this.name = functionName;
        this.fbsn = new FunctionBodyStringNames();
    }

    public override void execute()
    {
        if (Memory.instance.isAlgoRegime)
        {
            return;
        }

        List<decimal> vs = new List<decimal>();
        
        while(true){
            decimal pk = Memory.instance.peek().value;

            if(pk!=0){
                if (Memory.instance.peek().typetoken == "char*")
                {
                    char q = (char)Memory.instance.cellMemory.getValue((int)pk).value;
                    if (q == 'ё')
                    {  
                        break;
                    }
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

        string output = FlutterCConsole.Utils.output(formatString, obs);
        Memory.getInstance().output += output;
    }
}


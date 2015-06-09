using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlutterCConsole;

public class FunctionBodyStringNames
{
    public List<string> argNames;
    public List<List<string>> argsTypes;
    public List<string> returnType;

    public FunctionBodyStringNames()
    {
        argNames = new List<string>();
        argsTypes = new List<List<string>>();
        returnType = new List<string>();
    }

    public void setReturnType(List<string> type)
    {
        this.returnType = type;
    }

    public void addArgument(string name, List<string> tokens)
    {
        argNames.Add(name);
        argsTypes.Add(tokens);
    }
}

public class FunctionBody : LinearNode
{
    public static string RETURN_VAR = "__RETURN_VARIABLE";
    public int argNumber;
    public string functionName;
    public FunctionBodyStringNames fbsn;
    public Variable returnVar;

    public List<Variable> args; 

    public FunctionBody()
    {
    }

    public void init(int argNumber, string functionName, FunctionBodyStringNames fbsn)
    {
        this.argNumber = argNumber;
        this.name = functionName + "$" + argNumber;
        this.functionName = functionName;
        this.fbsn = fbsn;

        args = new List<Variable>();

        for (int i = 0; i < fbsn.argNames.Count; i++)
        {
            string type = Utils.merge(fbsn.argsTypes[i]);
            string name = fbsn.argNames[i];

            if (TypeParser.isValidType(fbsn.argsTypes[i]))
            {
                Value val = new Value(type, true);
                VariableDeclaration vd = new VariableDeclaration(this, name, val);
                this.addNode(vd);

                args.Add(vd.getVariable());
                PushVariable p = new PushVariable(vd.getVariable());
                FlutterCConsole.Nodes.InversedAssignment ia = new FlutterCConsole.Nodes.InversedAssignment();

                FlutterCConsole.Nodes.Pop pop = new FlutterCConsole.Nodes.Pop();

                addNode(p);
                addNode(ia);
                addNode(pop);
            }
        }

        Value ret = new Value(Utils.merge(fbsn.returnType), true);
        VariableDeclaration retDeclaration = new VariableDeclaration(this, RETURN_VAR, ret);
        returnVar = retDeclaration.getVariable();
        addNode(retDeclaration);

        addNode(new Push(new CallMarker()));
    }

    public override void execute()
    {
        base.execute();
        Memory.getInstance().returnToCall();
        Memory.getInstance().push(returnVar.values.Peek().makeCopy());
    }

    public override string ToString()
    {
        string res = "Function " + functionName + ": <";
        for (int i = 0; i < fbsn.argsTypes.Count; i++)
        {
            for (int j = 0; j < fbsn.argsTypes[i].Count; j++)
            {
                res += fbsn.argsTypes[i][j];
            }
            if (i != argNumber - 1)
            {
                res += " " + fbsn.argNames[i] + ", ";
            }
            else
            {
                res += " " + fbsn.argNames[i];
            }
        }
        res += "> -> ";

        for (int i = 0; i < fbsn.returnType.Count; i++ )
        {
            res += fbsn.returnType[i];
        }

        res += "\n";

        res += base.ToString();
        return res;
    }
}


﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан инструментальным средством
//     В случае повторного создания кода изменения, внесенные в этот файл, будут потеряны.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class VariableDeclaration : Node
{
	private Variable var;
    
    public Variable getVariable(){
        return var;
    }

    public VariableDeclaration(Node host, string name, Value value) {
        var = new Variable(host, name, value);
        Memory.getInstance().addVariable(name, var);
    }

    public override string print(int o)
    {
        return new string(' ', o)+"new "+var.ToString();
    }

    public override string ToString()
    {
        return print(0);
    }

    public override void execute()
    {
    }
}

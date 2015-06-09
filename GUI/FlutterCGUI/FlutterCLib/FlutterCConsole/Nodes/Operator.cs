using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Operator:Node
{
    protected Value res;
    private bool isCalculated = false;
    public virtual void calculateRes(){
        this.isCalculated = true;
    }
    public virtual Value getRes() {
        if (this.isCalculated)
        {
            return res;
        }
        else {
            throw new InvalidOperationException();
        }
    }
}


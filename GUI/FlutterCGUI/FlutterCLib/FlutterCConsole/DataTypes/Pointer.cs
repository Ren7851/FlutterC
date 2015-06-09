using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;

namespace FlutterCConsole
{
    /*
    public class Pointer : Value
    {
        public int size;
        public int initialAddress;

        public string valueToken;

        public Pointer(string valuetoken)
        {
            allocate();
            this.typetoken = valuetoken+"*";
            this.valueToken = valuetoken;
        }

        public override string ToString()
        {
            if(this.value == 0){
                return this.valueToken + "* " + "NULL" + "(" + this.address + ")";
            }
            else{
                return this.valueToken + "* " + this.value + "(" + this.address + ")";
            }
        }

        
        public Value operatorAsterix()
        {
            if (value == 0)
            {
                throw new FlutterCConsole.Exceptions.NullPointerAccess();
            }
            if(Memory.getInstance().cellMemory.isUsed((int)this.value)){
                Value val = Memory.getInstance().cellMemory.getValue((int)this.value);
                val.implicitCast(valueToken);
                return val;
            }
            else
            {
                throw new FlutterCConsole.Exceptions.AccessViolationException((int)this.value);
            }
        }
         

        public void set(Value value)
        {
            this.value = value.address;
            this.size = 1;
            this.initialAddress = (int)this.value;
        }

        public void set(int size, decimal[] array)
        {
            this.size = size;
            Memory.getInstance().cellMemory.allocatePointer(this, array);
        }

        public override Value makeCopy()
        {
            Pointer res = new Pointer(this.valueToken);
            res.value = this.value;
            return res;
        }
    }
     * */
}

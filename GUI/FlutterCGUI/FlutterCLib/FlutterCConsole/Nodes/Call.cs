using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlutterCConsole.Exceptions;

namespace FlutterCConsole
{
    public class Call : Node
    {
        public FunctionBody body;

        public Call(FunctionBody body)
        {
            this.body = body;
        }

        public override string print(int o)
        {
            return new string(' ', o) + "call(" + body.name + ")";
        }

        public override void execute()
        {
            base.execute();
            Memory.getInstance().pushCall(body);

            
            int n = body.argNumber;
            List<Value> temp = new List<Value>();
            while(n>0){
                if(Memory.getInstance().stackPointer == 0){
                    throw new FlutterCConsole.Exceptions.FunctionCallException();
                }

                temp.Add(Memory.getInstance().peek());
                Memory.getInstance().pop();
                n--;
            }

            for (int i = 0; i < temp.Count; i++ )
            {
                Memory.getInstance().push(temp[i]);
            }
            

            body.execute();
            Memory.getInstance().popCall();
        }
    }
}

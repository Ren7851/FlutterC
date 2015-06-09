using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class Condition : Node
    {
        public Node ifNode;
        public Node elseNode;
        public Node cond;

        public Condition(Node host) {
            this.name = host.name +"$"+ NameInventor.makeBlockName("i");
        }

        public void setStuff(Node ifNode, Node elseNode, Node cond){
            this.ifNode = ifNode;
            this.elseNode = elseNode;
            this.cond = cond;
        }

        public override void execute()
        {
            cond.execute();
            Value tot = Memory.instance.peek();
            if (tot.value == 0)
            {
                elseNode.execute();
            }
            else {
                ifNode.execute();
            }
        }

        public override string print(int o)
        {
            /*
            string res = new string(' ', o);
            res += "if (" + cond.print(o+Settings.OFFSET) + "):\n";
            res += ifNode.print(o + Settings.OFFSET) + "\n";
            res += new string(' ', o) + "else:\n";
            res += elseNode.print(o + Settings.OFFSET);
             * */

            string res = new string(' ', o)+name+"\n";
            res += new string(' ', o+ Settings.OFFSET)+"if {"+"\n";
            res += cond.print(o + Settings.OFFSET + Settings.OFFSET) + "\n";
            res += new string(' ', o+ Settings.OFFSET)+"}\n";
            res += ifNode.print(o + Settings.OFFSET + Settings.OFFSET) + "\n";
            res += elseNode.print(o + Settings.OFFSET + Settings.OFFSET) + "\n";
            return res;
        }

        public override string ToString()
        {
            return print(0);
        }
    }
}

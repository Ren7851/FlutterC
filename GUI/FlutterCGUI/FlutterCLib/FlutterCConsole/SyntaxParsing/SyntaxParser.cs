using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class SyntaxParser
    {
        public static Stack<int> numbers;

        public static int peek(){
            if(numbers.Count > 0){
                return numbers.Peek();
            }
            else
            {
                return 0;
            }
        }

        public static Node parseTokens(List<string> tokens)
        {
            numbers = new Stack<int>();

            Structure st = StructureParser.parseStructure(tokens);

            
            Rand rnd = new Rand();
            Memory.getInstance().addAvailableFunction(rnd);

            Srand srnd = new Srand();
            Memory.getInstance().addAvailableFunction(srnd);

            Printf printf = new Printf();
            Memory.getInstance().addAvailableFunction(printf);

            Scanf scanf = new Scanf();
            Memory.getInstance().addAvailableFunction(scanf);

            
            Malloc malloc = new Malloc();
            Memory.getInstance().addAvailableFunction(malloc);

            Free free = new Free();
            Memory.getInstance().addAvailableFunction(free);

            Dump dump = new Dump();
            Memory.getInstance().addAvailableFunction(dump);

            Sizeof sizeo = new Sizeof();
            Memory.getInstance().addAvailableFunction(sizeo);

            LinearNode global = OperatorsParser.parseBlock(st.outerTokens, Memory.getInstance().globalNode, "global");

            Memory.getInstance().setGlobal(global);

            for (int i = 0; i < st.functions.Count; i++)
            {
                OperatorsParser.parseFunction(st.functions[i], st.tokens[i]);
            }

            return global;
        }
    }
}

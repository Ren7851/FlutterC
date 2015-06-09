using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;


namespace FlutterCConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            List<string> files = new List<string>();
            files.Add("testcode.txt");

            List<string> tokensForParsing = TokensCollector.collectTokens(files);

            SyntaxParser.parseTokens(tokensForParsing);

            Memory.getInstance().showFunction();
            //Memory.getInstance().showStructs();

            
            /*
            Int a = new Int();
            a.value = 1;

            Int b = new Int();
            b.value = 2;

            Int c = new Int();
            c.value = 0;

            LinearNode program = new LinearNode();
            program.addNode(new Push(a));
            program.addNode(new Push(b));
            program.addNode(new Plus());
            program.addNode(new Push(c));
            program.addNode(new Assignment());

            program.execute();
            Console.WriteLine(c.value);
            //Console.WriteLine(Memory.getInstance().peek());
             * */

            
            Value x = new Value("int", true);
            x.value = 10;
            Memory.getInstance().push(x);
            
            FunctionBody main = Memory.getInstance().getFunctionsMap()["main$1"];
            //Console.WriteLine(main);
            stopwatch.Start();
            main.execute();
            stopwatch.Stop();

            Console.WriteLine("Time elapsed: {0}, counter = {1}", stopwatch.Elapsed, CellMemory.counter);

            var vars = Memory.getInstance().getVariablesMap();
            foreach(var i in vars){
                Console.WriteLine(i.Key+"="+i.Value);
            }
             

            
            /*
            string s = "a = a [ 2 ]";
            string[] spl = s.Split(' ');

            List<string> commandSetSample = new List<string>();
            for (int i = 0; i < spl.Length; i++ )
            {
                commandSetSample.Add(spl[i]);
            }

            List<List<string>> res = CommandPreparator.prepareCommand(commandSetSample);
            for (int i = 0; i < res.Count; i++ )
            {
                for (int j = 0; j < res[i].Count; j++ )
                {
                    Console.Write(res[i][j]+" ");
                }
                Console.WriteLine();
            }
             * */
            

            
            Console.WriteLine();
            Console.WriteLine();
            string pd = Memory.getInstance().getPerfomanceDump();
            Console.WriteLine(pd);
             
        }
    }
}

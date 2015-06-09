using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using FlutterCConsole;

using FlutterCLib;
using System.Threading;

namespace Console
{
    class Program
    {
        static void Main(string[] args){
            /*
            string content = "int main(){"                  +
                             "   int a;"                    +
                             "   int b;"                    +
                             "   scanf(\"%d %d\", &a, &b);" +
                             "   printf(\"%d\", a+b);"      +
                             "   return 0;"                 +
                             "}";
            API api = new API();
            api.setUp(content);
            LaunchResult result = api.Launch("1 2");
            System.Console.WriteLine(result);
            return;
            */

            /*
            string sample = "a [ 1 ] = 5";
            List<string> ansamble = sample.Split(' ').ToList<string>();
            List<string> resolved = CommandPreparator.obtainArrayLits(ansamble);
            string gueno = String.Join(" ", resolved);
            System.Console.WriteLine(gueno);
            return;
             */


            Stopwatch stopwatch = new Stopwatch();

            List<string> files = new List<string>();
            files.Add("testcode.txt");

            Memory.reinit();
            Memory.setUp(Memory.getContents(files));
            try
            {
                //Memory.setUp(Memory.getContents(files));
            }
            catch(Exception e){
                System.Console.WriteLine(e.Message);
                return;
            }

            Memory.getInstance().showFunction();
            

            stopwatch.Start();
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            //Memory.instance.launch("2.5 1.3", false);
            try
            {
                Memory.instance.launch("1", false);
            }
            catch(Exception e){
                System.Console.Write(e.Message);
                System.Console.WriteLine("\nin "+Memory.instance.currentInstruction());
                System.Console.WriteLine(Memory.instance.memoryDumpException(true));
                return;
            }


            stopwatch.Stop();

            System.Console.WriteLine("Time elapsed: {0}, counter = {1}, pointer = {2}", stopwatch.Elapsed, CellMemory.counter, Memory.getInstance().stackPointer);

            //System.Console.WriteLine(Memory.getInstance().getVarDump());

            System.Console.WriteLine();
            System.Console.WriteLine();
            string pd = Memory.getInstance().getPerfomanceDump();
            System.Console.WriteLine(pd);


            System.Console.WriteLine("\n\n\nOutput:");
            System.Console.WriteLine(Memory.instance.output);
        }
    }
}

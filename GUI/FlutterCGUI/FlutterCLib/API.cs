using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FlutterCLib
{
    public class LaunchResult
    {
        private FlutterCConsole.PerfomanceReport report;
        private string output;

        public LaunchResult(FlutterCConsole.PerfomanceReport report, string output)
        {
            this.report = report;
            this.output = output;
        }

        public override string ToString()
        {
            string res = "";
            res += "Output:\n";
            res += output + "\n\n";
            res += "Perfomance report:\n";
            res += report.ToString() + "\n";
            return res;
        }

        public FlutterCConsole.PerfomanceReport getPerfomanceReport()
        {
            return report;
        }

        public string getOutput()
        {
            return output;
        }
    }

    

    public class API
    {
        private bool setFlag = false;
        public void setUp(params string[] contents)
        {
            Memory.reinit();
            Memory.setUp(contents.ToList<string>());
            this.setFlag = true;
        }

        public LaunchResult Launch(string input)
        {
            if(!setFlag){
                throw new InvalidOperationException("You can't launch the code before you set up");
            }
            Memory.instance.report = new FlutterCConsole.PerfomanceReport();
            Memory.instance.stackPointer = 0;
            Memory.instance.callStack = new Stack<FunctionBody>();
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Memory.instance.launch(input, false);
            return new LaunchResult(Memory.instance.report, Memory.instance.output);
        }

        public string GetExceptionDump()
        {
            return Memory.instance.memoryDumpException(true);
        }

        public string GetLastInstruction()
        {
            return Memory.instance.currentInstruction();
        }
    }
}

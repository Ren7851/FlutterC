using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlutterCConsole
{
    public class PerfomanceReport
    {
        public int operationsTotal;

        public int softArithmetics;
        public int hardArithmetics;
        public int assignments;
        public int dereferencings;
        public int comparsions;
        public int calls;


        public Stack<Node> commands;

        public PerfomanceReport()
        {
            commands = new Stack<Node>();
        }

        public override string ToString()
        {
            double totalArithmeticsPercent = Math.Round((double)(softArithmetics+hardArithmetics)/(double)(operationsTotal)*100.0);
            double softArithmeticsPercent = Math.Round((double)(softArithmetics) / (double)(operationsTotal) * 100.0);
            double hardArithmeticsPercent = Math.Round((double)(hardArithmetics) / (double)(operationsTotal) * 100.0);
            double assignmentsPercent = Math.Round((double)(assignments) / (double)(operationsTotal) * 100.0);
            double asterixesPercent = Math.Round((double)(dereferencings) / (double)(operationsTotal) * 100.0);
            double comprasionsPercent = Math.Round((double)(comparsions) / (double)(operationsTotal) * 100.0);
            double callsPercent = Math.Round((double)(calls) / (double)(operationsTotal) * 100.0);

            int other = operationsTotal - softArithmetics - hardArithmetics - assignments - dereferencings - comparsions - calls;
            double otherPercent = Math.Round((double)(other) / (double)(operationsTotal) * 100.0);

            string res = "Total executed "+operationsTotal+"\n";
            res += "Arithmetics: " + (softArithmetics + hardArithmetics) + " (" + totalArithmeticsPercent + "%)\n";
            res += "\tSoft arithmetics: " + (softArithmetics) + " (" + softArithmeticsPercent + "%)\n";
            res += "\tHard arithmetics: " + (hardArithmetics) + " (" + hardArithmeticsPercent + "%)\n";
            res += "Assignments: " + (assignments) + " (" + assignmentsPercent + "%)\n";
            res += "Dereferencings: " + (dereferencings) + " (" + asterixesPercent + "%)\n";
            res += "Comparsions: " + (comparsions) + " (" + comprasionsPercent + "%)\n";
            res += "Function calls: " + (calls) + " (" + callsPercent + "%)\n";
            res += "Other: " + (other) + " (" + otherPercent + "%)\n";

            return res;
        }

        public void nodeExecuted(Node node)
        {

            if(node is Call){
                calls++;
                operationsTotal++;
            }

            if(node is Ampersand){
                operationsTotal++;
                commands.Push(node);
                return;
            }

            if(node is ArithmeticsCommand){
                if(node is Plus || node is Minus || node is PreDecrement || node is PostDecrement || node is PreIncrement || node is PostIncrement){
                    softArithmetics++;
                    operationsTotal++;
                }
                else
                {
                    hardArithmetics++;
                    operationsTotal++;
                }

                if(((ArithmeticsCommand)node).withAssignment){
                    assignments++;
                    operationsTotal++;
                }
            }

            if(node is ComprasionCommand){
                comparsions++;
                operationsTotal++;
            }

            if(node is Asterix || node is OperatorPoint){
                dereferencings++;
                operationsTotal++;
            }

            if(node is Assignment){
                assignments++;
                operationsTotal++;
            }

            commands.Push(node);
        }
    }
}

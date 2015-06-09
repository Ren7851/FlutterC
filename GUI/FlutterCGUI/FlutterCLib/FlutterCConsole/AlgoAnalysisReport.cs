using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterCConsole
{
    public class AlgoAnalysisReport
    {
        double beginN;
        double endN;

        double h;

        List<PerfomanceReport> list;

        public AlgoAnalysisReport(double beginN, double endN, double h)
        {
            list = new List<PerfomanceReport>();
            this.beginN = beginN;
            this.endN = endN;
            this.h = h;
        }

        public void done(PerfomanceReport report)
        {
            list.Add(report);
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < list.Count; i++ )
            {
                res += (beginN + h * i) + " -> " + list[i].operationsTotal+"\n";
            }
            return res;
        }

        public double getX(int index)
        {
            return beginN + h * index;
        }

        public double getSoftArithmetics(int index)
        {
            return list[index].softArithmetics;
        }

        public double getTotal(int index)
        {
            return list[index].operationsTotal;
        }

        public double getHardArithmetics(int index)
        {
            return list[index].hardArithmetics;
        }

        public double getAssignments(int index)
        {
            return list[index].assignments;
        }

        public double getComprasions(int index)
        {
            return list[index].comparsions;
        }

        public double getAsterixes(int index)
        {
            return list[index].dereferencings;
        }

        public double getCalls(int index)
        {
            return list[index].calls;
        }

        public int getLength()
        {
            return list.Count;
        }

        public double getOther(int index)
        {
            return getTotal(index) - getCalls(index) - getAsterixes(index) - getComprasions(index) - getAssignments(index) - getSoftArithmetics(index) - getHardArithmetics(index);
        }
    }
}

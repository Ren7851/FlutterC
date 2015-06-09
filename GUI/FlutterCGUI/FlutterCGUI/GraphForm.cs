using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace FlutterCGUI
{
    public partial class GraphForm : Form
    {
        public GraphForm()
        {
            InitializeComponent();
        }

        public bool isDoubleInt(double d)
        {
            if(Math.Abs(d - Math.Floor(d))<0.0000001){
                return true;
            }

            if (Math.Abs(d - Math.Ceiling(d)) < 0.0000001)
            {
                return true;
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double begin = (double)numericUpDown1.Value;
            double end = (double)numericUpDown2.Value;
            double h = (double)numericUpDown3.Value;

            if(!isDoubleInt((end-begin)/h)){
                MessageBox.Show("(end-int)/h should be integer");
                return;
            }


            if (!Memory.getInstance().canAlgoLaunch())
            {
                MessageBox.Show("Can't find main(double)");
                return;
            }
            try
            {
                FlutterCConsole.AlgoAnalysisReport report = Memory.getInstance().algoLaunch(begin, end, h);
                drawChart(report);
            }
            catch (Exception ex)
            {
                try
                {
                    string s = ex.Message + "\n";
                    s += "in " + Memory.instance.currentInstruction() + "\n";
                    s += Memory.instance.memoryDumpException(true) + "\n";
                    MessageBox.Show(s);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }


        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        public void drawChart(FlutterCConsole.AlgoAnalysisReport report)
        {

            this.chart1.Visible = true;
            this.chart1.ChartAreas[0].AxisX.Minimum = (double)numericUpDown1.Value;
            this.chart1.ChartAreas[0].AxisX.Maximum = (double)numericUpDown2.Value;

            int len = report.getLength();

            this.chart1.Series[0].Points.Clear();
            this.chart1.Series[1].Points.Clear();
            this.chart1.Series[2].Points.Clear();
            this.chart1.Series[3].Points.Clear();
            this.chart1.Series[4].Points.Clear();
            this.chart1.Series[5].Points.Clear();
            this.chart1.Series[6].Points.Clear();

            for (int i = 0; i < len; i++ )
            {
                this.chart1.Series[6 - 0].Points.AddXY(report.getX(i), report.getSoftArithmetics(i));
                this.chart1.Series[6 - 1].Points.AddXY(report.getX(i), report.getHardArithmetics(i));
                this.chart1.Series[6 - 2].Points.AddXY(report.getX(i), report.getComprasions(i));
                this.chart1.Series[6 - 3].Points.AddXY(report.getX(i), report.getAssignments(i));
                this.chart1.Series[6 - 4].Points.AddXY(report.getX(i), report.getAsterixes(i));
                this.chart1.Series[6 - 5].Points.AddXY(report.getX(i), report.getCalls(i));
                this.chart1.Series[6 - 6].Points.AddXY(report.getX(i), report.getOther(i));
            }


        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void GraphForm_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}

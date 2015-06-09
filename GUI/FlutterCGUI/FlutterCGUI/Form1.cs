using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace FlutterCGUI
{
    public partial class Form1 : Form
    {
        public FlutterCProject currentProject;
        public int currentIndex = -1;
        public bool isRemoving = false;
        bool isAddedFirst = false;
        bool isAdding = false;

        TextEditorControl area = new TextEditorControl();

        public Form1()
        {
            InitializeComponent();
            currentProject = new FlutterCProject();
            coding.Controls.Add(area);
            area.Width = 727;
            area.Height = outputBox.Bottom-area.Top - 30;
            area.TextEditorProperties.Font = new Font(FontFamily.GenericMonospace, 13);
            area.TextEditorProperties.DocumentSelectionMode = DocumentSelectionMode.Normal;
            area.Document.HighlightingStrategy =
                        HighlightingStrategyFactory.CreateHighlightingStrategyForFile("main.c");
            loadProject(currentProject);
            //addExistingFile("main.c");
            //addExistingFile("math.c");
            //addExistingFile("string.c");
            //area.TextEditorProperties.Encoding = ITextEditorProperties.
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (projectFilesList.SelectedIndex == -1)
            {
                return;
            }
            update();
        }

        public void update()
        {
            dropToMemory();
            area.Text = "";
            //area.BeginUpdate();
            currentIndex = projectFilesList.SelectedIndex;
            updateCodingArea();
        }

        public void updateCodingArea()
        {
            if(isRemoving || isAdding){
                return;
            }

            string fn = currentProject.cfiles[currentIndex];
            try
            {
                area.LoadFile(fn);
            }
            catch(Exception e){
                File.Create(fn).Dispose();
                area.Text = "";
            }
        }

        public void loadProject(FlutterCProject proj)
        {
            this.projectFilesList.DataSource = null;
            this.projectFilesList.DataSource = currentProject.shortNames;
        }

        public void dropToMemory(){
            if (!isAddedFirst)
            {
                return;
            }

            if(isRemoving || isAdding){
                return;
            }

            if(currentIndex >= projectFilesList.Items.Count){
                return;
            }
            File.WriteAllText(currentProject.cfiles[currentIndex], area.Text);
        }

        private void codingArea_TextChanged(object sender, EventArgs e){
        }

        public void addExistingFile(string name)
        {
            dropToMemory();
            if(currentProject.canAdd(name)){
                currentProject.addFile(name);
                isAdding = true;

                this.projectFilesList.DataSource = null;
                this.projectFilesList.DataSource = currentProject.shortNames;
                currentIndex = 0;
                projectFilesList.SelectedIndex = 0;

                isAdding = false;
                updateCodingArea();

                isAddedFirst = true;
            }
            else
            {
                MessageBox.Show("You can't add this file");
            }
        }

        public void deleteFile()
        {
            if(currentProject.cfiles.Count < 2){
                MessageBox.Show("You can't remove this file");
                return;
            }
            currentProject.removeAt(currentIndex);

            isRemoving = true;
            this.projectFilesList.DataSource = null;
            this.projectFilesList.DataSource = currentProject.shortNames;
            this.projectFilesList.SelectedIndex = 0;
            isRemoving = false;
            currentIndex = 0;
            updateCodingArea();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void addExistingFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = open.FileName;
                if (filename.Length >= 2 && filename[filename.Length - 1] == 'c' && filename[filename.Length - 2] == '.')
                {
                    addExistingFile(filename);
                }
                else
                {
                    MessageBox.Show("This is not a .c file");
                }
            }
        }

        private void projectFilesList_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dropToMemory();
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = save.FileName;
                string obj = FlutterCProject.Serialize(currentProject);
                File.WriteAllText(filename, obj);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dropToMemory();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = open.FileName;
                string s = File.ReadAllText(filename);
                FlutterCProject newProj = FlutterCProject.Deserialize(s);
                currentProject = newProj;
                currentIndex = 0;
                loadProject(newProj);
            }
        }

        bool cnt = false;

        public void run(bool wantPerfomanceReport)
        {
            dropToMemory();

            //if (!cnt)
            {
                Memory.reinit();
                try
                {
                    Memory.setUp(Memory.getContents(currentProject.cfiles));
                }
                catch (Exception e)
                {
                    outputBox.Text = e.Message;
                    cnt = true;
                    return;
                }

            }
            //else{
            //    Memory.reinit();
            //    Memory.setUp(currentProject.cfiles);
            //}



            
            if (!Memory.getInstance().canLaunch())
            {
                MessageBox.Show("Can't find main()");
                return;
            }
            try
            {
                string output = Memory.getInstance().launch(inputBox.Text, wantPerfomanceReport);
                outputBox.Text = output;
            }
            catch(Exception e){
                try{
                    string s = e.Message + "\n";
                    s += "in " + Memory.instance.currentInstruction() + "\n";
                    s += Memory.instance.memoryDumpException(true) + "\n";
                    outputBox.Text = s;
                }
                catch (Exception ex)
                {
                    outputBox.Text = ex.Message;
                }
            }
        }

        public void runGraph()
        {
            dropToMemory();
            try
            {
                Memory.reinit();
                Memory.setUp(Memory.getContents(currentProject.cfiles));
            }
            catch (Exception e)
            {
                outputBox.Text = e.Message;
                return;
            }

            if(!Memory.getInstance().canAlgoLaunch()){
                MessageBox.Show("Can't find main(double)");
                return;
            }

            GraphForm another = new GraphForm();
            another.ShowDialog(this);
        }

        private void runWithPerfomanceAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            run(true);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void runToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            run(false);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void runWithAlgorithmAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            runGraph();
        }

        private void codingArea_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

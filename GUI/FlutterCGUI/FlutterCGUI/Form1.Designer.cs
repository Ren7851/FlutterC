namespace FlutterCGUI
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.projectFilesList = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addExistingFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.runWithPerfomanceAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runWithAlgorithmAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.open = new System.Windows.Forms.OpenFileDialog();
            this.save = new System.Windows.Forms.SaveFileDialog();
            this.inputBox = new System.Windows.Forms.RichTextBox();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Output = new System.Windows.Forms.Label();
            this.coding = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectFilesList
            // 
            resources.ApplyResources(this.projectFilesList, "projectFilesList");
            this.projectFilesList.FormattingEnabled = true;
            this.projectFilesList.Name = "projectFilesList";
            this.projectFilesList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.projectFilesList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.projectFilesList_KeyPress);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.runToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            resources.ApplyResources(this.newToolStripMenuItem, "newToolStripMenuItem");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            resources.ApplyResources(this.loadToolStripMenuItem, "loadToolStripMenuItem");
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addExistingFileToolStripMenuItem,
            this.deleteFileToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // addExistingFileToolStripMenuItem
            // 
            this.addExistingFileToolStripMenuItem.Name = "addExistingFileToolStripMenuItem";
            resources.ApplyResources(this.addExistingFileToolStripMenuItem, "addExistingFileToolStripMenuItem");
            this.addExistingFileToolStripMenuItem.Click += new System.EventHandler(this.addExistingFileToolStripMenuItem_Click);
            // 
            // deleteFileToolStripMenuItem
            // 
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            resources.ApplyResources(this.deleteFileToolStripMenuItem, "deleteFileToolStripMenuItem");
            this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.deleteFileToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem1,
            this.runWithPerfomanceAnalysisToolStripMenuItem,
            this.runWithAlgorithmAnalysisToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            resources.ApplyResources(this.runToolStripMenuItem, "runToolStripMenuItem");
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem1
            // 
            this.runToolStripMenuItem1.Name = "runToolStripMenuItem1";
            resources.ApplyResources(this.runToolStripMenuItem1, "runToolStripMenuItem1");
            this.runToolStripMenuItem1.Click += new System.EventHandler(this.runToolStripMenuItem1_Click);
            // 
            // runWithPerfomanceAnalysisToolStripMenuItem
            // 
            this.runWithPerfomanceAnalysisToolStripMenuItem.Name = "runWithPerfomanceAnalysisToolStripMenuItem";
            resources.ApplyResources(this.runWithPerfomanceAnalysisToolStripMenuItem, "runWithPerfomanceAnalysisToolStripMenuItem");
            this.runWithPerfomanceAnalysisToolStripMenuItem.Click += new System.EventHandler(this.runWithPerfomanceAnalysisToolStripMenuItem_Click);
            // 
            // runWithAlgorithmAnalysisToolStripMenuItem
            // 
            this.runWithAlgorithmAnalysisToolStripMenuItem.Name = "runWithAlgorithmAnalysisToolStripMenuItem";
            resources.ApplyResources(this.runWithAlgorithmAnalysisToolStripMenuItem, "runWithAlgorithmAnalysisToolStripMenuItem");
            this.runWithAlgorithmAnalysisToolStripMenuItem.Click += new System.EventHandler(this.runWithAlgorithmAnalysisToolStripMenuItem_Click);
            // 
            // open
            // 
            this.open.FileName = "openFileDialog1";
            // 
            // inputBox
            // 
            resources.ApplyResources(this.inputBox, "inputBox");
            this.inputBox.Name = "inputBox";
            this.inputBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // outputBox
            // 
            resources.ApplyResources(this.outputBox, "outputBox");
            this.outputBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.outputBox.Name = "outputBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // Output
            // 
            resources.ApplyResources(this.Output, "Output");
            this.Output.Name = "Output";
            // 
            // coding
            // 
            this.coding.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.coding.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.coding.CausesValidation = false;
            resources.ApplyResources(this.coding, "coding");
            this.coding.Name = "coding";
            this.coding.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.coding);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.projectFilesList);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox projectFilesList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addExistingFileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog open;
        private System.Windows.Forms.SaveFileDialog save;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem runWithPerfomanceAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runWithAlgorithmAnalysisToolStripMenuItem;
        private System.Windows.Forms.RichTextBox inputBox;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Output;
        private System.Windows.Forms.Panel coding;
    }
}


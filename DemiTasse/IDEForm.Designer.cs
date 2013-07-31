namespace DemiTasse
{
    partial class IDEForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvFiles = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tcFiles = new System.Windows.Forms.TabControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtAST = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtIntRep = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewTestSuite = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpenTestSuite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileAddFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSepAddFiles = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileCloseTestSuite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunPause = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunContinue = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRunStop = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(664, 482);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvFiles
            // 
            this.tvFiles.CheckBoxes = true;
            this.tvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tvFiles.HideSelection = false;
            this.tvFiles.Location = new System.Drawing.Point(0, 0);
            this.tvFiles.Name = "tvFiles";
            this.tvFiles.Size = new System.Drawing.Size(218, 480);
            this.tvFiles.TabIndex = 0;
            this.tvFiles.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvFiles_NodeMouseClick);
            this.tvFiles.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvFiles_NodeMouseDoubleClick);
            this.tvFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvFiles_KeyUp);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tcFiles);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(440, 482);
            this.splitContainer2.SplitterDistance = 277;
            this.splitContainer2.TabIndex = 0;
            // 
            // tcFiles
            // 
            this.tcFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFiles.Location = new System.Drawing.Point(0, 0);
            this.tcFiles.Name = "tcFiles";
            this.tcFiles.SelectedIndex = 0;
            this.tcFiles.Size = new System.Drawing.Size(438, 275);
            this.tcFiles.TabIndex = 0;
            this.tcFiles.Visible = false;
            this.tcFiles.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcFiles_Selected);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(438, 199);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtConsole);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(430, 173);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Output";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtConsole
            // 
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.txtConsole.Location = new System.Drawing.Point(3, 3);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(424, 167);
            this.txtConsole.TabIndex = 1;
            this.txtConsole.WordWrap = false;
            this.txtConsole.TextChanged += new System.EventHandler(this.txtConsole_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtAST);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(428, 173);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Abstract Syntax Tree";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtAST
            // 
            this.txtAST.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAST.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAST.Location = new System.Drawing.Point(3, 3);
            this.txtAST.Multiline = true;
            this.txtAST.Name = "txtAST";
            this.txtAST.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAST.Size = new System.Drawing.Size(422, 167);
            this.txtAST.TabIndex = 0;
            this.txtAST.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtIntRep);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(428, 173);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Intermediate Representation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtIntRep
            // 
            this.txtIntRep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIntRep.Font = new System.Drawing.Font("Courier New", 11.25F);
            this.txtIntRep.Location = new System.Drawing.Point(3, 3);
            this.txtIntRep.Multiline = true;
            this.txtIntRep.Name = "txtIntRep";
            this.txtIntRep.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtIntRep.Size = new System.Drawing.Size(422, 167);
            this.txtIntRep.TabIndex = 0;
            this.txtIntRep.WordWrap = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuRun});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(664, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.toolStripMenuItem1,
            this.mnuFileAddFiles,
            this.mnuSepAddFiles,
            this.mnuFileSave,
            this.mnuFileSaveAll,
            this.toolStripMenuItem3,
            this.mnuFileClose,
            this.mnuFileCloseTestSuite,
            this.toolStripMenuItem2,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNewFile,
            this.mnuNewTestSuite});
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(152, 22);
            this.mnuFileNew.Text = "New";
            // 
            // mnuFileNewFile
            // 
            this.mnuFileNewFile.Name = "mnuFileNewFile";
            this.mnuFileNewFile.Size = new System.Drawing.Size(134, 22);
            this.mnuFileNewFile.Text = "File...";
            this.mnuFileNewFile.Click += new System.EventHandler(this.mnuFileNewFile_Click);
            // 
            // mnuNewTestSuite
            // 
            this.mnuNewTestSuite.Name = "mnuNewTestSuite";
            this.mnuNewTestSuite.Size = new System.Drawing.Size(134, 22);
            this.mnuNewTestSuite.Text = "Test Suite...";
            this.mnuNewTestSuite.Click += new System.EventHandler(this.mnuNewTestSuite_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpenFile,
            this.mnuFileOpenTestSuite});
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuFileOpen.Text = "Open";
            // 
            // mnuFileOpenFile
            // 
            this.mnuFileOpenFile.Name = "mnuFileOpenFile";
            this.mnuFileOpenFile.Size = new System.Drawing.Size(134, 22);
            this.mnuFileOpenFile.Text = "File...";
            this.mnuFileOpenFile.Click += new System.EventHandler(this.mnuFileOpenFile_Click);
            // 
            // mnuFileOpenTestSuite
            // 
            this.mnuFileOpenTestSuite.Name = "mnuFileOpenTestSuite";
            this.mnuFileOpenTestSuite.Size = new System.Drawing.Size(134, 22);
            this.mnuFileOpenTestSuite.Text = "Test Suite...";
            this.mnuFileOpenTestSuite.Click += new System.EventHandler(this.mnuFileOpenTestSuite_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileAddFiles
            // 
            this.mnuFileAddFiles.Name = "mnuFileAddFiles";
            this.mnuFileAddFiles.Size = new System.Drawing.Size(152, 22);
            this.mnuFileAddFiles.Text = "Add Files...";
            this.mnuFileAddFiles.Click += new System.EventHandler(this.mnuFileAddFiles_Click);
            // 
            // mnuSepAddFiles
            // 
            this.mnuSepAddFiles.Name = "mnuSepAddFiles";
            this.mnuSepAddFiles.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Enabled = false;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSave.Text = "Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAll
            // 
            this.mnuFileSaveAll.Enabled = false;
            this.mnuFileSaveAll.Name = "mnuFileSaveAll";
            this.mnuFileSaveAll.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSaveAll.Text = "Save All";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileClose
            // 
            this.mnuFileClose.Name = "mnuFileClose";
            this.mnuFileClose.Size = new System.Drawing.Size(152, 22);
            this.mnuFileClose.Text = "Close";
            this.mnuFileClose.Click += new System.EventHandler(this.mnuFileClose_Click);
            // 
            // mnuFileCloseTestSuite
            // 
            this.mnuFileCloseTestSuite.Name = "mnuFileCloseTestSuite";
            this.mnuFileCloseTestSuite.Size = new System.Drawing.Size(152, 22);
            this.mnuFileCloseTestSuite.Text = "Close Suite";
            this.mnuFileCloseTestSuite.Click += new System.EventHandler(this.mnuFileCloseTestSuite_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuRun
            // 
            this.mnuRun.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRunStart,
            this.mnuRunPause,
            this.mnuRunContinue,
            this.mnuRunStop});
            this.mnuRun.Name = "mnuRun";
            this.mnuRun.Size = new System.Drawing.Size(40, 20);
            this.mnuRun.Text = "Run";
            // 
            // mnuRunStart
            // 
            this.mnuRunStart.Name = "mnuRunStart";
            this.mnuRunStart.Size = new System.Drawing.Size(123, 22);
            this.mnuRunStart.Text = "Start";
            this.mnuRunStart.Click += new System.EventHandler(this.mnuRunStart_Click);
            // 
            // mnuRunPause
            // 
            this.mnuRunPause.Name = "mnuRunPause";
            this.mnuRunPause.Size = new System.Drawing.Size(123, 22);
            this.mnuRunPause.Text = "Pause";
            this.mnuRunPause.Click += new System.EventHandler(this.mnuRunPause_Click);
            // 
            // mnuRunContinue
            // 
            this.mnuRunContinue.Name = "mnuRunContinue";
            this.mnuRunContinue.Size = new System.Drawing.Size(123, 22);
            this.mnuRunContinue.Text = "Continue";
            this.mnuRunContinue.Click += new System.EventHandler(this.mnuRunContinue_Click);
            // 
            // mnuRunStop
            // 
            this.mnuRunStop.Name = "mnuRunStop";
            this.mnuRunStop.Size = new System.Drawing.Size(123, 22);
            this.mnuRunStop.Text = "Stop";
            this.mnuRunStop.Click += new System.EventHandler(this.mnuRunStop_Click);
            // 
            // IDEForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 506);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "IDEForm";
            this.Text = "DemiTasse (untitled)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IDEForm_FormClosing);
            this.Load += new System.EventHandler(this.IDEForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuRun;
        private System.Windows.Forms.ToolStripMenuItem mnuRunStart;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNewFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNewTestSuite;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpenFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpenTestSuite;
        private System.Windows.Forms.ToolStripMenuItem mnuFileAddFiles;
        private System.Windows.Forms.ToolStripSeparator mnuSepAddFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuFileClose;
        private System.Windows.Forms.ToolStripMenuItem mnuFileCloseTestSuite;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuRunPause;
        private System.Windows.Forms.ToolStripMenuItem mnuRunContinue;
        private System.Windows.Forms.ToolStripMenuItem mnuRunStop;
        private System.Windows.Forms.TreeView tvFiles;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtIntRep;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtAST;
        private System.Windows.Forms.TabControl tcFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}


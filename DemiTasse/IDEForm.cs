// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: IDEForm.cs
// *
// * Description:  Main IDE Form
// *
// * Author: Robin Murray
// *
// **********************************************************************************
// *
// * Granting License: The MIT License (MIT)
// * 
// *   Permission is hereby granted, free of charge, to any person obtaining a copy
// *   of this software and associated documentation files (the "Software"), to deal
// *   in the Software without restriction, including without limitation the rights
// *   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// *   copies of the Software, and to permit persons to whom the Software is
// *   furnished to do so, subject to the following conditions:
// *   The above copyright notice and this permission notice shall be included in
// *   all copies or substantial portions of the Software.
// *   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// *   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// *   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// *   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// *   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// *   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// *   THE SOFTWARE.
// * 
// **********************************************************************************

// **********************************************************************************
// * Using
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using DemiTasse.ast;
using DemiTasse.ir;
using DemiTasse.interp;
using DemiTasse.irpsr;
using DemiTasse.AppIDE;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse
{
    public partial class IDEForm : Form
    {
        private AppIDE.AppIDE _app = null;
        private TestSuiteManager _testSuiteManager = null;
        private enum IDEModes { SingleFile, TestSuite }
        private IDEModes _ideMode;
        private string _fileName;
        private bool _lockEdit = false;
        private StringBuilder _intermediateRepresentation = new StringBuilder();
        private TestSuiteFileEntry _currentTestFileEntry;

        private Command _cmdNewFile = null;
        private Command _cmdNewTestSuite = null;
        private Command _cmdOpenFile = null;
        private Command _cmdOpenTestSuite = null;
        private Command _cmdOpenTestSuiteFile = null;
        private Command _cmdAddTestSuiteFiles = null;
        private Command _cmdRemoveTestSuiteFile = null;
        private Command _cmdSaveFile = null;
        private Command _cmdClose = null;
        private Command _cmdCloseTestSuite = null;
        private Command _cmdRunStart = null;
        //private Command _cmdRunStartTestSuite = null;
        private Command _cmdRunPause = null;
        private Command _cmdRunContinue = null;
        private Command _cmdRunStop = null;

        private TestSuite _testSuite = null;

        private const string _dialogFilter = "MiniJava files (*.java)|*.java|Abstract Syntax Tree files (*.ast)|*.ast|Intermediate Representation files (*.ir)|*.ir|All files (*.*)|*.*";


        private void InitTestSuiteManager(string serializationFileName)
        {
            FileStream fs = null;

            if (!File.Exists(serializationFileName))
            {
                _testSuiteManager = new TestSuiteManager();
                return;
            }

            try
            {
                fs = new FileStream(serializationFileName, FileMode.Open);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to open Test Project config file. Reason: " + ex.Message);
                _testSuiteManager = new TestSuiteManager();
                return;
            }


            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.

                _testSuiteManager = (TestSuiteManager)formatter.Deserialize(fs);
            }
            catch (SerializationException ex)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + ex.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        #region Form Events

        public IDEForm()
        {
            InitializeComponent();

            InitTestSuiteManager("temp.bin");
            //(_testSuiteManager = new TestSuiteManager()).FakeInit();

            _app = new AppIDE.AppIDE(_testSuiteManager);
            _app.AddOnAppError(App_OnErrorOut);
            _app.AddOnAppException(App_OnExceptionOut);
            _app.AddOnOpenFile(App_OnOpenFile);
            _app.AddOnOpenTestSuite(App_OnOpenTestSuite);
            _app.AddOnOpenTestSuiteFile(App_OnOpenTestSuiteFile);
            _app.AddOnSaveFile(App_OnSaveFile);
            
            _app.AddOnSystemOut(App_OnSystemOut);
            _app.AddOnIrOut(App_OnIrOut);
            _app.AddOnAstOut(App_OnAstOut);

            _cmdNewFile = new CmdNewFile(_app);
            _cmdNewTestSuite = new CmdNewTestSuite(_app);
            _cmdOpenFile = new CmdOpenFile(_app);
            _cmdOpenTestSuite = new CmdOpenTestSuite(_app);
            _cmdOpenTestSuiteFile = new CmdOpenTestSuiteFile(_app);
            _cmdAddTestSuiteFiles = new CmdAddTestSuiteFiles(_app);
            _cmdRemoveTestSuiteFile = new CmdRemoveTestSuiteFile(_app);
            _cmdSaveFile = new CmdSaveFile(_app);
            _cmdClose = new CmdNotYetImplemented();
            _cmdCloseTestSuite = new CmdNotYetImplemented();
            _cmdRunStart = new CmdRunStart(_app);
            _cmdRunPause = new CmdNotYetImplemented();
            _cmdRunContinue = new CmdNotYetImplemented();
            _cmdRunStop = new CmdNotYetImplemented();
        }

        private void IDEForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (DialogResult.Cancel == SaveAllFiles(true))
                {
                    e.Cancel = true;
                }
                else
                {
                    SaveTestSuiteConfig();
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private DialogResult SaveAllFiles(bool queryUser)
        {
            TestSuiteFileEntry fileEntry = null;
            DialogResult rc;

            foreach (TabPage tp in tcFiles.TabPages)
            {
                fileEntry = tp.Tag as TestSuiteFileEntry;

                Debug.Assert(null != fileEntry);

                if (DialogResult.Cancel == (rc = SaveFile(fileEntry, queryUser)))
                    return rc;
            }

            return DialogResult.OK;
        }

        private void SaveTestSuiteConfig()
        {
            Stream stream = null;
            try
            {
                // To serialize the hashtable and its key/value pairs,  
                // you must first open a stream for writing. 
                // In this case, use a file stream.
                stream = File.Create("temp.bin");

                // Construct a BinaryFormatter and use it to serialize the data to the stream.
                BinaryFormatter bf = new BinaryFormatter();
                bf.Context = new System.Runtime.Serialization.StreamingContext(StreamingContextStates.All);
                bf.Serialize(stream, _testSuiteManager);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }
        }

        private void IDEForm_Load(object sender, EventArgs e)
        {
            Text = Application.ProductName;
            IDEMode = IDEModes.SingleFile;
            mnuFileClose.Enabled = false;
        }

        #endregion Form Events

        #region Menus

        private void mnuFileNewFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "New Mini-Java File";
                    dialog.Filter = "MiniJava files (*.java)|*.java|Abstract Syntax Tree files (*.ast)|*.ast|Intermediate Representation files (*.ir)|*.ir|All files (*.*)|*.*";
                    dialog.CheckPathExists = true;
                    dialog.CheckFileExists = false;

                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        if (this.tcFiles.Controls.ContainsKey(dialog.FileName))
                        {
                            DisplayInfoMessage("File already exists: " + dialog.FileName);
                            this.tcFiles.SelectTab(dialog.FileName);
                        }
                        else if (File.Exists(dialog.FileName))
                        {
                            DisplayInfoMessage("File already exists: " + dialog.FileName);
                            _cmdOpenFile.Execute(dialog.FileName);
                        }
                        else
                        {
                            _cmdNewFile.Execute(dialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuNewTestSuite_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    using (NewTestSuiteForm f = new NewTestSuiteForm(_testSuiteManager.TestSuiteNames))
                    {
                        if (DialogResult.OK == f.ShowDialog())
                        {
                            _cmdNewTestSuite.Execute(f.TestSuiteName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    DisplayException(ex);
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuFileOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Open Mini-Java File";
                    dialog.Filter = _dialogFilter;
                    dialog.CheckPathExists = true;
                    dialog.CheckFileExists = true;

                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        if (this.tcFiles.Controls.ContainsKey(dialog.FileName))
                        {
                            this.tcFiles.SelectTab(dialog.FileName);
                        }
                        else
                        {
                            _cmdOpenFile.Execute(dialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuFileOpenTestSuite_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenTestSuiteForm f = new OpenTestSuiteForm(_testSuiteManager.TestSuiteNames))
                {
                    if (DialogResult.OK == f.ShowDialog())
                    {
                        _cmdOpenTestSuite.Execute(f.TestSuiteName);
                    }
                }

            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuFileAddFiles_Click(object sender, EventArgs e)
        {
            TestSuiteFileEntry entry = null;
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Add Test Project File(s)";
                    dialog.Filter = _dialogFilter;
                    dialog.Multiselect = true;
                    dialog.CheckPathExists = true;
                    dialog.CheckFileExists = true;

                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        Debug.Assert(null != tvFiles.SelectedNode, "No node is selected.");

                        if (null == tvFiles.SelectedNode.Tag)
                        {
                            _cmdAddTestSuiteFiles.Execute(_testSuite.Name, -1, dialog.FileNames);
                        }
                        else if (null != (entry = tvFiles.SelectedNode.Tag as TestSuiteFileEntry))
                        {
                            _cmdAddTestSuiteFiles.Execute(_testSuite.Name, entry.Index, dialog.FileNames);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuFileRemoveFile_Click(object sender, EventArgs e)
        {
            try
            {
                TestSuiteFileEntry entry;

                Debug.Assert(null != tvFiles.SelectedNode, "No node has been selected for removal.");

                if (null != (entry = tvFiles.SelectedNode.Tag as TestSuiteFileEntry))
                {
                    _cmdRemoveTestSuiteFile.Execute(_testSuite.Name, entry.Index);
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }

        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            Debug.Assert(null != tcFiles.SelectedTab);

            if (null != tcFiles.SelectedTab)
            {
                TestSuiteFileEntry fileEntry = tcFiles.SelectedTab.Tag as TestSuiteFileEntry;

                Debug.Assert(null != fileEntry);

                if (fileEntry.Changed)
                {
                    SaveFile(fileEntry, false);
                }
            }
        }

        private DialogResult SaveFile(TestSuiteFileEntry fileEntry, bool queryUser)
        {
            DialogResult rc = DialogResult.OK;

            if (null != fileEntry)
            {
                if (fileEntry.Changed)
                {
                    if (queryUser)
                    {
                        rc = MessageBox.Show("Save file " + fileEntry.FileName, "Save File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    }
                    else
                        rc = DialogResult.Yes;

                    if (DialogResult.Yes == rc)
                    {
                        TextBox tb = tcFiles.SelectedTab.Controls[0] as TextBox;

                        Debug.Assert(tb != null, "TextBox == null");

                        try
                        {
                            _cmdSaveFile.Execute(fileEntry.FileName, tb.Text);
                        }
                        catch (Exception ex)
                        {
                            DisplayException(ex);
                        }
                    }
                    else if (DialogResult.Cancel == rc)
                    {
                        return DialogResult.Cancel;
                    }
                }
            }
            return rc;
        }

        private void mnuFileSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAllFiles(false);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuFileClose_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.Assert(null != tcFiles.SelectedTab, "No tab selected for close command.");

                TestSuiteFileEntry fileEntry = tcFiles.SelectedTab.Tag as TestSuiteFileEntry;

                Debug.Assert(null != fileEntry, "file entry missing from tab control");

                if (fileEntry.Changed)
                {
                    if (DialogResult.Cancel == SaveFile(fileEntry, true))
                    {
                        return;
                    }
                }

                TabPage tabPage = tcFiles.SelectedTab;
                TextBox textBox = tabPage.Controls[0] as TextBox;

                textBox.TextChanged -= new System.EventHandler(this.tcFiles_TextChanged);
                tabPage.Controls.Remove(textBox);
                tcFiles.Controls.Remove(tabPage);
                mnuFileClose.Enabled = (tcFiles.TabPages.Count > 0);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuFileCloseTestSuite_Click(object sender, EventArgs e)
        {
            try
            {
                _cmdCloseTestSuite.Execute();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuRunStart_Click(object sender, EventArgs e)
        {
            try
            {
                txtIntRep.Clear();
                txtAST.Clear();
                txtConsole.Clear();

                if (IDEMode == IDEModes.SingleFile)
                {
                    Debug.Assert(tcFiles.TabPages.ContainsKey(_fileName), "");
                    Debug.Assert(tcFiles.TabPages[_fileName].Controls.Count > 0, "");
                    Debug.Assert(null != (tcFiles.TabPages[_fileName].Controls[0] as TextBox), "");

                    TextBox textBox = tcFiles.TabPages[_fileName].Controls[0] as TextBox;

                    _cmdRunStart.Execute(_fileName, textBox.Text);
                }
                else
                {
                    Debug.Assert(tvFiles.Nodes.Count > 0, "");

                    ExecuteTestSuiteItems(tvFiles.Nodes[0]);
                }
            }
            catch (AppUserErrorException ex)
            {
                DisplayAppUserException(ex);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

#if false
        private void ExecuteTestSuiteItems(IList<TestSuiteEntry> Items)
        {
            TestSuiteSuiteEntry suiteEntry;
            TestSuiteFileEntry fileEntry;

            foreach (TestSuiteEntry item in Items)
            {
                if (null != (suiteEntry = item as TestSuiteSuiteEntry))
                {
                    TestSuite testSuite = TestSuiteManager.TestSuites[item.Name];
                    ExecuteTestSuiteItems(testSuite.Items);
                }
                else
                {
                    fileEntry = item as TestSuiteFileEntry;
                    cmdRunStartSingleFile.Execute(fileEntry.FileName);
                }
            }
        }
#endif

        private void ExecuteTestSuiteItems(TreeNode parent)
        {
            string fileName;

            if (parent.Checked)
            {
                foreach (TreeNode node in parent.Nodes)
                {
                    if (node.Checked)
                    {
                        if (null != (_currentTestFileEntry = (node.Tag as TestSuiteFileEntry)))
                        {
                            fileName = _currentTestFileEntry.FileName;

                            txtConsole.Text += Header(fileName);
                            txtAST.Text += Header(fileName);
                            txtIntRep.Text += Header(fileName);

                            if (tcFiles.TabPages.ContainsKey(fileName))
                            {
                                TabPage tabPage = tcFiles.TabPages[fileName];
                                TextBox textBox = tabPage.Controls[0] as TextBox;

                                Debug.Assert(null != textBox, "");

                                _cmdRunStart.Execute(fileName, textBox.Text);
                            }
                            else
                            {
                                _cmdRunStart.Execute(fileName);
                            }
                        }
                        else if (null != (node.Tag as TestSuiteSuiteEntry))
                        {
                            ExecuteTestSuiteItems(node);
                        }
                    }
                }
            }
        }

        private string Header(string title)
        {
            int len = title.Length;
            StringBuilder s = new StringBuilder(3 * title.Length);
            s.Append('-', len);
            s.AppendLine();
            s.AppendLine(title);
            s.Append('-', len);
            s.AppendLine();
            return s.ToString();
        }
        
        private void mnuRunPause_Click(object sender, EventArgs e)
        {
            try
            {
                _cmdRunPause.Execute();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuRunContinue_Click(object sender, EventArgs e)
        {
            try
            {
                _cmdRunContinue.Execute();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void mnuRunStop_Click(object sender, EventArgs e)
        {
            try
            {
                _cmdRunStop.Execute();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        #endregion Menus

        private void DisplayException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //txtConsole.Text += ex.ToString() + ":" + ex.Message;
        }

        private void DisplayInfoMessage(string message)
        {
            MessageBox.Show(message, "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DisplayAppUserException(Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //txtConsole.Text += ex.ToString() + ":" + ex.Message;
        }

        private void WriteConsole(string msg)
        {
            txtConsole.Text += msg;
        }

        private void App_OnSystemOut(object sender, InterpOutEventArgs e)
        {
            txtConsole.Text += e.Message;
        }

        private void App_OnIrOut(object sender, IrOutEventArgs e)
        {
            string data = e.Data.Replace("\n", "\r\n");
            txtIntRep.Text += data;
        }

        private void App_OnAstOut(object sender, AstOutEventArgs e)
        {
            string data = e.Data.Replace("\n", "\r\n");
            txtAST.Text += data;

            if (null != _currentTestFileEntry)
                CompareAst(_currentTestFileEntry.AstRefFileName, e.Data);
        }

        private void CompareAst(string AstRefFileName, string AstData)
        {
            string refData = File.ReadAllText(AstRefFileName, Encoding.ASCII);
            refData = refData.Replace("\r", "");
            refData = refData.Replace("\n", "");
            //refData = refData.Replace(" ", "");
            
            string astData = AstData.Replace("\r", "");
            astData = astData.Replace("\n", "");
            //astData = astData.Replace(" ", "");

            Debug.Write(astData.Length);
            Debug.Write(refData.Length);

            int comparisonResult = string.Compare(astData, refData);

            int min = Math.Min(astData.Length, refData.Length);

            for (int i = 0; i < min; ++i)
            {
                if (astData[i] != refData[i])
                {
                    Debug.WriteLine("Index = " + i.ToString() + ": " + astData[i].ToString() + ", " + refData[i].ToString());
                    break;
                }
            }

        }

        private void App_OnErrorOut(object sender, AppErrorEventArgs e)
        {
            txtConsole.Text += e.Message;
        }

        private void App_OnExceptionOut(object sender, AppExceptionEventArgs e)
        {
            txtConsole.Text += e.Message;
        }

        private void App_OnOpenFile(object sender, OpenFileEventArgs e)
        {
            TreeNode node = null;

            try
            {
                if (this.tcFiles.Controls.ContainsKey(e.FileName))
                {
                    this.tcFiles.SelectTab(e.FileName);
                }
                else
                {
                    _fileName = e.FileName;
                    this.Text = Application.ProductName + ": " + e.Name;
                    //txtFile.Text = e.Code;
                    IDEMode = IDEModes.SingleFile;
                    ClearAllFileTabs();
                    tvFiles.Nodes.Clear();
                    node = tvFiles.Nodes.Add(e.Name);
                    node.Tag = new TestSuiteFileEntry(e.FileName, null, null, null, 0);
                    ShowTabPage(node);
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void App_OnSaveFile(object sender, SaveFileEventArgs e)
        {
            Debug.Assert(tcFiles.TabPages.ContainsKey(e.FileName), "Can't reconcile saved file: " + e.FileName);

            if (tcFiles.TabPages.ContainsKey(e.FileName))
            {
                tcFiles.TabPages[e.FileName].Text = e.Name;
            }

            TestSuiteFileEntry fileEntry = tcFiles.TabPages[e.FileName].Tag as TestSuiteFileEntry;

            Debug.Assert(null != fileEntry, "Can't reconcile fileEntry for saved file: " + e.FileName);

            if (fileEntry != null)
                fileEntry.Changed = false;

            DetermineSaveMenuState();
        }

        private void ClearAllFileTabs()
        {
            TestSuiteFileEntry fileEntry = null;

            foreach (TabPage tabPage in tcFiles.TabPages)
            {
                fileEntry = tabPage.Tag as TestSuiteFileEntry;
                Debug.Assert(fileEntry != null);
                if (fileEntry.Changed)
                {
                    SaveFile(fileEntry, true);
                }
            }

            tcFiles.TabPages.Clear();
            DetermineSaveMenuState();
        }

        private void DetermineSaveMenuState()
        {
            TestSuiteFileEntry fileEntry = null;
            bool enableSaveAll = false;
            bool enableSave = false;

            foreach (TabPage tabPage in tcFiles.TabPages)
            {
                fileEntry = tabPage.Tag as TestSuiteFileEntry;
                Debug.Assert(fileEntry != null);
                if (fileEntry.Changed)
                {
                    enableSaveAll = true;
                    if (tabPage == tcFiles.SelectedTab)
                    {
                        enableSave = true;
                    }
                }
            }

            mnuFileSaveAll.Enabled = enableSaveAll;
            mnuFileSave.Enabled = enableSave;
        }

        private void App_OnOpenTestSuite(object sender, OpenTestSuiteEventArgs e)
        {
            TreeNode node;

            try
            {
                this.Text = Application.ProductName + " - " + e.TestSuite.Name;
                //txtFile.Text = "";
                IDEMode = IDEModes.TestSuite;
                ClearAllFileTabs();
                tvFiles.Nodes.Clear();
                node = tvFiles.Nodes.Add(e.TestSuite.Name);
                node.Tag = null;

                AddSuite(node, (_testSuite = e.TestSuite).Items);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void AddSuite(TreeNode parent, IList<TestSuiteEntry> Items)
        {
            // [REVISIT] NEED CATCH FOR INFINITE RECURSION
            TestSuiteSuiteEntry suiteEntry;
            TreeNode node;

            parent.Checked = true;

            foreach (TestSuiteEntry item in Items)
            {
                node = parent.Nodes.Add(item.Name);
                node.Tag = item;
                node.Checked = true;
                if (null != (suiteEntry = item as TestSuiteSuiteEntry))
                {
                    TestSuite testSuite = _testSuiteManager.TestSuites[item.Name];
                    AddSuite(node, testSuite.Items);
                }
            }
        }

        private void App_OnOpenTestSuiteFile(object sender, OpenTestSuiteFileEventArgs e)
        {
            //txtFile.Text = e.Code;
            IDEMode = IDEModes.TestSuite;
        }

        private IDEModes IDEMode 
        { 
            set 
            {
                if (value == IDEForm.IDEModes.SingleFile)
                {
                    _ideMode = IDEForm.IDEModes.SingleFile;
                    tvFiles.CheckBoxes = false;
                    tvFiles.ShowLines = false;
                    mnuFileAddFiles.Visible = false;
                    mnuFileRemoveFile.Visible = false;
                    mnuFileDeleteTestSuite.Visible = false;
                    mnuSepAddFiles.Visible = false;
                    mnuFileCloseTestSuite.Visible = false;
                    LockEdit = false;
                }
                else
                {
                    _ideMode = IDEForm.IDEModes.TestSuite;
                    tvFiles.CheckBoxes = true;
                    tvFiles.ShowLines = true;
                    mnuFileAddFiles.Visible = true;
                    mnuFileRemoveFile.Visible = true;
                    mnuFileDeleteTestSuite.Visible = true;
                    mnuFileCloseTestSuite.Visible = true;
                    mnuSepAddFiles.Visible = true;
                    LockEdit = true;
                }
            }
            get
            {
                return _ideMode;
            }
        }

        private bool LockEdit
        {
            get { return _lockEdit; }
            set { _lockEdit = value; }
        }

        private void txtFile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\u0001') ||
                (e.KeyChar == '\u0003'))
                return;

            if (_lockEdit)
                e.KeyChar = '\0';
        }

        private void txtFile_KeyDown(object sender, KeyEventArgs e)
        {
            if (_lockEdit && (e.KeyValue == 46)) // Suppress delete
            {
                e.SuppressKeyPress = true;;
            }
        }

        private void tvFiles_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                ShowTabPage(e.Node);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void ShowTabPage(TreeNode node)
        {
            TestSuiteFileEntry fileEntry = null;

            if ((null == node) || (null == (fileEntry = node.Tag as TestSuiteFileEntry)))
                return;

            mnuFileClose.Enabled = true;

            if (this.tcFiles.Controls.ContainsKey(fileEntry.FileName))
            {
                //System.Windows.Forms.TabPage tabPage1 = (System.Windows.Forms.TabPage)this.tcFiles.Controls[fileEntry.FileName];
                tcFiles.SelectTab(fileEntry.FileName);
                return;
            }

            //txtFile.Clear();
            // this.Text = Application.ProductName + " - " + _testSuite.Name + ": " + fileEntry.Name;
            // _cmdOpenTestSuiteFile.Execute(fileEntry.FileName);

            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox();
            System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage();

            this.tcFiles.SuspendLayout();
            tabPage.SuspendLayout();

            // 
            // tcFiles
            // 
            this.tcFiles.Controls.Add(tabPage);
            // 
            // tabPage
            // 
            tabPage.Controls.Add(textBox);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = fileEntry.FileName;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(430, 249);
            tabPage.TabIndex = 0;
            tabPage.UseVisualStyleBackColor = true;
            tabPage.Tag = node.Tag;
            // 
            // textBox
            // 
            textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox.Font = new System.Drawing.Font("Courier New", 11.25F);
            textBox.Location = new System.Drawing.Point(3, 3);
            textBox.Multiline = true;
            textBox.Name = "textBox1";
            textBox.Size = new System.Drawing.Size(424, 243);
            textBox.TabIndex = 0;
            textBox.TextChanged += new System.EventHandler(this.tcFiles_TextChanged);
            textBox.Tag = node.Tag;

            using (StreamReader sr = new StreamReader(fileEntry.FileName, Encoding.ASCII))
            {
                textBox.Text = sr.ReadToEnd();
                textBox.SelectionStart = 0;
                textBox.SelectionLength = 0;
                sr.Close();
            }
            tabPage.Text = fileEntry.Name;

            this.tcFiles.ResumeLayout(false);
            tabPage.ResumeLayout(false);
            tabPage.PerformLayout();
            tcFiles.SelectTab(fileEntry.FileName);
            tcFiles.Visible = true;
            fileEntry.Changed = false;
            DetermineSaveMenuState();
        }

        private void tvFiles_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DisplayFileNode(tvFiles.SelectedNode);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void DisplayFileNode(TreeNode node)
        {
            if (null != node)
            {
                TestSuiteFileEntry fileEntry = node.Tag as TestSuiteFileEntry;

                if (null != fileEntry)
                {
                    //txtFile.Clear();
                    this.Text = Application.ProductName + " - " + _testSuite.Name + ": " + fileEntry.Name;
                    _cmdOpenTestSuiteFile.Execute(fileEntry.FileName);
                }
            }
        }

        private void txtConsole_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void tcFiles_TextChanged(object sender, EventArgs e)
        {
            TestSuiteFileEntry fileEntry = null;
            System.Windows.Forms.TextBox textBox;

            Debug.Assert(null != sender, "null sender encountered");
            if (sender == null)
                return;

            textBox = sender as System.Windows.Forms.TextBox;
            Debug.Assert(null != textBox, "Unexpected sender: sender != textbox");
            if (textBox == null)
                return;

            fileEntry = textBox.Tag as TestSuiteFileEntry;
            Debug.Assert(null != fileEntry, "fileEntry missing from tag");
            if (null == fileEntry)
                return;

            fileEntry.Changed = true;

            TabPage tc = tcFiles.TabPages[fileEntry.FileName];
            tc.Text = fileEntry.Name + "*";

            mnuFileSave.Enabled = true;
            mnuFileSaveAll.Enabled = true;
        }

        private List<System.Windows.Forms.TabPage> tabPages = new List<System.Windows.Forms.TabPage>();

        private void tcFiles_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage != null)
            {
                TestSuiteFileEntry fileEntry = e.TabPage.Tag as TestSuiteFileEntry;

                Debug.Assert(null != fileEntry, "fileEntry missing from tag");

                if (null != fileEntry)
                {
                    mnuFileSave.Enabled = fileEntry.Changed;
                }
                mnuFileClose.Enabled = true;
            }
            else
            {
            }
        }

        private void mnuBuildCompile_Click(object sender, EventArgs e)
        {

        }
    }
}

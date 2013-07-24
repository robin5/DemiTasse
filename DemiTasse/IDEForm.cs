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
        private AppIDE.AppIDE app = null;
        private TestSuiteManager _testSuiteManager = null;
        private enum IDEModes { SingleFile, TestSuite }
        private IDEModes _ideMode;
        private string _fileName;
        private bool _lockEdit = false;
        private StringBuilder _intermediateRepresentation = new StringBuilder();
        private TestSuiteFileEntry _currentTestFileEntry;


        private Command cmdNewFile = null;
        private Command cmdNewTestSuite = null;
        private Command cmdOpenFile = null;
        private Command cmdOpenTestSuite = null;
        private Command cmdOpenTestSuiteFile = null;
        private Command cmdAddFile = null;
        private Command cmdClose = null;
        private Command cmdCloseTestSuite = null;
        private Command cmdRunStartSingleFile = null;
        private Command cmdRunStartTestSuite = null;
        private Command cmdRunPause = null;
        private Command cmdRunContinue = null;
        private Command cmdRunStop = null;

        private TestSuite _testSuite = null;

        public IDEForm()
        {
            InitializeComponent();

            (_testSuiteManager = new TestSuiteManager()).Init("");

            app = new AppIDE.AppIDE(_testSuiteManager);
            app.AddOnAppError(OnErrorOut);
            app.AddOnAppException(OnExceptionOut);
            app.AddOnOpenFile(OnOpenFile);
            app.AddOnOpenTestSuite(OnOpenTestSuite);
            app.AddOnOpenTestSuiteFile(OnOpenTestSuiteFile);
            app.AddOnSystemOut(OnSystemOut);
            app.AddOnIrOut(OnIrOut);
            app.AddOnAstOut(OnAstOut);

            cmdNewFile = new CmdNewFile(app);
            cmdNewTestSuite = new CmdNewTestSuite(app);
            cmdOpenFile = new CmdOpenFile(app);
            cmdOpenTestSuite = new CmdOpenTestSuite(app);
            cmdOpenTestSuiteFile = new CmdOpenTestSuiteFile(app);
            cmdAddFile = new CmdAddFile(app);
            cmdClose = new CmdNotYetImplemented();
            cmdCloseTestSuite = new CmdNotYetImplemented();
            cmdRunStartSingleFile = new CmdRunStartSingleFile(app);
            cmdRunPause = new CmdNotYetImplemented();
            cmdRunContinue = new CmdNotYetImplemented();
            cmdRunStop = new CmdNotYetImplemented();
        }

        private void IDEForm_Load(object sender, EventArgs e)
        {
            Text = "DemiTasse (untitled)";
            IDEMode = IDEModes.SingleFile;
        }

        private void mnuFileNewFile_Click(object sender, EventArgs e)
        {
            try
            {
                cmdNewFile.Execute();
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
                cmdNewTestSuite.Execute("New Validation Tests");
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
                    dialog.Filter = "MiniJava files (*.java)|*.java|Abstract Syntax Tree files (*.ast)|*.ast|Intermediate Representation files (*.ir)|*.ir|All files (*.*)|*.*";

                    if (DialogResult.OK == dialog.ShowDialog())
                    {
                        txtFile.Clear();
                        cmdOpenFile.Execute(dialog.FileName);
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
            string[] testSuiteNames = new string[_testSuiteManager.TestSuites.Count];

            _testSuiteManager.TestSuites.Keys.CopyTo(testSuiteNames, 0);

            try
            {
                using (OpenTestSuiteForm f = new OpenTestSuiteForm(testSuiteNames))
                {
                    if (DialogResult.OK == f.ShowDialog())
                    {
                        cmdOpenTestSuite.Execute(f.TestSuiteName);
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
            try
            {
                cmdAddFile.Execute();
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
                cmdClose.Execute();
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
                cmdCloseTestSuite.Execute();
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
                    cmdRunStartSingleFile.Execute(_fileName);
                }
                else
                {
                    if (tvTestSuite.Nodes.Count > 0)
                    {
                        ExecuteTestSuiteItems(tvTestSuite.Nodes[0]);
                    }
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
            if (parent.Checked)
            {
                foreach (TreeNode node in parent.Nodes)
                {
                    if (node.Checked)
                    {
                        if (null != (_currentTestFileEntry = (node.Tag as TestSuiteFileEntry)))
                        {
                            txtConsole.Text += Header(_currentTestFileEntry.FileName);
                            txtAST.Text += Header(_currentTestFileEntry.FileName);
                            txtIntRep.Text += Header(_currentTestFileEntry.FileName);
                            cmdRunStartSingleFile.Execute(_currentTestFileEntry.FileName);
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
                cmdRunPause.Execute();
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
                cmdRunContinue.Execute();
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
                cmdRunStop.Execute();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void DisplayException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //txtConsole.Text += ex.ToString() + ":" + ex.Message;
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

        private void OnSystemOut(object sender, InterpOutEventArgs e)
        {
            txtConsole.Text += e.Message;
        }

        private void OnIrOut(object sender, IrOutEventArgs e)
        {
            string data = e.Data.Replace("\n", "\r\n");
            txtIntRep.Text += data;
        }

        private void OnAstOut(object sender, AstOutEventArgs e)
        {
            string data = e.Data.Replace("\n", "\r\n");
            txtAST.Text += data;

            if (null != _currentTestFileEntry)
                CompareAst(_currentTestFileEntry.ReferenceFileName, e.Data);
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

        private void OnErrorOut(object sender, AppErrorEventArgs e)
        {
            txtConsole.Text += e.Message;
        }

        private void OnExceptionOut(object sender, AppExceptionEventArgs e)
        {
            txtConsole.Text += e.Message;
        }

        private void OnOpenFile(object sender, OpenFileEventArgs e)
        {
            string title = (new FileInfo(e.FileName)).Name;
            _fileName = e.FileName;
            this.Text = Application.ProductName + ": " + title;
            txtFile.Text = e.Code;
            IDEMode = IDEModes.SingleFile;
        }

        private void OnOpenTestSuite(object sender, OpenTestSuiteEventArgs e)
        {
            TreeNode node;

            try
            {
                this.Text = Application.ProductName + " - " + e.TestSuite.Name;
                txtFile.Text = "";
                IDEMode = IDEModes.TestSuite;

                tvTestSuite.Nodes.Clear();
                node = tvTestSuite.Nodes.Add(e.TestSuite.Name);
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

        private void OnOpenTestSuiteFile(object sender, OpenTestSuiteFileEventArgs e)
        {
            txtFile.Text = e.Code;
            IDEMode = IDEModes.TestSuite;
        }

        private IDEModes IDEMode 
        { 
            set 
            {
                if (value == IDEForm.IDEModes.SingleFile)
                {
                    _ideMode = IDEForm.IDEModes.SingleFile;
                    splitContainer1.Panel1Collapsed = true;
                    LockEdit = false;
                }
                else
                {
                    _ideMode = IDEForm.IDEModes.TestSuite;
                    splitContainer1.Panel1Collapsed = false;
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

        private void tvTestSuite_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                DisplayFileNode(e.Node);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void tvTestSuite_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DisplayFileNode(tvTestSuite.SelectedNode);
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
                    txtFile.Clear();
                    this.Text = Application.ProductName + " - " + _testSuite.Name + ": " + fileEntry.Name;
                    cmdOpenTestSuiteFile.Execute(fileEntry.FileName);
                }
            }
        }
    }
}

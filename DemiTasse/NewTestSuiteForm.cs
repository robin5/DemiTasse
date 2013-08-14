// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: NewTestSuiteForm.cs
// *
// * Description:  Used to select an existing test project
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
using System.Diagnostics;

namespace DemiTasse
{
    public partial class NewTestSuiteForm : Form
    {
        private string _newTestProjectName = "";
        private string[] _testProjectNames;

        public NewTestSuiteForm(string[] testSuitNames)
        {
            InitializeComponent();
            _newTestProjectName = "";
            _testProjectNames = testSuitNames;
            btnOK.Enabled = false;
        }

        public string TestSuiteName
        {
            get { return _newTestProjectName; }
        }

        private void NewTestSuiteForm_Load(object sender, EventArgs e)
        {
            Debug.Assert(_testProjectNames != null, "_TestSuitNames = null");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (IsUniqueTestProjectName(_newTestProjectName))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Test project '" + _newTestProjectName + "' already exists. Please enter a unique project name.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtNewTestProject_TextChanged(object sender, EventArgs e)
        {
            _newTestProjectName = txtNewTestProject.Text.Trim();
            btnOK.Enabled = (_newTestProjectName.Length > 0);
        }

        private bool IsUniqueTestProjectName(string newTestProjectName)
        {
            foreach (string name in _testProjectNames)
            {
                if (0 == string.Compare(name, newTestProjectName, true))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

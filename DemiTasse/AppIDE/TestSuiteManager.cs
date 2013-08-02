// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: TestSuiteManager.cs
// *
// * Description:  
// *
// *
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
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.AppIDE
{
    [Serializable]
    class TestSuiteManager : ITestSuiteManager
    {
        public void FakeInit()
        {

#if true
            TestSuite ts = null;
            string[] files = null;

            if (_testSuites.Count > 0)
                return;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string parserDir = baseDir + @"tst\parser\";
            string editDir = baseDir + @"tst\edit\";
            string astDir = baseDir + @"tst\ast\";
            string irDir = baseDir + @"tst\ir\";

            // ---------------
            // Mini-Java Tests
            // ---------------
            files = Directory.GetFiles(parserDir, "test??.java", SearchOption.TopDirectoryOnly);
            ts = Create("Mini-Java Tests");
            for (int i = 0; i < files.Length; ++i)
            {
                ts.AddTestFile(
                    files[i], // Mini-java test file
                    files[i].Replace(".java", ".ast.ref"),  // AST reference file
                    files[i].Replace(".java", ".ir.ref"),  // Intermedite Representation reference file
                    files[i].Replace(".java", ".so.ref")); // System Out reference file
            }

            // -----------------------------------
            // Mini-Java Tests For Testing Editing
            // -----------------------------------

            files = Directory.GetFiles(editDir, "test??.java", SearchOption.TopDirectoryOnly);
            ts = Create("Mini-Java tests for testing editing");
            for (int i = 0; i < files.Length; ++i)
            {
                ts.AddTestFile(
                    files[i],   // Mini-java test file
                    null,       // AST reference file
                    null,       // Intermedite Representation reference file
                    null);      // System Out reference file
            }

            // ---------------------------------------
            // Mini-Java Tests For Editing Test Suites
            // ---------------------------------------

            files = Directory.GetFiles(editDir, "test??.java", SearchOption.TopDirectoryOnly);
            ts = Create("Mini-Java tests for editing test suites");
            for (int i = 0; i < files.Length; ++i)
            {
                ts.AddTestFile(
                    files[i],   // Mini-java test file
                    null,       // AST reference file
                    null,       // Intermedite Representation reference file
                    null);      // System Out reference file
            }

            // ----------------------------------
            // Mini-Java Tests For parsing errors
            // ----------------------------------

            files = Directory.GetFiles(parserDir, "errp??.java", SearchOption.TopDirectoryOnly);
            ts = Create("Error Parser Tests");
            for (int i = 0; i < files.Length; ++i)
            {
                ts.AddTestFile(
                    files[i],                                // Mini-java test file with parsing errors
                    null,                                    // AST reference file - not generated
                    null,                                    // IR reference file - not generated
                    files[i].Replace(".java", ".perr.ref")); // System Out reference file
            }

            files = Directory.GetFiles(astDir, "test??.ast", SearchOption.TopDirectoryOnly);
            ts = Create("Abstract Syntax Tree -> IR Tests");
            for (int i = 0; i < files.Length; ++i)
            {
                ts.AddTestFile(
                    files[i],                             // AST input file
                    null,                                 // AST reference file - not generated
                    files[i].Replace(".ast", ".ir.ref"), // IR reference file
                    null);                                // System Out reference file - not generated
            }

            ts = Create("IR Execution tests");
            files = Directory.GetFiles(irDir, "test??.ir", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; ++i)
            {
                ts.AddTestFile(
                    files[i],                        // IR input file
                    null,                            // AST reference file - not generated
                    null,                            // IR reference file - not generated
                    files[i].Replace(".ir", ".so")); // System Out reference file
            }
#endif

        }

        public TestSuite Create(string name)
        {
            if (_testSuites.ContainsKey(name))
            {
                throw new AppUserErrorException("Suite name already exists: " + name);
            }

            TestSuite testSuite = new TestSuite(name);
            _testSuites.Add(name, testSuite);

            return testSuite;
        }

        public void AddTestSuiteFiles(string name, int index, string[] fileNames)
        {
            if (!_testSuites.ContainsKey(name))
                throw new Exception("Could not update test suite: " + name);

            TestSuite t = _testSuites[name];
            t.InsertTestFiles(index, fileNames);
        }

        public void RemoveTestSuiteFile(string name, int index)
        {
            if (!_testSuites.ContainsKey(name))
                throw new Exception("Could not update test suite: " + name);

            _testSuites[name].Items.RemoveAt(index);
        }

        public Dictionary<string, TestSuite> TestSuites
        { 
            get { return _testSuites; } 
        }

        public string[] TestSuiteNames
        {
            get
            {
                string[] testSuiteNames = new string[_testSuites.Count];

                _testSuites.Keys.CopyTo(testSuiteNames, 0);
                return testSuiteNames;
            }
        }

        private Dictionary<string, TestSuite> _testSuites = new Dictionary<string, TestSuite>();
    }
}

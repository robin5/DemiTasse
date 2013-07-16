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

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.AppIDE
{
    class TestSuiteManager
    {
        public static void Init(string serializationFileName)
        {
            TestSuite ts;
            string[] files = null;

            try
            {
                ts = Create("IR Validation Tests (Good)");
                files = Directory.GetFiles(@"C:\Users\Robin\Documents\GitHub\DemiTasse\DemiTasse\tst\ir\", "*.ir", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < files.Length; ++i)
                {
                    ts.AddTestFile(files[i]);
                }

                ts = Create("IR Validation Tests (Bad First Test)");
                ts.AddTestFile(@"C:\Users\Robin\Documents\GitHub\DemiTasse\DemiTasse\tst\ir\test00.ir");
                for (int i = 0; ((i < files.Length) && (i < 10)); ++i)
                {
                    ts.AddTestFile(files[i]);
                }

                ts = Create("Misc Tests + additional test suite");
                for (int i = 0; i < 10; ++i)
                {
                    ts.AddTestFile(files[i]);
                }
                ts.AddTestSuite("IR Validation Tests (Good)");

                files = Directory.GetFiles(@"C:\Users\Robin\Documents\GitHub\DemiTasse\DemiTasse\tst\ast\", "*.ast", SearchOption.TopDirectoryOnly);
                ts = Create("Abstract Syntax Tree Tests");
                for (int i = 0; i < files.Length; ++i)
                {
                    ts.AddTestFile(files[i]);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static TestSuite Create(string name)
        {
            if (_testSuites.ContainsKey(name))
            {
                throw new AppUserErrorException("Suite name already exists: " + name);
            }

            TestSuite testSuite = new TestSuite(name);
            _testSuites.Add(name, testSuite);

            return testSuite;
        }

        public static Dictionary<string, TestSuite> TestSuites
        { 
            get { return _testSuites; } 
        }

        public static TestSuite Open(string name)
        {
            return new TestSuite(name);
        }

        private static Dictionary<string, TestSuite> _testSuites = new Dictionary<string, TestSuite>();
    }
}

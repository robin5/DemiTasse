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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    class TestSuiteManager
    {
        public static void Init(string serializationFileName)
        {
            TestSuite ts;

            ts = Create("IR Validation Tests (Good)");
            for (int i = 1; i < 10; ++i)
            {
                ts.AddTestFile(@"C:\Users\Robin\Documents\Visual Studio 2010\Projects\PSU\DemiTasse\DemiTasse\bin\Debug\tst\test0" + i.ToString() + ".ir");
            }

            ts = Create("IR Validation Tests (Bad First Test)");
            for (int i = 0; i < 10; ++i)
            {
                ts.AddTestFile(@"C:\Users\Robin\Documents\Visual Studio 2010\Projects\PSU\DemiTasse\DemiTasse\bin\Debug\tst\test0" + i.ToString() + ".ir");
            }

            ts = Create("Lexical Tests");
            for (int i = 0; i < 10; ++i)
            {
                ts.AddTestFile(@"C:\Users\Robin\Documents\Visual Studio 2010\Projects\PSU\DemiTasse\DemiTasse\bin\Debug\tst\test1" + i.ToString() + ".ir");
            }
            ts.AddTestSuite("IR Validation Tests (Good)");
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

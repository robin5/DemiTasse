// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: TestSuite.cs
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
    public class TestSuite
    {
        public TestSuite(string name)
        {
            _name = name;
        }

        public void AddTestFile(string fileName, string astRefFileName, string irRefFileName, string soRefFileName)
        {
            TestSuiteFileEntry entry = new TestSuiteFileEntry(fileName, astRefFileName, irRefFileName, soRefFileName);
            _items.Add(entry);
        }

        public void AddTestSuite(string name)
        {
            _items.Add(new TestSuiteSuiteEntry(name));
        }

        public string Name
        {
            get { return _name; }
        }

        public IList<TestSuiteEntry> Items
        {
            get { return _items.AsReadOnly(); }
        }

        public override string ToString()
        {
            return _name;
        }

        private string _name;
        private List<TestSuiteEntry> _items = new List<TestSuiteEntry>();
    }
}

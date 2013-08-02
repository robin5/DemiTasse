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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DemiTasse.AppIDE
{
    [Serializable]
    public class TestSuite
    {
        private int entryIndex = 0;

        public TestSuite(string name)
        {
            _name = name;
        }

#if false
        private TestSuite(SerializationInfo info, StreamingContext context)
        {
            _name = info.GetString("Name");
            _items = (List<TestSuiteEntry>)info.Get
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", _name);
            info.AddValue("Items", _items);
        }

#endif

        public TestSuiteFileEntry AddTestFile(string fileName)
        {
            TestSuiteFileEntry entry = new TestSuiteFileEntry(fileName, null, null, null, entryIndex);
            _items.Add(entry);
            entryIndex++;
            return entry;
        }

        public TestSuiteFileEntry InsertTestFile(int index, string fileName)
        {
            TestSuiteFileEntry entry = new TestSuiteFileEntry(fileName, null, null, null, entryIndex);
            _items.Insert(index, entry);
            return entry;
        }

        public TestSuiteFileEntry InsertTestFile(int index, TestSuiteFileEntry entry)
        {
            _items.Insert(index, entry);
            return entry;
        }

        public void InsertTestFiles(int firstIndex, string[] fileNames)
        {
            int index = firstIndex;

            if (index < 0)
            {
                foreach (string fileName in fileNames)
                {
                    AddTestFile(fileName, null, null, null);
                }
            }
            else
            {
                foreach (string fileName in fileNames)
                {
                    _items.Insert(index, new TestSuiteFileEntry(fileName, null, null, null, index));
                    index++;
                }
            }
            ReIndexTestSuiteEntries();
        }

        private void ReIndexTestSuiteEntries()
        {
            entryIndex = 0;
            foreach (TestSuiteEntry entry in _items)
            {
                entry.ReIndex(entryIndex++);
            }
        }

        public void AddTestFile(string fileName, string astRefFileName, string irRefFileName, string soRefFileName)
        {
            TestSuiteFileEntry entry = new TestSuiteFileEntry(fileName, astRefFileName, irRefFileName, soRefFileName, entryIndex);
            entryIndex++;
            _items.Add(entry);
        }

        public void AddTestSuite(string name)
        {
            _items.Add(new TestSuiteSuiteEntry(name, entryIndex));
            entryIndex++;
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

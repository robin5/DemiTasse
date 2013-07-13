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

        public void AddTestFile(string fileName)
        {
            _items.Add(new TestSuiteFileEntry(fileName));
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

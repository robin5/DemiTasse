using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DemiTasse.AppIDE
{
    public class TestSuiteEntry
    {
        public TestSuiteEntry(string title)
        {
            _name = title;
        }

        public override string ToString()
        {
            return _name;
        }

        public string Name 
        { 
            get { return _name; } 
        }

        private string _name;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DemiTasse.AppIDE
{
    class TestSuiteFileEntry : TestSuiteEntry
    {
        public TestSuiteFileEntry(string fileName)
            : base((new FileInfo(fileName)).Name)
        {
            _fileName = fileName;
        }

        public string FileName 
        { 
            get { return _fileName; } 
        }

        private string _fileName;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    interface ITestSuiteManager
    {
        //void Init(string serializationFileName);
        TestSuite Create(string name);
        void AddTestSuiteFiles(string name, int index, string[] fileNames);
        Dictionary<string, TestSuite> TestSuites { get; }
    }
}

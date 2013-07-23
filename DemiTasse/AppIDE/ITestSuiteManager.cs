using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    interface ITestSuiteManager
    {
        void Init(string serializationFileName);
        TestSuite Create(string name);
        TestSuite Open(string name);
        Dictionary<string, TestSuite> TestSuites { get; }
    }
}

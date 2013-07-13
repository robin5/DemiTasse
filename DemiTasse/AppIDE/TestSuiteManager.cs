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

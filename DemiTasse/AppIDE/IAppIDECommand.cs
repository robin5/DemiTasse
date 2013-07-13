using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    interface IAppIDECommand
    {
        void NewFile();
        void NewTestSuite(string name);
        void OpenFile(string fileName);
        void OpenTestSuite(string name);
        void OpenTestSuiteFile(string fileName);
        void AddFile();
        void Close();
        void CloseTestSuite();
        void RunStartSingleFile(string fileName);
        void RunStartTestSuite(string name);
        void RunPause();
        void RunContinue();
        void RunStop();
    }
}

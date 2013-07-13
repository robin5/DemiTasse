using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    public class OpenFileEventArgs : EventArgs
    {
        private string _code = null;
        private string _fileName = null;

        public OpenFileEventArgs(string fileName, string code)
        {
            _fileName = fileName;
            _code = code;
        }

        public string FileName { get { return _fileName; } }
        public string Code { get { return _code; } }
    }
}

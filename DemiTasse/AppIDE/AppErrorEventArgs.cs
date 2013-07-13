using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    public class AppErrorEventArgs : EventArgs
    {
        private string _message = null;

        public AppErrorEventArgs(string message)
        {
            _message = message + "\r\n";
        }

        public string Message { get { return _message; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.interp
{
    public class InterpOutEventArgs : EventArgs
    {
        private string _message = null;

        public InterpOutEventArgs()
        {
            _message = "\r\n";
        }

        public InterpOutEventArgs(string message)
        {
            _message = message + "\r\n";
        }

        public string Message { get { return _message; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class IrOutEventArgs : EventArgs
    {
        private string _message = null;

        public IrOutEventArgs()
        {
            _message = "\r\n";
        }

        public IrOutEventArgs(string message)
        {
            _message = message + "\r\n";
        }

        public string Message { get { return _message; } }
    }
}

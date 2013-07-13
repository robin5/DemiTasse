using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    public class AppExceptionEventArgs : EventArgs
    {
        private string _message = null;

        public AppExceptionEventArgs(Exception ex)
        {
            _message = ex.Message + "\r\n";
        }

        public string Message { get { return _message; } }
    }

}

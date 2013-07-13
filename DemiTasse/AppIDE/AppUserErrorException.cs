using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    class AppUserErrorException : Exception
    {
        public AppUserErrorException(string message)
            : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.irpsr
{
    public class Error : Exception
    {
        public Error()
            : base("Error")
        {
        }

        public Error(string msg)
            : base("Error: " + msg)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.irpsr
{
    public class UnsupportedEncodingException : Exception
    {
        public UnsupportedEncodingException()
            : base()
        {
        }
        
        public UnsupportedEncodingException(string msg)
            : base(msg)
        {
        }
        
        public UnsupportedEncodingException(Exception ex)
            : base(ex.Message)
        {
        }
    }

    public class RuntimeException : Exception
    {
        public RuntimeException()
            : base()
        {
        }
        
        public RuntimeException(string msg)
            : base(msg)
        {
        }
        
        public RuntimeException(Exception ex)
            : base(ex.Message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.interp
{
    public class InterpException : Exception 
    {
        public InterpException()
            : base()
        {
        }
        
        public InterpException(String msg)
            : base("InterpException: " + msg)
        {
        }

        public InterpException(string message, Exception innerException)
            : base(message,  innerException)
        {
        }

        public InterpException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}

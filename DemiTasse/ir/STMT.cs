using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public abstract class STMT : IR 
    {
        public abstract STMT accept(IIrVI v);
        public abstract int accept(IIntVI v);
    }
}

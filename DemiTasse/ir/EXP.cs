using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public abstract class EXP : IR
    {
        public abstract EXP accept(IIrVI v);
        public abstract int accept(IIntVI v);
    }
}

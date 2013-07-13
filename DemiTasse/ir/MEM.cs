using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class MEM : EXP 
    {
        public EXP exp;

        public MEM(EXP e) { exp=e; }

        public override void dump() { DUMP(" (MEM"); DUMP(exp); DUMP(")"); }

        public override EXP accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class FIELD : EXP
    {
        public EXP obj;
        public int idx;

        public FIELD(EXP o, int i) { obj = o; idx = i; }

        public override void dump() { DUMP(" (FIELD"); DUMP(obj); DUMP(" " + idx + ")"); }

        public override EXP accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }
}

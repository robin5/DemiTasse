using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class PARAM : EXP
    {
        public int idx;

        public PARAM(int i) { idx=i; }

        public override void dump() { DUMP(" (PARAM " + idx + ")"); }

        public override EXP accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }
}

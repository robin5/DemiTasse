using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class VAR : EXP
    {
        public int idx;

        public VAR(int i) { idx=i; }

        public override void dump() { DUMP(" (VAR " + idx + ")"); }

        public override EXP accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }
}

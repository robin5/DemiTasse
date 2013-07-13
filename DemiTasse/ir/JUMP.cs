using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class JUMP : STMT
    {
        public NAME target;

        public JUMP(NAME e) { target=e; }

        public override void dump() { DUMP(" [JUMP"); DUMP(target); DUMP("]\n"); }

        public override STMT accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }

}

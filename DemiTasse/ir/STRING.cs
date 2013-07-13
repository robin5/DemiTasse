using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class STRING : EXP
    {
        public String s;

        public STRING(String v) { s=v; }

        public override void dump() { DUMP(" (STRING \"" + s + "\")"); }

        public override EXP accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class LABEL : STMT
    { 
        public String lab;

        public LABEL(String s) { lab=s; }
        public LABEL(NAME n)   { lab=n.id; }

        public override void dump() { DUMP(" [LABEL " + lab +"]\n"); }

        public override STMT accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }
}

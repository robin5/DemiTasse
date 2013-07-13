using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class NAME : EXP
    {
        public static int count=0;
        
        public String id;

        public NAME() { id = "L" + count++; }
        public NAME(String n) { id = n; }

        public override void dump() { DUMP(" (NAME " + id + ")"); }

        public override EXP accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class TEMP : EXP
    {
        private static int count=0;
        public int num;
  
        public TEMP() { num = ++count; }
        public TEMP(int n) { num=n; }

        public override void dump() { DUMP(" (TEMP " + num + ")"); }

        public override EXP accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class RETURN : STMT
    {
        public EXP exp;

        public RETURN() { exp=null; }
        public RETURN(EXP e) { exp=e; }

        public override void dump() { 
        DUMP(" [RETURN"); if (exp!=null) exp.dump(); DUMP("]\n");
        }

        public override STMT accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }
}

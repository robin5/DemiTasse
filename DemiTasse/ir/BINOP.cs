using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class BINOP : EXP 
    {
        public enum OP { ADD=0, SUB=1, MUL=2, DIV=3, AND=4, OR=5}
        public OP op;
        public EXP left, right;

        public BINOP(OP b, EXP l, EXP r) { op=b; left=l; right=r; }

        private void dumpOp(OP op) 
        {
            switch (op) {
            case OP.ADD: DUMP("+"); break;
            case OP.SUB: DUMP("-"); break;
            case OP.MUL: DUMP("*"); break;
            case OP.DIV: DUMP("/"); break;
            case OP.AND: DUMP("&&"); break;
            case OP.OR:  DUMP("||"); break;
            //default:  DUMP("?");
            }
        }

        public override void dump() 
        { 
            DUMP(" (BINOP "); 
            dumpOp(op); 
            DUMP(left); 
            DUMP(right); 
            DUMP(")");
        }

          public override EXP accept(IIrVI v) { return v.visit(this); }
          public override int accept(IIntVI v) { return v.visit(this); }
    }

}

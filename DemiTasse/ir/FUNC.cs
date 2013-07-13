using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class FUNC : IR {
        public String label;
        public int varCnt, tmpCnt, argCnt;
        public STMTlist stmts;

        public FUNC(String l, STMTlist sl) 
        {
            label=l; varCnt=0; tmpCnt=0; argCnt=0; stmts=sl; 
        }

        public FUNC(String l, int vc, int tc, int ac, STMTlist sl) 
        {
            label=l; varCnt=vc; tmpCnt=tc; argCnt=ac; stmts=sl; 
        }

        public override void dump() 
        { 
            DUMP("" + label + " (varCnt=" + varCnt + ", tmpCnt=" + tmpCnt
            + ", argCnt=" + argCnt + ") {\n"); 
            DUMP(stmts);
            DUMP("}\n");
        }

        public FUNC accept(IIrVI v)
        { 
            return v.visit(this); 
        }

        public void accept(IIntVI v) 
        { 
            v.visit(this); 
        }
    }
}

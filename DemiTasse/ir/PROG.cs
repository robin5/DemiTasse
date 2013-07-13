using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class PROG : IR
    {
        public FUNClist funcs;

        public PROG(FUNClist fl) 
        { 
            funcs = fl; 
        }

        public override void dump() 
        { 
            DUMP("IR_PROGRAM\n"); funcs.dump(); 
        }
        
        public PROG accept(IIrVI v)
        { 
            return v.visit(this); 
        }

        public void accept(IIntVI v)
        { 
            v.visit(this); 
        }
    }
}

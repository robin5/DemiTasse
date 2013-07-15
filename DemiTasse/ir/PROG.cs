// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: PROG.cs
// *
// * Author:  Jingke Li
// *
// **********************************************************************************
// *
// * Granting License: TBD
// * 
// **********************************************************************************

// **********************************************************************************
// * Using
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// **********************************************************************************
// * Implementation
// **********************************************************************************

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

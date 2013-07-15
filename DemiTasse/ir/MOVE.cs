// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: MOVE.cs
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
    public class MOVE : STMT
    {
        public EXP dst, src;

        public MOVE(EXP d, EXP s) 
        { 
            dst=d; 
            src=s; 
        }

        public override void dump() 
        { 
            DUMP(" [MOVE"); DUMP(dst); DUMP(src); DUMP("]\n"); 
        }

        public override STMT accept(IIrVI v) 
        { 
            return v.visit(this); 
        }

        public override int accept(IIntVI v) 
        { 
            return v.visit(this); 
        }
    }
}

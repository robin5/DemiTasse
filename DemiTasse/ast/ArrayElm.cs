// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: ArrayElm.cs
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

using DemiTasse.ir;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ast
{
    public class ArrayElm : Exp
    {
        public Exp array, idx;

        public ArrayElm(Exp e1, Exp e2)
        { 
            array = e1; 
            idx = e2; 
        }

        public override void dump()
        {
            DUMP("(ArrayElm "); DUMP(array); DUMP(idx); DUMP(") ");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) /* throws Exception */ { return v.visit(this); }
        public override EXP accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

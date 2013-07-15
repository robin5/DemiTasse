// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: If.cs
// *
// * Author:  Jingke Li
// *
// **********************************************************************************
// *
// * Granting License: TBD
// * 
// **********************************************************************************

using DemiTasse.ir;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ast
{

    public class If : Stmt
    {
        public Exp e;
        public Stmt s1, s2;

        public If(Exp ae, Stmt as1, Stmt as2) { e = ae; s1 = as1; s2 = as2; }

        public override void dump()
        {
            DUMP("\n (If "); DUMP(e); DUMP(s1); DUMP(s2); DUMP(") ");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public override void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        public override STMT accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}


// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: Ast.cs
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
    public class Assign : Stmt
    {
        public Exp lhs, rhs;

        public Assign(Exp e1, Exp e2) { lhs = e1; rhs = e2; }

        public override void dump()
        {
            DUMP("\n (Assign "); DUMP(lhs); DUMP(rhs); DUMP(") ");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public override void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        public override STMT accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}


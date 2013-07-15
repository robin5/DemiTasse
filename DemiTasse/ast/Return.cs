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
    public class Return : Stmt
    {
        public Exp e;

        public Return(Exp ae) { e = ae; }

        public override void dump() { DUMP("\n (Return "); DUMP(e); DUMP(") "); }

        public override void accept(VoidVI v) { v.visit(this); }
        public override void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        public override STMT accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

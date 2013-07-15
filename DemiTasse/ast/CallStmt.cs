// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: CallStmt.cs
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

    public class CallStmt : Stmt
    {
        public Exp obj;
        public Id mid;
        public ExpList args;

        public CallStmt(Exp e, Id mi, ExpList el) { obj = e; mid = mi; args = el; }

        public override void dump()
        {
            DUMP("\n (CallStmt "); DUMP(obj); DUMP(mid);
            DUMP("ExpList", args); DUMP(") ");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public override void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        public override STMT accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

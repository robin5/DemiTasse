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

    public class Block : Stmt
    {
        public StmtList sl;

        public Block(StmtList asl) { sl = asl; }

        public override void dump() { DUMP("\n (Block "); DUMP("StmtList", sl); DUMP(") "); }

        public override void accept(VoidVI v) { v.visit(this); }
        public override void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        public override STMT accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}


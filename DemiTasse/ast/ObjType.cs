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

namespace DemiTasse.ast
{
    public class ObjType : Type
    {
        public Id cid;

        public ObjType(Id i) { cid = i; }

        public override void dump() { DUMP("(ObjType "); DUMP(cid); DUMP(") "); }

        public override string toString() { return cid.s; }
        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) { return v.visit(this); } /* throws Exception */
    }
}

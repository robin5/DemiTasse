// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: Call.cs
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

    public class Call : Exp
    {
        public Exp obj;
        public Id mid;
        public ExpList args;

        public Call(Exp e, Id mi, ExpList el) { obj = e; mid = mi; args = el; }

        public override void dump()
        {
            DUMP("(Call "); DUMP(obj); DUMP(mid);
            DUMP("ExpList", args); DUMP(") ");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) /* throws Exception */ { return v.visit(this); }
        public override EXP accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

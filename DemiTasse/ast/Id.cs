// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: Id.cs
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
    public class Id : Exp
    {
        public string s;

        public Id(string _as) { s = _as; }

        public override void dump() { DUMP("(Id " + s + ") "); }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) /* throws Exception */ { return v.visit(this); }
        public override EXP accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

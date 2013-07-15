// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: Formal.cs
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
    public class Formal : Ast
    {
        public Type t;
        public Id id;

        public Formal(Type at, Id i) { t = at; id = i; }

        public override void dump() { DUMP("(Formal "); DUMP(t); DUMP(id); DUMP(") "); }

        public override void accept(VoidVI v) { v.visit(this); }
        public void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
    }
}
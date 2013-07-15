// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: Field.cs
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

    public class Field : Exp
    {
        public Exp obj;
        public Id var;

        public Field(Exp e, Id v) { obj = e; var = v; }

        public override void dump()
        {
            DUMP("(Field "); DUMP(obj); DUMP(var); DUMP(") ");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) /* throws Exception */ { return v.visit(this); }
        public override EXP accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

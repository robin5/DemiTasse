// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: IntVal.cs
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
    public class IntVal : Exp
    {
        public int i;

        public IntVal(int ai) { i = ai; }

        public string toString() { return "int"; }

        public override void dump() { DUMP("(IntVal " + i + ") "); }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) { return v.visit(this); }
        public override EXP accept(TransVI v) { return v.visit(this); }
    }
}
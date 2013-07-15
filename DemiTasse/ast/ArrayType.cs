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
    public class ArrayType : Type
    {
        public Type et;

        public ArrayType(Type t) { et = t; }

        public override string toString() { return et.toString() + "[]"; }

        public override void dump() { DUMP("(ArrayType "); DUMP(et); DUMP(") "); }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) { return v.visit(this); }
    }
}

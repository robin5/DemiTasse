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
    public class BasicType : Type
    {
        public static readonly int Int = 0;
        public static readonly int Bool = 1;
        public int typ;

        public BasicType(int t) { typ = t; }

        public string typName(int typ)
        {
            switch (typ)
            {
                case 0 /* BasicType.Int */: return "int";
                case 1 /* BasicType.Bool */: return "boolean";
                default: return "?";
            }
        }

        public override string toString() { return typName(typ); }

        public override void dump() { DUMP("(BasicType " + typName(typ) + ") "); }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) { return v.visit(this); }
    }
}
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
    public class Relop : Exp
    {
        public enum OP { EQ = 0, NE = 1, LT = 2, LE = 3, GT = 4, GE = 5 }
        public OP op;
        public Exp e1, e2;

        public Relop(OP o, Exp ae1, Exp ae2)
        {
            op = o; e1 = ae1; e2 = ae2;
        }

        public string opName(Relop.OP op)
        {
            switch (op)
            {
                case Relop.OP.EQ: return "== ";
                case Relop.OP.NE: return "!= ";
                case Relop.OP.LT: return "< ";
                case Relop.OP.LE: return "<= ";
                case Relop.OP.GT: return "> ";
                case Relop.OP.GE: return ">= ";
                default: return "?? ";
            }
        }

        public override void dump()
        { 
            DUMP("(Relop " + opName(op)); DUMP(e1); DUMP(e2); DUMP(") ");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) /* throws Exception */ { return v.visit(this); }
        public override EXP accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

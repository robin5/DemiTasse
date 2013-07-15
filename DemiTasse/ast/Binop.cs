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
    public class Binop : Exp
    {
        public enum OP { ADD = 0, SUB = 1, MUL = 2, DIV = 3, AND = 4, OR = 5 }
        public OP op;
        public Exp e1, e2;
  
        public Binop(OP o, Exp ae1, Exp ae2) { op=o; e1=ae1; e2=ae2; }

        public string opName(OP op)
        {
            switch (op)
            {
                case Binop.OP.ADD: return "+";
                case Binop.OP.SUB: return "-";
                case Binop.OP.MUL: return "*";
                case Binop.OP.DIV: return "/";
                case Binop.OP.AND: return "&&";
                case Binop.OP.OR: return "||";
                default:  return "?";
            }
        }

        public override void dump()
        {
            DUMP("(Binop " + opName(op) + " "); DUMP(e1); DUMP(e2); DUMP(") ");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) /* throws Exception */ { return v.visit(this); }
        public override EXP accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

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
    public class Unop : Exp
    {
        public enum OP { NEG = 0, NOT = 1 };
        public OP op;
        public Exp e;
  
        public Unop(OP o, Exp ae) { op=o; e=ae; }

        public string opName(OP op)
        {
            switch (op)
            {
                case OP.NEG : return "-";
                case OP.NOT : return "!";
                default:  return "?";
            }
        }

        public override void dump() { DUMP("(Unop " + opName(op) + " "); DUMP(e); DUMP(") "); }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) /* throws Exception */ { return v.visit(this); }
        public override EXP accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: BINOP.cs
// *
// * Author:  Jingke Li
// *
// **********************************************************************************
// *
// * Granting License: TBD
// * 
// **********************************************************************************

// **********************************************************************************
// * Using
// **********************************************************************************

using System.Diagnostics;

using DemiTasse.ast;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ir
{
    public class CJUMP : STMT
    {
        public enum OP { EQ=0, NE=1, LT=2, LE=3, GT=4, GE=5}
        public OP op;
        public EXP left, right;
        public NAME target;

        public CJUMP(OP o, EXP l, EXP r, NAME t)
        {
            op = o; left = l; right = r; target = t;
        }

        public CJUMP(Relop.OP o, EXP l, EXP r, NAME t)
        {
            left = l; right = r; target = t;
            switch (o)
            {
                case Relop.OP.EQ: op = OP.EQ; break;
                case Relop.OP.NE: op = OP.NE; break;
                case Relop.OP.LT: op = OP.LT; break;
                case Relop.OP.LE: op = OP.LE; break;
                case Relop.OP.GT: op = OP.GT; break;
                case Relop.OP.GE: op = OP.GE; break;
                default: Debug.Assert(false, "Encountered unknown binary operator: value = " + o.ToString()); break;
            }
        }

        private void dumpOp(OP op)
        {
        switch (op)
        {
            case OP.EQ: DUMP("=="); break;
            case OP.NE: DUMP("!="); break;
            case OP.LT: DUMP("<"); break;
            case OP.LE: DUMP("<="); break;
            case OP.GT: DUMP(">"); break;
            case OP.GE: DUMP(">="); break;
            //default: DUMP("??");
        }
  }

  public override void dump() { 
    DUMP(" [CJUMP "); dumpOp(op);
    DUMP(left); DUMP(right); DUMP(target); DUMP("]\n");
  }

  public override STMT accept(IIrVI v) { return v.visit(this); }
  public override int accept(IIntVI v) { return v.visit(this); }
}
}

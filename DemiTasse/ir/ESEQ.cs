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
// * Implementation
// **********************************************************************************

namespace DemiTasse.ir
{
public class ESEQ : EXP {
  public STMT stmt;
  public EXP exp;

  public ESEQ(STMT s, EXP e) { stmt=s; exp=e; }

  public override void dump() { 
    DUMP(" (ESEQ\n"); DUMP(stmt); DUMP(exp); DUMP(")");
  }

  public override EXP accept(IIrVI v) { return v.visit(this); }
  public override int accept(IIntVI v) { return v.visit(this); }
}
}

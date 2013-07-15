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
public class CONST : EXP {
  public int val;

  public CONST(int v) { val=v; }

  public override void dump() { DUMP(" (CONST " + val + ")"); }

  public override EXP accept(IIrVI v) { return v.visit(this); }
  public override int accept(IIntVI v) { return v.visit(this); }
}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

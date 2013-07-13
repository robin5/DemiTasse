using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
public class CALL : EXP {
  public NAME func;
  public EXPlist args;

  public CALL(NAME f, EXPlist a) { func=f; args=a; }

  public override void dump() { 
    DUMP(" (CALL"); DUMP(func); DUMP(args); DUMP(")");
  }

  public override EXP accept(IIrVI v) { return v.visit(this); }
  public override int accept(IIntVI v) { return v.visit(this); }
}

}

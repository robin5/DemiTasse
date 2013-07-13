using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
public class CALLST : STMT {
  public NAME func;
  public EXPlist args;

  public CALLST(NAME f, EXPlist a) { func=f; args=a; }

  public override void dump() { 
    DUMP(" [CALLST"); DUMP(func); DUMP(args); DUMP("]\n");
  }

  public override STMT accept(IIrVI v) { return v.visit(this); }
  public override int accept(IIntVI v) { return v.visit(this); }
}

}

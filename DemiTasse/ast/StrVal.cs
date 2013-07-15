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

using System;
using System.Collections.Generic;
using System.Text;

using DemiTasse.ir;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ast
{
    public class StrVal : Exp
    {
        public string s;

        public StrVal(string n) { s = n; }

        public override void dump() { DUMP("(StrVal \"" + s + "\") "); }

        public override void accept(VoidVI v) { v.visit(this); }
        public override Type accept(TypeVI v) { return v.visit(this); }
        public override EXP accept(TransVI v) { return v.visit(this); }
    }
}

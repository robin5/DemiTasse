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

    public class Program : Ast
    {
        public ClassDeclList cl;

        public Program(ClassDeclList acl) { cl = acl; }

        public override void dump()
        {
            DUMP("(Program "); DUMP("ClassDeclList\n", cl); DUMP(")\n");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        public PROG accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

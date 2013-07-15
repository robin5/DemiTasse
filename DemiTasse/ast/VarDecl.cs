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
    public class VarDecl : Ast 
    {
        public Type t;
        public Id var;
        public Exp e;
  
        public VarDecl(Type at, Id i, Exp ae)
        { 
            t = at; 
            var = i; 
            e = ae; 
        }

        public override void dump()
        {
            DUMP("(VarDecl ");
            DUMP(t);
            DUMP(var);
            DUMP(e);
            DUMP(") "); 
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        public STMT accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

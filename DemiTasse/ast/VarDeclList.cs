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
    public class VarDeclList : AstList
    {
        public VarDeclList()
            : base()
        {
        }

        public void add(VarDecl n) { base.add(n); }
        
        new public VarDecl elementAt(int i)  { return (VarDecl)base.elementAt(i); }

        public override void accept(VoidVI v) { v.visit(this); }
        
        public void accept(TypeVI v)  { v.visit(this); } /* throws Exception */
        
        public STMTlist accept(TransVI v) { return v.visit(this); } /* throws Exception */
    }
}
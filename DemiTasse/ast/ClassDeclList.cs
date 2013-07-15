// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: ClassDeclList.cs
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

    public class ClassDeclList : AstList
    {
        public ClassDeclList():base() 
        { 
        }
        public void add(ClassDecl n) { base.add(n); }
        new public ClassDecl elementAt(int i)
        {
            return (ClassDecl)base.elementAt(i);
        }
        public override void accept(VoidVI v) { v.visit(this); }
        public void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        public FUNClist accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

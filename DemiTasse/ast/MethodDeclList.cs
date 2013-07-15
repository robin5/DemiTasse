// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: MethodDeclList.cs
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

    public class MethodDeclList : AstList
    {
        public MethodDeclList()
            :base()
        { 
        }

        public void add(MethodDecl n)
        { 
            base.add(n); 
        }

        public MethodDecl elementAt(int i)
        {
            return (MethodDecl)base.elementAt(i);
        }

        public override void accept(VoidVI v) { v.visit(this); }

        public void accept(TypeVI v) /* throws Exception */ { v.visit(this); }

        public FUNClist accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

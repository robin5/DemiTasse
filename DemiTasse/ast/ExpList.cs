// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: ExpList.cs
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

    public class ExpList : AstList
    {
        public ExpList()
            :base()
        { 
        }

        public void add(Exp n)
        { 
            base.add(n); 
        }

        public void addAll(ExpList el)
        { 
            base.addAll(el); 
        }
        
        public new Exp elementAt(int i)
        { 
            return (Exp)base.elementAt(i); 
        }

        public override void accept(VoidVI v) { v.visit(this); }
        
        public void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        
        public EXPlist accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

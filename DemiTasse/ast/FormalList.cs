// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: FormalList.cs
// *
// * Author:  Jingke Li
// *
// **********************************************************************************
// *
// * Granting License: TBD
// * 
// **********************************************************************************

namespace DemiTasse.ast
{
    public class FormalList : AstList
    {
        public FormalList()
            : base()
        {
        }

        public void add(Formal n) { base.add(n); }
        
        new public Formal elementAt(int i)
        {
            return (Formal)base.elementAt(i);
        }
        
        public override void accept(VoidVI v) { v.visit(this); }
        public void accept(TypeVI v) { v.visit(this); } /* throws Exception */
    }
}

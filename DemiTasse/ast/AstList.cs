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
using System.Linq;
using System.Text;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ast
{
    public class AstList : Ast
    {
        private List<Ast> list;

        public AstList()
        { 
            list = new List<Ast>(); 
        }

        public void add(Ast n)
        { 
            list.Add(n); 
        }

        public void addAll(AstList l)
        { 
            list.AddRange(l.list); 
        }

        public Ast elementAt(int i)
        { 
            return list[i]; 
        }

        public int size()
        { 
            return list.Count(); 
        }

        public override void dump() 
        {
            for (int i=0; i<size(); i++) 
            elementAt(i).dump();
        }

        public Ast this[int i]
        {
            get { return list[i]; }
            set { list[i] = value; }
        }


        public override void accept(VoidVI v) { v.visit(this); }
        public void accept(TypeVI v) { v.visit(this); } /* throws Exception */
    }

}
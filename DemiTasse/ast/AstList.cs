// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: AstList.cs
// *
// * Author:  Jingke Li
// *
// **********************************************************************************
// *
// * Granting License: The MIT License (MIT)
// * 
// *   Permission is hereby granted, free of charge, to any person obtaining a copy
// *   of this software and associated documentation files (the "Software"), to deal
// *   in the Software without restriction, including without limitation the rights
// *   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// *   copies of the Software, and to permit persons to whom the Software is
// *   furnished to do so, subject to the following conditions:
// *   The above copyright notice and this permission notice shall be included in
// *   all copies or substantial portions of the Software.
// *   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// *   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// *   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// *   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// *   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// *   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// *   THE SOFTWARE.
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
        //public void accept(TypeVI v) { v.visit(this); } /* throws Exception */
    }

}
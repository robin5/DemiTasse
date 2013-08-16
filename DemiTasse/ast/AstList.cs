// **********************************************************************************
// * Copyright (c) 2013 Jingke Li, Robin Murray
// **********************************************************************************
// *
// * File: AstList.cs
// *
// * Author:  Jingke Li (Portland State University)
// *
// * C# Translation:  Robin Murray
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
        private List<Ast> _list;

        public AstList()
        { 
            _list = new List<Ast>(); 
        }

        public void Add(Ast n)
        { 
            _list.Add(n); 
        }

        public int Count()
        { 
            return _list.Count(); 
        }

        public override void GenerateAstData()
        {
            for (int i = 0; i < Count(); i++)
                this[i].GenerateAstData();
        }

        public Ast this[int i]
        {
            get { return _list[i]; }
            set { _list[i] = value; }
        }

        public override void accept(VoidVI v) { v.visit(this); }
    }

}
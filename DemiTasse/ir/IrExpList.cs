// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: EXPlist.cs
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

// **********************************************************************************
// * Using
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ir
{
    public class IrExpList : IrExp
    {
        private List<IrExp> list;

        public IrExpList() { list = new List<IrExp>(); }
        public IrExpList(IrExp e) { list = new List<IrExp>(); list.Add(e); }

        public void add(IrExp e) { list.Add(e); }
        public void addAll(IrExpList el) { list.AddRange(el.list); }
        public IrExp elementAt(int i) { return list[i]; }
        public int size() { return list.Count(); }

        public override void GenerateIrData() 
        {
            Append(" ("); 
            for (int i=0; i<size(); i++)
                Append(elementAt(i));
            Append(")"); 
        }

        public override IrExp accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }
}
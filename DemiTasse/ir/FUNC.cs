// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: FUNC.cs
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

// **********************************************************************************
// * Using
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ir
{
    public class FUNC : IR {
        public String label;
        public int varCnt, tmpCnt, argCnt;
        public STMTlist stmts;

        public FUNC(String l, STMTlist sl) 
        {
            label=l; varCnt=0; tmpCnt=0; argCnt=0; stmts=sl; 
        }

        public FUNC(String l, int vc, int tc, int ac, STMTlist sl) 
        {
            label=l; varCnt=vc; tmpCnt=tc; argCnt=ac; stmts=sl; 
        }

        public override void dump() 
        { 
            DUMP("" + label + " (varCnt=" + varCnt + ", tmpCnt=" + tmpCnt
            + ", argCnt=" + argCnt + ") {\n"); 
            DUMP(stmts);
            DUMP("}\n");
        }

        public FUNC accept(IIrVI v)
        { 
            return v.visit(this); 
        }

        public void accept(IIntVI v) 
        { 
            v.visit(this); 
        }
    }
}

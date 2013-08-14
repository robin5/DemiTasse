﻿// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: BINOP.cs
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

using System.Diagnostics;

using DemiTasse.ast;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ir
{
    public class BINOP : EXP 
    {
        public enum OP { ADD=0, SUB=1, MUL=2, DIV=3, AND=4, OR=5}
        public OP op;
        public EXP left, right;

        public BINOP(OP b, EXP l, EXP r)
        { 
            op = b; left = l; right = r; 
        }

        public BINOP(Binop.OP b, EXP l, EXP r)
        { 
            left = l; right = r;
            switch (b)
            {
                case Binop.OP.ADD: op = OP.ADD; break;
                case Binop.OP.SUB: op = OP.SUB; break;
                case Binop.OP.MUL: op = OP.MUL; break;
                case Binop.OP.DIV: op = OP.DIV; break;
                case Binop.OP.AND: op = OP.AND; break;
                case Binop.OP.OR: op = OP.OR; break;
                default: Debug.Assert(false, "Encountered unknown binary operator: value = " + b.ToString()); break;
            }
        }



        private void dumpOp(OP op) 
        {
            switch (op)
            {
                case OP.ADD: DUMP("+"); break;
                case OP.SUB: DUMP("-"); break;
                case OP.MUL: DUMP("*"); break;
                case OP.DIV: DUMP("/"); break;
                case OP.AND: DUMP("&&"); break;
                case OP.OR:  DUMP("||"); break;
                default: DUMP("?"); break;
            }
        }

        public override void dump() 
        { 
            DUMP(" (BINOP "); 
            dumpOp(op); 
            DUMP(left); 
            DUMP(right); 
            DUMP(")");
        }

          public override EXP accept(IIrVI v) { return v.visit(this); }
          public override int accept(IIntVI v) { return v.visit(this); }
    }

}

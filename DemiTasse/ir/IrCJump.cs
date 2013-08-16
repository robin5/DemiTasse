// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: CJUMP.cs
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

using System.Diagnostics;

using DemiTasse.ast;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ir
{
    public class IrCJump : IrStmt
    {
        public enum OP { EQ = 0, NE = 1, LT = 2, LE = 3, GT = 4, GE = 5 }
        public OP op;
        public IrExp left, right;
        public IrName target;

        public IrCJump(OP o, IrExp l, IrExp r, IrName t)
        {
            op = o; left = l; right = r; target = t;
        }

        public IrCJump(AstRelop.OP o, IrExp l, IrExp r, IrName t)
        {
            left = l; right = r; target = t;
            switch (o)
            {
                case AstRelop.OP.EQ: op = OP.EQ; break;
                case AstRelop.OP.NE: op = OP.NE; break;
                case AstRelop.OP.LT: op = OP.LT; break;
                case AstRelop.OP.LE: op = OP.LE; break;
                case AstRelop.OP.GT: op = OP.GT; break;
                case AstRelop.OP.GE: op = OP.GE; break;
                default: Debug.Assert(false, "Encountered unknown binary operator: value = " + o.ToString()); break;
            }
        }

        private void dumpOp(OP op)
        {
            switch (op)
            {
                case OP.EQ: Append("=="); break;
                case OP.NE: Append("!="); break;
                case OP.LT: Append("<"); break;
                case OP.LE: Append("<="); break;
                case OP.GT: Append(">"); break;
                case OP.GE: Append(">="); break;
                //default: DUMP("??");
            }
        }

        public override void GenerateIrData()
        {
            Append(" [CJUMP "); dumpOp(op);
            Append(left); Append(right); Append(target); Append("]\n");
        }

        public override IrStmt accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }
}

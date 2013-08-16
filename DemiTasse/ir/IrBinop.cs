// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: BINOP.cs
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
    public class IrBinop : IrExp 
    {
        public enum OP { ADD = 0, SUB = 1, MUL = 2, DIV = 3, AND = 4, OR = 5 }
        public OP op;
        public IrExp left, right;

        public IrBinop(OP b, IrExp l, IrExp r)
        { 
            op = b; left = l; right = r; 
        }

        public IrBinop(AstBinop.OP b, IrExp l, IrExp r)
        { 
            left = l; 
            right = r;
            
            switch (b)
            {
                case AstBinop.OP.ADD: op = OP.ADD; break;
                case AstBinop.OP.SUB: op = OP.SUB; break;
                case AstBinop.OP.MUL: op = OP.MUL; break;
                case AstBinop.OP.DIV: op = OP.DIV; break;
                case AstBinop.OP.AND: op = OP.AND; break;
                case AstBinop.OP.OR: op = OP.OR; break;
                default: Debug.Assert(false, "Encountered unknown binary operator: value = " + b.ToString()); break;
            }
        }

        private void Append(OP op) 
        {
            switch (op)
            {
                case OP.ADD: Append("+"); break;
                case OP.SUB: Append("-"); break;
                case OP.MUL: Append("*"); break;
                case OP.DIV: Append("/"); break;
                case OP.AND: Append("&&"); break;
                case OP.OR:  Append("||"); break;
                default: Append("?"); break;
            }
        }

        public override void GenerateIrData() 
        { 
            Append(" (BINOP "); 
            Append(op); 
            Append(left); 
            Append(right); 
            Append(")");
        }

          public override IrExp accept(IIrVI v) { return v.visit(this); }
          public override int accept(IIntVI v) { return v.visit(this); }
    }

}

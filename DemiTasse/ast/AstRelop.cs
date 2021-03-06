// **********************************************************************************
// * Copyright (c) 2013 Jingke Li, Robin Murray
// **********************************************************************************
// *
// * File: AstRelop.cs
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

using DemiTasse.ir;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ast
{
    public class AstRelop : AstExp
    {
        public enum OP { EQ = 0, NE = 1, LT = 2, LE = 3, GT = 4, GE = 5 }
        public OP op;
        public AstExp e1, e2;

        public AstRelop(OP o, AstExp ae1, AstExp ae2)
        {
            op = o; e1 = ae1; e2 = ae2;
        }

        public string opName(AstRelop.OP op)
        {
            switch (op)
            {
                case AstRelop.OP.EQ: return "== ";
                case AstRelop.OP.NE: return "!= ";
                case AstRelop.OP.LT: return "< ";
                case AstRelop.OP.LE: return "<= ";
                case AstRelop.OP.GT: return "> ";
                case AstRelop.OP.GE: return ">= ";
                default: return "?? ";
            }
        }

        public override void GenerateAstData()
        { 
            Append("(Relop " + opName(op)); Append(e1); Append(e2); Append(") ");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public override AstType accept(TypeVI v) /* throws Exception */ { return v.visit(this); }
        public override IrExp accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

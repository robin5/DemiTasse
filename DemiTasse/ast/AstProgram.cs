// **********************************************************************************
// * Copyright (c) 2013 Jingke Li, Robin Murray
// **********************************************************************************
// *
// * File: AstProgram.cs
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
    public class AstProgram : Ast
    {
        private string _astData = null;

        public AstClassDeclList cl;

        public AstProgram(AstClassDeclList acl) { cl = acl; }

        public void GenerateAstData(bool resetData)
        {
            if (resetData)
                Ast.ResetAstData();

            GenerateAstData();
            _astData = Ast.AstData;
        }

        public string AstData
        {
            get { return _astData; }
        }

        public override void GenerateAstData()
        {
            Append("(Program "); Append("ClassDeclList\n", cl); Append(")\n");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        public IrProg accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}

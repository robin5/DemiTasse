// **********************************************************************************
// * Copyright (c) 2013 Jingke Li, Robin Murray
// **********************************************************************************
// *
// * File: VoidVI.cs
// *
// * Description:  A simple visitor interface for MINI v1.6
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

namespace DemiTasse.ast
{
    public interface VoidVI
    {
        void visit(AstProgram n);

        // Lists
        void visit(AstList n);
        void visit(AstClassDeclList n);
        void visit(AstMethodDeclList n);
        void visit(AstVarDeclList n);
        void visit(AstFormalList n);
        void visit(AstStmtList n);
        void visit(AstExpList n);

        // Declarations
        void visit(AstClassDecl n);
        void visit(AstMethodDecl n);
        void visit(AstVarDecl n);
        void visit(AstFormal n);

        // Types
        void visit(AstBasicType n);
        void visit(AstObjType n);
        void visit(AstArrayType n);

        // Statements
        void visit(AstBlock n);
        void visit(AstAssign n);
        void visit(AstCallStmt n);
        void visit(AstIf n);
        void visit(AstWhile n);
        void visit(AstPrint n);
        void visit(AstReturn n);

        // Expressions
        void visit(AstBinop n);
        void visit(AstRelop n);
        void visit(AstUnop n);
        void visit(AstArrayElm n);
        void visit(AstArrayLen n);
        void visit(AstField n);
        void visit(AstCall n);
        void visit(AstNewArray n);
        void visit(AstNewObj n);

        // Base values
        void visit(AstId n);
        void visit(AstThis n);
        void visit(AstIntVal n);
        void visit(AstBoolVal n);
        void visit(AstStrVal n);
    }
}
// **********************************************************************************
// * Copyright (c) 2013 Jingke Li, Robin Murray
// **********************************************************************************
// *
// * File: TypeVI.cs
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
    public interface TypeVI
    {
        void visit(AstProgram n) /* throws Exception */;

        // Lists
        void visit(AstList n) /* throws Exception */;
        void visit(AstClassDeclList n) /* throws Exception */;
        void visit(AstMethodDeclList n) /* throws Exception */;
        void visit(AstVarDeclList n) /* throws Exception */;
        void visit(AstFormalList n) /* throws Exception */;
        void visit(AstStmtList n) /* throws Exception */;
        void visit(AstExpList n) /* throws Exception */;

        // Declarations
        void visit(AstClassDecl n) /* throws Exception */;
        void visit(AstMethodDecl n) /* throws Exception */;
        void visit(AstVarDecl n) /* throws Exception */;
        void visit(AstFormal n) /* throws Exception */;

        // Types
        AstType visit(AstBasicType n);
        AstType visit(AstObjType n) /* throws Exception */;
        AstType visit(AstArrayType n);

        // Statements
        void visit(AstBlock n) /* throws Exception */;
        void visit(AstAssign n) /* throws Exception */;
        void visit(AstCallStmt n) /* throws Exception */;
        void visit(AstIf n) /* throws Exception */;
        void visit(AstWhile n) /* throws Exception */;
        void visit(AstPrint n) /* throws Exception */;
        void visit(AstReturn n) /* throws Exception */;

        // Expressions
        AstType visit(AstBinop n) /* throws Exception */;
        AstType visit(AstRelop n) /* throws Exception */;
        AstType visit(AstUnop n) /* throws Exception */;
        AstType visit(AstArrayElm n) /* throws Exception */;
        AstType visit(AstArrayLen n) /* throws Exception */;
        AstType visit(AstField n) /* throws Exception */;
        AstType visit(AstCall n) /* throws Exception */;
        AstType visit(AstNewArray n) /* throws Exception */;
        AstType visit(AstNewObj n) /* throws Exception */;
        AstType visit(AstId n) /* throws Exception */;
        AstType visit(AstThis n) /* throws Exception */;

        // Base values
        AstType visit(AstIntVal n);
        AstType visit(AstBoolVal n);
        AstType visit(AstStrVal n);
    }
}
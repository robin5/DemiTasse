// **********************************************************************************
// * Copyright (c) 2013 Jingke Li, Robin Murray
// **********************************************************************************
// *
// * File: TransVI.cs
// *
// * Description:  A visitor interface for IR codegen for MINI v1.7.
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
    public interface TransVI
    {
        IrProg visit(AstProgram n);

        // Declarations
        IrFuncList visit(AstClassDeclList n);
        IrFuncList visit(AstClassDecl n);
        IrFuncList visit(AstMethodDeclList n);
        IrFunc visit(AstMethodDecl n);
        IrStmtList visit(AstVarDeclList n);
        IrStmt visit(AstVarDecl n);

        // Types --- no need to process these nodes

        // Statements
        IrStmtList visit(AstStmtList n);
        IrStmt visit(AstBlock n);
        IrStmt visit(AstAssign n);
        IrStmt visit(AstCallStmt n);
        IrStmt visit(AstIf n);
        IrStmt visit(AstWhile n);
        IrStmt visit(AstPrint n);
        IrStmt visit(AstReturn n);

        // Expressions
        IrExpList visit(AstExpList n);
        IrExp visit(AstBinop n);
        IrExp visit(AstRelop n);
        IrExp visit(AstUnop n);
        IrExp visit(AstArrayElm n);
        IrExp visit(AstArrayLen n);
        IrExp visit(AstField n);
        IrExp visit(AstCall n);
        IrExp visit(AstNewArray n);
        IrExp visit(AstNewObj n);
        IrExp visit(AstId n);
        IrExp visit(AstThis n);

        // Base values
        IrExp visit(AstIntVal n);
        IrExp visit(AstBoolVal n);
        IrExp visit(AstStrVal n);
    }
}
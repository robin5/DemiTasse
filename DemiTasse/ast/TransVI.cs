// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: TransVI.cs
// *
// * Description:  A visitor interface for IR codegen for MINI v1.7.
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

using DemiTasse.ir;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ast
{
    public interface TransVI
    {
        PROG visit(Program n) /* throws Exception */;

        // Declarations
        FUNClist visit(ClassDeclList n) /* throws Exception */;
        FUNClist visit(ClassDecl n) /* throws Exception */;
        FUNClist visit(MethodDeclList n) /* throws Exception */;
        FUNC visit(MethodDecl n) /* throws Exception */;
        STMTlist visit(VarDeclList n) /* throws Exception */;
        STMT visit(VarDecl n) /* throws Exception */;

        // Types --- no need to process these nodes

        // Statements
        STMTlist visit(StmtList n) /* throws Exception */;
        STMT visit(Block n) /* throws Exception */;
        STMT visit(Assign n) /* throws Exception */;
        STMT visit(CallStmt n) /* throws Exception */;
        STMT visit(If n) /* throws Exception */;
        STMT visit(While n) /* throws Exception */;
        STMT visit(Print n) /* throws Exception */;
        STMT visit(Return n) /* throws Exception */;

        // Expressions
        EXPlist visit(ExpList n) /* throws Exception */;
        EXP visit(Binop n) /* throws Exception */;
        EXP visit(Relop n) /* throws Exception */;
        EXP visit(Unop n) /* throws Exception */;
        EXP visit(ArrayElm n) /* throws Exception */;
        EXP visit(ArrayLen n) /* throws Exception */;
        EXP visit(Field n) /* throws Exception */;
        EXP visit(Call n) /* throws Exception */;
        EXP visit(NewArray n) /* throws Exception */;
        EXP visit(NewObj n) /* throws Exception */;
        EXP visit(Id n) /* throws Exception */;
        EXP visit(This n);

        // Base values
        EXP visit(IntVal n);
        EXP visit(BoolVal n);
        EXP visit(StrVal n);
    }
}
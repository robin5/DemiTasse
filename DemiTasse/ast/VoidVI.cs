// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: VoidVI.cs
// *
// * Description:  A simple visitor interface for MINI v1.6
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

namespace DemiTasse.ast
{
    public interface VoidVI
    {
        void visit(Program n);

        // Lists
        void visit(AstList n);
        void visit(ClassDeclList n);
        void visit(MethodDeclList n);
        void visit(VarDeclList n);
        void visit(FormalList n);
        void visit(StmtList n);
        void visit(ExpList n);

        // Declarations
        void visit(ClassDecl n);
        void visit(MethodDecl n);
        void visit(VarDecl n);
        void visit(Formal n);

        // Types
        void visit(BasicType n);
        void visit(ObjType n);
        void visit(ArrayType n);

        // Statements
        void visit(Block n);
        void visit(Assign n);
        void visit(CallStmt n);
        void visit(If n);
        void visit(While n);
        void visit(Print n);
        void visit(Return n);

        // Expressions
        void visit(Binop n);
        void visit(Relop n);
        void visit(Unop n);
        void visit(ArrayElm n);
        void visit(ArrayLen n);
        void visit(Field n);
        void visit(Call n);
        void visit(NewArray n);
        void visit(NewObj n);

        // Base values
        void visit(Id n);
        void visit(This n);
        void visit(IntVal n);
        void visit(BoolVal n);
        void visit(StrVal n);
    }
}
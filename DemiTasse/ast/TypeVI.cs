// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: TypeVI.cs
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
    public interface TypeVI
    {
        void visit(Program n) /* throws Exception */;

        // Lists
        void visit(AstList n) /* throws Exception */;
        void visit(ClassDeclList n) /* throws Exception */;
        void visit(MethodDeclList n) /* throws Exception */;
        void visit(VarDeclList n) /* throws Exception */;
        void visit(FormalList n) /* throws Exception */;
        void visit(StmtList n) /* throws Exception */;
        void visit(ExpList n) /* throws Exception */;

        // Declarations
        void visit(ClassDecl n) /* throws Exception */;
        void visit(MethodDecl n) /* throws Exception */;
        void visit(VarDecl n) /* throws Exception */;
        void visit(Formal n) /* throws Exception */;

        // Types
        Type visit(BasicType n);
        Type visit(ObjType n) /* throws Exception */;
        Type visit(ArrayType n);

        // Statements
        void visit(Block n) /* throws Exception */;
        void visit(Assign n) /* throws Exception */;
        void visit(CallStmt n) /* throws Exception */;
        void visit(If n) /* throws Exception */;
        void visit(While n) /* throws Exception */;
        void visit(Print n) /* throws Exception */;
        void visit(Return n) /* throws Exception */;

        // Expressions
        Type visit(Binop n) /* throws Exception */;
        Type visit(Relop n) /* throws Exception */;
        Type visit(Unop n) /* throws Exception */;
        Type visit(ArrayElm n) /* throws Exception */;
        Type visit(ArrayLen n) /* throws Exception */;
        Type visit(Field n) /* throws Exception */;
        Type visit(Call n) /* throws Exception */;
        Type visit(NewArray n) /* throws Exception */;
        Type visit(NewObj n) /* throws Exception */;
        Type visit(Id n) /* throws Exception */;
        Type visit(This n) /* throws Exception */;

        // Base values
        Type visit(IntVal n);
        Type visit(BoolVal n);
        Type visit(StrVal n);
    }
}
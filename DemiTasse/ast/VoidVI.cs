// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: Ast.cs
// *
// * Description:  A simple visitor interface for MINI v1.6
// *
// * Author:  Jingke Li
// *
// **********************************************************************************
// *
// * Granting License: TBD
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
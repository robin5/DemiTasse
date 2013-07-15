// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: Ast.cs
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
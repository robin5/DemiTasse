// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: Ast.cs
// *
// * Description:  A visitor interface for IR codegen for MINI v1.7.
// *
// * Author:  Jingke Li
// *
// **********************************************************************************
// *
// * Granting License: TBD
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
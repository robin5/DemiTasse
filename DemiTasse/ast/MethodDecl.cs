// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: MethodDecl.cs
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
    public class MethodDecl : Ast
    {
        public Type t;
        public Id mid;
        public FormalList fl;
        public VarDeclList vl;
        public StmtList sl;

        public MethodDecl(Type at, Id i, FormalList afl, 
        VarDeclList avl, StmtList asl)
        {
            t=at; mid=i; fl=afl; vl=avl; sl=asl;
        }
 
        public override void dump()
        { 
            DUMP("(MethodDecl "); DUMP(t); DUMP(mid); DUMP("FormalList", fl); 
            DUMP("VarDeclList", vl); DUMP("StmtList", sl); DUMP(")\n ");
        }

        public override void accept(VoidVI v) { v.visit(this); }
        public void accept(TypeVI v) /* throws Exception */ { v.visit(this); }
        public FUNC accept(TransVI v) /* throws Exception */ { return v.visit(this); }
    }
}
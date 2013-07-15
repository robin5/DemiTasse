// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: irParseException.cs
// *
// * Description: 
// *
// * Author: Jingke Li
// *
// * Java to C# Translator: Robin Murray
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

// **********************************************************************************
// * Using
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DemiTasse.ir;
using DemiTasse.ast;
using DemiTasse.symbol;
using DemiTasse.typechk;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.irgen
{
    class IrgenVisitor : TransVI
    {
        private CONST cZero, cOne;
        private NAME cWordSize;
        private Table symTable;         // the top-scope symbol table
        private TypeVisitor tv;
        private ClassRec currClass;    // the current class scope
        private MethodRec currMethod;  // the current method scope
        private int maxArgCnt = -1;

        public IrgenVisitor(Table symTable_, TypeVisitor tv_)
        {
            cOne = new CONST(1);
            cZero = new CONST(0);
            cWordSize = new NAME("wSZ");
            symTable = symTable_;
            tv = tv_;
            currClass = null;
            currMethod = null;
        }

        private STMTlist flattern(STMTlist list)
        {
            STMTlist sl = new STMTlist();
            for (int i = 0; i < list.size(); i++)
            {
                STMT s = (STMT)list.elementAt(i);
                if (null != (s as STMTlist))
                {
                    sl.add(flattern((STMTlist)s));
                }
                else
                {
                    sl.add(s);
                }
            }
            return sl;
        }

        private bool trivialEXP(EXP e)
        {
            return ((null != (e as NAME)) ||
                    (null != (e as CONST)) ||
                    (null != (e as TEMP)));
        }

        private bool nullSTMT(STMT s)
        {
            if (null != (s as STMTlist))
                return ((STMTlist)s).size() == 0;
            else
                return s == null;
        }

        private STMT mergeSTMTs(STMT s1, STMT s2)
        {
            if (nullSTMT(s1)) return s2;
            if (nullSTMT(s2)) return s1;
            STMTlist sl = new STMTlist();
            sl.add(s1);
            sl.add(s2);
            return sl;
        }

        private STMT mergeSTMTs(STMT s1, STMT s2, STMT s3)
        {
            if (nullSTMT(s1)) return mergeSTMTs(s2, s3);
            if (nullSTMT(s2)) return mergeSTMTs(s1, s3);
            STMTlist sl = new STMTlist();
            sl.add(s1);
            sl.add(mergeSTMTs(s2, s3));
            return sl;
        }

        // Program ---
        // ClassDeclList cl;
        public PROG visit(DemiTasse.ast.Program n) /* throws Exception */
        {
            FUNClist funcs = n.cl.accept(this);
            return new PROG(funcs);
        }

        // DECLARATIONS

        // *** partial implementation, will be fixed in proj2 **** 
        // ClassDeclList ---
        public FUNClist visit(ClassDeclList n) /* throws Exception */ {
            FUNClist funcs = new FUNClist();
            for (int i = 0; i < n.size(); i++)
                funcs.AddRange(n.elementAt(i).accept(this));
            return funcs;
        }

        // MethodDeclList ---
        public FUNClist visit(MethodDeclList n) /* throws Exception */ {
            FUNClist funcs = new FUNClist();
            for (int i = 0; i < n.size(); i++)
                funcs.Add(n.elementAt(i).accept(this));
            return funcs;
        }

        // VarDeclList ---
        public STMTlist visit(VarDeclList n) /* throws Exception */ {
            STMTlist sl = new STMTlist();
            STMT s;
            for (int i = 0; i < n.size(); i++)
            {
                s = n.elementAt(i).accept(this);
                if (s != null)
                    sl.add(s);
            }
            if (sl.size() > 0)
                return sl;
            else
                return null;
        }

        // ClassDecl ---
        // Id cid, pid;
        // VarDeclList vl;
        // MethodDeclList ml;
        public FUNClist visit(ClassDecl n) /* throws Exception */ {

            currClass = symTable.getClass(n.cid);

            FUNClist funcs = n.ml.accept(this);
            currClass = null;
            return funcs;
        }

        // *** partial implementation, will be fixed in proj2 **** 
        // MethodDecl ---
        // Type t;
        // Id mid;
        // FormalList fl;
        // VarDeclList vl;
        // StmtList sl;
        public FUNC visit(MethodDecl n) /* throws Exception */ {
            string label;
            int varCnt = 0;
            int tmpCnt = 0;
            maxArgCnt = 0;       // note this is a class variable

            // Look up the corresponding MethodRec in the symbol table
            currMethod = currClass.getMethod(n.mid);

            TEMP.reset();

            STMTlist stmts = n.vl.accept(this);
            if (stmts == null)
                stmts = n.sl.accept(this);
            else
                stmts.add(n.sl.accept(this));

            // Create an unique label for the method by concatenating class name
            //  and method name together, with an underscore ( ) in between.

            if (currMethod.id().s.CompareTo("main") == 0)
                label = "main";
            else
                label = symTable.uniqueMethodName(currClass, currMethod.id());

            varCnt = currMethod.localCnt();
            tmpCnt = TEMP.getCount();

            currMethod = null;
            return new FUNC(label, varCnt, tmpCnt, maxArgCnt, stmts);
        }

        // VarDecl ---
        // Type t;
        // Id var;
        // Exp e;
        public STMT visit(VarDecl n) /* throws Exception */ {
            if (n.e != null)
            {
                EXP id = n.var.accept(this);
                EXP e = n.e.accept(this);
                return new MOVE(id, e);
            }
            return null;
        }

        // STATEMENTS

        // StmtList ---
        public STMTlist visit(StmtList n) /* throws Exception */ {
            STMTlist sl = new STMTlist();
            for (int i = 0; i < n.size(); i++)
                sl.add(n.elementAt(i).accept(this));
            return sl;
        }

        // Block ---
        // StmtList sl;
        public STMT visit(Block n) /* throws Exception */ {
            return n.sl.accept(this);
        }

        // Assign ---
        // Exp lhs, rhs;
        public STMT visit(Assign n) /* throws Exception */ {
            EXP lhs = n.lhs.accept(this);
            EXP rhs = n.rhs.accept(this);
            return new MOVE(lhs, rhs);
        }

        // *** partial implementation, will be fixed in proj2 **** 
        // CallStmt ---
        // Exp obj; 
        // Id mid;
        // ExpList args;
        public STMT visit(CallStmt n) /* throws Exception */ {

            string label;

            // An Important Detail: The TypeVisitor program has its own copy of
            // currClass and currMethod. These two variables need to be set to the
            // current environment before typechecking can work. Do the following:

            tv.setClass(currClass);
            tv.setMethod(currMethod);

            // 1. Perform typechecking on the obj component of the Call/CallStmt
            // node, which should return an ObjType representing the obj's class.

            ObjType t = (ObjType)n.obj.accept(tv);

            // 2. Look up the MethodRec with the class info.
            ClassRec classRec = symTable.getClass(t.cid);
            MethodRec methodRec = symTable.getMethod(classRec, n.mid);

            EXP accessLink = n.obj.accept(this);
            EXPlist el = new EXPlist(accessLink);
            el.addAll(n.args.accept(this));

            if (maxArgCnt <= n.args.size())
                maxArgCnt = n.args.size() + 1;

            if (methodRec.id().s.CompareTo("main") == 0)
                label = "main";
            else
                label = symTable.uniqueMethodName(classRec, methodRec.id());

            return new CALLST(new NAME(label), el);
            //return new CALLST(new NAME(n.mid.s), el);
        }

        // If ---
        // Exp e;
        // Stmt s1, s2;
        public STMT visit(If n) /* throws Exception */ {
            NAME alt = new NAME();
            STMTlist sl = new STMTlist();
            EXP exp = n.e.accept(this);
            sl.add(new CJUMP(CJUMP.OP.EQ, exp, cZero, alt));
            sl.add(n.s1.accept(this));
            if (n.s2 == null)
            {
                sl.add(new LABEL(alt));
            }
            else
            {
                NAME done = new NAME();
                sl.add(new JUMP(done));
                sl.add(new LABEL(alt));
                sl.add(n.s2.accept(this));
                sl.add(new LABEL(done));
            }
            return sl;
        }

        // While ---
        // Exp e;
        // Stmt s;
        public STMT visit(While n) /* throws Exception */ {
            NAME test = new NAME();
            NAME done = new NAME();
            STMTlist sl = new STMTlist();
            sl.add(new LABEL(test));
            EXP exp = n.e.accept(this);
            sl.add(new CJUMP(CJUMP.OP.EQ, exp, cZero, done));
            sl.add(n.s.accept(this));
            sl.add(new JUMP(test));
            sl.add(new LABEL(done));
            return sl;
        }

        // Print ---
        // Exp e;
        public STMT visit(Print n) /* throws Exception */ {

            NAME func = new NAME("print");

            EXPlist args = new EXPlist();

            if (n.e != null)
                args.add(n.e.accept(this));

            return new CALLST(func, args);
        }

        // Return ---  
        // Exp e;
        public STMT visit(Return n) /* throws Exception */ {
            if (n.e == null)
                return new RETURN();
            else
                return new RETURN(n.e.accept(this));
        }

        // EXPRESSIONS

        public EXPlist visit(ExpList n) /* throws Exception */ {
            EXPlist el = new EXPlist();
            for (int i = 0; i < n.size(); i++)
                el.add(n.elementAt(i).accept(this));
            return el;
        }

        // Binop ---
        // int op;
        // Exp e1, e2;
        public EXP visit(Binop n) /* throws Exception */ {
            EXP l = n.e1.accept(this);
            EXP r = n.e2.accept(this);
            return new BINOP(n.op, l, r);
        }

        // Relop ---
        // int op;
        // Exp e1, e2;
        public EXP visit(Relop n) /* throws Exception */ {
            EXP l = n.e1.accept(this);
            EXP r = n.e2.accept(this);
            STMTlist sl = new STMTlist();
            TEMP tmp = new TEMP();
            NAME done = new NAME();
            sl.add(new MOVE(tmp, cOne));
            sl.add(new CJUMP(n.op, l, r, done));
            sl.add(new MOVE(tmp, cZero));
            sl.add(new LABEL(done));
            return new ESEQ(sl, tmp);
        }

        // Unop ---
        // Exp e;
        public EXP visit(Unop n) /* throws Exception */ {
            EXP e = n.e.accept(this);
            if (n.op == Unop.OP.NEG)
                return new BINOP(BINOP.OP.SUB, cZero, e);
            else // n.op == Unop.NOT
                return new BINOP(BINOP.OP.SUB, cOne, e);
        }

        // NewArray ---
        // int size;
        public EXP visit(NewArray n) /* throws Exception */ {
            EXP size = new CONST(n.size);
            STMTlist sl = new STMTlist();
            NAME top = new NAME();
            TEMP array = new TEMP();
            TEMP cnt = new TEMP();
            EXP bound = new BINOP(BINOP.OP.MUL, new CONST(n.size + 1), cWordSize);
            sl.add(new MOVE(array, new CALL(new NAME("malloc"), new EXPlist(bound))));
            sl.add(new MOVE(new MEM(array), size));
            sl.add(new MOVE(cnt, new BINOP(BINOP.OP.ADD, array,
                           new BINOP(BINOP.OP.MUL, size, cWordSize))));
            sl.add(new LABEL(top));
            sl.add(new MOVE(new MEM(cnt), cZero));
            sl.add(new MOVE(cnt, new BINOP(BINOP.OP.SUB, cnt, cWordSize)));
            sl.add(new CJUMP(CJUMP.OP.GT, cnt, array, top));
            return new ESEQ(sl, array);
        }

        // ArrayElm ---
        // Exp array, idx;
        // BasicType et;
        public EXP visit(ArrayElm n) /* throws Exception */ {
            EXP array = n.array.accept(this);
            EXP idx = n.idx.accept(this);
            return new MEM(new BINOP(BINOP.OP.ADD, array, new BINOP(BINOP.OP.MUL,
                     new BINOP(BINOP.OP.ADD, idx, cOne), cWordSize)));
        }

        // ArrayLen ---
        // Exp array;
        public EXP visit(ArrayLen n) /* throws Exception */ {
            return new MEM(n.array.accept(this));
        }

        // *** partial implementation, will be fixed in proj2 **** 
        // NewObj ---
        // Id cid;
        public EXP visit(NewObj n) /* throws Exception */ {

            // A NewObj node should be translated into a CALL to "malloc" with a single argument representing the size
            // of the object, followed by a sequence of statements for initializing the objectâ€™s class variables.
            // A special note: the initialization expressions saved in the the symbol table entry for this purpose are
            // AST expressions, and need to be translated into IR expressions. Furthermore, this translation should be
            // conducted in the proper scope environment â€” the IrgenVisitor variable currClass needs to point to the
            // objectâ€™s class.

            STMTlist stmts = new STMTlist();

            // Calculate the size of the object
            ClassRec classRec = symTable.getClass(n.cid);              // <get the object's ClassRec from symbol table>;
            int numVars = GetNumObjVars(classRec);

            // Construct malloc call node
            EXP size = new BINOP(BINOP.OP.MUL, new CONST(numVars > 0 ? numVars : 1), cWordSize);
            CALL call = new CALL(new NAME("malloc"), new EXPlist(size));

            TEMP tmp = new TEMP();                // This TEMP will contain the location of the object variables
            stmts.add(new MOVE(tmp, call));       // Move the results of the malloc call into the temp

            GetInitStmts(classRec, stmts, tmp);   // Recursively get object's variable initialization statements

            return new ESEQ(stmts, tmp);          // Construct resulting statement
        }

        private int GetNumObjVars(ClassRec c)
        {

            int numVars = 0;

            while (null != c)
            {
                numVars += c.varCnt();
                c = c.parent();
            }
            return numVars;
        }

        private int GetInitStmts(ClassRec c, STMTlist stmts, TEMP tmp)  /* throws Exception */ {

            ClassRec saveCurrClass;
            VarRec v;
            int varCnt = 0;

            // Get init STMTs from inherited class
            if (null != c.parent())
            {
                varCnt = GetInitStmts(c.parent(), stmts, tmp);
            }

            // Add init STMTs from this class
            for (int i = 0; i < c.varCnt(); i++)
            {

                v = c.getClassVarAt(i);

                if (null == v.init())
                {
                    stmts.add(ObjVarInitStmt(tmp, varCnt + i, v.idx() - 1, cZero));
                }
                else
                {
                    saveCurrClass = currClass;
                    currClass = c;
                    EXP e = v.init().accept(this);
                    currClass = saveCurrClass;

                    stmts.add(ObjVarInitStmt(tmp, varCnt + i, v.idx() - 1, e));
                }
            }
            return varCnt + c.varCnt();
        }


        private STMT ObjVarInitStmt(EXP varLoc, int varOffset, int varIndex, EXP e)
        {
            if (0 == (varOffset))
                return (new MOVE(new MEM(varLoc), e));
            else
                return (new MOVE(new MEM(new BINOP(BINOP.OP.ADD, varLoc, (new BINOP(BINOP.OP.MUL, new CONST(varIndex), cWordSize)))), e));
        }



        // *** partial implementation, will be fixed in proj2 **** 
        // Field ---
        // Exp obj; 
        // Id var;
        public EXP visit(Field n) /* throws Exception */ {

            //return new NAME(n.var.s);
            tv.setClass(currClass);
            tv.setMethod(currMethod);

            ObjType t = (ObjType)n.obj.accept(tv);


            ClassRec classRec = symTable.getClass(t.cid);

            VarRec v = symTable.getVar(classRec, n.var);

            EXP field = n.obj.accept(this);

            return new FIELD(field, v.idx() - 1);

        }

        // *** partial implementation, will be fixed in proj2 **** 
        // Call ---
        // Exp obj; 
        // Id cid, mid;
        // ExpList args;
        public EXP visit(Call n) /* throws Exception */ {

            string label;

            // An Important Detail: The TypeVisitor program has its own copy of
            // currClass and currMethod. These two variables need to be set to the
            // current environment before typechecking can work. Do the following:

            tv.setClass(currClass);
            tv.setMethod(currMethod);

            // 1. Perform typechecking on the obj component of the Call/CallStmt
            // node, which should return an ObjType representing the obj's class.

            ObjType t = (ObjType)n.obj.accept(tv);

            // 2. Look up the MethodRec with the class info.
            ClassRec classRec = symTable.getClass(t.cid);
            MethodRec methodRec = symTable.getMethod(classRec, n.mid);

            EXP accessLink = n.obj.accept(this);
            EXPlist el = new EXPlist(accessLink);
            el.addAll(n.args.accept(this));

            if (maxArgCnt <= n.args.size())
                maxArgCnt = n.args.size() + 1;

            if (methodRec.id().s.CompareTo("main") == 0)
                label = "main";
            else
                label = symTable.uniqueMethodName(classRec, methodRec.id());

            return new CALL(new NAME(label), el);
        }

        // *** partial implementation, will be fixed in proj2 **** 
        // Id ---
        // string s;
        public EXP visit(Id n) /* throws Exception */ {

            VarRec v;

            if (currMethod != null)
            {

                if ((v = currMethod.getLocal(n)) != null)
                    return new VAR(v.idx());

                if ((v = currMethod.getParam(n)) != null)
                    return new PARAM(v.idx());
            }

            v = symTable.getVar(currClass, n);

            return new FIELD(new PARAM(0), v.idx() - 1);
        }

        // *** partial implementation, will be fixed in proj2 **** 
        // This ---
        public EXP visit(This n)
        {
            return new PARAM(0);
        }

        // IntVal ---
        // int i;
        public EXP visit(IntVal n)
        {
            return new CONST(n.i);
        }

        // BoolVal ---
        // bool b;
        public EXP visit(BoolVal n)
        {
            return new CONST(n.b ? 1 : 0);
        }

        // StrVal ---
        // string s;
        public EXP visit(StrVal n)
        {
            return new STRING(n.s);
        }
    }
}

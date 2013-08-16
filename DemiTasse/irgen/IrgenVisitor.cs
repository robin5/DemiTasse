// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: IrgenVisitor.cs
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
        private IrConst cZero, cOne;
        private IrName cWordSize;
        private SymbolTable symTable;         // the top-scope symbol table
        private TypeVisitor tv;
        private ClassRec currClass;    // the current class scope
        private MethodRec currMethod;  // the current method scope
        private int maxArgCnt = -1;

        public IrgenVisitor(SymbolTable symTable_, TypeVisitor tv_)
        {
            cOne = new IrConst(1);
            cZero = new IrConst(0);
            cWordSize = new IrName("wSZ");
            symTable = symTable_;
            tv = tv_;
            currClass = null;
            currMethod = null;
        }

        private IrStmtList flattern(IrStmtList list)
        {
            IrStmtList sl = new IrStmtList();
            for (int i = 0; i < list.size(); i++)
            {
                IrStmt s = (IrStmt)list.elementAt(i);
                if (null != (s as IrStmtList))
                {
                    sl.add(flattern((IrStmtList)s));
                }
                else
                {
                    sl.add(s);
                }
            }
            return sl;
        }

        private bool trivialEXP(IrExp e)
        {
            return ((null != (e as IrName)) ||
                    (null != (e as IrConst)) ||
                    (null != (e as IrTemp)));
        }

        private bool nullSTMT(IrStmt s)
        {
            if (null != (s as IrStmtList))
                return ((IrStmtList)s).size() == 0;
            else
                return s == null;
        }

        private IrStmt mergeSTMTs(IrStmt s1, IrStmt s2)
        {
            if (nullSTMT(s1)) return s2;
            if (nullSTMT(s2)) return s1;
            IrStmtList sl = new IrStmtList();
            sl.add(s1);
            sl.add(s2);
            return sl;
        }

        private IrStmt mergeSTMTs(IrStmt s1, IrStmt s2, IrStmt s3)
        {
            if (nullSTMT(s1)) return mergeSTMTs(s2, s3);
            if (nullSTMT(s2)) return mergeSTMTs(s1, s3);
            IrStmtList sl = new IrStmtList();
            sl.add(s1);
            sl.add(mergeSTMTs(s2, s3));
            return sl;
        }

        // Program ---
        // ClassDeclList cl;
        public IrProg visit(AstProgram n) /* throws Exception */
        {
            IrFuncList funcs = n.cl.accept(this);
            return new IrProg(funcs);
        }

        // DECLARATIONS

        // *** partial implementation, will be fixed in proj2 **** 
        // ClassDeclList ---
        public IrFuncList visit(AstClassDeclList n) {
            IrFuncList funcs = new IrFuncList();
            for (int i = 0; i < n.Count(); i++)
                funcs.AddRange(n[i].accept(this));
            return funcs;
        }

        // MethodDeclList ---
        public IrFuncList visit(AstMethodDeclList n)
        {
            IrFuncList funcs = new IrFuncList();
            for (int i = 0; i < n.Count(); i++)
                funcs.Add(n[i].accept(this));
            return funcs;
        }

        // VarDeclList ---
        public IrStmtList visit(AstVarDeclList n) {
            IrStmtList sl = new IrStmtList();
            IrStmt s;
            for (int i = 0; i < n.Count(); i++)
            {
                s = n[i].accept(this);
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
        public IrFuncList visit(AstClassDecl n) {

            currClass = symTable.GetClass(n.cid);

            IrFuncList funcs = n.ml.accept(this);
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
        public IrFunc visit(AstMethodDecl n) {
            string label;
            int varCnt = 0;
            int tmpCnt = 0;
            maxArgCnt = 0;       // note this is a class variable

            // Look up the corresponding MethodRec in the symbol table
            currMethod = currClass.GetMethod(n.mid);

            IrTemp.Reset();

            IrStmtList stmts = n.vl.accept(this);
            if (stmts == null)
                stmts = n.sl.accept(this);
            else
                stmts.add(n.sl.accept(this));

            // Create an unique label for the method by concatenating class name
            //  and method name together, with an underscore ( ) in between.

            if (currMethod.Id().s.CompareTo("main") == 0)
                label = "main";
            else
                label = symTable.UniqueMethodName(currClass, currMethod.Id());

            varCnt = currMethod.LocalCnt();
            tmpCnt = IrTemp.Count;

            currMethod = null;
            return new IrFunc(label, varCnt, tmpCnt, maxArgCnt, stmts);
        }

        // VarDecl ---
        // Type t;
        // Id var;
        // Exp e;
        public IrStmt visit(AstVarDecl n) {
            if (n.e != null)
            {
                IrExp id = n.var.accept(this);
                IrExp e = n.e.accept(this);
                return new IrMove(id, e);
            }
            return null;
        }

        // STATEMENTS

        // StmtList ---
        public IrStmtList visit(AstStmtList n) {
            IrStmtList sl = new IrStmtList();
            for (int i = 0; i < n.Count(); i++)
                sl.add(n[i].accept(this));
            return sl;
        }

        // Block ---
        // StmtList sl;
        public IrStmt visit(AstBlock n) {
            return n.sl.accept(this);
        }

        // Assign ---
        // Exp lhs, rhs;
        public IrStmt visit(AstAssign n) {
            IrExp lhs = n.lhs.accept(this);
            IrExp rhs = n.rhs.accept(this);
            return new IrMove(lhs, rhs);
        }

        // *** partial implementation, will be fixed in proj2 **** 
        // CallStmt ---
        // Exp obj; 
        // Id mid;
        // ExpList args;
        public IrStmt visit(AstCallStmt n) {

            string label;

            // An Important Detail: The TypeVisitor program has its own copy of
            // currClass and currMethod. These two variables need to be set to the
            // current environment before typechecking can work. Do the following:

            tv.setClass(currClass);
            tv.setMethod(currMethod);

            // 1. Perform typechecking on the obj component of the Call/CallStmt
            // node, which should return an ObjType representing the obj's class.

            AstObjType t = (AstObjType)n.obj.accept(tv);

            // 2. Look up the MethodRec with the class info.
            ClassRec classRec = symTable.GetClass(t.cid);
            MethodRec methodRec = symTable.GetMethod(classRec, n.mid);

            IrExp accessLink = n.obj.accept(this);
            IrExpList el = new IrExpList(accessLink);
            el.addAll(n.args.accept(this));

            if (maxArgCnt <= n.args.Count())
                maxArgCnt = n.args.Count() + 1;

            if (methodRec.Id().s.CompareTo("main") == 0)
                label = "main";
            else
                label = symTable.UniqueMethodName(classRec, methodRec.Id());

            return new IrCallst(new IrName(label), el);
            //return new CALLST(new NAME(n.mid.s), el);
        }

        // If ---
        // Exp e;
        // Stmt s1, s2;
        public IrStmt visit(AstIf n) {
            IrName alt = new IrName();
            IrStmtList sl = new IrStmtList();
            IrExp exp = n.e.accept(this);
            sl.add(new IrCJump(IrCJump.OP.EQ, exp, cZero, alt));
            sl.add(n.s1.accept(this));
            if (n.s2 == null)
            {
                sl.add(new IrLabel(alt));
            }
            else
            {
                IrName done = new IrName();
                sl.add(new IrJump(done));
                sl.add(new IrLabel(alt));
                sl.add(n.s2.accept(this));
                sl.add(new IrLabel(done));
            }
            return sl;
        }

        // While ---
        // Exp e;
        // Stmt s;
        public IrStmt visit(AstWhile n) {
            IrName test = new IrName();
            IrName done = new IrName();
            IrStmtList sl = new IrStmtList();
            sl.add(new IrLabel(test));
            IrExp exp = n.e.accept(this);
            sl.add(new IrCJump(IrCJump.OP.EQ, exp, cZero, done));
            sl.add(n.s.accept(this));
            sl.add(new IrJump(test));
            sl.add(new IrLabel(done));
            return sl;
        }

        // Print ---
        // Exp e;
        public IrStmt visit(AstPrint n) {

            IrName func = new IrName("print");

            IrExpList args = new IrExpList();

            if (n.e != null)
                args.add(n.e.accept(this));

            return new IrCallst(func, args);
        }

        // Return ---  
        // Exp e;
        public IrStmt visit(AstReturn n) {
            if (n.e == null)
                return new IrReturn();
            else
                return new IrReturn(n.e.accept(this));
        }

        // EXPRESSIONS

        public IrExpList visit(AstExpList n) {
            IrExpList el = new IrExpList();
            for (int i = 0; i < n.Count(); i++)
                el.add(n[i].accept(this));
            return el;
        }

        // Binop ---
        // int op;
        // Exp e1, e2;
        public IrExp visit(AstBinop n) {
            IrExp l = n.e1.accept(this);
            IrExp r = n.e2.accept(this);
            return new IrBinop(n.op, l, r);
        }

        // Relop ---
        // int op;
        // Exp e1, e2;
        public IrExp visit(AstRelop n) {
            IrExp l = n.e1.accept(this);
            IrExp r = n.e2.accept(this);
            IrStmtList sl = new IrStmtList();
            IrTemp tmp = new IrTemp();
            IrName done = new IrName();
            sl.add(new IrMove(tmp, cOne));
            sl.add(new IrCJump(n.op, l, r, done));
            sl.add(new IrMove(tmp, cZero));
            sl.add(new IrLabel(done));
            return new IrEseq(sl, tmp);
        }

        // Unop ---
        // Exp e;
        public IrExp visit(AstUnop n) {
            IrExp e = n.e.accept(this);
            if (n.op == AstUnop.OP.NEG)
                return new IrBinop(IrBinop.OP.SUB, cZero, e);
            else // n.op == Unop.NOT
                return new IrBinop(IrBinop.OP.SUB, cOne, e);
        }

        // NewArray ---
        // int size;
        public IrExp visit(AstNewArray n) {
            IrExp size = new IrConst(n.size);
            IrStmtList sl = new IrStmtList();
            IrName top = new IrName();
            IrTemp array = new IrTemp();
            IrTemp cnt = new IrTemp();
            IrExp bound = new IrBinop(IrBinop.OP.MUL, new IrConst(n.size + 1), cWordSize);
            sl.add(new IrMove(array, new IrCall(new IrName("malloc"), new IrExpList(bound))));
            sl.add(new IrMove(new IrMem(array), size));
            sl.add(new IrMove(cnt, new IrBinop(IrBinop.OP.ADD, array,
                           new IrBinop(IrBinop.OP.MUL, size, cWordSize))));
            sl.add(new IrLabel(top));
            sl.add(new IrMove(new IrMem(cnt), cZero));
            sl.add(new IrMove(cnt, new IrBinop(IrBinop.OP.SUB, cnt, cWordSize)));
            sl.add(new IrCJump(IrCJump.OP.GT, cnt, array, top));
            return new IrEseq(sl, array);
        }

        // ArrayElm ---
        // Exp array, idx;
        // BasicType et;
        public IrExp visit(AstArrayElm n) {
            IrExp array = n.array.accept(this);
            IrExp idx = n.idx.accept(this);
            return new IrMem(new IrBinop(IrBinop.OP.ADD, array, new IrBinop(IrBinop.OP.MUL,
                     new IrBinop(IrBinop.OP.ADD, idx, cOne), cWordSize)));
        }

        // ArrayLen ---
        // Exp array;
        public IrExp visit(AstArrayLen n) {
            return new IrMem(n.array.accept(this));
        }

        // *** partial implementation, will be fixed in proj2 **** 
        // NewObj ---
        // Id cid;
        public IrExp visit(AstNewObj n) {

            // A NewObj node should be translated into a CALL to "malloc" with a single argument representing the size
            // of the object, followed by a sequence of statements for initializing the objectâ€™s class variables.
            // A special note: the initialization expressions saved in the the symbol table entry for this purpose are
            // AST expressions, and need to be translated into IR expressions. Furthermore, this translation should be
            // conducted in the proper scope environment â€” the IrgenVisitor variable currClass needs to point to the
            // objectâ€™s class.

            IrStmtList stmts = new IrStmtList();

            // Calculate the size of the object
            ClassRec classRec = symTable.GetClass(n.cid);              // <get the object's ClassRec from symbol table>;
            int numVars = GetNumObjVars(classRec);

            // Construct malloc call node
            IrExp size = new IrBinop(IrBinop.OP.MUL, new IrConst(numVars > 0 ? numVars : 1), cWordSize);
            IrCall call = new IrCall(new IrName("malloc"), new IrExpList(size));

            IrTemp tmp = new IrTemp();                // This TEMP will contain the location of the object variables
            stmts.add(new IrMove(tmp, call));       // Move the results of the malloc call into the temp

            GetInitStmts(classRec, stmts, tmp);   // Recursively get object's variable initialization statements

            return new IrEseq(stmts, tmp);          // Construct resulting statement
        }

        private int GetNumObjVars(ClassRec c)
        {

            int numVars = 0;

            while (null != c)
            {
                numVars += c.VarCnt();
                c = c.Parent();
            }
            return numVars;
        }

        private int GetInitStmts(ClassRec c, IrStmtList stmts, IrTemp tmp)  {

            ClassRec saveCurrClass;
            VarRec v;
            int varCnt = 0;

            // Get init STMTs from inherited class
            if (null != c.Parent())
            {
                varCnt = GetInitStmts(c.Parent(), stmts, tmp);
            }

            // Add init STMTs from this class
            for (int i = 0; i < c.VarCnt(); i++)
            {

                v = c.GetClassVarAt(i);

                if (null == v.Init())
                {
                    stmts.add(ObjVarInitStmt(tmp, varCnt + i, v.Idx - 1, cZero));
                }
                else
                {
                    saveCurrClass = currClass;
                    currClass = c;
                    IrExp e = v.Init().accept(this);
                    currClass = saveCurrClass;

                    stmts.add(ObjVarInitStmt(tmp, varCnt + i, v.Idx - 1, e));
                }
            }
            return varCnt + c.VarCnt();
        }


        private IrStmt ObjVarInitStmt(IrExp varLoc, int varOffset, int varIndex, IrExp e)
        {
            if (0 == (varOffset))
                return (new IrMove(new IrMem(varLoc), e));
            else
                return (new IrMove(new IrMem(new IrBinop(IrBinop.OP.ADD, varLoc, (new IrBinop(IrBinop.OP.MUL, new IrConst(varIndex), cWordSize)))), e));
        }



        // *** partial implementation, will be fixed in proj2 **** 
        // Field ---
        // Exp obj; 
        // Id var;
        public IrExp visit(AstField n) {

            //return new NAME(n.var.s);
            tv.setClass(currClass);
            tv.setMethod(currMethod);

            AstObjType t = (AstObjType)n.obj.accept(tv);


            ClassRec classRec = symTable.GetClass(t.cid);

            VarRec v = symTable.GetVar(classRec, n.var);

            IrExp field = n.obj.accept(this);

            return new IrField(field, v.Idx - 1);

        }

        // *** partial implementation, will be fixed in proj2 **** 
        // Call ---
        // Exp obj; 
        // Id cid, mid;
        // ExpList args;
        public IrExp visit(AstCall n) {

            string label;

            // An Important Detail: The TypeVisitor program has its own copy of
            // currClass and currMethod. These two variables need to be set to the
            // current environment before typechecking can work. Do the following:

            tv.setClass(currClass);
            tv.setMethod(currMethod);

            // 1. Perform typechecking on the obj component of the Call/CallStmt
            // node, which should return an ObjType representing the obj's class.

            AstObjType t = (AstObjType)n.obj.accept(tv);

            // 2. Look up the MethodRec with the class info.
            ClassRec classRec = symTable.GetClass(t.cid);
            MethodRec methodRec = symTable.GetMethod(classRec, n.mid);

            IrExp accessLink = n.obj.accept(this);
            IrExpList el = new IrExpList(accessLink);
            el.addAll(n.args.accept(this));

            if (maxArgCnt <= n.args.Count())
                maxArgCnt = n.args.Count() + 1;

            if (methodRec.Id().s.CompareTo("main") == 0)
                label = "main";
            else
                label = symTable.UniqueMethodName(classRec, methodRec.Id());

            return new IrCall(new IrName(label), el);
        }

        // *** partial implementation, will be fixed in proj2 **** 
        // Id ---
        // string s;
        public IrExp visit(AstId n) {

            VarRec v;

            if (currMethod != null)
            {

                if ((v = currMethod.GetLocal(n)) != null)
                    return new IrVar(v.Idx);

                if ((v = currMethod.GetParam(n)) != null)
                    return new IrParam(v.Idx);
            }

            v = symTable.GetVar(currClass, n);

            return new IrField(new IrParam(0), v.Idx - 1);
        }

        // *** partial implementation, will be fixed in proj2 **** 
        // This ---
        public IrExp visit(AstThis n)
        {
            return new IrParam(0);
        }

        // IntVal ---
        // int i;
        public IrExp visit(AstIntVal n)
        {
            return new IrConst(n.i);
        }

        // BoolVal ---
        // bool b;
        public IrExp visit(AstBoolVal n)
        {
            return new IrConst(n.b ? 1 : 0);
        }

        // StrVal ---
        // string s;
        public IrExp visit(AstStrVal n)
        {
            return new IrString(n.s);
        }
    }
}

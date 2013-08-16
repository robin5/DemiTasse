// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: Canon.cs
// *
// * Description: 
// *
// * Author: Jingke Li (Portland State University)
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

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.irgen
{
    class Canon : IIrVI
    {
        private IrStmt getStmt(IrExp e)
        {
            if (null != (e as IrEseq))
                return ((IrEseq) e).stmt;
            return null;
        }

        private IrExp getExp(IrExp e)
        {
            if (null != (e as IrEseq))
                return ((IrEseq) e).exp;
            return e;
        }

        private IrStmt mergeStmts(IrStmt s1, IrStmt s2)
        {
            if (s1 == null) return s2;
            if (s2 == null) return s1;
            IrStmtList sl = new IrStmtList();
            sl.add(s1);
            sl.add(s2);
            return sl;
        }

        public IrProg visit(IrProg t)
        {
            return new IrProg(t.funcs.accept(this));
        }

        public IrFuncList visit(IrFuncList t)
        {
            IrFuncList funcs = new IrFuncList();
            for (int i = 0; i < t.Count(); i++)
            {
                IrFunc s = ((IrFunc)t[i]).accept(this);
                funcs.Add(s);
            }
            return funcs;
        }

        public IrFunc visit(IrFunc t)
        {
            IrTemp.Reset(t.tmpCnt);
            IrStmtList sl = (IrStmtList)t.stmts.accept(this);
            int newTmpCnt = t.tmpCnt + IrTemp.Count;
            return new IrFunc(t.label, t.varCnt, newTmpCnt, t.argCnt, sl);
        }

        public IrStmt visit(IrStmtList t)
        {
            IrStmtList stmts = new IrStmtList();
            for (int i = 0; i < t.size(); i++)
            {
                IrStmt s = ((IrStmt)t.elementAt(i)).accept(this);
                stmts.add(s);
            }
            return stmts;
        }

        public IrStmt visit(IrMove t)
        {
            IrExp dst = t.dst.accept(this);
            IrExp src = t.src.accept(this);
            IrStmt s = mergeStmts(getStmt(dst), getStmt(src));
            if (s != null)
                return mergeStmts(s, new IrMove(getExp(dst), getExp(src)));
            return t;
        }

        public IrStmt visit(IrJump t) { return t; }

        public IrStmt visit(IrCJump t)
        {
            IrExp left = t.left.accept(this);
            IrExp right = t.right.accept(this);
            IrStmt s = mergeStmts(getStmt(left), getStmt(right));
            if (s != null)
                return mergeStmts(s, new IrCJump(t.op, getExp(left),
                               getExp(right), t.target));
            return t;
        }

        public IrStmt visit(IrLabel t) { return t; }

        public IrStmt visit(IrCallst t)
        {
            IrExp args = t.args.accept(this);
            IrStmt s = getStmt(args);
            if (s != null)
                return mergeStmts(s, new IrCallst(t.func, (IrExpList)getExp(args)));
            return t;
        }

        public IrStmt visit(IrReturn t)
        {
            IrExp exp = t.exp.accept(this);
            IrStmt s = getStmt(exp);
            if (s != null)
                return mergeStmts(s, new IrReturn(getExp(exp)));
            return t;
        }

        public IrExp visit(IrExpList t)
        {
            IrStmt s = null;
            IrExpList args = new IrExpList();
            for (int i = 0; i < t.size(); i++)
            {
                IrExp e = ((IrExp)t.elementAt(i)).accept(this);
                IrStmt s1 = getStmt(e);
                if (s1 != null) s = mergeStmts(s, s1);
                args.add(getExp(e));
            }
            if (s != null)
                return new IrEseq(s, args);
            return t;
        }

        public IrExp visit(IrEseq t)
        {
            IrStmt stmt = t.stmt.accept(this);
            IrExp exp = t.exp.accept(this);
            IrStmt s = mergeStmts(stmt, getStmt(exp));
            if (s != null)
                return new IrEseq(s, getExp(exp));
            return t;
        }

        public IrExp visit(IrMem t)
        {
            IrExp exp = t.exp.accept(this);
            IrStmt s = getStmt(exp);
            if (s != null)
                return new IrEseq(s, new IrMem(getExp(exp)));
            return t;
        }

        public IrExp visit(IrCall t)
        {
            IrExp args = t.args.accept(this);
            IrStmt s = getStmt(args);
            if (t.func.id != "malloc")
            {
                IrTemp tmp = new IrTemp();
                IrStmt s1 = new IrMove(tmp, new IrCall(t.func, (IrExpList)getExp(args)));
                return new IrEseq(mergeStmts(s, s1), tmp);
            }
            else if (s != null)
            {
                return new IrEseq(s, new IrCall(t.func, (IrExpList)getExp(args)));
            }
            else
            {
                return t;
            }
        }

        public IrExp visit(IrBinop t)
        {
            IrExp left = t.left.accept(this);
            IrExp right = t.right.accept(this);
            IrStmt s = mergeStmts(getStmt(left), getStmt(right));
            if (s != null)
                return new IrEseq(s, new IrBinop(t.op, getExp(left), getExp(right)));
            return t;
        }

        public IrExp visit(IrField t)
        {
            IrExp obj = t.obj.accept(this);
            IrStmt s = getStmt(obj);
            if (s != null)
                return new IrEseq(s, new IrField(getExp(obj), t.idx));
            return t;
        }

        public IrExp visit(IrName t) { return t; }
        public IrExp visit(IrTemp t) { return t; }
        public IrExp visit(IrParam t) { return t; }
        public IrExp visit(IrVar t) { return t; }
        public IrExp visit(IrConst t) { return t; }
        public IrExp visit(IrString t) { return t; }
    }
}

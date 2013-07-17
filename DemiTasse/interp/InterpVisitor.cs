// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: InterpVisitor.cs
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
using System.Diagnostics;
using DemiTasse.ir;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.interp
{
    public class InterpVisitor : IIntVI
    {
        private static readonly int maxTemps = 512;
        private static readonly int maxStack = 2048;
        private static readonly int maxHeap = 4096;
        //private static readonly int wordSize = 1;
        private static readonly int STATUS_DEFAULT = -1;
        private static readonly int STATUS_RETURN = -2;

        private int[] temps = new int[maxTemps];
        private int[] stack = new int[maxStack];
        private int[] heap = new int[maxHeap];
        private int sp = maxStack - 2;  // stack starts from high index; reserve 1 slot
        private int fp = maxStack - 2;  // for main classâ€™ (PARAM 0)
        private int hp = maxHeap - 1;   // heap also starts from high index
        private int retVal = 0;         // special storage for return value
        private FUNClist funcs = null;  // keeping a copy of input programâ€™s funcs
        private STMTlist stmts = null;  // keeping a copy of current statement list
        private Stack<STMTlist> stmtLists = new Stack<STMTlist>();

        public delegate void SystemOutEventHandler(object sender, InterpOutEventArgs e);

        public event SystemOutEventHandler OnSystemOutHandler;

        public void AddOnSystemOut(SystemOutEventHandler handler)
        {
            OnSystemOutHandler += handler;
        }

        public void RemoveOnSystemOut(SystemOutEventHandler handler)
        {
            OnSystemOutHandler -= handler;
        }

        protected void OnSystemOut(InterpOutEventArgs e)
        {
            if (OnSystemOutHandler != null)
                OnSystemOutHandler(this, e);
        }

        public void visit(PROG p)
        {
            funcs = p.funcs;
            FUNC mf = findFunc("main");
            if (null == mf)
                throw new Exception("main function not found");

            //sp = sp - mf.varCnt - mf.argCnt - 1;
            mf.accept(this);
            //sp = sp + mf.varCnt + mf.argCnt + 1;
        }

        private FUNC findFunc(String name)
        {
            FUNC func;

            for (int i = 0; i < funcs.Count(); ++i) {
                func = funcs[i];
                if (0 == func.label.CompareTo(name)) {
                    return func;
                }
            }

            return null;
        }

        public void visit(FUNC f)
        {
            stmtLists.Push(stmts);
            stmts = f.stmts;
            sp = sp - f.varCnt - f.argCnt - 1;
            f.stmts.accept(this);
            stmts = stmtLists.Pop();
            sp = sp + f.varCnt + f.argCnt + 1;
        }

        public void visit(FUNClist t)
        {
        }

        // Statements
        public int visit(STMTlist sl)
        {
            int ret = STATUS_DEFAULT;
            int i = 0;
            while (i < sl.size())
            {
                int next = ((STMT) sl.elementAt(i)).accept(this);
                if (next == STATUS_RETURN)
                {
                    ret = STATUS_RETURN;
                    break;
                }
                i = (next >= 0) ? next : i+1;
            }
            return ret;
        }

        public int visit(MOVE s)
        {

            int rVal = s.src.accept(this);
            int lVal;

            if (s.dst is TEMP) {

                lVal = getLValue((TEMP) s.dst);
                temps[lVal] = rVal;

            } else if (s.dst is MEM) {

                lVal = GetMemAddress((MEM) s.dst);
                heap[lVal] = rVal;

            } else if (s.dst is FIELD) {

                lVal = GetFieldAddress((FIELD) s.dst);
                heap[lVal] = rVal;

            } else if (s.dst is PARAM) {

                lVal = GetParamAddress((PARAM) s.dst);
                stack[lVal] = rVal;
            } else if (s.dst is VAR) {

                lVal = GetVarAddress((VAR) s.dst);
                stack[lVal] = rVal;
            }
            return STATUS_DEFAULT;
        }

    private int getLValue(TEMP t) {
        int addr = t.num;
        return addr;
    }

    private int GetMemAddress(MEM m) {
        int addr = m.exp.accept(this);
        return addr;
    }

    private int GetFieldAddress(FIELD f) {
        int addr = f.obj.accept(this);
        addr += f.idx;
        return addr;
    }

    private int GetParamAddress(PARAM p) {
        int addr = fp + p.idx;
        return addr;
    }

    private int GetVarAddress(VAR v) {
        int addr = fp - v.idx;
        return addr;
    }

    public int visit(JUMP s) {
        return findStmtIdx(s.target);
    }

    public int visit(CJUMP s) {

        bool cond;
        int left = s.left.accept(this);
        int right = s.right.accept(this);

        switch (s.op) {
            case CJUMP.OP.EQ: cond = (left == right); break;
            case CJUMP.OP.NE: cond = (left != right); break;
            case CJUMP.OP.LT: cond = (left < right); break;
            case CJUMP.OP.LE: cond = (left <= right); break;
            case CJUMP.OP.GT: cond = (left > right); break;
            case CJUMP.OP.GE: cond = (left >= right); break;
            default: throw new Exception("Encountered invalid CJUMP op value");
        }
        if (cond)
            return findStmtIdx(s.target);
        else
            return STATUS_DEFAULT;
    }

    private int findStmtIdx(NAME target)  {

        STMT stmt;

        if (target.id.CompareTo("L5") == 0) {

        }

        for(int i = 0; i < stmts.size(); ++i) {
            stmt = stmts.elementAt(i);
            if (stmt is LABEL) {
                LABEL l = (LABEL) stmt;
                if (l.lab.CompareTo(target.id) == 0)
                {
                    return i;
                }
            }
        }

        throw new Exception("Label does not exist: " + target.id);
    }

    public int visit(LABEL t) {
        return STATUS_DEFAULT;
    }

    public int visit(CALLST s) {

        if (s.func.id.Equals("print")) {

            // Print the argument
            if (0 == s.args.size()) 
            {
                OnSystemOut(new InterpOutEventArgs());
            }
            else 
            {
                EXP arg0 = s.args.elementAt(0);

                if (arg0 is STRING)
                {
                    OnSystemOut(new InterpOutEventArgs(((STRING)arg0).s));
                }
                else
                {
                    int val = arg0.accept(this);
                    OnSystemOut(new InterpOutEventArgs(val.ToString()));
                }
            }

        } else {

            //â€“ find the FUNC node corresponding to the routine from the programâ€™s funcs list, and switch there
            FUNC func = findFunc(s.func.id);

            //push parameters onto the stack (at stack[sp+1], stack[sp+2], etc.);

            for (int i = 0; i < s.args.size(); ++i) {
                stack[sp + i + 1] = s.args.elementAt(i).accept(this);
            }

            // adjust the frame pointer (save the current fp in stack[sp], and set sp to be the new fp);
            stack[sp] = fp;

            fp = sp;

            //to execute (by invoking accept() on the FUNC node);
            func.accept(this);

            // After control returns, restore the saved frame pointer.
            sp = fp;
            fp = stack[sp];
        }

        return STATUS_DEFAULT; // index of first statement to execute?
    }

    public int visit(RETURN r) {

        retVal = r.exp.accept(this);

		return STATUS_RETURN;
	}

    // Expressions
    public int visit(EXPlist t) {
		return STATUS_DEFAULT;
	}

    public int visit(ESEQ t) {
        throw new Exception("AN ESEQ node was encountered in the IR code.");
    }

    public int visit(MEM t) {

        int addr = t.exp.accept(this);

        return heap[addr];
    }

    public int visit(CALL s) {

        if (s.func.id.Equals("malloc")) {

            EXP arg0 = s.args.elementAt(0);
            int val = arg0.accept(this);

            hp -= val;
            retVal=hp;

        } else {

            //â€“ find the FUNC node corresponding to the routine from the programâ€™s funcs list, and switch there
            FUNC func = findFunc(s.func.id);

            //push parameters onto the stack (at stack[sp+1], stack[sp+2], etc.);

            for (int i = 0; i < s.args.size(); ++i) {
                stack[sp + i + 1] = s.args.elementAt(i).accept(this);
            }

            // adjust the frame pointer (save the current fp in stack[sp], and set sp to be the new fp);
            stack[sp] = fp;

            fp = sp;

            //to execute (by invoking accept() on the FUNC node);
            func.accept(this);

            // After control returns, restore the saved frame pointer.
            sp = fp;
            fp = stack[sp];
        }

        return retVal;
    }

    public int visit(BINOP e) {

        // evaluate both operands to lval and rval
        int lval = e.left.accept(this);
        int rval = e.right.accept(this);

        // evaluate expression value
        switch (e.op) {
            case BINOP.OP.ADD: return lval + rval;
            case BINOP.OP.SUB: return lval - rval;
            case BINOP.OP.MUL: return lval * rval;
            case BINOP.OP.DIV: return lval / rval;
            case BINOP.OP.AND: return (2 == (lval + rval)) ? 1 : 0;
            case BINOP.OP.OR: return (0 < (lval + rval)) ? 1 : 0;
            default: throw new Exception("Encountered undefined BINOP value: " + e.op);
        }
    }

    public int visit(NAME t) {

        if (t.id.Equals("wSZ"))
            return 1;
        return STATUS_DEFAULT;
    }

    public int visit(TEMP t) {
		return temps[t.num];
	}

    public int visit(FIELD f) {
		return heap[GetFieldAddress(f)];
	}

    public int visit(PARAM p) {
		return stack[GetParamAddress(p) + 1];
	}

    public int visit(VAR v) {
		return stack[GetVarAddress(v)];
	}

    public int visit(CONST t) {
		return t.val;
	}

    public int visit(STRING t) {
		return STATUS_DEFAULT;
	}
    }
}

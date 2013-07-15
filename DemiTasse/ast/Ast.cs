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

using System;
using System.Diagnostics;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ast
{
    public abstract class Ast
    {
        public static void DUMP(string s) { Debug.Write(s); }
        public static void DUMP(int i) { Debug.Write(i); }

        public static void DUMP(string name, AstList l)
        {
            Debug.Write("(" + name + " ");
            if (l != null) l.dump();
            Debug.Write(") ");
        }

        public static void DUMP(DemiTasse.ast.Type t)
        {
            if (t == null) Debug.Write("(NullType) ");
            else t.dump();
        }

        public static void DUMP(Stmt s)
        {
            if (s == null) Debug.Write("(NullStmt) ");
            else s.dump();
        }

        public static void DUMP(Exp e)
        {
            if (e == null) Debug.Write("(NullExp) ");
            else e.dump();
        }

        public abstract void dump();
        public abstract void accept(VoidVI v);
    }
}
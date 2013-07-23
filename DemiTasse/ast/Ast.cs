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
using System.Text;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ast
{
    public abstract class Ast
    {
        private static StringBuilder text = new StringBuilder();

        public static void DUMP(string s) 
        { 
            text.Append(s); 
        }
        
        public static void DUMP(int i) 
        { 
            text.Append(i.ToString()); 
        }

        public static void DUMP(string name, AstList l)
        {
            text.Append("(" + name + " ");
            if (l != null)
                l.dump();
            text.Append(") ");
        }

        public static void DUMP(DemiTasse.ast.Type t)
        {
            if (t == null) 
                text.Append("(NullType) ");
            else 
                t.dump();
        }

        public static void DUMP(Stmt s)
        {
            if (s == null) 
                text.Append("(NullStmt) ");
            else 
                s.dump();
        }

        public static void DUMP(Exp e)
        {
            if (e == null) 
                text.Append("(NullExp) ");
            else 
                e.dump();
        }

        public static string getAst()
        {
            return text.ToString();
        }

        public static void Clear()
        {
            text.Length = 0;
        }

        public abstract void dump();
        
        public abstract void accept(VoidVI v);
    }
}
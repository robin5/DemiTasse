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
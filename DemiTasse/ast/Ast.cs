// **********************************************************************************
// * Copyright (c) 2013 Jingke Li, Robin Murray
// **********************************************************************************
// *
// * File: Ast.cs
// *
// * Author:  Jingke Li (Portland State University)
// *
// * C# Translation:  Robin Murray
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
        private static StringBuilder _astData = new StringBuilder();

        public static void Append(string s) 
        { 
            _astData.Append(s);
        }
        
        public static void Append(int i) 
        { 
            _astData.Append(i.ToString()); 
        }

        public static void Append(string name, AstList l)
        {
            _astData.Append("(" + name + " ");
            if (l != null)
                l.GenerateAstData();
            _astData.Append(") ");
        }

        public static void Append(AstType t)
        {
            if (t == null) 
                _astData.Append("(NullType) ");
            else 
                t.GenerateAstData();
        }

        public static void Append(AstStmt s)
        {
            if (s == null) 
                _astData.Append("(NullStmt) ");
            else 
                s.GenerateAstData();
        }

        public static void Append(AstExp e)
        {
            if (e == null) 
                _astData.Append("(NullExp) ");
            else 
                e.GenerateAstData();
        }

        public static string AstData
        {
            get { return _astData.ToString(); }
        }

        public static void ResetAstData()
        {
            _astData.Length = 0;
        }

        public abstract void GenerateAstData();
        
        public abstract void accept(VoidVI v);
    }
}
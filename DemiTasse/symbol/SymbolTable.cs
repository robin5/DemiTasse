// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: Table.cs
// *
// * Description:  Top-level symbol table
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
using System.Diagnostics;

using DemiTasse.ast;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.symbol
{
    public class SymbolTable
    {
        private Dictionary<string, ClassRec> _classRecs;

        public SymbolTable()
        { 
            _classRecs = new Dictionary<string, ClassRec>(); 
        }

        public void AddClass(AstId cid)
        {
            if (_classRecs.ContainsKey(cid.s))
            {
                throw new SymbolException("Class " + cid.s + " already defined");
            }
            else
            {
                _classRecs.Add(cid.s, new ClassRec(cid));
            }
        }

        public ClassRec GetClass(AstId cid)
        {
            if (!_classRecs.ContainsKey(cid.s))
                throw new SymbolException("Class " + cid.s + " not defined");

            return _classRecs[cid.s];
        }

        public MethodRec GetMethod(ClassRec c, AstId mid)
        {
            MethodRec m;
            while (c != null)
            {
                if ((m = c.GetMethod(mid)) != null) 
                    return m;
                c = c.Parent();
            }
            throw new SymbolException("Method " + mid.s + " not defined");
        }

        public string UniqueMethodName(ClassRec c, AstId mid)
        {
            MethodRec m;
            while (c != null)
            {
                if ((m = c.GetMethod(mid)) != null) 
                    return c.Id().s + "_" + mid.s;
                c = c.Parent();
            }
            throw new SymbolException("Method " + mid.s + " not defined");
        }

        public VarRec GetVar(ClassRec c, MethodRec m, AstId vid)
        {
            VarRec v;
            if (m != null)
            {
                if ((v = m.GetLocal(vid)) != null) 
                    return v;
                if ((v = m.GetParam(vid)) != null) 
                    return v;
            }
            return GetVar(c, vid);
        }

        public VarRec GetVar(ClassRec c, AstId vid)
        {
            VarRec v;
            while (c != null)
            {
                if ((v = c.GetClassVar(vid)) != null) 
                    return v;
                c = c.Parent();
            }
            throw new SymbolException("Var " + vid.s + " not defined");
        }

        public void Show() 
        {
            Debug.WriteLine("Symbol Table:");
            if (_classRecs != null)
            {
                foreach (KeyValuePair<string, ClassRec> kvp in _classRecs)
                {
                    kvp.Value.Show();
                }
                Debug.WriteLine("");
            }
        }
    }
}
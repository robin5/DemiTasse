// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: ClassRec.cs
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
    public class Table
    {
        private Dictionary<string, ClassRec> classes;	// a table of ClassRecs

        public Table()
        { 
            classes = new Dictionary<string, ClassRec>(); 
        }

        public void addClass(Id cid) /* throws SymbolException */
        {
            if (classes.ContainsKey(cid.s))
            {
                throw new SymbolException("Class " + cid.s + " already defined");
            }
            else
            {
                classes.Add(cid.s, new ClassRec(cid));
            }
        }

        public ClassRec getClass(Id cid) /* throws SymbolException */
        {
            if (!classes.ContainsKey(cid.s))
                throw new SymbolException("Class " + cid.s + " not defined");

            return classes[cid.s];
        }

        public MethodRec getMethod(ClassRec c, Id mid) /* throws SymbolException */
        {
            MethodRec m;
            while (c != null)
            {
                if ((m = c.getMethod(mid)) != null) return m;
                c = c.parent();
            }
            throw new SymbolException("Method " + mid.s + " not defined");
        }

        // for future use (irgen)
        public string uniqueMethodName(ClassRec c, Id mid) /* throws SymbolException */
        {
            MethodRec m;
            while (c != null)
            {
                if ((m = c.getMethod(mid)) != null) return c.id().s + "_" + mid.s;
                c = c.parent();
            }
            throw new SymbolException("Method " + mid.s + " not defined");
        }

        public VarRec getVar(ClassRec c, MethodRec m, Id vid) /* throws SymbolException */
        {
            VarRec v;
            if (m != null)
            {
                if ((v = m.getLocal(vid)) != null) return v;
                if ((v = m.getParam(vid)) != null) return v;
            }
            return getVar(c, vid);
        }

        public VarRec getVar(ClassRec c, Id vid) /* throws SymbolException */
        {
            VarRec v;
            while (c != null)
            {
                if ((v = c.getClassVar(vid)) != null) return v;
                c = c.parent();
            }
            throw new SymbolException("Var " + vid.s + " not defined");
        }

        public void show() 
        {
            Debug.WriteLine("Symbol Table:");
            if (classes != null)
            {
                foreach (KeyValuePair<string, ClassRec> kvp in classes)
                {
                    kvp.Value.show();
                }
                Debug.WriteLine("");
            }
        }
    }
}
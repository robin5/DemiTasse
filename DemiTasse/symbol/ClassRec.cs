// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: ClassRec.cs
// *
// * Description:  Symbol table module for MINI v1.7.
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
using System.Text;

using DemiTasse.ast;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.symbol
{
    public class ClassRec
    {
        private Id _id;
        private ClassRec _parent;
        private List<VarRec> class_vars;
        private Dictionary<String, MethodRec> methods;
    
        public ClassRec(Id cid)
        {
            _id = cid;
            _parent = null;
            class_vars = new List<VarRec>();
            methods = new Dictionary<String, MethodRec>();
        }

        public Id id() { return _id; }
        
        public ClassRec parent()
        {
            return _parent; 
        }
  
        public int varCnt()
        {
            return class_vars.Count; 
        }

        public VarRec getClassVar(Id vid)
        {
            for (int i=0; i<class_vars.Count; i++)
            {
                VarRec v = class_vars[i];
                if (v.id().s.Equals(vid.s)) return v;
            }
            return null;
        }
    
        public VarRec getClassVarAt(int i)
        {
            if (i<class_vars.Count) 
                return class_vars[i];
            return null;
        }

        public void addClassVar(Id vid, DemiTasse.ast.Type type, Exp e) /* throws SymbolException */
        {
            if(getClassVar(vid) != null) 
                throw new SymbolException("ClassVar " + vid.s + " already defined");

            class_vars.Add(new VarRec(vid, type, VarRec.CLASS, class_vars.Count+1, e));
        }
  
        private int ancestorVarCnt()
        {
            if (_parent != null) 
                return _parent.ancestorVarCnt() + _parent.varCnt();
            return 0;
        }      

        public void linkParent(ClassRec p)
        {
            _parent = p;
            int start_idx = ancestorVarCnt() + 1;
            for (int i = 0; i < class_vars.Count; i++) 
                class_vars[i].setIdx(start_idx + i);
        }

        public MethodRec getMethod(Id mid)
        {
            return methods[mid.s]; // may return null
        }

        public void addMethod(Id mid, DemiTasse.ast.Type rtype) /* throws SymbolException */
        {
            MethodRec m = new MethodRec(mid, rtype);
            methods.Add(mid.s, m);

            if (m != null)
                throw new SymbolException("Method " + mid.s + " already defined");
        }
  
        public void show()
        {
            Debug.WriteLine("Class " + _id.s + " (pid=" + (_parent==null ? "null" : _parent.id().s) + "):");
            for (int i = 0; i < class_vars.Count; i++)
            {
                Debug.Write("  [cl var] ");
                class_vars[i].show();
            }
            foreach (MethodRec rec in methods.Values)
            {
                rec.show();
            }
        }
    }
}


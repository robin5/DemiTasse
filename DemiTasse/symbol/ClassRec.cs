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
        private AstId _id;
        private ClassRec _parent;
        private List<VarRec> _vars;
        private Dictionary<String, MethodRec> _methods;
    
        public ClassRec(AstId cid)
        {
            _id = cid;
            _parent = null;
            _vars = new List<VarRec>();
            _methods = new Dictionary<String, MethodRec>();
        }

        public AstId Id() 
        { 
            return _id; 
        }
        
        public ClassRec Parent()
        {
            return _parent; 
        }
  
        public int VarCnt()
        {
            return _vars.Count; 
        }

        public VarRec GetClassVar(AstId vid)
        {
            for (int i = 0; i < _vars.Count; i++)
            {
                VarRec v = _vars[i];
                if (v.Id().s.Equals(vid.s)) return v;
            }

            return null;
        }

        public VarRec GetClassVarAt(int i)
        {
            if (i < _vars.Count)
                return _vars[i];
            
            return null;
        }

        public void AddClassVar(AstId vid, DemiTasse.ast.AstType type, AstExp e)
        {
            if(GetClassVar(vid) != null) 
                throw new SymbolException("ClassVar " + vid.s + " already defined");

            _vars.Add(new VarRec(vid, type, VarRec.CLASS, _vars.Count+1, e));
        }
  
        private int AncestorVarCnt()
        {
            if (_parent != null) 
                return _parent.AncestorVarCnt() + _parent.VarCnt();

            return 0;
        }      

        public void LinkParent(ClassRec p)
        {
            _parent = p;
            int start_idx = AncestorVarCnt() + 1;
            for (int i = 0; i < _vars.Count; i++) 
                _vars[i].Idx = (start_idx + i);
        }

        public MethodRec GetMethod(AstId mid)
        {
            if (!_methods.ContainsKey(mid.s))
                return null;

            return _methods[mid.s]; // may return null
        }

        public void AddMethod(AstId mid, DemiTasse.ast.AstType rtype)
        {
            if (_methods.ContainsKey(mid.s))
                throw new SymbolException("Method " + mid.s + " already defined");

            MethodRec m = new MethodRec(mid, rtype);

            _methods.Add(mid.s, m);
        }
  
        public void Show()
        {
            Debug.WriteLine("Class " + _id.s + " (pid=" + (_parent==null ? "null" : _parent.Id().s) + "):");
            for (int i = 0; i < _vars.Count; i++)
            {
                Debug.Write("  [cl var] ");
                _vars[i].Show();
            }
            foreach (MethodRec rec in _methods.Values)
            {
                rec.Show();
            }
        }
    }
}


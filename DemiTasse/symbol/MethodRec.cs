// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: MethodRec.cs
// *
// * Description:  Method record for Symbol table module for MINI v1.7.
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

using System.Diagnostics;
using System.Collections.Generic;

using DemiTasse.ast;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.symbol
{
    public class MethodRec
    {
        private Id _id;
        private DemiTasse.ast.Type _rtype;
        private List<VarRec> _params;
        private List<VarRec> _locals;

        public MethodRec(Id id, DemiTasse.ast.Type type)
        {
            _id = id;
            _rtype = type;
            _params = new List<VarRec>();
            _locals = new List<VarRec>();
        }

        public Id id()   	   { return _id; }
        public DemiTasse.ast.Type rtype() { return _rtype; }
        public int paramCnt()    { return _params.Count; }
        public int localCnt()    { return _locals.Count; }

        public VarRec getParam(Id vid)
        {
            for (int i=0; i<_params.Count; i++)
            {
                VarRec v = _params[i];
                if (v.id().s.Equals(vid.s)) return v;
            }
            return null;
        }
    
        public VarRec getParamAt(int i)
        {
            if (i<_params.Count) 
                return _params[i];
            return null;
        }

        public VarRec getLocal(Id vid)
        {
            for (int i=0; i<_locals.Count; i++)
            {
                VarRec v = _locals[i];
                if (v.id().s.Equals(vid.s)) return v;
            }
            return null;
        }
    
        public VarRec getLocalAt(int i)
        {
            if (i<_locals.Count)
                return _locals[i];
            return null;
        }

        public void addParam(Id vid, DemiTasse.ast.Type type) /* throws SymbolException */
        {
            if(getParam(vid) != null) 
                throw new SymbolException("Param " + vid.s + " already defined");
            _params.Add(new VarRec(vid, type, VarRec.PARAM, _params.Count+1));
        }

        public void addLocal(Id vid, DemiTasse.ast.Type type) /* throws SymbolException */
        {
            if(getLocal(vid) != null) 
                throw new SymbolException("Var " + vid.s + " already defined");

            _locals.Add(new VarRec(vid, type, VarRec.LOCAL, 
            _locals.Count+1));
        }
    
        public void show()
        {
            string rt = (_rtype == null) ? "void" : _rtype.toString();
            Debug.WriteLine(" <method> " + _id.s + " (rtype=" + rt +
                ", paramCnt=" + paramCnt() + 
                ", localCnt=" + localCnt() + "):");
            for (int i = 0; i < _params.Count; i++)
            {
                Debug.Write("  [param] ");
                _params[i].show();
            }

            for (int i = 0; i < _locals.Count; i++)
            {
                Debug.Write("  [local] ");
                _locals[i].show();
            }
        }
    }
}
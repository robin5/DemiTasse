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
        private AstId _id;
        private AstType _rtype;
        private List<VarRec> _params;
        private List<VarRec> _locals;

        public MethodRec(AstId id, AstType type)
        {
            _id = id;
            _rtype = type;
            _params = new List<VarRec>();
            _locals = new List<VarRec>();
        }

        public AstId Id()
        {
            return _id;
        }

        public AstType RType()
        {
            return _rtype;
        }

        public int ParamCnt()
        {
            return _params.Count;
        }

        public int LocalCnt()
        {
            return _locals.Count;
        }

        public VarRec GetParam(AstId vid)
        {
            for (int i = 0; i < _params.Count; i++)
            {
                VarRec v = _params[i];
                if (v.Id().s.Equals(vid.s)) return v;
            }
            return null;
        }

        public VarRec GetParamAt(int i)
        {
            if (i < _params.Count)
                return _params[i];
            return null;
        }

        public VarRec GetLocal(AstId vid)
        {
            for (int i = 0; i < _locals.Count; i++)
            {
                VarRec v = _locals[i];
                if (v.Id().s.Equals(vid.s)) return v;
            }
            return null;
        }

        public VarRec GetLocalAt(int i)
        {
            if (i < _locals.Count)
                return _locals[i];
            return null;
        }

        public void AddParam(AstId id, DemiTasse.ast.AstType type)
        {
            if (GetParam(id) != null)
                throw new SymbolException("Param " + id.s + " already defined");

            _params.Add(new VarRec(id, type, VarRec.PARAM, _params.Count + 1));
        }

        public void AddLocal(AstId vid, DemiTasse.ast.AstType type)
        {
            if (GetLocal(vid) != null)
                throw new SymbolException("Var " + vid.s + " already defined");

            _locals.Add(new VarRec(vid, type, VarRec.LOCAL,
            _locals.Count + 1));
        }

        public void Show()
        {
            string rt = (_rtype == null) ? "void" : _rtype.toString();
            Debug.WriteLine(" <method> " + _id.s + " (rtype=" + rt +
                ", paramCnt=" + ParamCnt() +
                ", localCnt=" + LocalCnt() + "):");
            for (int i = 0; i < _params.Count; i++)
            {
                Debug.Write("  [param] ");
                _params[i].Show();
            }

            for (int i = 0; i < _locals.Count; i++)
            {
                Debug.Write("  [local] ");
                _locals[i].Show();
            }
        }
    }
}
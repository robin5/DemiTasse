// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: ClassRec.cs
// *
// * Description:  Variable and parameter record for Symbol table module for MINI v1.7.
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

using DemiTasse.ast;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.symbol
{
    public class VarRec
    {
        public static readonly int CLASS = 0;
        public static readonly int LOCAL = 1;
        public static readonly int PARAM = 2;

        private Id _id;
        private Type _type;
        private int _kind;  // one of the three categories
        private int _idx;   // position in its scope
        private Exp _init;  // initial value
  
        public VarRec(Id vid, Type vtype, int vkind, int vidx)
        {
            _id = vid;
            _type = vtype;
            _kind = vkind;
            _idx = vidx;
            _init = null;
        }

        public VarRec(Id vid, Type vtype, int vkind, int vidx, Exp e)
        {
            _id = vid;
            _type = vtype;
            _kind = vkind;
            _idx = vidx;
            _init = e;
        }

        public Id id()     { return _id; }
        public Type type() { return _type; }
        public int kind()  { return _kind; }
        public int idx()   { return _idx; }
        public Exp init()  { return _init; }

        public void setIdx(int vidx) { _idx = vidx; }

        public void show()
        {
            Debug.Write("(" + _idx + ") " + _id.s + " " + _type.toString());
            if (_init != null)
            {
                Debug.Write(" ");
                _init.dump();
            }
            Debug.WriteLine("");
        }	
    }
}
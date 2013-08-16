// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: IIrVI.cs
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

// **********************************************************************************
// * Using
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ir
{
    public interface IIrVI
    {
        IrProg visit(IrProg t);
        IrFunc visit(IrFunc t);
        IrFuncList visit(IrFuncList t);
        
        //STMT visit(STMT t);
        IrStmt visit(IrStmtList t);
        IrStmt visit(IrMove t);
        IrStmt visit(IrJump t);
        IrStmt visit(IrCJump t);
        IrStmt visit(IrLabel t);
        IrStmt visit(IrCallst t);
        IrStmt visit(IrReturn t);
        
        //EXP visit(EXP t);
        IrExp visit(IrExpList t);
        IrExp visit(IrEseq t);
        IrExp visit(IrMem t);
        IrExp visit(IrCall t);
        IrExp visit(IrBinop t);
        IrExp visit(IrName t);
        IrExp visit(IrTemp t);
        IrExp visit(IrField t);
        IrExp visit(IrParam t);
        IrExp visit(IrVar t);
        IrExp visit(IrConst t);
        IrExp visit(IrString t);
    }
}

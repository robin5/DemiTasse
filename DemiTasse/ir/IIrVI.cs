// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: IIrVI.cs
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
        PROG visit(PROG t);
        FUNC visit(FUNC t);
        FUNClist visit(FUNClist t);
        
        //STMT visit(STMT t);
        STMT visit(STMTlist t);
        STMT visit(MOVE t);
        STMT visit(JUMP t);
        STMT visit(CJUMP t);
        STMT visit(LABEL t);
        STMT visit(CALLST t);
        STMT visit(RETURN t);
        
        //EXP visit(EXP t);
        EXP visit(EXPlist t);
        EXP visit(ESEQ t);
        EXP visit(MEM t);
        EXP visit(CALL t);
        EXP visit(BINOP t);
        EXP visit(NAME t);
        EXP visit(TEMP t);
        EXP visit(FIELD t);
        EXP visit(PARAM t);
        EXP visit(VAR t);
        EXP visit(CONST t);
        EXP visit(STRING t);
    }
}

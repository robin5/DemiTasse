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
// * Granting License: TBD
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

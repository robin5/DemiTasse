using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public interface IIntVI 
    {
        // Program and functions
        void visit(PROG t);
        void visit(FUNC t);
        void visit(FUNClist t);
        // Statements
        int visit(STMTlist t);
        int visit(MOVE t);
        int visit(JUMP t);
        int visit(CJUMP t);
        int visit(LABEL t);
        int visit(CALLST t);
        int visit(RETURN t);
        // Expressions
        int visit(EXPlist t);
        int visit(ESEQ t);
        int visit(MEM t);
        int visit(CALL t);
        int visit(BINOP t);
        int visit(NAME t);
        int visit(TEMP t);
        int visit(FIELD t);
        int visit(PARAM t);
        int visit(VAR t);
        int visit(CONST t);
        int visit(STRING t);
    }
}

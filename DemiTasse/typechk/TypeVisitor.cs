// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: TypeVisitor.cs
// *
// * Description:  A type-checking visitor for MINI 1.7.
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

using DemiTasse.ast;
using DemiTasse.symbol;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.typechk
{
    public class TypeVisitor : TypeVI 
    {
        private SymbolTable symTable;
        private ClassRec currClass;
        private MethodRec currMethod;
        private bool hasReturn;

        // ************************************************
        // * define the basic type nodes only once ...
        // ************************************************

        private AstBasicType IntType = new AstBasicType(AstBasicType.Int);    
        private AstBasicType BoolType = new AstBasicType(AstBasicType.Bool);

        // ***********************************************************
        // * constructor -- a symbol table is passed in as a parameter
        // ***********************************************************

        public TypeVisitor(SymbolTable symtab)
        {
            symTable = symtab;
            currClass = null;
            currMethod = null;
            hasReturn = false;
        }

        public TypeVisitor(DemiTasse.symbol.SymbolTable symtab, ClassRec c, MethodRec m)
        {
            symTable = symtab;
            currClass = c;
            currMethod = m;
            hasReturn = false;
        }

        public void setClass(ClassRec c)
        {
            currClass = c;
        }

        public void setMethod(MethodRec m)
        {
            currMethod = m;
        }

        private bool isIntType(AstType t)
        {
            return (null != (t as AstBasicType)) && (((AstBasicType)t).typ == AstBasicType.Int);
        }

        private bool isBoolType(AstType t)
        {
            return (null != (t as AstBasicType)) && (((AstBasicType)t).typ == AstBasicType.Bool);
        }

        private bool isArithOp(AstBinop.OP op)
        {
            return (AstBinop.OP.ADD <= op) && (op <= AstBinop.OP.DIV);
        }

        private bool compatible(AstType t1, AstType t2) /* throws Exception */
        {
            if (t1 == t2) 
                return true;
            else if ((null != (t1 as AstBasicType)) && (null != (t2 as AstBasicType)))
            {
                int bt1 = ((AstBasicType)t1).typ;
                int bt2 = ((AstBasicType)t2).typ;
                if (bt1 == bt2)
                return true;
            }
            else if ((null != (t1 as AstObjType)) && (null != (t2 as AstObjType)))
            {
                AstId cid1 = ((AstObjType) t1).cid;
                AstId cid2 = ((AstObjType) t2).cid;
                if (cid1.s.Equals(cid2.s))
                {
                    return true;
                } 
                else 
                {
                    ClassRec c = symTable.GetClass(cid2);
                    while ((c = c.Parent()) != null)
                    {
                        if (cid1.s.Equals(c.Id().s))
                        return true; 
                    }
                }
            }
            else if ((null != (t1 as AstArrayType)) && (null != (t2 as AstArrayType)))
            {
                return compatible(((AstArrayType) t1).et, ((AstArrayType) t2).et);
            }
            return false;	
        }
  
        // Program ---
        // ClassDeclList cl;
        public void visit(AstProgram n) /* throws Exception */
        {
            n.cl.accept(this);
        }

        #region LISTS

        public void visit(AstList n) /* throws Exception */
        {
        }

        public void visit(AstClassDeclList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.Count(); i++)
                n[i].accept(this);
        }
  
        public void visit(AstVarDeclList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.Count(); i++)
                n[i].accept(this);
        }

        public void visit(AstMethodDeclList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.Count(); i++)
                n[i].accept(this);
        }

        public void visit(AstFormalList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.Count(); i++)
                n[i].accept(this);
        }

        public void visit(AstStmtList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.Count(); i++)
                n[i].accept(this);
        }

        public void visit(AstExpList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.Count(); i++)
                n[i].accept(this);
        }

        #endregion LISTS

        #region DECLARATIONS
  
        // VarDecl ---
        // Type t;
        // Id var;
        // Exp e;
        public void visit(AstVarDecl n) /* throws Exception */ 
        {
            if (null != (n.t as AstObjType))
                // make sure type is defined
                symTable.GetClass(((AstObjType) n.t).cid);

            if (n.e != null)
            {
                AstType t2 = n.e.accept(this);
                if (!compatible(n.t,t2))
                    throw new TypeException("Incompatible types in VarDecl: " + n.t + " <- " + t2);
            }
        }

        // Formal ---
        // Type t;
        // Id id;
        public void visit(AstFormal n) /* throws Exception */ 
        { 
            if (null != (n.t as AstObjType)) // make sure type is defined
                symTable.GetClass(((AstObjType) n.t).cid);
        }

        // ClassDecl ---
        // Id cid, pid;
        // VarDeclList vl;
        // MethodDeclList ml;
        public void visit(AstClassDecl n) /* throws Exception */ 
        {
            currClass = symTable.GetClass(n.cid);
            n.vl.accept(this);
            n.ml.accept(this);
            currClass = null;
        }

        // MethodDecl ---
        // Type t;
        // Id mid;
        // FormalList fl;
        // VarDeclList vl;
        // StmtList sl;
        public void visit(AstMethodDecl n) /* throws Exception */ 
        {
            currMethod = currClass.GetMethod(n.mid);
            hasReturn = false;
            n.fl.accept(this);
            n.vl.accept(this);
            n.sl.accept(this);

            // make sure return type is defined
            if (null != (n.t as AstObjType))
                symTable.GetClass(((AstObjType) n.t).cid);

            if ((n.t != null) && !hasReturn)
                throw new TypeException("Method " + n.mid.s + " is missing a Return statment");
            currMethod = null;
        }

        #endregion DECLARATIONS

        #region TYPES

        public AstType visit(AstBasicType n) { return n; }
        public AstType visit(AstArrayType n) { return n; }

        // ObjType ---
        // Id cid;
        public AstType visit(AstObjType n) /* throws Exception */ 
        { 
            symTable.GetClass(n.cid); // verify that the class exits
            return n; 
        }

        #endregion TYPES

        #region STATEMENTS

        // Block ---
        // StmtList sl;
        public void visit(AstBlock n) /* throws Exception */ 
        {
            n.sl.accept(this);
        }

        // Assign ---
        // Exp lhs, rhs;
        public void visit(AstAssign n) /* throws Exception */ 
        {
            AstType t1 = n.lhs.accept(this);
            AstType t2 = n.rhs.accept(this);

            if (! ((null != (n.lhs as AstId) )
                || (null != (n.lhs as AstField) )
                || (null != (n.lhs as AstArrayElm))))
                throw new TypeException("Invalid LHS in Assign: " + n.lhs);
            
            if (!compatible(t1,t2))
                throw new TypeException("Incompatible types in Assign: " + t1 + " <- " + t2);
        }

        // CallStmt ---
        // Exp obj;
        // Id mid;
        // ExpList args;
        public void visit(AstCallStmt n) /* throws Exception */ 
        {
            AstType t = n.obj.accept(this);
            
            if ((null == (t as AstObjType)))
                throw new TypeException("Object in CallStmt is not ObjType: " + t);

            ClassRec c = symTable.GetClass(((AstObjType) t).cid);
            MethodRec m = symTable.GetMethod(c, n.mid);
            int paramCnt = m.ParamCnt();
            int argCnt = (n.args == null) ? 0 : n.args.Count();
            
            if (paramCnt != argCnt)
            {
                throw new TypeException("Formals' and actuals' counts don't match: " + paramCnt + " vs. " + argCnt);
            }

            for (int i=0; i<paramCnt; i++)
            {
                AstType t1 = m.GetParamAt(i).Type();
                AstType t2 = n.args[i].accept(this);
                if (!compatible(t1,t2))
                throw new TypeException("Formal's and actual's types not compatible: " + t1 + " vs. " + t2);
            }
        }

        // If ---
        // Exp e;
        // Stmt s1, s2;
        public void visit(AstIf n) /* throws Exception */ 
        {
            AstType t = n.e.accept(this);
            if (!isBoolType(t))
                throw new TypeException("Test of If is not of bool type: " + t);
            n.s1.accept(this);
            if (n.s2 != null) 
                n.s2.accept(this);
        }

        // While ---
        // Exp e;
        // Stmt s;
        public void visit(AstWhile n) /* throws Exception */ 
        {
            AstType t = n.e.accept(this);

            if (!isBoolType(t))
                throw new TypeException("Test of While is not of bool type: " + t);
            
            n.s.accept(this);
        }

        // Print ---
        // Exp e;
        public void visit(AstPrint n) /* throws Exception */ 
        {
            if (n.e != null)
            {
                AstType t = n.e.accept(this);
                if ((null == (t as AstBasicType)))
                    throw new TypeException("Argument to Print must be of a basic type" + " or a string literal: " + t);
            }
        }
  
        // Return ---
        // Exp e;
        public void visit(AstReturn n) /* throws Exception */ 
        {
            if (currMethod == null)
                throw new TypeException("Return is outside a method scope");

            AstType rt = currMethod.RType();
            AstType t = (n.e==null) ? null : n.e.accept(this);
            
            if (t==null)
            {
                if (rt != null)
                    throw new TypeException("Return is missing an expr of type " + rt);
            } 
            else if (rt == null) 
            {
                throw new TypeException("Extraneous return expr in void method");
            } 
            else if (!compatible(rt, t))
            {
                throw new TypeException("Wrong return type for method " + currMethod.Id().s + ": " + t);
            }
            hasReturn = true;
        }

        #endregion STATEMENTS

        #region EXPRESSIONS

        // Binop ---
        // int op;
        // Exp e1, e2;
        public AstType visit(AstBinop n) /* throws Exception */ 
        {
            AstType t1 = n.e1.accept(this);
            AstType t2 = n.e2.accept(this);

            if (isArithOp(n.op))
            {
                if (isIntType(t1) && isIntType(t2)) 
                    return IntType;
            } 
            else if (isBoolType(t1) && isBoolType(t2)) 
            {
                return BoolType;
            }
            
            throw new TypeException("Binop operands' type mismatch: " + t1 + n.opName(n.op) + t2);
        }

        // Relop ---
        // int op;
        // Exp e1, e2;
        public AstType visit(AstRelop n) /* throws Exception */ 
        {
            AstType t1 = n.e1.accept(this);
            AstType t2 = n.e2.accept(this);

            if (n.op == AstRelop.OP.EQ || n.op == AstRelop.OP.NE) 
            {
                if (compatible(t1,t2) || compatible(t2,t1)) 
                    return BoolType;
            } 
            else if (isIntType(t1) && isIntType(t2)) 
            {
                return BoolType;
            }

            throw new TypeException("Incorrect operand types in Relop: " + t1 + n.opName(n.op) + t2);
        }

        // Unop ---
        // int op;
        // Exp e;
        public AstType visit(AstUnop n) /* throws Exception */ 
        {
            AstType t = n.e.accept(this);
            
            if ((n.op == AstUnop.OP.NEG && isIntType(t)) || (n.op == AstUnop.OP.NOT && isBoolType(t)))
                return t;

            throw new TypeException("Unop type mismatch: " + n.opName(n.op) + t);
        }

        // ArrayElm ---
        // Exp array, idx;
        // Type et;
        public AstType visit(AstArrayElm n) /* throws Exception */ 
        {
            AstType t1 = n.array.accept(this);
            AstType t2 = n.idx.accept(this);

            if (null == (t1 as AstArrayType))
                throw new TypeException("ArrayElm object is not an array: " + t1);

            if (!isIntType(t2))
                throw new TypeException("ArrayElm index is not integer: " + t2);

            return ((AstArrayType) t1).et;
        }

        // ArrayLen ---
        // Exp array;
        public AstType visit(AstArrayLen n) /* throws Exception */ 
        {
            AstType t = n.array.accept(this);

            if (null == (t as AstArrayType))
                throw new TypeException("ArrayLen object is not an array: " + t);
            
            return IntType;
        }

        // NewArray ---
        // Type et;
        // int size;
        public AstType visit(AstNewArray n) /* throws Exception */ 
        {
            return new AstArrayType(n.et);
        }

        // NewObj ---
        // Id cid;
        // ExpList args;
        public AstType visit(AstNewObj n) /* throws Exception */ 
        {
            symTable.GetClass(n.cid);
            return new AstObjType(n.cid);
        }

        // Field ---
        // Exp obj;
        // Id var;
        public AstType visit(AstField n) /* throws Exception */ 
        {
            AstType t = n.obj.accept(this);

            if (null == (t as AstObjType))
                throw new TypeException("Object in Field is not ObjType: " + t);

            ClassRec c = symTable.GetClass(((AstObjType) t).cid);
            VarRec v = symTable.GetVar(c, n.var);
            
            return v.Type();
        }

        // Call ---
        // Exp obj;
        // Id mid;
        // ExpList args;
        public AstType visit(AstCall n) /* throws Exception */ 
        {
            AstType t = n.obj.accept(this);

            if (null == (t as AstObjType))
                throw new TypeException("Object in Call is not ObjType: " + t);
    
            ClassRec c = symTable.GetClass(((AstObjType) t).cid);
            MethodRec m = symTable.GetMethod(c, n.mid);
            int paramCnt = m.ParamCnt();
            int argCnt = (n.args == null) ? 0 : n.args.Count();
            
            if (paramCnt != argCnt)
                throw new TypeException("Formals' and actuals' counts don't match: " + paramCnt + " vs. " + argCnt);

            for (int i=0; i<paramCnt; i++)
            {
                AstType t1 = m.GetParamAt(i).Type();
                AstType t2 = n.args[i].accept(this);

                if (!compatible(t1,t2))
                    throw new TypeException("Formal's and actual's types not compatible: " + t1 + " vs. " + t2);
            }

            return m.RType();
        }

        // Id ---
        // String s;
        public AstType visit(AstId n) /* throws Exception */ 
        {      
            VarRec id = symTable.GetVar(currClass, currMethod, n);
            return id.Type();
        }

        // This ---
        public AstType visit(AstThis n) 
        {
            AstId cid = new AstId(currClass.Id().s);
            return new AstObjType(cid);
        }

        public AstType visit(AstIntVal n)   { return IntType; }
        public AstType visit(AstBoolVal n)  { return BoolType; }
        public AstType visit(AstStrVal n)   { return IntType; }
    
        #endregion EXPRESSIONS
    }

}
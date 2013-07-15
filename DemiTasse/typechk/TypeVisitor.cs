// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: ClassRec.cs
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
        private Table symTable;
        private ClassRec currClass;
        private MethodRec currMethod;
        private bool hasReturn;
        private BasicType IntType = new BasicType(BasicType.Int);    
        private BasicType BoolType = new BasicType(BasicType.Bool);    

        public TypeVisitor(DemiTasse.symbol.Table symtab)
        {
            symTable = symtab;
            currClass = null;
            currMethod = null;
            hasReturn = false;
        }

        public TypeVisitor(DemiTasse.symbol.Table symtab, ClassRec c, MethodRec m)
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

        private bool isIntType(Type t)
        {
            return (null != (t as BasicType)) && (((BasicType)t).typ == BasicType.Int);
        }

        private bool isBoolType(Type t)
        {
            return (null != (t as BasicType)) && (((BasicType)t).typ == BasicType.Bool);
        }

        private bool isArithOp(Binop.OP op)
        {
            return (Binop.OP.ADD <= op) && (op <= Binop.OP.DIV);
        }

        private bool compatible(Type t1, Type t2) /* throws Exception */
        {
            if (t1 == t2) 
                return true;
            else if ((null != (t1 as BasicType)) && (null != (t2 as BasicType)))
            {
                int bt1 = ((BasicType)t1).typ;
                int bt2 = ((BasicType)t2).typ;
                if (bt1 == bt2)
                return true;
            } 
            else if ((null != (t1 as BasicType)) && (null != (t2 as ObjType)))
            {
                Id cid1 = ((ObjType) t1).cid;
                Id cid2 = ((ObjType) t2).cid;
                if (cid1.s.Equals(cid2.s))
                {
                    return true;
                } 
                else 
                {
                    ClassRec c = symTable.getClass(cid2);
                    while ((c = c.parent()) != null)
                    {
                        if (cid1.s.Equals(c.id().s))
                        return true; 
                    }
                }
            }
            else if ((null != (t1 as ArrayType)) && (null != (t2 as ArrayType)))
            {
                return compatible(((ArrayType) t1).et, ((ArrayType) t2).et);
            }
            return false;	
        }
  
        // Program ---
        // ClassDeclList cl;
        public void visit(DemiTasse.ast.Program n) /* throws Exception */
        {
            n.cl.accept(this);
        }

        #region LISTS

        public void visit(AstList n) /* throws Exception */
        {
        }

        public void visit(ClassDeclList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }
  
        public void visit(VarDeclList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

        public void visit(MethodDeclList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

        public void visit(FormalList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

        public void visit(StmtList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

        public void visit(ExpList n) /* throws Exception */ 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

        #endregion LISTS

        #region DECLARATIONS
  
        // VarDecl ---
        // Type t;
        // Id var;
        // Exp e;
        public void visit(VarDecl n) /* throws Exception */ 
        {
            if (null != (n.t as ObjType))
                // make sure type is defined
                symTable.getClass(((ObjType) n.t).cid);

            if (n.e != null)
            {
                Type t2 = n.e.accept(this);
                if (!compatible(n.t,t2))
                    throw new TypeException("Incompatible types in VarDecl: " + n.t + " <- " + t2);
            }
        }

        // Formal ---
        // Type t;
        // Id id;
        public void visit(Formal n) /* throws Exception */ 
        { 
            if (null != (n.t as ObjType)) // make sure type is defined
                symTable.getClass(((ObjType) n.t).cid);
        }

        // ClassDecl ---
        // Id cid, pid;
        // VarDeclList vl;
        // MethodDeclList ml;
        public void visit(ClassDecl n) /* throws Exception */ 
        {
            currClass = symTable.getClass(n.cid);
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
        public void visit(MethodDecl n) /* throws Exception */ 
        {
            currMethod = currClass.getMethod(n.mid);
            hasReturn = false;
            n.fl.accept(this);
            n.vl.accept(this);
            n.sl.accept(this);

            // make sure return type is defined
            if (null != (n.t as ObjType))
                symTable.getClass(((ObjType) n.t).cid);

            if ((n.t != null) && !hasReturn)
                throw new TypeException("Method " + n.mid.s + " is missing a Return statment");
            currMethod = null;
        }

        #endregion DECLARATIONS

        #region TYPES

        public Type visit(BasicType n) { return n; }
        public Type visit(ArrayType n) { return n; }

        // ObjType ---
        // Id cid;
        public Type visit(ObjType n) /* throws Exception */ 
        { 
            symTable.getClass(n.cid); // verify that the class exits
            return n; 
        }

        #endregion TYPES

        #region STATEMENTS

        // Block ---
        // StmtList sl;
        public void visit(Block n) /* throws Exception */ 
        {
            n.sl.accept(this);
        }

        // Assign ---
        // Exp lhs, rhs;
        public void visit(Assign n) /* throws Exception */ 
        {
            Type t1 = n.lhs.accept(this);
            Type t2 = n.rhs.accept(this);

            if (! ((null != (n.lhs as Id) )
                || (null != (n.lhs as Field) )
                || (null != (n.lhs as ArrayElm))))
                throw new TypeException("Invalid LHS in Assign: " + n.lhs);
            
            if (!compatible(t1,t2))
                throw new TypeException("Incompatible types in Assign: " + t1 + " <- " + t2);
        }

        // CallStmt ---
        // Exp obj;
        // Id mid;
        // ExpList args;
        public void visit(CallStmt n) /* throws Exception */ 
        {
            Type t = n.obj.accept(this);
            
            if ((null == (t as ObjType)))
                throw new TypeException("Object in CallStmt is not ObjType: " + t);

            ClassRec c = symTable.getClass(((ObjType) t).cid);
            MethodRec m = symTable.getMethod(c, n.mid);
            int paramCnt = m.paramCnt();
            int argCnt = (n.args == null) ? 0 : n.args.size();
            
            if (paramCnt != argCnt)
            {
                throw new TypeException("Formals' and actuals' counts don't match: " + paramCnt + " vs. " + argCnt);
            }

            for (int i=0; i<paramCnt; i++)
            {
                Type t1 = m.getParamAt(i).type();
                Type t2 = n.args.elementAt(i).accept(this);
                if (!compatible(t1,t2))
                throw new TypeException("Formal's and actual's types not compatible: " + t1 + " vs. " + t2);
            }
        }

        // If ---
        // Exp e;
        // Stmt s1, s2;
        public void visit(If n) /* throws Exception */ 
        {
            Type t = n.e.accept(this);
            if (!isBoolType(t))
                throw new TypeException("Test of If is not of bool type: " + t);
            n.s1.accept(this);
            if (n.s2 != null) 
                n.s2.accept(this);
        }

        // While ---
        // Exp e;
        // Stmt s;
        public void visit(While n) /* throws Exception */ 
        {
            Type t = n.e.accept(this);

            if (!isBoolType(t))
                throw new TypeException("Test of While is not of bool type: " + t);
            
            n.s.accept(this);
        }

        // Print ---
        // Exp e;
        public void visit(Print n) /* throws Exception */ 
        {
            if (n.e != null)
            {
                Type t = n.e.accept(this);
                if ((null == (t as BasicType)))
                    throw new TypeException("Argument to Print must be of a basic type" + " or a string literal: " + t);
            }
        }
  
        // Return ---
        // Exp e;
        public void visit(Return n) /* throws Exception */ 
        {
            if (currMethod == null)
                throw new TypeException("Return is outside a method scope");

            Type rt = currMethod.rtype();
            Type t = (n.e==null) ? null : n.e.accept(this);
            
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
                throw new TypeException("Wrong return type for method " + currMethod.id().s + ": " + t);
            }
            hasReturn = true;
        }

        #endregion STATEMENTS

        #region EXPRESSIONS

        // Binop ---
        // int op;
        // Exp e1, e2;
        public Type visit(Binop n) /* throws Exception */ 
        {
            Type t1 = n.e1.accept(this);
            Type t2 = n.e2.accept(this);

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
        public Type visit(Relop n) /* throws Exception */ 
        {
            Type t1 = n.e1.accept(this);
            Type t2 = n.e2.accept(this);

            if (n.op == Relop.OP.EQ || n.op == Relop.OP.NE) 
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
        public Type visit(Unop n) /* throws Exception */ 
        {
            Type t = n.e.accept(this);
            
            if ((n.op == Unop.OP.NEG && isIntType(t)) || (n.op == Unop.OP.NOT && isBoolType(t)))
                return t;

            throw new TypeException("Unop type mismatch: " + n.opName(n.op) + t);
        }

        // ArrayElm ---
        // Exp array, idx;
        // Type et;
        public Type visit(ArrayElm n) /* throws Exception */ 
        {
            Type t1 = n.array.accept(this);
            Type t2 = n.idx.accept(this);

            if (null == (t1 as ArrayType))
                throw new TypeException("ArrayElm object is not an array: " + t1);

            if (!isIntType(t2))
                throw new TypeException("ArrayElm index is not integer: " + t2);

            return ((ArrayType) t1).et;
        }

        // ArrayLen ---
        // Exp array;
        public Type visit(ArrayLen n) /* throws Exception */ 
        {
            Type t = n.array.accept(this);

            if (null == (t as ArrayType))
                throw new TypeException("ArrayLen object is not an array: " + t);
            
            return IntType;
        }

        // NewArray ---
        // Type et;
        // int size;
        public Type visit(NewArray n) /* throws Exception */ 
        {
            return new ArrayType(n.et);
        }

        // NewObj ---
        // Id cid;
        // ExpList args;
        public Type visit(NewObj n) /* throws Exception */ 
        {
            symTable.getClass(n.cid);
            return new ObjType(n.cid);
        }

        // Field ---
        // Exp obj;
        // Id var;
        public Type visit(Field n) /* throws Exception */ 
        {
            Type t = n.obj.accept(this);

            if (null == (t as ObjType))
                throw new TypeException("Object in Field is not ObjType: " + t);

            ClassRec c = symTable.getClass(((ObjType) t).cid);
            VarRec v = symTable.getVar(c, n.var);
            
            return v.type();
        }

        // Call ---
        // Exp obj;
        // Id mid;
        // ExpList args;
        public Type visit(Call n) /* throws Exception */ 
        {
            Type t = n.obj.accept(this);

            if (null == (t as ObjType))
                throw new TypeException("Object in Call is not ObjType: " + t);
    
            ClassRec c = symTable.getClass(((ObjType) t).cid);
            MethodRec m = symTable.getMethod(c, n.mid);
            int paramCnt = m.paramCnt();
            int argCnt = (n.args == null) ? 0 : n.args.size();
            
            if (paramCnt != argCnt)
                throw new TypeException("Formals' and actuals' counts don't match: " + paramCnt + " vs. " + argCnt);

            for (int i=0; i<paramCnt; i++)
            {
                Type t1 = m.getParamAt(i).type();
                Type t2 = n.args.elementAt(i).accept(this);

                if (!compatible(t1,t2))
                    throw new TypeException("Formal's and actual's types not compatible: " + t1 + " vs. " + t2);
            }

            return m.rtype();
        }

        // Id ---
        // String s;
        public Type visit(Id n) /* throws Exception */ 
        {      
            VarRec id = symTable.getVar(currClass, currMethod, n);
            return id.type();
        }

        // This ---
        public Type visit(This n) 
        {
            Id cid = new Id(currClass.id().s);
            return new ObjType(cid);
        }

        public Type visit(IntVal n)   { return IntType; }
        public Type visit(BoolVal n)  { return BoolType; }
        public Type visit(StrVal n)   { return IntType; }
    
        #endregion EXPRESSIONS
    }

}
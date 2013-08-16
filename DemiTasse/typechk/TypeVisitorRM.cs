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
    /**
     * Created with IntelliJ IDEA.
     * User: Robin
     * Date: 3/6/13
     * Time: 10:19 AM
     * To change this template use File | Settings | File Templates.
     */
    public class TypeVisitorRM : TypeVI 
    {
        private SymbolTable symTable;
        private ClassRec currClass;
        private MethodRec currMethod;
        private bool hasReturn;

        // ************************************************
        // * define the basic type nodes only once ...
        // ************************************************

        private AstBasicType intType = new AstBasicType(AstBasicType.Int);
        private AstBasicType boolType = new AstBasicType(AstBasicType.Bool);

        // ***********************************************************
        // * constructor -- a symbol table is passed in as a parameter
        // ***********************************************************

        public TypeVisitorRM(SymbolTable symtab) 
        {
            symTable = symtab;
        }

        // ************************************************
        // * top level visit routine
        // ************************************************

        public void visit(AstProgram n) 
        {
            n.cl.accept(this);
        }

        // ************************************************
        // * LISTS
        // ************************************************

        public void visit(AstList n)
        {
        }

        public void visit(AstClassDeclList n)
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

        public void visit(AstMethodDeclList n) 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

        public void visit(AstVarDeclList n) 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

        public void visit(AstFormalList n) 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

        public void visit(AstStmtList n) 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

        public void visit(AstExpList n) 
        {
            for (int i = 0; i < n.size(); i++)
                n.elementAt(i).accept(this);
        }

    // ************************************************
    // * TYPES
    // ************************************************

    public AstType visit(AstBasicType n) { return n; }

    public AstType visit(AstArrayType n) { return n; }

    public AstType visit(AstObjType n) {
    // need to verify that the class exists
        return n;
    }

    // ************************************************
    // * DECLARATIONS
    // ************************************************

    public void visit(AstVarDecl n) {
    // need to check class existence if type is ObjType
    // need to check init expr if it exists

        if (null != (n.t as AstObjType))
            // make sure type is defined
            symTable.getClass(((AstObjType) n.t).cid);

        if (n.e != null)
        {
            AstType t2 = n.e.accept(this);
            if (!compatible(n.t,t2))
                throw new TypeException("Incompatible types in VarDecl: " + n.t + " <- " + t2);
        }
    }

    public void visit(AstClassDecl n) {

        currClass = symTable.getClass(n.cid);
        //n.vl.accept(this);
        n.ml.accept(this);
        currClass = null;

    }

    public void visit(AstMethodDecl n) {

        // Set currMethod to the method table
        currMethod = currClass.getMethod(n.mid);
        hasReturn = false;
        n.fl.accept(this);
        n.vl.accept(this);
        n.sl.accept(this);

        // if method has a return value verify that method had a return statement
            if (null != (n.t as AstObjType))
                symTable.getClass(((AstObjType) n.t).cid);

            if ((n.t != null) && !hasReturn)
                throw new TypeException("Method " + n.mid.s + " is missing a Return statment");
            currMethod = null;
    }

    public void visit(Formal n) {

        ClassRec c;

        Class astType = n.t.getClass();

        // If the formal type is object type, we must check that the class definition exists
        if (astType == ObjType.class) {

            String s = ((ObjType)n.t).cid.s;
            if (null == (c = symTable.getClass(new Id(s)))) {
                throw new TypeException("Class " + s + " not defined");
            }
        }
    }

    // ************************************************
    // * STATEMENTS
    // ************************************************

    public void visit(Block n) {
        n.sl.accept(this);
    }

    public void visit(Assign n) {

        // check for well-formedness of lhs
        // check for type compatibility of both sides

        Type t1 = n.lhs.accept(this);
        Type t2 = n.rhs.accept(this);

        if (compatible(t1,  t2))
            return;

        throw new TypeException("Incompatible types in Assign: " + t1.toString() + " <- " + t2.toString());
    }

    public void visit(CallStmt n) {

        // Validate n.obj
        // Validate n.mid
        // Validate n.args

        ObjType objType;
        MethodRec m;

        if (null != n.obj) {
            objType = (ObjType)n.obj.accept(this);

            ClassRec rec = symTable.getClass(objType.cid);
            m = symTable.getMethod(rec, n.mid);
        }
        else {
            m = symTable.getMethod(currClass, n.mid);
        }

        if (m == null)
            throw new TypeException("Method name missing from symbol table");

        int paramCnt = m.paramCnt();
        int argCnt = n.args.size();

        if (paramCnt != argCnt)
            throw new TypeException(" Formals' and actuals' counts don't match: " + paramCnt + " vs. " + argCnt);

        VarRec param;
        Type t1, t2;

        for (int i = 0; i < paramCnt; ++i){
            param = m.getParamAt(i);

            t1 = param.type().accept(this);
            t2 = n.args.elementAt(i).accept(this);

            if (!compatible(t1,  t2)) {
                throw new TypeException("Formal's and actual's types not compatible: " + t1.toString() + " vs. " + t2.toString());
            }
        }
    }

    public void visit(If n) {

        Type t1 = n.e.accept(this);

        if (compatible(t1, boolType)) {
            if (null != n.s1)
                n.s1.accept(this);
            if (null != n.s2)
                n.s2.accept(this);
            return;
        }

        throw new TypeException("Test of If is not of bool type: " + t1.toString());
    }

    public void visit(While n) {

        Type t1 = n.e.accept(this);
        Type t2 = boolType.accept(this);

        if (compatible(t1,  t2)) {
            n.s.accept(this);
            return;
        }

        throw new TypeException("types not compatible");
    }

    public void visit(Print n) {

        if (null == n.e)
            return;

        Type t1 = n.e.accept(this);

        if (compatible(t1, intType) || compatible(t1, boolType)) {
            return;
        }

        throw new TypeException("Argument to Print must be of a basic type or a string literal: " + t1.toString());
    }

    public void visit(Return n) {

        // If method is void, verify there is not a return expression
        if (null == currMethod.rtype()) {
            if (null == n.e) {
                return;
            }
            throw new TypeException("Return should not contain an expression");
        }

        // If method has a return type verify method has a return expression
        Type t1 = currMethod.rtype().accept(this);

        if (null == n.e) {
            throw new TypeException("Return is missing an expr of type " + t1.toString());
        }

        // Verify method's return expression is compatible with its' return type
        Type t2 = n.e.accept(this);

        if (compatible(t1,  t2)) {
            hasReturn = true;
            return;
        }

        throw new TypeException("Wrong return type for method " + currMethod.id().s + ": " + t2.toString());
    }

    // ************************************************
    // * EXPRESSIONS
    // ************************************************

    public Type visit(Binop n) {

        // based on op type, perform corresponding checks

        Type t1 = n.e1.accept(this);
        Type t2 = n.e2.accept(this);

        if (compatible(t1,  t2)) {

            if ((n.op == Binop.ADD) || (n.op == Binop.SUB) ||(n.op == Binop.MUL) ||(n.op == Binop.DIV)) {
                if (compatible(t1,  intType)) {
                    return t1;
                }
            }

            else if ((n.op == Binop.AND) || (n.op == Binop.OR)) {
                if (compatible(t1,  boolType)) {
                    return t1;
                }
            }
            else {
                throw new TypeException("Indeterminate Binop encountered.");
            }
        }

        throw new TypeException("Binop operands' type mismatch: "+ t1.toString()+ n.opName(n.op) + t2.toString());
    }

    public Type visit(Id n) {

        // lookup the Idâ€™s type, and return it

        VarRec vr;

        // Search locals
        if (null == (vr = currMethod.getLocal(n)))

            // Search parameters
            if (null == (vr = currMethod.getParam(n)))

                // Search class variables
                if(null == (vr = currClass.getClassVar(n)))
                {
                    // Search class ancestors
                    ClassRec r = currClass;

                    while (null != r.parent()) {
                        if(null != (vr = currClass.getClassVar(n)))
                            break;
                    }
                    throw new SymbolException("Var " + n.s + " not defined");
                }

        return vr.type().accept(this);
    }

    public Type visit(This n) {

        // lookup current objectâ€™s type, return a corresponding ObjType

        return new ObjType(currClass.id());
    }

    public Type visit(Relop n) {

        // based on op type, perform corresponding checks

        Type t1 = n.e1.accept(this);
        Type t2 = n.e2.accept(this);

        if ((n.op == Relop.LT) || (n.op == Relop.LE) ||(n.op == Relop.GT) ||(n.op == Relop.GE)) {
            if (compatible(t1, intType) && compatible(t2, intType)) {
                return boolType;
            }
        }
        else if ((n.op == Relop.EQ) || (n.op == Relop.NE)) {

            if (compatible(t1, t2, false)) {
                return boolType;
            }
        }

        throw new TypeException(" Incorrect operand types in Relop: "+ t1.toString()+ n.opName(n.op).trim() + t2.toString());
    }

    public Type visit(Unop n) {

        Type t1 = n.e.accept(this);




        return t1;
    }

    public Type visit(ArrayElm n) {

        Type t1 = n.idx.accept(this);
        if (!compatible(t1, intType))
            throw new TypeException("Array index type not integer");

        Type t2 = n.array.accept(this);
        if (ArrayType.class != t2.getClass())
            throw new TypeException("ArrayElm object is not an array: " + t2.toString());

        return ((ArrayType)t2).et;
    }

    public Type visit(ArrayLen n) {

        return intType;
    }

    public Type visit(Field n) {

        // The Task: Find the class of the object, and then the type of the field
        // from the name of the variable, and name of the field

        // check that object has been defined in the method
        VarRec varClassRec;
        VarRec varRec;
        Id varId = null;
        Id classId = null;
        Class objAstType = n.obj.getClass();
        ClassRec classRec;

        // Check that this is a known object type
        if (objAstType == Id.class) { // then obj contains the variables name

            varId = (Id)n.obj;

            if (null == (varClassRec = currMethod.getParam(varId)))
                varClassRec = currMethod.getLocal(varId);

            // The var record has the type information

            if (ObjType.class == varClassRec.type().getClass()) {

                ObjType varType = (ObjType)varClassRec.type();

                //classRec = symTable.getClass(((ObjType)varClassRec.type()).cid);
                classRec = symTable.getClass(varType.cid);
                varRec = symTable.getVar(classRec, n.var);

                if(null == varRec)
                {
                    // Search class ancestors
                    while (null != classRec.parent()) {
                        if(null != (varRec = symTable.getVar(classRec, n.var)))
                            break;
                    }
                    throw new SymbolException("Var " + ((ObjType)varClassRec.type()).cid.s + " not defined");
                }
            }
            else {
                throw new TypeException("Object in Field is not ObjType: " + varClassRec.type().toString());
            }
        }
        else if (objAstType == This.class) {
            classId = currClass.id();

            // Find the class of this object
            classRec = symTable.getClass(classId);
            varRec = symTable.getVar(classRec, n.var);

            if(null == varRec)
            {
                // Search class ancestors
                while (null != classRec.parent()) {
                    if(null != (varRec = currClass.getClassVar(classId)))
                        break;
                }
                throw new SymbolException("Var " + classId.s + " not defined");
            }

        }
        else {
            throw new TypeException("Object in Field is not ObjType: " + n.obj.getClass().toString());
        }


        // find field variable
        //if (null == (v = currMethod.getParam(classId)))
        //v = currMethod.getLocal(classId);

        return varRec.type();
    }

    public Type visit(Call n) {

        // Validate n.obj
        // Validate n.mid
        // Validate n.args

        ObjType objType;
        MethodRec m;

        if (null != n.obj) {
            objType = (ObjType)n.obj.accept(this);

            ClassRec rec = symTable.getClass(objType.cid);
            m = symTable.getMethod(rec, n.mid);
        }
        else {
            m = symTable.getMethod(currClass, n.mid);
        }


        if (m == null)
            throw new TypeException("Method name missing from symbol table");

        int paramCnt = m.paramCnt();
        int argCnt = n.args.size();

        if (paramCnt != argCnt)
            throw new TypeException(" Formals' and actuals' counts don't match: " + paramCnt + " vs. " + argCnt);

        VarRec param;
        Type t1, t2;

        for (int i = 0; i < paramCnt; ++i){
            param = m.getParamAt(i);

            t1 = param.type().accept(this);
            t2 = n.args.elementAt(i).accept(this);

            if (!compatible(t1,  t2)) {
                throw new TypeException("Formal's and actual's types not compatible: " + t1.toString() + " vs. " + t2.toString());
            }


        }

        return m.rtype();
    }

    public Type visit(NewArray n) {

        return new ArrayType(n.et);
    }

    public Type visit(NewObj n) {

        ClassRec r = symTable.getClass(n.cid);
        return new ObjType(r.id());
    }

    // ************************************************
    // * Base values
    // ************************************************

    public Type visit(IntVal n) {

        return intType;
    }

    public Type visit(BoolVal n) {

        return boolType;
    }

    public Type visit(StrVal n) {

        return intType;
    }

    private bool compatible(Type t1, Type t2) {
        return compatible(t1, t2, true);
    }

     private bool compatible(Type t1, Type t2, bool allowAncestors) {

        if (t1 == t2)
            return true;

        Class c1 = t1.getClass();
        Class c2 = t2.getClass();
        Class cbt = BasicType.class;

        if ((c1 == cbt) && (c2 == cbt)) {
            if (((BasicType)t1).typ == ((BasicType)t2).typ){
                return true;
            }
            return false;
        }

        Class clo = ObjType.class;

        if ((c1 == clo) && (c2 == clo)) // name equivalence
        {
            if ((sameClassId((ObjType)t1, (ObjType)t2)) || // their class ids are the same
                (allowAncestors && isAncestor((ObjType)t1, (ObjType)t2)))    // t1â€™s cid matches an ancestorâ€™s cid of t2))
            {
                return true;
            }
            return false;
        }

        Class cla = ArrayType.class;

        if ((c1 == cla) && (c2 == cla)) // both are ArrayType: structure equivalence
        {
            // recursively test the compatibility of their elementsâ€™ types
            return isStructurallyEquiv((ArrayType)t1, (ArrayType)t2);
        }

        return false;
    }

    private bool sameBasicType(Type t1, Type t2) {


        return false;
    }

    private bool sameClassId(ObjType t1, ObjType t2){

        bool result = t1.cid.s.equals(t2.cid.s);
        return result;
    }

    private bool isAncestor(ObjType t1, ObjType t2) throws Exception{

        ClassRec rec = symTable.getClass(t2.cid);
        while (true) {
            if (null == (rec = rec.parent()))
                return false;
            else if (rec.id().s.equals(t1.cid.s))
                return true;
        }
    }

    private bool isStructurallyEquiv(ArrayType t1, ArrayType t2) {

        return compatible(t1.et, t2.et);

    }

    private String typeName(Type t){

        if (t.getClass() == BasicType.class){
            return ((BasicType)t).toString();
        }
        else
            return "TBD";
    }
}

    }
}

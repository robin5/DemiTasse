// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: SymbolVisitor.cs
// *
// * Description:  Processing declarations and creating symbol tables.
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

using System;
using System.Collections.Generic;

using DemiTasse.ast;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.symbol
{
    public class SymbolVisitor : TypeVI
    {
        public Table symTable;         // the top-scope symbol table
        private ClassRec currClass;    // the current class scope
        private MethodRec currMethod;  // the current method scope

        public SymbolVisitor()
        {
            symTable = new symbol.Table();
            currClass = null;
            currMethod = null;
        }

        private void setupClassHierarchy(ClassDeclList cl) /* throws Exception */
        {
            List<ClassDecl> work = new List<ClassDecl>();
            List<String> done = new List<String>();
            
            for (int i = 0; i < cl.size(); i++)
                work.Add(cl.elementAt(i));

            while (work.Count > 0)
            {
                for (int i = 0; i < work.Count; i++)
                {
                    ClassDecl cd = (ClassDecl) work[i];
                    if (cd.pid != null)
                    {
                        if (!done.Contains(cd.pid.s))
                        {
                            continue;
                        }

                        ClassRec cr = symTable.getClass(cd.cid);
                        ClassRec pr = symTable.getClass(cd.pid);
                        cr.linkParent(pr);
                    }
                done.Add(cd.cid.s);
                work.Remove(cd);
                }
            }
        }

        // Program ---
        // ClassDeclList cl;
        public void visit(DemiTasse.ast.Program n) /* throws Exception */
        {
            n.cl.accept(this);
            setupClassHierarchy(n.cl); // establish class hierarchy
        }

        // LISTS --- use default traversal
  
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

        // DECLARATIONS
  
        // ClassDecl ---
        // Id cid, pid;
        // VarDeclList vl;
        // MethodDeclList ml;
        public void visit(ClassDecl n) /* throws Exception */
        {
            symTable.addClass(n.cid);
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
            currClass.addMethod(n.mid, n.t);
            currMethod = currClass.getMethod(n.mid);
            n.fl.accept(this);
            n.vl.accept(this);
            currMethod = null;
        }
  
        // VarDecl ---
        // Type t;
        // Id var;
        // Exp e;
        public void visit(VarDecl n) /* throws Exception */
        {
            if (currMethod != null)
            { 	// a local variable	
                if (currMethod.getParam(n.var) != null)
                    throw new SymbolException(n.var.s + " already defined as a param");
                currMethod.addLocal(n.var, n.t);
            } 
            else 
            {   // a class variable
                currClass.addClassVar(n.var, n.t, n.e);
            }
        }
  
        // Formal ---
        // Type t;
        // Id id;
        public void visit(Formal n) /* throws Exception */
        {
            if (currMethod == null) 
                throw new SymbolException("currMethod does not exits");
            currMethod.addParam(n.id, n.t);
        }

        // TYPES --- use the nodes themselves 

        public DemiTasse.ast.Type visit(BasicType n) { return n; }
        public DemiTasse.ast.Type visit(ObjType n) /* throws Exception */ { return n; }
        public DemiTasse.ast.Type visit(ArrayType n) { return n; }

        // No need to implement statements or expressions nodes.

        public void visit(StmtList n) /* throws Exception */ {}
        public void visit(Block n) /* throws Exception */ {}
        public void visit(Assign n) /* throws Exception */ {}
        public void visit(CallStmt n) /* throws Exception */ {}
        public void visit(If n) /* throws Exception */ {}
        public void visit(While n) /* throws Exception */ {}
        public void visit(Print n) /* throws Exception */ {}
        public void visit(Return n) /* throws Exception */ {}

        public void visit(ExpList n) /* throws Exception */ {}
        public DemiTasse.ast.Type visit(Binop n) /* throws Exception */    { return null; }
        public DemiTasse.ast.Type visit(Relop n) /* throws Exception */    { return null; }
        public DemiTasse.ast.Type visit(Unop n) /* throws Exception */ 	   { return null; }
        public DemiTasse.ast.Type visit(ArrayElm n) /* throws Exception */ { return null; }
        public DemiTasse.ast.Type visit(ArrayLen n) /* throws Exception */ { return null; }
        public DemiTasse.ast.Type visit(Field n) /* throws Exception */    { return null; }
        public DemiTasse.ast.Type visit(Call n) /* throws Exception */     { return null; }
        public DemiTasse.ast.Type visit(NewArray n) /* throws Exception */ { return null; }
        public DemiTasse.ast.Type visit(NewObj n) /* throws Exception */   { return null; }
        public DemiTasse.ast.Type visit(Id n) /* throws Exception */       { return null; }
        public DemiTasse.ast.Type visit(This n) /* throws Exception */     { return null; }

        public DemiTasse.ast.Type visit(IntVal n) { return null; }
        public DemiTasse.ast.Type visit(BoolVal n) { return null; }
        public DemiTasse.ast.Type visit(StrVal n) { return null; }
    }
}
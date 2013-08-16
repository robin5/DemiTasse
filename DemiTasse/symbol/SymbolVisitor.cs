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
        public SymbolTable symTable;         // the top-scope symbol table
        private ClassRec currClass;    // the current class scope
        private MethodRec currMethod;  // the current method scope

        public SymbolVisitor()
        {
            symTable = new symbol.SymbolTable();
            currClass = null;
            currMethod = null;
        }

        private void setupClassHierarchy(AstClassDeclList cl)
        {
            List<AstClassDecl> work = new List<AstClassDecl>();
            List<String> done = new List<String>();
            
            for (int i = 0; i < cl.Count(); i++)
                work.Add(cl[i]);

            while (work.Count > 0)
            {
                for (int i = 0; i < work.Count; i++)
                {
                    AstClassDecl cd = (AstClassDecl) work[i];
                    if (cd.pid != null)
                    {
                        if (!done.Contains(cd.pid.s))
                        {
                            continue;
                        }

                        ClassRec cr = symTable.GetClass(cd.cid);
                        ClassRec pr = symTable.GetClass(cd.pid);
                        cr.LinkParent(pr);
                    }
                done.Add(cd.cid.s);
                work.Remove(cd);
                }
            }
        }

        // Program ---
        // ClassDeclList cl;
        public void visit(AstProgram n)
        {
            n.cl.accept(this);
            setupClassHierarchy(n.cl); // establish class hierarchy
        }

        // LISTS --- use default traversal
  
        public void visit(AstList n)
        {
        }

        public void visit(AstClassDeclList n)
        {
            for (int i = 0; i < n.Count(); i++)
                n[i].accept(this);
        }
  
        public void visit(AstVarDeclList n)
        {
            for (int i = 0; i < n.Count(); i++)
                n[i].accept(this);
        }

        public void visit(AstMethodDeclList n)
        {
            for (int i = 0; i < n.Count(); i++)
                n[i].accept(this);
        }

        public void visit(AstFormalList n)
        {
            for (int i = 0; i < n.Count(); i++)
                n[i].accept(this);
        }

        // DECLARATIONS
  
        // ClassDecl ---
        // Id cid, pid;
        // VarDeclList vl;
        // MethodDeclList ml;
        public void visit(AstClassDecl n)
        {
            symTable.AddClass(n.cid);
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
        public void visit(AstMethodDecl n)
        {
            currClass.AddMethod(n.mid, n.t);
            currMethod = currClass.GetMethod(n.mid);
            n.fl.accept(this);
            n.vl.accept(this);
            currMethod = null;
        }
  
        // VarDecl ---
        // Type t;
        // Id var;
        // Exp e;
        public void visit(AstVarDecl n)
        {
            if (currMethod != null)
            { 	// a local variable	
                if (currMethod.GetParam(n.var) != null)
                    throw new SymbolException(n.var.s + " already defined as a param");
                currMethod.AddLocal(n.var, n.t);
            } 
            else 
            {   // a class variable
                currClass.AddClassVar(n.var, n.t, n.e);
            }
        }
  
        // Formal ---
        // Type t;
        // Id id;
        public void visit(AstFormal n)
        {
            if (currMethod == null) 
                throw new SymbolException("currMethod does not exits");
            currMethod.AddParam(n.id, n.t);
        }

        // TYPES --- use the nodes themselves 

        public AstType visit(AstBasicType n) { return n; }
        public AstType visit(AstObjType n) { return n; }
        public AstType visit(AstArrayType n) { return n; }

        // No need to implement statements or expressions nodes.

        public void visit(AstStmtList n) {}
        public void visit(AstBlock n) {}
        public void visit(AstAssign n) {}
        public void visit(AstCallStmt n) {}
        public void visit(AstIf n) {}
        public void visit(AstWhile n) {}
        public void visit(AstPrint n) {}
        public void visit(AstReturn n) {}

        public void visit(AstExpList n) {}
        public AstType visit(AstBinop n)    { return null; }
        public AstType visit(AstRelop n)    { return null; }
        public AstType visit(AstUnop n) 	{ return null; }
        public AstType visit(AstArrayElm n) { return null; }
        public AstType visit(AstArrayLen n) { return null; }
        public AstType visit(AstField n)    { return null; }
        public AstType visit(AstCall n)     { return null; }
        public AstType visit(AstNewArray n) { return null; }
        public AstType visit(AstNewObj n)   { return null; }
        public AstType visit(AstId n)       { return null; }
        public AstType visit(AstThis n)     { return null; }

        public AstType visit(AstIntVal n)   { return null; }
        public AstType visit(AstBoolVal n)  { return null; }
        public AstType visit(AstStrVal n)   { return null; }
    }
}
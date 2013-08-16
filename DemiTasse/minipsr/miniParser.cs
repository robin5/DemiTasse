// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: miniParser.cs
// *
// * Description: 
// *
// * Author: Robin Murray
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
using System.IO;

using DemiTasse.ast;
using DemiTasse.psrutil;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.minipsr
{
    // *******************************************************************************************
    // *
    // *                                  MINI LANGUAGE GRAMMAR
    // *
    // *******************************************************************************************

    public class MiniParser : MiniParserConstants
    {
        AstProgram _astProgram = null;
        // -----------------------------------
        // Program    -> MainClass {ClassDecl}
        // -----------------------------------

        public AstProgram ParseProgram()
        {
            try
            {
                if (_astProgram == null)
                {
                    _astProgram = MiniParser.Program();
                    return _astProgram;
                }
                return _astProgram;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
        }

        static /* final */ public AstProgram Program() /* throws ParseException */
        {
            bool done = false;
            AstClassDecl c;
            AstClassDeclList cl = new AstClassDeclList();
            // Get a reference to the Main class and add it to the list of classes
            c = MainClass();
            cl.Add(c);
            
            // label_1:
            while (!done)
            {
                switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case MpRegExpId.CLASS:
                        break;
                    default:
                        jj_la1[0] = jj_gen;
                        done = true;
                        break /* label_1 */;
                }
                if (!done)
                {
                    c = ClassDecl();
                    cl.Add(c);
                }
            }
            jj_consume_token(0);
            return new AstProgram( cl );
        }

        // ----------------------------------------------------------
        // MainClass  -> "class" <ID> "{" MainMethod {MethodDecl} "}"
        // ----------------------------------------------------------

        static /* final */ public AstClassDecl MainClass() /* throws ParseException */
        {
            AstId cid;                                     // Name of this class
            AstId pid = null;                              // Name of the super class (if it exists)
            AstMethodDecl m;                               // Reference to a class level method
            AstMethodDeclList ml = new AstMethodDeclList();   // List of class methods
            AstVarDecl v;                                  // Reference to a class level variable
            AstVarDeclList vl = new AstVarDeclList();
            bool done = false;

            jj_consume_token(MpRegExpId.CLASS);
            cid = Id();
            jj_consume_token(MpRegExpId.LBRACE);

            /* label_2: */
            while (!done)
            {
                switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case MpRegExpId.BOOLEAN:
                        break;
                    case MpRegExpId.INT:
                        break;
                    case MpRegExpId.ID:
                        break;
                    default:
                        jj_la1[1] = jj_gen;
                        done = true;
                        break /* label_2 */;
                }
                if (!done)
                {
                    v = VarDecl();
                    vl.Add(v);
                }
            }

            // Get a reference to the Main method and add it to the list of methods in this class
            m = MainMethod();
            ml.Add(m);
            
            done = false;
            /* label_3: */
            while (!done)
            {
                switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case MpRegExpId.PUBLIC:
                        break;
                    default:
                        jj_la1[2] = jj_gen;
                        done = true;
                        break /* label_3 */;
                }
                if (!done)
                {
                    m = MethodDecl();
                    ml.Add(m);
                }
            }
            jj_consume_token(MpRegExpId.RBRACE);
            return new AstClassDecl( cid, pid, vl, ml );
        }

        // --------------------------------------------------------------------------
        // ClassDecl  -> "class" <ID> ["extends" <ID>] "{" {VarDecl} {MethodDecl} "}"
        // --------------------------------------------------------------------------

        static /* final */ public AstClassDecl ClassDecl() /* throws ParseException */
        {
            AstId cid;                                     // Name of this class
            AstId pid = null;                              // Name of the super class (if it exists)
            AstMethodDecl m;                               // Reference to a class level method
            AstMethodDeclList ml = new AstMethodDeclList();   // List of class methods
            AstVarDecl v;                                  // Reference to a class level variable
            AstVarDeclList vl = new AstVarDeclList();
            bool done;

            jj_consume_token(MpRegExpId.CLASS);
            cid = Id();

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.EXTENDS:
                    jj_consume_token(MpRegExpId.EXTENDS);
                    pid = Id();
                    break;
                default:
                    jj_la1[3] = jj_gen;
                    break;
            }

            jj_consume_token(MpRegExpId.LBRACE);

            /* label_4: */
            done = false;
            while (!done)
            {
                switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case MpRegExpId.BOOLEAN:
                        break;
                    case MpRegExpId.INT:
                        break;
                    case MpRegExpId.ID:
                        break;

                    default:
                        jj_la1[4] = jj_gen;
                        done = true;
                        break /* label_4 */;
                }
                if (!done)
                {
                    v = VarDecl();
                    vl.Add(v);
                }
            }

            /* label_5: */
            done = false;
            while (!done) 
            {
                switch ((jj_ntk==MpRegExpId.UNDEFINED)?jj_ntk_fn():jj_ntk)
                {
                    case MpRegExpId.PUBLIC:
                        break;
                    default:
                        jj_la1[5] = jj_gen;
                        done = true;
                        break /* label_5 */;
                }
                if (!done)
                {
                    m = MethodDecl();
                    ml.Add(m);
                }
            }
            jj_consume_token(MpRegExpId.RBRACE);
            return new AstClassDecl( cid, pid, vl, ml );
        }

        /// <summary>
        /// MainMethod  -> "public" "static" "void" "main" "(" "String" "[" "]" <ID> ")" "{" {VarDecl} {Statement} "}"
        /// </summary>
        /// <returns></returns>
        
        static /* final */ public AstMethodDecl MainMethod() /* throws ParseException */
        {
            DemiTasse.ast.AstType t = null;
            AstId id = new AstId( "main" );
            AstFormalList fl = null;
            AstVarDecl v;
            AstVarDeclList vl = new AstVarDeclList();
            AstStmt s;
            AstStmtList sl = new AstStmtList();
            bool done;
            
            jj_consume_token(MpRegExpId.PUBLIC);
            jj_consume_token(MpRegExpId.STATIC);
            jj_consume_token(MpRegExpId.VOID);
            jj_consume_token(MpRegExpId.MAIN);
            jj_consume_token(MpRegExpId.LPAREN);
            jj_consume_token(MpRegExpId.STRING);
            jj_consume_token(MpRegExpId.LBRACKET);
            jj_consume_token(MpRegExpId.RBRACKET);
            jj_consume_token(MpRegExpId.ID);
            jj_consume_token(MpRegExpId.RPAREN);
            jj_consume_token(MpRegExpId.LBRACE);

            /* label_6: */
            while (jj_2_1(2))
            {
                v = VarDecl();
                vl.Add(v);
            }

            /* label_7: */
            done = false;
            while (!done)
            {
                switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case MpRegExpId.IF:
                        break;
                    case MpRegExpId.RETURN:
                        break;
                    case MpRegExpId.SYSTEM_OUT_PRINTLN:
                        break;
                    case MpRegExpId.THIS:
                        break;
                    case MpRegExpId.WHILE:
                        break;
                    case MpRegExpId.ID:
                        break;
                    case MpRegExpId.LBRACE:
                        break;
                    default:
                        jj_la1[6] = jj_gen;
                        done = true;
                        break /* label_7 */;
                }
                if(!done)
                {
                    s = Statement();
                    sl.Add(s);
                }
            }
            jj_consume_token(MpRegExpId.RBRACE);
            return new AstMethodDecl( t, id, fl, vl, sl );
        }

        // -----------------------------------------------------------------------------
        // "public" (Type | "void") <ID> "(" [Formals] ")" "{" {VarDecl} {Statement} "}"
        // -----------------------------------------------------------------------------
        
        static /* final */ public AstMethodDecl MethodDecl() /* throws ParseException */
        {
            DemiTasse.ast.AstType t = null;
            AstId id;
            AstFormalList fl = null;
            AstVarDecl v;
            AstVarDeclList vl = new AstVarDeclList();
            AstStmt s;
            AstStmtList sl = new AstStmtList();

            jj_consume_token(MpRegExpId.PUBLIC);
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.VOID:
                    jj_consume_token(MpRegExpId.VOID);
                    break;
                case MpRegExpId.BOOLEAN:
                    t = Type();
                    break;
                case MpRegExpId.INT:
                    t = Type();
                    break;
                case MpRegExpId.ID:
                    t = Type();
                    break;
                default:
                    jj_la1[7] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
            }
            
            id = Id();
            jj_consume_token(MpRegExpId.LPAREN);
            
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.BOOLEAN:
                    fl = Formals();
                    break;
                case MpRegExpId.INT:
                    fl = Formals();
                    break;
                case MpRegExpId.ID:
                    fl = Formals();
                    break;
                default:
                    jj_la1[8] = jj_gen;
                    break;
            }
            
            jj_consume_token(MpRegExpId.RPAREN);
            jj_consume_token(MpRegExpId.LBRACE);
            
            /* label_8: */
            while (jj_2_2(2))
            {
                v = VarDecl();
                vl.Add(v);
            }

            /* label_9:*/
            bool done = false;
            while (!done)
            {
                switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case MpRegExpId.IF:
                        break;
                    case MpRegExpId.RETURN:
                        break;
                    case MpRegExpId.SYSTEM_OUT_PRINTLN:
                        break;
                    case MpRegExpId.THIS:
                        break;
                    case MpRegExpId.WHILE:
                        break;
                    case MpRegExpId.ID:
                        break;
                    case MpRegExpId.LBRACE:
                        break;
                    default:
                        jj_la1[9] = jj_gen;
                        done = true;
                        break /* label_9 */;
                }
                if (!done)
                {
                    s = Statement();
                    sl.Add(s);
                }
            }

            jj_consume_token(MpRegExpId.RBRACE);
            return new AstMethodDecl( t, id, fl, vl, sl );
        }

        // --------------------------------------------------------------------------
        // Formals  ->  Type <ID> {"," Type <ID>}
        // --------------------------------------------------------------------------

        static /* final */ public AstFormalList Formals() /* throws ParseException */
        {
            DemiTasse.ast.AstType t = null;
            AstId id = null;
            AstFormalList fl = new AstFormalList();

            t = Type();
            id = Id();
            fl.Add( new AstFormal( t, id ) );

            /* label_10: */
            bool done = false;
            while (!done)
            {
                switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case MpRegExpId.COMMA:
                        break;
                    default:
                        jj_la1[10] = jj_gen;
                        done = true;
                        break /* label_10 */;
                }
                if (!done)
                {
                    jj_consume_token(MpRegExpId.COMMA);
                    t = Type();
                    id = Id();
                    fl.Add(new AstFormal(t, id));
                }
            }
            {if (true) return fl;}
            throw new Error("Missing return statement in function");
}

        // --------------------------------------------------------------------------
        // VarDec  ->  Type <ID> ["=" InitExpr] ";"
        // --------------------------------------------------------------------------

        static /* final */ public AstVarDecl VarDecl() /* throws ParseException */
        {
            DemiTasse.ast.AstType t = null;
            AstId id = null;
            AstExp e = null;
            t = Type();
            id = Id();
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.ASSIGN:
                    jj_consume_token(MpRegExpId.ASSIGN);
                    e = InitExpr();
                    break;
                default:
                    jj_la1[11] = jj_gen;
                    ;
                    break;
            }
            jj_consume_token(MpRegExpId.SEMICOLON);
            return new AstVarDecl( t, id, e );
        }

        // --------------------------------------------------------------------------
        // Type  ->  BasicType ["[" "]"] | <ID>
        // --------------------------------------------------------------------------
        
        static /* final */ public DemiTasse.ast.AstType Type() /* throws ParseException */
        {
            DemiTasse.ast.AstType t = null;
            AstId id = null;

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.BOOLEAN:

                    t = BasicType();
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.LBRACKET:
                            jj_consume_token(MpRegExpId.LBRACKET);
                            jj_consume_token(MpRegExpId.RBRACKET);
                            t = new AstArrayType( t );
                            break;
                        default:
                            jj_la1[12] = jj_gen;
                            break;
                    }
                    return t;

                case MpRegExpId.INT:

                    t = BasicType();
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.LBRACKET:
                            jj_consume_token(MpRegExpId.LBRACKET);
                            jj_consume_token(MpRegExpId.RBRACKET);
                            t = new AstArrayType( t );
                            break;
                        default:
                            jj_la1[12] = jj_gen;
                            break;
                    }
                    return t;

                case MpRegExpId.ID:
                    id = Id();
                    return new AstObjType( id );

                default:
                    jj_la1[13] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
            }
        throw new Error("Missing return statement in function");
        }

        // --------------------------------------------------------------------------
        // BasicType  ->  "int" | "bool"
        // --------------------------------------------------------------------------

        static /* final */ public AstBasicType BasicType() /* throws ParseException */
        {
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.INT:

                    jj_consume_token(MpRegExpId.INT);
                    return new AstBasicType(DemiTasse.ast.AstBasicType.Int);

                case MpRegExpId.BOOLEAN:

                    jj_consume_token(MpRegExpId.BOOLEAN);
                    return new AstBasicType(DemiTasse.ast.AstBasicType.Bool);

                default:
                    jj_la1[14] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
            }
            throw new Error("Missing return statement in function");
        }

        // --------------------------------------------------------------------------
        // Statement  -> BlockStmt
        //            |  AssignOrCallStmt
        //            |  IfStmt
        //            |  WhileStmt
        //            |  PrintStmt
        //            |  ReturnStm
        // --------------------------------------------------------------------------

        static /* final */ public AstStmt Statement() /* throws ParseException */
        {
            AstStmt s = null;
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.LBRACE:
                    s = BlockStmt();
                    return s;

                case MpRegExpId.THIS:
                    s = AssignOrCallStmt();
                    return s;

                case MpRegExpId.ID:
                    s = AssignOrCallStmt();
                    return s;

                case MpRegExpId.IF:
                    s = IfStmt();
                    return s;

                case MpRegExpId.WHILE:
                    s = WhileStmt();
                    return s;

                case MpRegExpId.SYSTEM_OUT_PRINTLN:
                    s = PrintStmt();
                    return s;

                case MpRegExpId.RETURN:
                    s = ReturnStmt();
                    return s;

                default:
                    jj_la1[15] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
                }
            throw new Error("Missing return statement in function");
        }

        // --------------------------------------------------------------------------
        // BlockStmt  ->  <LBRACE> ( Statement() )* <RBRACE>
        // --------------------------------------------------------------------------
        
        static /* final */ public AstBlock BlockStmt() /* throws ParseException */
        {
            AstStmt s = null;
            AstStmtList sl = new AstStmtList();
            jj_consume_token( MpRegExpId.LBRACE);

            /* label_11: */
            bool done = false;
            while (!done)
            {
                switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case  MpRegExpId.IF:
                        break;
                    case  MpRegExpId.RETURN:
                        break;
                    case  MpRegExpId.SYSTEM_OUT_PRINTLN:
                        break;
                    case  MpRegExpId.THIS:
                        break;
                    case  MpRegExpId.WHILE:
                        break;
                    case  MpRegExpId.ID:
                        break;
                    case  MpRegExpId.LBRACE:
                        break;
                    default:
                        jj_la1[16] = jj_gen;
                        done = true;
                        break /* label_11 */;
                }
                if (!done)
                {
                    s = Statement();
                    sl.Add(s);
                }
            }
            jj_consume_token(MpRegExpId.RBRACE);
            return new AstBlock( sl );
        }


        // --------------------------------------------------------------------------
        // ReturnStmt  ->  <RETURN> [ Expr() ] <SEMICOLON>
        // --------------------------------------------------------------------------
        
        static /* final */ public AstReturn ReturnStmt() /* throws ParseException */
        {
            AstExp e = null;
            
            jj_consume_token(MpRegExpId.RETURN);
            
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.FALSE:
                    e = Expr();
                    break;
                case MpRegExpId.THIS:
                    e = Expr();
                    break;
                case MpRegExpId.TRUE:
                    e = Expr();
                    break;
                case MpRegExpId.ID:
                    e = Expr();
                    break;
                case MpRegExpId.LPAREN:
                    e = Expr();
                    break;
                case MpRegExpId.SUB:
                    e = Expr();
                    break;
                case MpRegExpId.NOT:
                    e = Expr();
                    break;
                case MpRegExpId.INTVAL:
                    e = Expr();
                    break;

                default:
                    jj_la1[17] = jj_gen;
                    ;
                    break;
            }
            jj_consume_token(MpRegExpId.SEMICOLON);
            return new AstReturn( e );
        }

        // --------------------------------------------------------------------------
        // AssignOrCallStmt  -> Lvalue "(" Args ")" | = InitExpr
        // --------------------------------------------------------------------------
        static /* final */ public AstStmt AssignOrCallStmt() /* throws ParseException */
        {
            AstExp lhs = null;
            AstExp rhs = null;
            AstExpList args = null;
            bool isCall = false;
            lhs = Lvalue();
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.LPAREN:

                    jj_consume_token(MpRegExpId.LPAREN);
                    isCall = true;

                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.FALSE:
                            args = Args();
                            break;
                        case MpRegExpId.THIS:
                            args = Args();
                            break;
                        case MpRegExpId.TRUE:
                            args = Args();
                            break;
                        case MpRegExpId.ID:
                            args = Args();
                            break;
                        case MpRegExpId.LPAREN:
                            args = Args();
                            break;
                        case MpRegExpId.SUB:
                            args = Args();
                            break;
                        case MpRegExpId.NOT:
                            args = Args();
                            break;
                        case MpRegExpId.INTVAL:
                            args = Args();
                            break;
                        default:
                            jj_la1[18] = jj_gen;
                            break;
                    }
                    jj_consume_token(MpRegExpId.RPAREN);
                    isCall = true;
                    break;

                case MpRegExpId.ASSIGN:

                    jj_consume_token(MpRegExpId.ASSIGN);
                    rhs = InitExpr();
                    break;

                default:

                    jj_la1[19] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
            }

            jj_consume_token(MpRegExpId.SEMICOLON);
            if ( isCall )
            {
                if (null != ( lhs as AstId ))
                {
                    // System.out.println("CallStmt: ID");
                    return new AstCallStmt( new AstThis(), (AstId)lhs, args );
                }
                else if (null != (lhs as AstField ))
                {
                    // System.out.println("CallStmt: Field");
                    return new AstCallStmt( ((AstField)lhs).obj, ((AstField)lhs).var, args );
                }
                else
                    return null;
            }
            else
            {
                return new AstAssign( lhs, rhs );
            }
            throw new Error("Missing return statement in function");
        }

        // --------------------------------------------------------------------------
        // IfStmt  ->  "if" "(" Expr() ")" Statement ["else" Statement]
        // --------------------------------------------------------------------------

        static /* final */ public AstIf IfStmt() /* throws ParseException */
        {
            AstExp e = null;
            AstStmt s1 = null;
            AstStmt s2 = null;

            jj_consume_token(MpRegExpId.IF);
            jj_consume_token(MpRegExpId.LPAREN);
            e = Expr();
            jj_consume_token(MpRegExpId.RPAREN);
            s1 = Statement();
            
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.ELSE:
                    jj_consume_token(MpRegExpId.ELSE);
                    s2 = Statement();
                    break;
                default:
                    jj_la1[20] = jj_gen;
                    break;
            }
            return new AstIf( e, s1, s2);
        }

        // --------------------------------------------------------------------------
        // WhileStmt  ->  <WHILE> <LPAREN> e = Expr() <RPAREN> s = Statement()
        // --------------------------------------------------------------------------

        static /* final */ public AstWhile WhileStmt() /* throws ParseException */
        {
            AstExp e = null;
            AstStmt s = null;
            jj_consume_token(MpRegExpId.WHILE);
            jj_consume_token(MpRegExpId.LPAREN);
            e = Expr();
            jj_consume_token(MpRegExpId.RPAREN);
            s = Statement();
            return new AstWhile( e, s );
        }

        // --------------------------------------------------------------------------------------
        // PrintStmt  ->  <SYSTEM_OUT_PRINTLN> <LPAREN> [ Expr() | <STRVAL> ] <RPAREN> <SEMICOLON>
        // --------------------------------------------------------------------------------------
        
        static /* final */ public AstPrint PrintStmt() /* throws ParseException */
        {
            AstExp e = null;

            jj_consume_token(MpRegExpId.SYSTEM_OUT_PRINTLN);
            jj_consume_token(MpRegExpId.LPAREN);

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.FALSE:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.FALSE: e = Expr(); break;
                        case MpRegExpId.THIS: e = Expr(); break;
                        case MpRegExpId.TRUE: e = Expr(); break;
                        case MpRegExpId.ID: e = Expr(); break;
                        case MpRegExpId.LPAREN: e = Expr(); break;
                        case MpRegExpId.SUB: e = Expr(); break;
                        case MpRegExpId.NOT: e = Expr(); break;
                        case MpRegExpId.INTVAL: e = Expr(); break;
                        case MpRegExpId.STRVAL: e = StrVal(); break;
                        default: jj_la1[21] = jj_gen; jj_consume_token(MpRegExpId.UNDEFINED); throw new MiniParseException();
                    }
                    break;
                case MpRegExpId.THIS:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.FALSE: e = Expr(); break;
                        case MpRegExpId.THIS: e = Expr(); break;
                        case MpRegExpId.TRUE: e = Expr(); break;
                        case MpRegExpId.ID: e = Expr(); break;
                        case MpRegExpId.LPAREN: e = Expr(); break;
                        case MpRegExpId.SUB: e = Expr(); break;
                        case MpRegExpId.NOT: e = Expr(); break;
                        case MpRegExpId.INTVAL: e = Expr(); break;
                        case MpRegExpId.STRVAL: e = StrVal(); break;
                        default: jj_la1[21] = jj_gen; jj_consume_token(MpRegExpId.UNDEFINED); throw new MiniParseException();
                    }
                    break;
                case MpRegExpId.TRUE:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.FALSE: e = Expr(); break;
                        case MpRegExpId.THIS: e = Expr(); break;
                        case MpRegExpId.TRUE: e = Expr(); break;
                        case MpRegExpId.ID: e = Expr(); break;
                        case MpRegExpId.LPAREN: e = Expr(); break;
                        case MpRegExpId.SUB: e = Expr(); break;
                        case MpRegExpId.NOT: e = Expr(); break;
                        case MpRegExpId.INTVAL: e = Expr(); break;
                        case MpRegExpId.STRVAL: e = StrVal(); break;
                        default: jj_la1[21] = jj_gen; jj_consume_token(MpRegExpId.UNDEFINED); throw new MiniParseException();
                    }
                    break;
                case MpRegExpId.ID:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.FALSE: e = Expr(); break;
                        case MpRegExpId.THIS: e = Expr(); break;
                        case MpRegExpId.TRUE: e = Expr(); break;
                        case MpRegExpId.ID: e = Expr(); break;
                        case MpRegExpId.LPAREN: e = Expr(); break;
                        case MpRegExpId.SUB: e = Expr(); break;
                        case MpRegExpId.NOT: e = Expr(); break;
                        case MpRegExpId.INTVAL: e = Expr(); break;
                        case MpRegExpId.STRVAL: e = StrVal(); break;
                        default: jj_la1[21] = jj_gen; jj_consume_token(MpRegExpId.UNDEFINED); throw new MiniParseException();
                    }
                    break;
                case MpRegExpId.LPAREN:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.FALSE: e = Expr(); break;
                        case MpRegExpId.THIS: e = Expr(); break;
                        case MpRegExpId.TRUE: e = Expr(); break;
                        case MpRegExpId.ID: e = Expr(); break;
                        case MpRegExpId.LPAREN: e = Expr(); break;
                        case MpRegExpId.SUB: e = Expr(); break;
                        case MpRegExpId.NOT: e = Expr(); break;
                        case MpRegExpId.INTVAL: e = Expr(); break;
                        case MpRegExpId.STRVAL: e = StrVal(); break;
                        default: jj_la1[21] = jj_gen; jj_consume_token(MpRegExpId.UNDEFINED); throw new MiniParseException();
                    }
                    break;
                case MpRegExpId.SUB:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.FALSE: e = Expr(); break;
                        case MpRegExpId.THIS: e = Expr(); break;
                        case MpRegExpId.TRUE: e = Expr(); break;
                        case MpRegExpId.ID: e = Expr(); break;
                        case MpRegExpId.LPAREN: e = Expr(); break;
                        case MpRegExpId.SUB: e = Expr(); break;
                        case MpRegExpId.NOT: e = Expr(); break;
                        case MpRegExpId.INTVAL: e = Expr(); break;
                        case MpRegExpId.STRVAL: e = StrVal(); break;
                        default: jj_la1[21] = jj_gen; jj_consume_token(MpRegExpId.UNDEFINED); throw new MiniParseException();
                    }
                    break;
                case MpRegExpId.NOT:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.FALSE: e = Expr(); break;
                        case MpRegExpId.THIS: e = Expr(); break;
                        case MpRegExpId.TRUE: e = Expr(); break;
                        case MpRegExpId.ID: e = Expr(); break;
                        case MpRegExpId.LPAREN: e = Expr(); break;
                        case MpRegExpId.SUB: e = Expr(); break;
                        case MpRegExpId.NOT: e = Expr(); break;
                        case MpRegExpId.INTVAL: e = Expr(); break;
                        case MpRegExpId.STRVAL: e = StrVal(); break;
                        default: jj_la1[21] = jj_gen; jj_consume_token(MpRegExpId.UNDEFINED); throw new MiniParseException();
                    }
                    break;
                case MpRegExpId.INTVAL:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.FALSE: e = Expr(); break;
                        case MpRegExpId.THIS: e = Expr(); break;
                        case MpRegExpId.TRUE: e = Expr(); break;
                        case MpRegExpId.ID: e = Expr(); break;
                        case MpRegExpId.LPAREN: e = Expr(); break;
                        case MpRegExpId.SUB: e = Expr(); break;
                        case MpRegExpId.NOT: e = Expr(); break;
                        case MpRegExpId.INTVAL: e = Expr(); break;
                        case MpRegExpId.STRVAL: e = StrVal(); break;
                        default: jj_la1[21] = jj_gen; jj_consume_token(MpRegExpId.UNDEFINED); throw new MiniParseException();
                    }
                    break;
                case MpRegExpId.STRVAL:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.FALSE: e = Expr(); break;
                        case MpRegExpId.THIS: e = Expr(); break;
                        case MpRegExpId.TRUE: e = Expr(); break;
                        case MpRegExpId.ID: e = Expr(); break;
                        case MpRegExpId.LPAREN: e = Expr(); break;
                        case MpRegExpId.SUB: e = Expr(); break;
                        case MpRegExpId.NOT: e = Expr(); break;
                        case MpRegExpId.INTVAL: e = Expr(); break;
                        case MpRegExpId.STRVAL: e = StrVal(); break;
                        default: jj_la1[21] = jj_gen; jj_consume_token(MpRegExpId.UNDEFINED); throw new MiniParseException();
                    }
                    break;

                default:
                    jj_la1[22] = jj_gen;
                    break;
            }
            jj_consume_token(MpRegExpId.RPAREN);
            jj_consume_token(MpRegExpId.SEMICOLON);
            return new AstPrint( e );
        }

        // --------------------------------------------------------------------------
        // Args -> Expr {"," Expr}
        // --------------------------------------------------------------------------

        static /* final */ public AstExpList Args() /* throws ParseException */
        {
            AstExp e = null;
            AstExpList el = new AstExpList();
            e = Expr();
            el.Add( e );

            /* label_12: */
            bool done = false;
            while (!done)
            {
                switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case MpRegExpId.COMMA:
                        break;
                    default:
                        jj_la1[23] = jj_gen;
                        done = true;
                        break /* label_12 */;
                }
                if (!done)
                {
                    jj_consume_token(MpRegExpId.COMMA);
                    e = Expr();
                    el.Add(e);
                }
            }
            return el;
        }

        // --------------------------------------------------------------------------
        // InitExpr -> "new" BasicType "[" <INTVAL> "]" 
        //           | "new" <ID> "(" [Args] ")"
        //           |  Expr 
        // --------------------------------------------------------------------------

        static /* final */ public AstExp InitExpr() /* throws ParseException */
        {
            DemiTasse.ast.AstType t = null;
            AstExp e = null;
            AstId id = null;
            AstExpList args = null;
            int[] i = new int[1];

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.NEW:
                    jj_consume_token(MpRegExpId.NEW);
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.BOOLEAN:

                            t = BasicType();
                            jj_consume_token(MpRegExpId.LBRACKET);
                            IntVal(i);
                            jj_consume_token(MpRegExpId.RBRACKET);
                            return new AstNewArray(t, i[0]);

                        case MpRegExpId.INT:

                            t = BasicType();
                            jj_consume_token(MpRegExpId.LBRACKET);
                            IntVal(i);
                            jj_consume_token(MpRegExpId.RBRACKET);
                            return new AstNewArray( t, i[0] );

                        case MpRegExpId.ID:

                            id = Id();
                            jj_consume_token(MpRegExpId.LPAREN);
                            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                            {
                                case MpRegExpId.FALSE: args = Args(); break;
                                case MpRegExpId.THIS: args = Args(); break;
                                case MpRegExpId.TRUE: args = Args(); break;
                                case MpRegExpId.ID: args = Args(); break;
                                case MpRegExpId.LPAREN: args = Args(); break;
                                case MpRegExpId.SUB: args = Args(); break;
                                case MpRegExpId.NOT: args = Args(); break;
                                case MpRegExpId.INTVAL: args = Args(); break;
                                default: jj_la1[24] = jj_gen; break;
                            }
                            jj_consume_token(MpRegExpId.RPAREN);
                            return new AstNewObj( id, args );

                        default:
                            jj_la1[25] = jj_gen;
                            jj_consume_token(MpRegExpId.UNDEFINED);
                            throw new MiniParseException();
                    }
                    //break;

                case MpRegExpId.FALSE: e = Expr(); return e;
                case MpRegExpId.THIS: e = Expr(); return e;
                case MpRegExpId.TRUE: e = Expr(); return e;
                case MpRegExpId.ID: e = Expr(); return e;
                case MpRegExpId.LPAREN: e = Expr(); return e;
                case MpRegExpId.SUB: e = Expr(); return e;
                case MpRegExpId.NOT: e = Expr(); return e;
                case MpRegExpId.INTVAL: e = Expr(); return e;

                default:
                    jj_la1[26] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
            }
            throw new Error("Missing return statement in function");
        }

        // ----------------------------------------------------------------
        // Expr -> AndExpr OrTail
        // ----------------------------------------------------------------

        static /* final */ public AstExp Expr() /* throws ParseException */
        {
            AstExp e1 = null;
            AstExp e2 = null;
            e1 = AndExpr();
            e2 = OrTail(e1);
            return e2 == null ? e1 : e2;
        }

        // ----------------------------------------------------------------
        // OrTail  ->  [ "||" AndExpr OrTail ]
        // ----------------------------------------------------------------
        
        static /* final */ public AstExp OrTail(AstExp e0) /* throws ParseException */
        {
            AstExp e1 = null;
            AstExp e2 = null;

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.OR:
                    jj_consume_token(MpRegExpId.OR);
                    e1 = AndExpr();
                    e1 = new AstBinop(DemiTasse.ast.AstBinop.OP.OR, e0, e1);
                    e2 = OrTail(e1);
                    break;
                default:
                    jj_la1[27] = jj_gen;
                    break;
            }
            return e2 == null ? e1 : e2;
        }

        // ----------------------------------------------------------------
        // AndExpr -> RelExpr AndTail
        // ----------------------------------------------------------------

        static /* final */ public AstExp AndExpr() /* throws ParseException */
        {
            AstExp e1 = null;
            AstExp e2 = null;
            e1 = RelExpr();
            e2 = AndTail(e1);
            return e2 == null ? e1 : e2;
        }

        // ----------------------------------------------------------------
        // AndTail  ->  [ "&&" RelExpr AndTail ]
        // ----------------------------------------------------------------

        static /* final */ public AstExp AndTail(AstExp e0) /* throws ParseException */
        {
            AstExp e1 = null;
            AstExp e2 = null;

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.AND:

                    jj_consume_token(MpRegExpId.AND);
                    e1 = RelExpr();
                    e1 = new AstBinop(DemiTasse.ast.AstBinop.OP.AND, e0, e1);
                    e2 = AndTail(e1);
                    break;

                default:

                    jj_la1[28] = jj_gen;
                    break;
            }
            return e2 == null ? e1 : e2;
        }

        // ----------------------------------------------------------------
        // RelExpr  ->  ArithExpr [ RelOp ArithExpr ]
        // ----------------------------------------------------------------

        static /* final */ public AstExp RelExpr() /* throws ParseException */
        {
            AstExp e1 = null;
            AstExp e2 = null;
            AstRelop.OP op = (AstRelop.OP)(-1);
            e1 = ArithExpr();

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.EQ:
                    op = Relop();
                    e2 = ArithExpr();
                    break;

                case MpRegExpId.NE:
                    op = Relop();
                    e2 = ArithExpr();
                    break;

                case MpRegExpId.LT:
                    op = Relop();
                    e2 = ArithExpr();
                    break;

                case MpRegExpId.LE:
                    op = Relop();
                    e2 = ArithExpr();
                    break;

                case MpRegExpId.GT:
                    op = Relop();
                    e2 = ArithExpr();
                    break;

                case MpRegExpId.GE:
                    op = Relop();
                    e2 = ArithExpr();
                    break;

                default:
                    jj_la1[29] = jj_gen;
                    break;
            }
            return e2 == null ? e1 : new AstRelop( op, e1, e2);
        }

        // ----------------------------------------------------------------
        // ArithExpr  ->  Term ArithTail
        // ----------------------------------------------------------------

        static /* final */ public AstExp ArithExpr() /* throws ParseException */
        {
            AstExp e1 = null;
            AstExp e2 = null;
            e1 = Term();
            e2 = ArithTail(e1);
            return e2 == null ? e1 : e2;
        }

        // ----------------------------------------------------------------
        // ArithTail  ->  [ ( "+" | "-" ) Term ArithTail ]
        // ----------------------------------------------------------------

        static /* final */ public AstExp ArithTail(AstExp e0) /* throws ParseException */
        {
            AstExp e1 = null;
            AstExp e2 = null;
            DemiTasse.ast.AstBinop.OP op;

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.ADD:
                
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.ADD:
                            jj_consume_token(MpRegExpId.ADD);
                            op = DemiTasse.ast.AstBinop.OP.ADD;
                            break;
                        case MpRegExpId.SUB:
                            jj_consume_token(MpRegExpId.SUB);
                            op = DemiTasse.ast.AstBinop.OP.SUB;
                            break;
                        default:
                            jj_la1[30] = jj_gen;
                            jj_consume_token(MpRegExpId.UNDEFINED);
                            throw new MiniParseException();
                    }
                    e1 = Term();
                    e1 = new AstBinop(op, e0, e1);
                    e2 = ArithTail(e1);
                    break;

                case MpRegExpId.SUB:

                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.ADD:
                            jj_consume_token(MpRegExpId.ADD);
                            op = DemiTasse.ast.AstBinop.OP.ADD;
                            break;
                        case MpRegExpId.SUB:
                            jj_consume_token(MpRegExpId.SUB);
                            op = DemiTasse.ast.AstBinop.OP.SUB;
                            break;
                        default:
                            jj_la1[30] = jj_gen;
                            jj_consume_token(MpRegExpId.UNDEFINED);
                            throw new MiniParseException();
                    }
                    e1 = Term();
                    e1 = new AstBinop( op, e0, e1 );
                    e2 = ArithTail(e1);
                    break;

                default:
                jj_la1[31] = jj_gen;
                break;
            }
            return e2 == null ? e1 : e2;
        }

        // ----------------------------------------------------------------
        // Term  ->  Factor TermTail
        // ----------------------------------------------------------------
        
        static /* final */ public AstExp Term() /* throws ParseException */
        {
            AstExp e1 = null;
            AstExp e2 = null;
            e1 = Factor();
            e2 = TermTail(e1);
            return e2 == null ? e1 : e2;
        }

        // ----------------------------------------------------------------
        // TermTail  -> ("*" | "/" ) Factor TermTail
        // ----------------------------------------------------------------

        static /* final */ public AstExp TermTail(AstExp e0) /* throws ParseException */
        {
            AstExp e1 = null;
            AstExp e2 = null;
            DemiTasse.ast.AstBinop.OP op;

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.MUL:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.MUL:
                            jj_consume_token(MpRegExpId.MUL);
                            op = DemiTasse.ast.AstBinop.OP.MUL;
                            break;

                        case MpRegExpId.DIV:
                            jj_consume_token(MpRegExpId.DIV);
                            op = DemiTasse.ast.AstBinop.OP.DIV;
                            break;

                        default:
                            jj_la1[32] = jj_gen;
                            jj_consume_token(MpRegExpId.UNDEFINED);
                            throw new MiniParseException();
                    }
                    e1 = Factor();
                    e1 = new AstBinop(op, e0, e1);
                    e2 = TermTail(e1);
                    break;

                case MpRegExpId.DIV:
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.MUL:
                            jj_consume_token(MpRegExpId.MUL);
                            op = DemiTasse.ast.AstBinop.OP.MUL;
                            break;

                        case MpRegExpId.DIV:
                            jj_consume_token(MpRegExpId.DIV);
                            op = DemiTasse.ast.AstBinop.OP.DIV;
                            break;

                        default:
                            jj_la1[32] = jj_gen;
                            jj_consume_token(MpRegExpId.UNDEFINED);
                            throw new MiniParseException();
                    }
                    e1 = Factor();
                    e1 = new AstBinop( op, e0, e1 );
                    e2 = TermTail(e1);
                    break;

                default:
                    jj_la1[33] = jj_gen;
                    break;
            }
            return e2 == null ? e1 : e2;
        }

        // ----------------------------------------------------------------
        // Factor  ->  [ "-" | "!" ] Factor
        //             | Lvalue [ "." "length" "(" ")" | ["(" Args ")]" ]
        //             | "(" Expr ")"
        //             | Literal
        // ----------------------------------------------------------------
        static /* final */ public AstExp Factor() /* throws ParseException */
        {
            AstExp e = null;
            AstExp factor = null;
            bool neg = false;
            bool not = false;
            AstExpList args = null;
            bool isCall = false;

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.SUB:

                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.SUB:
                            jj_consume_token(MpRegExpId.SUB);
                            neg = true;
                            break;
                        case MpRegExpId.NOT:
                            jj_consume_token(MpRegExpId.NOT);
                            not = true;
                            break;
                        default:
                            jj_la1[34] = jj_gen;
                            jj_consume_token(MpRegExpId.UNDEFINED);
                            throw new MiniParseException();
                    }
                    factor = Factor();
                    if (neg)
                        return new AstUnop(AstUnop.OP.NEG, factor);
                    else if (not)
                        return new AstUnop(AstUnop.OP.NOT, factor);
                    else
                        return factor;

                case MpRegExpId.NOT:
                    
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.SUB:
                            jj_consume_token(MpRegExpId.SUB);
                            neg = true;
                            break;
                        case MpRegExpId.NOT:
                            jj_consume_token(MpRegExpId.NOT);
                            not = true;
                            break;
                        default:
                            jj_la1[34] = jj_gen;
                            jj_consume_token(MpRegExpId.UNDEFINED);
                            throw new MiniParseException();
                    }
                    factor = Factor();
                    if ( neg )
                        return new AstUnop( AstUnop.OP.NEG, factor );
                    else if ( not )
                        return new AstUnop( AstUnop.OP.NOT, factor );
                    else
                        return factor;

                case MpRegExpId.THIS:
                    e = Lvalue();
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.LPAREN:
                            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                            {
                                case MpRegExpId.DOT:

                                    jj_consume_token(MpRegExpId.DOT);
                                    jj_consume_token(MpRegExpId.LENGTH);
                                    jj_consume_token(MpRegExpId.LPAREN);
                                    jj_consume_token(MpRegExpId.RPAREN);
                                    e = new AstArrayLen(e);
                                    break;

                                case MpRegExpId.LPAREN:

                                    jj_consume_token(MpRegExpId.LPAREN);
                                    isCall = true;
                                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                                    {
                                        case MpRegExpId.FALSE: args = Args(); break;
                                        case MpRegExpId.THIS: args = Args(); break;
                                        case MpRegExpId.TRUE: args = Args(); break;
                                        case MpRegExpId.ID: args = Args(); break;
                                        case MpRegExpId.LPAREN: args = Args(); break;
                                        case MpRegExpId.SUB: args = Args(); break;
                                        case MpRegExpId.NOT: args = Args(); break;
                                        case MpRegExpId.INTVAL: args = Args(); break;
                                        default: jj_la1[35] = jj_gen; break;
                                    }
                                    jj_consume_token(MpRegExpId.RPAREN);
                                    break;
                                default:
                                    jj_la1[36] = jj_gen;
                                    jj_consume_token(MpRegExpId.UNDEFINED);
                                    throw new MiniParseException();
                            }
                            break;
                        case MpRegExpId.DOT:
                            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                            {
                                case MpRegExpId.DOT:

                                    jj_consume_token(MpRegExpId.DOT);
                                    jj_consume_token(MpRegExpId.LENGTH);
                                    jj_consume_token(MpRegExpId.LPAREN);
                                    jj_consume_token(MpRegExpId.RPAREN);
                                    e = new AstArrayLen(e);
                                    break;

                                case MpRegExpId.LPAREN:

                                    jj_consume_token(MpRegExpId.LPAREN);
                                    isCall = true;
                                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                                    {
                                        case MpRegExpId.FALSE: args = Args(); break;
                                        case MpRegExpId.THIS: args = Args(); break;
                                        case MpRegExpId.TRUE: args = Args(); break;
                                        case MpRegExpId.ID: args = Args(); break;
                                        case MpRegExpId.LPAREN: args = Args(); break;
                                        case MpRegExpId.SUB: args = Args(); break;
                                        case MpRegExpId.NOT: args = Args(); break;
                                        case MpRegExpId.INTVAL: args = Args(); break;
                                        default: jj_la1[35] = jj_gen; break;
                                    }
                                    jj_consume_token(MpRegExpId.RPAREN);
                                    break;
                                default:
                                    jj_la1[36] = jj_gen;
                                    jj_consume_token(MpRegExpId.UNDEFINED);
                                    throw new MiniParseException();
                            }
                            break;
                        default:
                            jj_la1[37] = jj_gen;
                            break;
                    }
                    if (isCall)
                    {
                        if (null != (e as AstId))
                            return new AstCall(new AstThis(), (AstId)e, args);
                        if (null != (e as AstField))
                            return new AstCall(((AstField)e).obj, ((AstField)e).var, args);
                        else
                            return null;
                    }
                    else
                    {
                        return e;
                    }

                case MpRegExpId.ID:
                    e = Lvalue();
                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                    {
                        case MpRegExpId.LPAREN:
                            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                            {
                                case MpRegExpId.DOT:

                                    jj_consume_token(MpRegExpId.DOT);
                                    jj_consume_token(MpRegExpId.LENGTH);
                                    jj_consume_token(MpRegExpId.LPAREN);
                                    jj_consume_token(MpRegExpId.RPAREN);
                                    e = new AstArrayLen(e);
                                    break;

                                case MpRegExpId.LPAREN:

                                    jj_consume_token(MpRegExpId.LPAREN);
                                    isCall = true;
                                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                                    {
                                        case MpRegExpId.FALSE: args = Args(); break;
                                        case MpRegExpId.THIS: args = Args(); break;
                                        case MpRegExpId.TRUE: args = Args(); break;
                                        case MpRegExpId.ID: args = Args(); break;
                                        case MpRegExpId.LPAREN: args = Args(); break;
                                        case MpRegExpId.SUB: args = Args(); break;
                                        case MpRegExpId.NOT: args = Args(); break;
                                        case MpRegExpId.INTVAL: args = Args(); break;
                                        default: jj_la1[35] = jj_gen; break;
                                    }
                                    jj_consume_token(MpRegExpId.RPAREN);
                                    break;
                                default:
                                    jj_la1[36] = jj_gen;
                                    jj_consume_token(MpRegExpId.UNDEFINED);
                                    throw new MiniParseException();
                            }
                            break;
                        case MpRegExpId.DOT:
                            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                            {
                                case MpRegExpId.DOT:

                                    jj_consume_token(MpRegExpId.DOT);
                                    jj_consume_token(MpRegExpId.LENGTH);
                                    jj_consume_token(MpRegExpId.LPAREN);
                                    jj_consume_token(MpRegExpId.RPAREN);
                                    e = new AstArrayLen( e );
                                    break;

                                case MpRegExpId.LPAREN:

                                    jj_consume_token(MpRegExpId.LPAREN);
                                    isCall = true;
                                    switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                                    {
                                        case MpRegExpId.FALSE: args = Args(); break;
                                        case MpRegExpId.THIS: args = Args(); break;
                                        case MpRegExpId.TRUE: args = Args(); break;
                                        case MpRegExpId.ID: args = Args(); break;
                                        case MpRegExpId.LPAREN: args = Args(); break;
                                        case MpRegExpId.SUB: args = Args(); break;
                                        case MpRegExpId.NOT: args = Args(); break;
                                        case MpRegExpId.INTVAL: args = Args(); break;
                                        default: jj_la1[35] = jj_gen; break;
                                    }
                                    jj_consume_token(MpRegExpId.RPAREN);
                                    break;
                                default:
                                    jj_la1[36] = jj_gen;
                                    jj_consume_token(MpRegExpId.UNDEFINED);
                                    throw new MiniParseException();
                            }
                            break;
                        default:
                            jj_la1[37] = jj_gen;
                            break;
                    }
                    if ( isCall )
                    {
                        if (null != (e as AstId))
                           return new AstCall( new AstThis(), (AstId)e, args );
                        if (null != (e as AstField))
                            return new AstCall( ((AstField)e).obj, ((AstField)e).var, args );
                        else
                            return null;
                    }
                    else
                    {
                        return e;
                    }

                case MpRegExpId.LPAREN:

                    jj_consume_token(MpRegExpId.LPAREN);
                    e = Expr();
                    jj_consume_token(MpRegExpId.RPAREN);
                    return e;

                case MpRegExpId.FALSE: e = Literal(); return e;
                case MpRegExpId.TRUE: e = Literal(); return e;
                case MpRegExpId.INTVAL: e = Literal(); return e;

                default:
                    jj_la1[38] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
            }
            throw new Error("Missing return statement in function");
        }

        // --------------------------------------------------------------------------
        // Lvalue   -> ["this" "."] <ID> {"." <ID>} ["[" Expr "]"]
        // --------------------------------------------------------------------------
        static /* final */ public AstExp Lvalue() /* throws ParseException */
        {
            AstExp id = null;
            AstExp ae = null;
            
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.THIS:
                    id = This();
                    jj_consume_token(MpRegExpId.DOT);
                    break;
                default:
                    jj_la1[39] = jj_gen;
                    break;
            }

            id = FieldId(id);

            /* label_13: */
            while (jj_2_3(2))
            {
                jj_consume_token(MpRegExpId.DOT);
                id = FieldId(id);
            }

            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.LBRACKET:
                    jj_consume_token(MpRegExpId.LBRACKET);
                    ae = Expr();
                    jj_consume_token(MpRegExpId.RBRACKET);
                    break;

                default:
                    jj_la1[40] = jj_gen;
                    break;
            }
            return ae == null ? id : new AstArrayElm( id, ae );
        }

        // --------------------------------------------------------------------------
        // Id  ->  <ID>
        // --------------------------------------------------------------------------

        static /* final */ public AstId Id() /* throws ParseException */ 
        {
            //    System.out.println("ID  ->");
            string id;
            jj_consume_token(MpRegExpId.ID);
            id = token.image;
            return new AstId( id );
        }

        // --------------------------------------------------------------------------
        // FieldId  ->  Id
        // --------------------------------------------------------------------------

        static /* final */ public AstExp FieldId(AstExp e) /* throws ParseException */
        {
            AstId id;
            id = Id();
            return e == null ? (AstExp)id : (AstExp)(new AstField(e, id));
        }

        // --------------------------------------------------------------------------
        // ArrayElm  ->  "[" Expr "]"
        // --------------------------------------------------------------------------
  
        static /* final */ public AstExp ArrayElm(AstExp pid) /* throws ParseException */
        {
            AstExp e;
            jj_consume_token(MpRegExpId.LBRACKET);
            e = Expr();
            jj_consume_token(MpRegExpId.RBRACKET);
            return new AstArrayElm( pid, e );
        }

        // --------------------------------------------------------------------------
        // This  -> <THIS>
        // --------------------------------------------------------------------------

        static /* final */ public AstExp This() /* throws ParseException */
        {
            jj_consume_token(MpRegExpId.THIS);
            return new AstThis();
        }

        // --------------------------------------------------------------------------
        // Literal  ->  IntVal | BoolVal
        // --------------------------------------------------------------------------
        
        static /* final */ public AstExp Literal() /* throws ParseException */
        {
            AstExp e;
            int[] i = new int[1];
            
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.INTVAL: e = IntVal(i); return e;
                case MpRegExpId.FALSE: e = BoolVal(); return e;
                case MpRegExpId.TRUE: e = BoolVal(); return e;

                default:
                    jj_la1[41] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
            }
            throw new Error("Missing return statement in function");
        }

        // --------------------------------------------------------------------------
        // <BOOLVAL>  -> <TRUE> | <FALSE>
        // --------------------------------------------------------------------------
        
        static /* final */ public AstBoolVal BoolVal() /* throws ParseException */
        {
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.TRUE: jj_consume_token(MpRegExpId.TRUE); return new AstBoolVal( true );
                case MpRegExpId.FALSE: jj_consume_token(MpRegExpId.FALSE); return new AstBoolVal( false );
                default:
                    jj_la1[42] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
            }
            throw new Error("Missing return statement in function");
        }

        // --------------------------------------------------------------------------
        // IntVal  -> <INTVAL>
        // --------------------------------------------------------------------------

        static /* final */ public AstIntVal IntVal(int[] i) /* throws ParseException */
        {
            string s;
            jj_consume_token(MpRegExpId.INTVAL);
            s = token.image;
            i[0] = int.Parse(s);
            return new AstIntVal( i[0] );
        }

        // --------------------------------------------------------------------------
        // StrVal  ->  <STRVAL>
        // --------------------------------------------------------------------------
  
        static /* final */ public AstStrVal StrVal() /* throws ParseException */
        {
            string s;
            jj_consume_token(MpRegExpId.STRVAL);
            s = token.image;
            //s = s.Substring(1, s.Length - 1);
            s = s.Substring(1, s.Length - 2);
            return new AstStrVal(s);
        }

        // --------------------------------------------------------------------------
        // RelOp  ->  <EQ> | <NE> | <LT> | <LE> | <GT> | <GE>
        // --------------------------------------------------------------------------
        
        static /* final */ public AstRelop.OP Relop() /* throws ParseException */ 
        {
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk) 
            {
                case MpRegExpId.EQ:
                    jj_consume_token(MpRegExpId.EQ);
                    return ast.AstRelop.OP.EQ;

                case MpRegExpId.NE:
                    jj_consume_token(MpRegExpId.NE);
                    return ast.AstRelop.OP.NE;

                case MpRegExpId.LT:
                    jj_consume_token(MpRegExpId.LT);
                    return ast.AstRelop.OP.LT;

                case MpRegExpId.LE:
                    jj_consume_token(MpRegExpId.LE);
                    return ast.AstRelop.OP.LE;

                case MpRegExpId.GT:
                    jj_consume_token(MpRegExpId.GT);
                    return ast.AstRelop.OP.GT;

                case MpRegExpId.GE:
                    jj_consume_token(MpRegExpId.GE);
                    return ast.AstRelop.OP.GE;

                default:
                    jj_la1[43] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
            }
            throw new Error("Missing return statement in function");
        }

        // --------------------------------------------------------------------------
        // Binop  ->  <ADD> | <SUB> | <MUL> | <DIV> | <AND> | <OR>
        // --------------------------------------------------------------------------

        static /* final */ public AstBinop Binop(AstExp e1, AstExp e2) /* throws ParseException */
        {
            ast.AstBinop.OP op;
            switch ((jj_ntk == MpRegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case MpRegExpId.ADD:
                    jj_consume_token(MpRegExpId.ADD);
                    op = ast.AstBinop.OP.ADD;
                    break;

                case MpRegExpId.SUB:
                    jj_consume_token(MpRegExpId.SUB);
                    op = ast.AstBinop.OP.SUB;
                    break;

                case MpRegExpId.MUL:
                    jj_consume_token(MpRegExpId.MUL);
                    op = ast.AstBinop.OP.MUL;
                    break;

                case MpRegExpId.DIV:
                    jj_consume_token(MpRegExpId.DIV);
                    op = ast.AstBinop.OP.DIV;
                    break;

                case MpRegExpId.AND:
                    jj_consume_token(MpRegExpId.AND);
                    op = ast.AstBinop.OP.AND;
                    break;

                case MpRegExpId.OR:
                    jj_consume_token(MpRegExpId.OR);
                    op = ast.AstBinop.OP.OR;
                    return new AstBinop( op, e1, e2 );

                default:
                    jj_la1[44] = jj_gen;
                    jj_consume_token(MpRegExpId.UNDEFINED);
                    throw new MiniParseException();
            }
            throw new Error("Missing return statement in function");
        }

        static private bool jj_2_1(int xla)
        {
            jj_la = xla; jj_lastpos = jj_scanpos = token;
            try { return !jj_3_1(); }
            catch(LookaheadSuccess ls) { return true; }
            finally { jj_save(0, xla); }
        }

        static private bool jj_2_2(int xla)
        {
            jj_la = xla; jj_lastpos = jj_scanpos = token;
            try
            { 
                return !jj_3_2(); 
            }
            catch(LookaheadSuccess ls) 
            { 
                return true; 
            }
            finally
            { 
                jj_save(1, xla); 
            }
        }

        static private bool jj_2_3(int xla)
        {
            jj_la = xla; jj_lastpos = jj_scanpos = token;
            try { return !jj_3_3(); }
            catch(LookaheadSuccess ls) { return true; }
            finally { jj_save(2, xla); }
        }

        static private bool jj_3R_16()
        {
            MiniToken xsp;
            xsp = jj_scanpos;
            if (jj_3R_18()) {
            jj_scanpos = xsp;
            if (jj_3R_19()) return true;
            }
            return false;
        }

        static private bool jj_3R_18()
        {
            if (jj_3R_20()) return true;
            MiniToken xsp;
            xsp = jj_scanpos;
            if (jj_3R_21()) jj_scanpos = xsp;
            return false;
        }

        static private bool jj_3R_15()
        {
            if (jj_3R_17()) return true;
                return false;
        }

        static private bool jj_3R_21()
        {
            if (jj_scan_token(MpRegExpId.LBRACKET)) return true;
                return false;
        }

        static private bool jj_3R_22()
        {
            if (jj_scan_token(MpRegExpId.INT)) return true;
                return false;
        }

        static private bool jj_3R_20() 
        {
            MiniToken xsp;
            xsp = jj_scanpos;
            if (jj_3R_22()) {
            jj_scanpos = xsp;
            if (jj_3R_23()) return true;
            }
            return false;
        }

        static private bool jj_3_1() 
        {
            if (jj_3R_14()) return true;
                return false;
        }

        static private bool jj_3_3() 
        {
            if (jj_scan_token(MpRegExpId.DOT)) return true;
            if (jj_3R_15()) return true;
                return false;
        }

        static private bool jj_3R_17() 
        {
            if (jj_scan_token(MpRegExpId.ID)) return true;
                return false;
        }

        static private bool jj_3R_19() 
        {
            if (jj_3R_17()) return true;
                return false;
        }

        static private bool jj_3R_23() 
        {
            if (jj_scan_token(MpRegExpId.BOOLEAN)) return true;
                return false;
        }

        static private bool jj_3_2() 
        {
            if (jj_3R_14()) return true;
                return false;
        }

        static private bool jj_3R_14() 
        {
            if (jj_3R_16()) return true;
            if (jj_3R_17()) return true;
            return false;
        }

        static private bool jj_initialized_once = false;
        /** Generated Token Manager. */
        static public MiniParserTokenManager token_source;
        static SimpleCharStream jj_input_stream = null;
        /** Current token. */
        static public MiniToken token;
        /** Next token. */
        static public MiniToken jj_nt;
        static private MpRegExpId jj_ntk;
        static private MiniToken jj_scanpos, jj_lastpos;
        static private int jj_la;
        static private int jj_gen;
        static readonly private int[] jj_la1 = new int[45];
        static private int[] jj_la1_0;
        static private int[] jj_la1_1;
        static MiniParser()
        {
            jj_la1_init_0();
            jj_la1_init_1();
        }

        private static void jj_la1_init_0()
        {
            jj_la1_0 = new int[] 
            {
                0x80,0x2001040,0x10000,0x200,0x2001040,0x10000,0x43320800,0x2801040,0x2001040,
                0x43320800,0x0,0x0,0x10000000,0x2001040,0x1040,0x43320800,0x43320800,0x6600400,
                0x6600400,0x4000000,0x100,0x6600400,0x6600400,0x0,0x6600400,0x2001040,0x6608400,
                0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x6600400,0x4000000,0x4000000,0x6600400,0x200000,
                0x10000000,0x400400,0x400400,0x0,0x0,
            };
        }

        private static void jj_la1_init_1()
        {
            jj_la1_1 = new int[] 
            {
                0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x1,0x2,0x0,0x0,0x0,0x0,0x0,0x80440,0x80440,
                0x2,0x0,0x180440,0x180440,0x1,0x80440,0x0,0x80440,0x8,0x10,0x1f800,0x60,0x60,0x180,
                0x180,0x440,0x80440,0x200,0x200,0x80440,0x0,0x0,0x80000,0x0,0x1f800,0x1f8,
            };
        }

        static readonly private JJCalls[] jj_2_rtns = new JJCalls[3];
        static private bool jj_rescan = false;
        static private int jj_gc = 0;

        /** Constructor with InputStream. */
        public MiniParser(Stream stream)
            : this(stream, Encoding.ASCII)
        {
        }

        /** Constructor with InputStream and supplied encoding */
        public MiniParser(Stream stream, Encoding encoding)
        {
            if (jj_initialized_once)
            {
                ReInit(stream, encoding);
                // System.out.println("ERROR: Second call to constructor of static parser.  ");
                // System.out.println("       You must either use ReInit() or set the JavaCC option STATIC to false");
                // System.out.println("       during parser generation.");
                // throw new Error();
            }
            else
            {
                jj_initialized_once = true;

                try
                {
                    jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1);
                }
                catch (UnsupportedEncodingException e)
                {
                    throw new RuntimeException(e);
                }
                token_source = new MiniParserTokenManager(jj_input_stream);
            }

            token = new MiniToken();
            jj_ntk = MpRegExpId.UNDEFINED;
            jj_gen = 0;
            for (int i = 0; i < 45; i++) jj_la1[i] = -1;
            for (int i = 0; i < jj_2_rtns.Length; i++) jj_2_rtns[i] = new JJCalls();
        }

        /** Reinitialise. */
        static public void ReInit(Stream stream)
        {
            ReInit(stream, Encoding.ASCII);
        }

        /** Reinitialise. */
        static public void ReInit(Stream stream, Encoding encoding)
        {
            try
            { 
                jj_input_stream.ReInit(stream, encoding, 1, 1);
            } 
            catch(UnsupportedEncodingException e)
            { 
                throw new RuntimeException(e);
            }

            MiniParserTokenManager.ReInit(jj_input_stream);
            token = new MiniToken();
            jj_ntk = MpRegExpId.UNDEFINED;
            jj_gen = 0;
            for (int i = 0; i < 45; i++) jj_la1[i] = -1;
            for (int i = 0; i < jj_2_rtns.Length; i++) jj_2_rtns[i] = new JJCalls();
        }

#if false
        /** Constructor. */
        public miniParser(java.io.Reader stream)
        {
            if (jj_initialized_once)
            {
                // System.out.println("ERROR: Second call to constructor of static parser. ");
                // System.out.println("       You must either use ReInit() or set the JavaCC option STATIC to false");
                // System.out.println("       during parser generation.");
                // throw new Error();
            }
            jj_initialized_once = true;
            jj_input_stream = new SimpleCharStream(stream, 1, 1);
            token_source = new miniParserTokenManager(jj_input_stream);
            token = new PsrToken();
            jj_ntk = -1;
            jj_gen = 0;
            for (int i = 0; i < 45; i++) jj_la1[i] = -1;
            for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
        }

        /** Reinitialise. */
        static public void ReInit(java.io.Reader stream) {
        jj_input_stream.ReInit(stream, 1, 1);
        token_source.ReInit(jj_input_stream);
        token = new PsrToken();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 45; i++) jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
        }

        /** Constructor with generated Token Manager. */
        public miniParser(miniParserTokenManager tm) {
        if (jj_initialized_once) {
        System.out.println("ERROR: Second call to constructor of static parser. ");
        System.out.println("       You must either use ReInit() or set the JavaCC option STATIC to false");
        System.out.println("       during parser generation.");
        throw new Error();
        }
        jj_initialized_once = true;
        token_source = tm;
        token = new PsrToken();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 45; i++) jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
        }

        /** Reinitialise. */
        public void ReInit(miniParserTokenManager tm) {
        token_source = tm;
        token = new PsrToken();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 45; i++) jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
        }
#endif

        static private MiniToken jj_consume_token(MpRegExpId kind) /* throws ParseException */
        {
            MiniToken oldToken;

            if ((oldToken = token).next != null) 
                token = token.next;
            else
                token = token.next = MiniParserTokenManager.getNextToken();
            
            jj_ntk = MpRegExpId.UNDEFINED;
            
            if (token.kind == kind)
            {
                jj_gen++;
                if (++jj_gc > 100)
                {
                    jj_gc = 0;
                    for (int i = 0; i < jj_2_rtns.Length; i++) 
                    {
                        JJCalls c = jj_2_rtns[i];
                        while (c != null) 
                        {
                            if (c.gen < jj_gen) c.first = null;
                            c = c.next;
                        }
                    }
                }
                return token;
            }
            token = oldToken;
            jj_kind = kind;
            throw generateParseException();
        }

        /* static*/ private /* final */ class LookaheadSuccess : Error
        { 
        }

        static readonly private LookaheadSuccess jj_ls = new LookaheadSuccess();

        static private bool jj_scan_token(MpRegExpId kind)
        {
            if (jj_scanpos == jj_lastpos)
            {
                jj_la--;
                if (jj_scanpos.next == null)
                {
                    jj_lastpos = jj_scanpos = jj_scanpos.next = MiniParserTokenManager.getNextToken();
                } 
                else 
                {
                    jj_lastpos = jj_scanpos = jj_scanpos.next;
                }
            } 
            else 
            {
                jj_scanpos = jj_scanpos.next;
            }
            if (jj_rescan)
            {
                int i = 0; MiniToken tok = token;
                while (tok != null && tok != jj_scanpos)
                { 
                    i++; tok = tok.next; 
                }
                if (tok != null) 
                    jj_add_error_token(kind, i);
            }
            if (jj_scanpos.kind != kind) return true;
            if (jj_la == 0 && jj_scanpos == jj_lastpos) throw jj_ls;
            return false;
        }

        /** Get the next Token. */
        static /* final */ public MiniToken getNextToken()
        {
            if (token.next != null)
                token = token.next;
            else
                token = token.next = MiniParserTokenManager.getNextToken();
            jj_ntk = MpRegExpId.UNDEFINED;
            jj_gen++;
            return token;
        }

        /** Get the specific Token. */
        static /* final */ public MiniToken getToken(int index)
        {
            MiniToken t = token;
            for (int i = 0; i < index; i++)
            {
                if (t.next != null)
                    t = t.next;
                else
                    t = t.next = MiniParserTokenManager.getNextToken();
            }
            return t;
        }

        static private MpRegExpId jj_ntk_fn()
        {
            if ((jj_nt = token.next) == null)
                return (jj_ntk = (token.next = MiniParserTokenManager.getNextToken()).kind);
            else
                return (jj_ntk = jj_nt.kind);
        }

        static private List<int[]> jj_expentries = new List<int[]>();
        static private int[] jj_expentry;
        static private MpRegExpId jj_kind = MpRegExpId.UNDEFINED;
        static private int[] jj_lasttokens = new int[100];
        static private int jj_endpos;

        static private void jj_add_error_token(MpRegExpId kind, int pos)
        {
            if (pos >= 100)
                return;

            if (pos == jj_endpos + 1)
            {
                jj_lasttokens[jj_endpos++] = (int)kind;
            } 
            else if (jj_endpos != 0) 
            {
                jj_expentry = new int[jj_endpos];
                for (int i = 0; i < jj_endpos; i++) 
                {
                    jj_expentry[i] = jj_lasttokens[i];
                }
                
                //jj_entries_loop:

                /* for (List<int[]>.Enumerator it = jj_expentries.GetEnumerator(); it.hasNext(); )  */
                foreach (int[] oldentry in jj_expentries)
                {
                    /* int[] oldentry = (int[])(it.Current()); it.MoveNext(); */

                    if (oldentry.Length == jj_expentry.Length)
                    {
                        bool found = false;

                        for (int i = 0; (i < jj_expentry.Length) && !found; i++)
                        {
                            if (oldentry[i] != jj_expentry[i])
                            {
                                /* continue jj_entries_loop; */
                                found = true;
                            }
                        }

                        if (!found)
                        {
                            jj_expentries.Add(jj_expentry);
                            break /* jj_entries_loop */;
                        }
                    }
                }

                if (pos != 0) 
                    jj_lasttokens[(jj_endpos = pos) - 1] = (int) kind;
            }
        }

        /** Generate ParseException. */
        static public MiniParseException generateParseException()
        {
            jj_expentries.Clear();
            bool[] la1tokens = new bool[53];
            if (jj_kind >= 0) 
            {
                la1tokens[(int)jj_kind] = true;
                jj_kind = MpRegExpId.UNDEFINED;
            }
            for (int i = 0; i < 45; i++) 
            {
                if (jj_la1[i] == jj_gen) 
                {
                    for (int j = 0; j < 32; j++) 
                    {
                        if ((jj_la1_0[i] & (1<<j)) != 0) 
                        {
                            la1tokens[j] = true;
                        }
                        if ((jj_la1_1[i] & (1<<j)) != 0) 
                        {
                            la1tokens[32+j] = true;
                        }
                    }
                }
            }
            for (int i = 0; i < 53; i++) {
                if (la1tokens[i]) {
                    jj_expentry = new int[1];
                    jj_expentry[0] = i;
                jj_expentries.Add(jj_expentry);
                }
            }
            jj_endpos = 0;
            jj_rescan_token();
            jj_add_error_token(0, 0);
            int[][] exptokseq = new int[jj_expentries.Count()][];
            for (int i = 0; i < jj_expentries.Count(); i++)
            {
                exptokseq[i] = jj_expentries[i];
            }
            return new MiniParseException(token, exptokseq, tokenImage);
        }

        /** Enable tracing. */
        static /* final */ public void enable_tracing() 
        {
        }

        /** Disable tracing. */
        static /* final */ public void disable_tracing() 
        {
        }

        static private void jj_rescan_token()
        {
            jj_rescan = true;
            for (int i = 0; i < 3; i++)
            {
                try 
                {
                    JJCalls p = jj_2_rtns[i];
                    do
                    {
                        if (p.gen > jj_gen)
                        {
                            jj_la = p.arg; jj_lastpos = jj_scanpos = p.first;
                            switch (i)
                            {
                                case 0: jj_3_1(); break;
                                case 1: jj_3_2(); break;
                                case 2: jj_3_3(); break;
                            }
                    }
                    p = p.next;
                    } while (p != null);
                } 
                catch(LookaheadSuccess ls) 
                { 
                }
            }
            jj_rescan = false;
        }

        static private void jj_save(int index, int xla)
        {
            JJCalls p = jj_2_rtns[index];
            while (p.gen > jj_gen)
            {
                if (p.next == null)
                {
                    p = p.next = new JJCalls();
                    break;
                }
                p = p.next;
            }
            p.gen = jj_gen + xla - jj_la;
            p.first = token; p.arg = xla;
        }

        /*static*/ /* final */ public class JJCalls
        {
            public int gen;
            public MiniToken first;
            public int arg;
            public JJCalls next;
        }
    }
}

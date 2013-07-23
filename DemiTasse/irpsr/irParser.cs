// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: irParser.cs
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

using DemiTasse.ir;
using DemiTasse.psrutil;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.irpsr
{
    public class irParser : irParserConstants
    {
        // *************************************************************************
        // * Static
        // *************************************************************************

        public static FUNC FUNC() /* throws ParseException */ 
        {
            IrToken t; int vc, tc, ac; STMTlist sl=new STMTlist(); STMT s;
            t = jj_consume_token(RegExpId.ID);
            jj_consume_token(RegExpId.kw32);
            jj_consume_token(RegExpId.kwVARCNT);
            jj_consume_token(RegExpId.kw33);
            vc = INT();
            jj_consume_token(RegExpId.kw34);
            jj_consume_token(RegExpId.kwTMPCNT);
            jj_consume_token(RegExpId.kw33);
            tc = INT();
            jj_consume_token(RegExpId.kw34);
            jj_consume_token(RegExpId.kwARGCNT);
            jj_consume_token(RegExpId.kw33);
            ac = INT();
            jj_consume_token(RegExpId.kw35);
            jj_consume_token(RegExpId.kw36);

            // label_2:
            while (true) 
            {
                switch ((jj_ntk == RegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case RegExpId.kw44:
                        break;

                    default:
                        jj_la1[1] = jj_gen;
                        goto label_2a; // break label_2;
                }

                s = STMT();
                sl.add(s);
            }

            label_2a:

            jj_consume_token(RegExpId.kw37);
            {if (true) return new FUNC(t.image,vc,tc,ac,sl);}
            throw new Error("Missing return statement in function");
            }

        public static CJUMP.OP relopCode() /* throws ParseException */
            {
                CJUMP.OP n;
                switch ((jj_ntk == RegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case RegExpId.kw38:
                        jj_consume_token(RegExpId.kw38);
                        n = ir.CJUMP.OP.EQ;
                        break;

                    case RegExpId.kw39:
                        jj_consume_token(RegExpId.kw39);
                        n = ir.CJUMP.OP.NE;
                        break;

                    case RegExpId.kw40:
                        jj_consume_token(RegExpId.kw40);
                        n = ir.CJUMP.OP.LT;
                        break;

                    case RegExpId.kw41:
                        jj_consume_token(RegExpId.kw41);
                        n = ir.CJUMP.OP.LE;
                        break;

                    case RegExpId.kw42:
                        jj_consume_token(RegExpId.kw42);
                        n = ir.CJUMP.OP.GT;
                        break;

                    case RegExpId.kw43:
                        jj_consume_token(RegExpId.kw43);
                        n = ir.CJUMP.OP.GE;
                        break;

                    default:
                        jj_la1[2] = jj_gen;
                        jj_consume_token(RegExpId.UNDEFINED);
                        throw new irParseException();
                }
                {
                    if (true) return n;
                }
                //throw new Error("Missing return statement in function");
                throw new Exception("Missing return statement in function");
            }

        public static STMT STMT() /* throws ParseException */
        {
            IrToken t; 
            CJUMP.OP n; 
            STMT s; 
            EXP e1 = null, e2, e3;
            EXPlist el=new EXPlist();

            jj_consume_token(RegExpId.kw44);
            switch ((jj_ntk == RegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case RegExpId.kwMOVE:

                    jj_consume_token(RegExpId.kwMOVE);
                    e1 = EXP();
                    e2 = EXP();
                    s = new MOVE(e1,e2);
                    break;

                case RegExpId.kwJUMP:

                    jj_consume_token(RegExpId.kwJUMP);
                    e1 = EXP();
                    s = new JUMP((NAME)e1);
                    break;

                case RegExpId.kwCJUMP:

                    jj_consume_token(RegExpId.kwCJUMP);
                    n = relopCode();
                    e1 = EXP();
                    e2 = EXP();
                    e3 = EXP();
                    s = new CJUMP(n,e1,e2,(NAME)e3);
                    break;

                case RegExpId.kwLABEL:
                    
                    jj_consume_token(RegExpId.kwLABEL);
                    t = jj_consume_token(RegExpId.ID);
                    s = new LABEL(t.image);
                    break;

                case RegExpId.kwCALLST:
                    
                    jj_consume_token(RegExpId.kwCALLST);
                    e1 = EXP();
                    jj_consume_token(RegExpId.kw32);

                    RegExpId tmp2;
                    //label_3:
                    while (true) 
                    {
                        tmp2 = (jj_ntk == RegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk;
                        switch (tmp2)
                        {
                            case RegExpId.kw32:

                                break;
                            
                            default:
                                jj_la1[3] = jj_gen;
                                goto label_3a; // break label_3;
                        }
                        e2 = EXP();
                        el.add(e2);
                    }

                    label_3a:

                    jj_consume_token(RegExpId.kw35);
                    s = new CALLST((NAME)e1,el);
                    break;

                case RegExpId.kwRETURN:

                    jj_consume_token(RegExpId.kwRETURN);
                    switch ((jj_ntk == RegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk) 
                    {
                        case RegExpId.kw32:
                            e1 = EXP();
                            break;
                        default:
                            jj_la1[4] = jj_gen;
                            break;
                    }
                    s = new RETURN(e1);
                    break;

                    default:
                        jj_la1[5] = jj_gen;
                        jj_consume_token(RegExpId.UNDEFINED);
                        throw new irParseException();
                }

            jj_consume_token(RegExpId.kw45);
                return s;

            //throw new Error("Missing return statement in function");
            //return s;
            }

        public static BINOP.OP binopCode() /* throws ParseException */
        {
            BINOP.OP n;
            switch ((jj_ntk == RegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case RegExpId.kw46:

                    jj_consume_token(RegExpId.kw46);
                    n = ir.BINOP.OP.ADD;
                    break;

                    case RegExpId.kw47:

                        jj_consume_token(RegExpId.kw47);
                        n = ir.BINOP.OP.SUB;
                        break;

                    case RegExpId.kw48:

                        jj_consume_token(RegExpId.kw48);
                        n = ir.BINOP.OP.MUL;
                        break;

                    case RegExpId.kw49:

                        jj_consume_token(RegExpId.kw49);
                        n = ir.BINOP.OP.DIV;
                        break;

                    case RegExpId.kw50:

                        jj_consume_token(RegExpId.kw50);
                        n = ir.BINOP.OP.AND;
                        break;

                    case RegExpId.kw51:

                        jj_consume_token(RegExpId.kw51);
                        n = ir.BINOP.OP.OR;
                        break;

                    default:
                        jj_la1[6] = jj_gen;
                        jj_consume_token(RegExpId.UNDEFINED);
                        throw new irParseException();
                    }
            #if false
                    {if (true) return n;}
                    throw new Error("Missing return statement in function");
            #endif
            return n;
        }

        public static EXP EXP() /* throws ParseException */
        {
            IrToken t; 
            int n; 
            String str;
            STMT s; 
            EXP e, e2; 
            EXPlist el = new EXPlist();
            jj_consume_token(RegExpId.kw32);

            switch ((jj_ntk == RegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
            {
                case RegExpId.kwESEQ:

                    jj_consume_token(RegExpId.kwESEQ);
                    s = STMT();
                    e = EXP();
                    e = new ESEQ(s,e);
                    break;

                case RegExpId.kwMEM:

                    jj_consume_token(RegExpId.kwMEM);
                    e = EXP();
                    e = new MEM(e);
                    break;

                case RegExpId.kwBINOP:

                    jj_consume_token(RegExpId.kwBINOP);
                    BINOP.OP op = binopCode();
                    e = EXP();
                    e2 = EXP();
                    e = new BINOP(op,e,e2);
                    break;

                case RegExpId.kwCALL:

                    jj_consume_token(RegExpId.kwCALL);
                    e = EXP();
                    jj_consume_token(RegExpId.kw32);
                    // label_4:
                    while (true)
                    {
                        switch ((jj_ntk == RegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                        {
                            case RegExpId.kw32:
                                break;

                            default:
                                jj_la1[7] = jj_gen;
                                goto label_4a; // break label_4;
                        }
                        e2 = EXP();
                        el.add(e2);
                    }
                    label_4a:

                    jj_consume_token(RegExpId.kw35);
                    e = new CALL((NAME)e,el);
                    break;

                case RegExpId.kwTEMP:

                    jj_consume_token(RegExpId.kwTEMP);
                    n = INT();
                    e = new TEMP(n);
                    break;

                case RegExpId.kwNAME:

                    jj_consume_token(RegExpId.kwNAME);
                    t = jj_consume_token(RegExpId.ID);
                    e = new NAME(t.image);
                    break;

                case RegExpId.kwFIELD:

                    jj_consume_token(RegExpId.kwFIELD);
                    e = EXP();
                    n = INT();
                    e = new FIELD(e,n);
                    break;

                case RegExpId.kwPARAM:

                    jj_consume_token(RegExpId.kwPARAM);
                    n = INT();
                    e = new PARAM(n);
                    break;

                case RegExpId.kwVAR:

                    jj_consume_token(RegExpId.kwVAR);
                    n = INT();
                    e = new VAR(n);
                    break;

                case RegExpId.kwCONST:

                    jj_consume_token(RegExpId.kwCONST);
                    n = INT();
                    e = new CONST(n);
                    break;

                case RegExpId.kwSTRING:

                    jj_consume_token(RegExpId.kwSTRING);
                    str = STR();
                    e = new STRING(str);
                    break;

                default:

                    jj_la1[8] = jj_gen;
                    jj_consume_token(RegExpId.UNDEFINED);
                    throw new irParseException();
            }

            jj_consume_token(RegExpId.kw35);
            {if (true) return e;}
            throw new Error("Missing return statement in function");
        }

        public static int INT() /* throws ParseException */
        {
            IrToken t;

            t = jj_consume_token(RegExpId.INTVAL);
            {if (true) return int.Parse(t.image);}
            throw new Error("Missing return statement in function");
        }

        public static String STR() /* throws ParseException */ 
        {
            IrToken t; 
            String s;

            t = jj_consume_token(RegExpId.STRVAL);
            s=t.image; {if (true) return s.Substring(1,s.Length-2);}
            throw new Error("Missing return statement in function");
        }

        private static bool jj_initialized_once = false;
      
        /** Generated Token Manager. */
        public static irParserTokenManager token_source = null;
        public static SimpleCharStream jj_input_stream = null;

        /** Current token. */
        public static IrToken token;

        /** Next token. */
        public static IrToken jj_nt;
        private static RegExpId jj_ntk;
        private static int jj_gen;
        private static readonly int[] jj_la1 = new int[9];
        private static int[] jj_la1_0;
        private static int[] jj_la1_1;
      
        static irParser()
        {
            jj_la1_init_0();
            jj_la1_init_1();
        }

        private static void jj_la1_init_0()
        {
            jj_la1_0 = new int[] {unchecked((int)0x80000000),0x0,0x0,0x0,0x0,0xfc00,0x0,0x0,0x7ff0000,};
        }
       
        private static void jj_la1_init_1()
        {
            jj_la1_1 = new int[] {0x0,0x1000,0xfc0,0x1,0x1,0x0,0xfc000,0x1,0x0,};
        }

        /** Reinitialise. */
        public static void ReInit(Stream stream)
        {
            ReInit(stream, Encoding.ASCII);
        }

        /** Reinitialise. */
        public static void ReInit(Stream stream, Encoding encoding)
        {
            try
            {
                jj_input_stream.ReInit(stream, encoding, 1, 1);
            }
            catch (UnsupportedEncodingException e)
            {
                throw new RuntimeException(e);
            }

            //token_source.ReInit(jj_input_stream);
            irParserTokenManager.ReInit(jj_input_stream);
            token = new IrToken();
            jj_ntk = RegExpId.UNDEFINED;
            jj_gen = 0;
            for (int i = 0; i < 9; i++) jj_la1[i] = -1;
        }

        /** Reinitialise. */
        public static void ReInit(/* java.io.Reader */ StreamReader streamReader)
        {
            jj_input_stream.ReInit(streamReader, 1, 1);
            //token_source.ReInit(jj_input_stream);
            irParserTokenManager.ReInit(jj_input_stream);
            token = new IrToken();
            jj_ntk = RegExpId.UNDEFINED;
            jj_gen = 0;
            for (int i = 0; i < 9; i++) jj_la1[i] = -1;
        }

        private static IrToken jj_consume_token(irParserConstants.RegExpId kind) /* throws ParseException */
        {
            IrToken oldToken;
            if ((oldToken = token).next != null)
                token = token.next;
            else
            {
                //token = token.next = token_source.getNextToken();
                token.next = irParserTokenManager.getNextToken();
                token = token.next;
            }
            jj_ntk = RegExpId.UNDEFINED;
            if (token.kind == kind)
            {
                jj_gen++;
                return token;
            }
            token = oldToken;
            jj_kind = kind;
            throw generateParseException();
        }

        /** Get the next Token. */
        public static IrToken getNextToken()
        {
            if (token.next != null)
                token = token.next;
            else
                //token = token.next = token_source.getNextToken();
                token = token.next = irParserTokenManager.getNextToken();
            jj_ntk = RegExpId.UNDEFINED;
            jj_gen++;
            return token;
        }

        /** Get the specific Token. */
        public static IrToken getToken(int index)
        {
            IrToken t = token;
            for (int i = 0; i < index; i++)
            {
                if (t.next != null)
                    t = t.next;
                else
                    //t = t.next = token_source.getNextToken();
                    t = t.next = irParserTokenManager.getNextToken();
            }
            return t;
        }

        private static RegExpId jj_ntk_fn()
        {
            if ((jj_nt = token.next) == null)
                //return (jj_ntk = (token.next = token_source.getNextToken()).kind);
                return (jj_ntk = (token.next = irParserTokenManager.getNextToken()).kind);
            else
                return (jj_ntk = jj_nt.kind);
        }

        //private static java.util.List<int[]> jj_expentries = new java.util.ArrayList<int[]>();
        private static List<int[]> jj_expentries = new List<int[]>();
        private static int[] jj_expentry;
        private static irParserConstants.RegExpId jj_kind = irParserConstants.RegExpId.UNDEFINED;

        /** Generate ParseException. */
        public static irParseException generateParseException()
        {
            jj_expentries.Clear();
            bool[] la1tokens = new bool[52];

            if (jj_kind >= 0)
            {
                la1tokens[(int)jj_kind] = true;
                jj_kind = irParserConstants.RegExpId.UNDEFINED;
            }

            for (int i = 0; i < 9; i++)
            {
                if (jj_la1[i] == jj_gen)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        if ((jj_la1_0[i] & (1 << j)) != 0)
                        {
                            la1tokens[j] = true;
                        }
                        if ((jj_la1_1[i] & (1 << j)) != 0)
                        {
                            la1tokens[32 + j] = true;
                        }
                    }
                }
            }

            for (int i = 0; i < 52; i++)
            {
                if (la1tokens[i])
                {
                    jj_expentry = new int[1];
                    jj_expentry[0] = i;
                    jj_expentries.Add(jj_expentry);
                }
            }

            int[][] exptokseq = new int[jj_expentries.Count()][];

            for (int i = 0; i < jj_expentries.Count(); i++)
            {
                exptokseq[i] = jj_expentries[i];
            }

            return new irParseException(token, exptokseq, tokenImage);
        }

        /** Enable tracing. */
        public static void enable_tracing()
        {
        }

        /** Disable tracing. */
        public static void disable_tracing()
        {
        }

        // *************************************************************************
        // * Instance
        // *************************************************************************

        /** Constructor with InputStream. */
        public irParser(Stream stream)
            : this(stream, Encoding.ASCII)
        {
            //this(stream, null);
        }

        /** Constructor with InputStream and supplied encoding */
        public irParser(Stream stream, Encoding encoding)
        {
            if (jj_initialized_once)
            {
                ReInit(stream, encoding);
                //Console.Out.WriteLine("ERROR: Second call to constructor of static parser.  ");
                //Console.Out.WriteLine("       You must either use ReInit() or set the JavaCC option STATIC to false");
                //Console.Out.WriteLine("       during parser generation.");
                //throw new Error();
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

                token_source = new irParserTokenManager(jj_input_stream);
            }

            token = new IrToken();
            jj_ntk = RegExpId.UNDEFINED;
            jj_gen = 0;
            for (int i = 0; i < 9; i++) jj_la1[i] = -1;
        }

#if false
        /** Constructor. */
        public irParser(/* java.io.Reader */ InputStreamReader stream)
        {
            if (jj_initialized_once)
            {
                Console.Out.WriteLine("ERROR: Second call to constructor of static parser. ");
                Console.Out.WriteLine("       You must either use ReInit() or set the JavaCC option STATIC to false");
                Console.Out.WriteLine("       during parser generation.");
                throw new Error();
            }
            jj_initialized_once = true;
            jj_input_stream = new SimpleCharStream(stream, 1, 1);
            token_source = new irParserTokenManager(jj_input_stream);
            token = new IrToken();
            jj_ntk = RegExpId.UNDEFINED;
            jj_gen = 0;
            for (int i = 0; i < 9; i++) jj_la1[i] = -1;
        }
#endif
#if false
        /** Constructor with generated Token Manager. */
        public irParser(irParserTokenManager tm)
        {
            if (jj_initialized_once)
            {
                Console.Out.WriteLine("ERROR: Second call to constructor of static parser. ");
                Console.Out.WriteLine("       You must either use ReInit() or set the JavaCC option STATIC to false");
                Console.Out.WriteLine("       during parser generation.");
                throw new Error();
            }
            jj_initialized_once = true;
            token_source = tm;
            token = new IrToken();
            jj_ntk = RegExpId.UNDEFINED;
            jj_gen = 0;
            for (int i = 0; i < 9; i++) jj_la1[i] = -1;
        }
#endif

#if false
        /** Reinitialise. */
        public void ReInit(irParserTokenManager tm)
        {
            token_source = tm;
            token = new IrToken();
            jj_ntk = RegExpId.UNDEFINED;
            jj_gen = 0;
            for (int i = 0; i < 9; i++) jj_la1[i] = -1;
        }
#endif

        public PROG Program()
        {
            FUNClist funcs = new FUNClist();
            FUNC seg;
            bool done = false;

            jj_consume_token(RegExpId.kwPROG);

            while (!done)
            {
                switch ((jj_ntk == RegExpId.UNDEFINED) ? jj_ntk_fn() : jj_ntk)
                {
                    case RegExpId.ID:

                        seg = FUNC();
                        funcs.Add(seg);
                        break;
                    
                    default:

                        jj_la1[0] = jj_gen;
                        done=true;
                        break;
                }
            }

            jj_consume_token(0);
            return new PROG(funcs);
        }
    }
}

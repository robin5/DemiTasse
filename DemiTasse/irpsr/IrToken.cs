﻿// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: IrToken.cs
// *
// * Description: Describes the input token stream.
// *
// * Author: Robin Murray
// *
// * Generated By: JavaCC - Do not edit this line. Token.java Version 5.0
// *     JavaCCOptions:TOKEN_EXTENDS=,KEEP_LINE_COL=null,SUPPORT_CLASS_VISIBILITY_PUBLIC=true
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
using System.Text;

using DemiTasse.psrutil;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.irpsr
{
    public class IrToken : Token /*: java.io.Serializable*/
    {
        /**
        * The version identifier for this Serializable class.
        * Increment only if the <i>serialized</i> form of the
        * class changes.
        */
        private static readonly long serialVersionUID = 1L;

        /**
        * An integer that describes the kind of this token.  This numbering
        * system is determined by JavaCCParser, and a table of these numbers is
        * stored in the file ...Constants.java.
        */
        public IrParserConstants.RegExpId kind;

        /**
        * A reference to the next regular (non-special) token from the input
        * stream.  If this is the last token from the input stream, or if the
        * token manager has not read tokens beyond this one, this field is
        * set to null.  This is true only if this token is also a regular
        * token.  Otherwise, see below for a description of the contents of
        * this field.
        */
        public IrToken next;

        /**
        * This field is used to access special tokens that occur prior to this
        * token, but after the immediately preceding regular (non-special) token.
        * If there are no such special tokens, this field is set to null.
        * When there are more than one such special token, this field refers
        * to the last of these special tokens, which in turn refers to the next
        * previous special token through its specialToken field, and so on
        * until the first special token (whose specialToken field is null).
        * The next fields of special tokens refer to other special tokens that
        * immediately follow it (without an intervening regular token).  If there
        * is no such token, this field is null.
        */
        public IrToken specialToken;

        /**
        * No-argument constructor
        */
        public IrToken()
        {
        }

        /**
        * Constructs a new token for the specified Image.
        */
        public IrToken(IrParserConstants.RegExpId kind)
            : this(kind, null)
        {
        }

        /**
        * Constructs a new token for the specified Image and Kind.
        */
        public IrToken(IrParserConstants.RegExpId kind, String image)
        {
            this.kind = kind;
            this.image = image;
        }

        /**
        * Returns a new Token object, by default. However, if you want, you
        * can create and return subclass objects based on the value of ofKind.
        * Simply add the cases to the switch for all those special cases.
        * For example, if you have a subclass of Token called IDToken that
        * you want to create if ofKind is ID, simply add something like :
        *
        *    case MyParserConstants.ID : return new IDToken(ofKind, image);
        *
        * to the following switch statement. Then you can cast matchedToken
        * variable to the appropriate type and use sit in your lexical actions.
        */
        public static IrToken newToken(IrParserConstants.RegExpId ofKind, String image)
        {
            switch (ofKind)
            {
                default: return new IrToken(ofKind, image);
            }
        }

        public static IrToken newToken(IrParserConstants.RegExpId ofKind)
        {
            return newToken(ofKind, null);
        }
    }
    /* JavaCC - OriginalChecksum=95ec07fd565ccea83cc17ac7fe5f1466 (do not edit this line) */
}

// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: IR.cs
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
using System.Diagnostics;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ir
{
    public abstract class IR
    {
        private static StringBuilder text = new StringBuilder();

        public static void DUMP(string s)
        {
            text.Append(s); 
        }
        
        public static void DUMP(STMT s)
        {
            if (s != null) s.dump(); else DUMP(" [null]\n");
        }
        
        public static void DUMP(EXP e)
        {
            if (e != null) e.dump(); else DUMP(" (null)");
        }

        public static string getIr()
        {
            return text.ToString();
        }

        public static void Clear()
        {
            text.Length = 0;
        }

        public abstract void dump();
    }
}

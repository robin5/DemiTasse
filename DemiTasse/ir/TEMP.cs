// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: TEMP.cs
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

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ir
{
    public class TEMP : EXP
    {
        private static int count=0;
        public int num;
  
        public TEMP() { num = ++count; }
        public TEMP(int n) { num=n; }
        public static void reset() { count = 0; }
        public static void reset(int cnt) { count = cnt; }
        public static int getCount() { return count; }

        public override void dump() { DUMP(" (TEMP " + num + ")"); }

        public override EXP accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }

}

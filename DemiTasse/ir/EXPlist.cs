// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: BINOP.cs
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
using System.IO;

// **********************************************************************************
// * Implementation
// **********************************************************************************

namespace DemiTasse.ir
{
    public class EXPlist : EXP
    {
        private List<EXP> list;

        public EXPlist() { list = new List<EXP>(); }
        public EXPlist(EXP e) { list = new List<EXP>(); list.Add(e); }

        public void add(EXP e) { list.Add(e); }
        public void addAll(EXPlist el) { list.AddRange(el.list); }
        public EXP elementAt(int i) { return list[i]; }
        public int size() { return list.Count(); }

        public override void dump() 
        {
            Console.Out.Write(" ("); 
            for (int i=0; i<size(); i++)
                DUMP(elementAt(i));
            Console.Out.Write(")"); 
        }

        public override EXP accept(IIrVI v) { return v.visit(this); }
        public override int accept(IIntVI v) { return v.visit(this); }
    }
}
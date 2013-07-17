// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: STMTlist.cs
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
    public class STMTlist : STMT
    {
        private List<STMT> list = null;

        public STMTlist()
        { 
            list = new List<STMT>();
        }

        public STMT elementAt(int i)
        { 
            return list[i]; 
        }
        
        public int size()
        { 
            return list.Count();
        }

        public void add(STMT s)
        { 
            if (s is STMTlist) 
            {
                STMTlist sl = (STMTlist) s;
                for (int i=0; i<sl.size(); i++)
                    list.Add(sl.elementAt(i));
            } 
            else 
            {
                list.Add(s); 
            }
        }

        public override void dump()
        {
            for (int i = 0; i < size(); i++)
            {
                DUMP(elementAt(i));
            }
        }

        public override STMT accept(IIrVI v)
        {
            return v.visit(this);
        }

        public override int accept(IIntVI v)
        {
            return v.visit(this);
        }
    }
}

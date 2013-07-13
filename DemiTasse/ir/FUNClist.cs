using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.ir
{
    public class FUNClist : List<FUNC>
    {
        public void dump() 
        { 
            for (int i=0; i< this.Count(); i++)
                this[i].dump();
        }

        public FUNClist accept(IIrVI v)
        { 
            return v.visit(this); 
        }

        public void accept(IIntVI v)
        { 
            v.visit(this); 
        }
    }
}

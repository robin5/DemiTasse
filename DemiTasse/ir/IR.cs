using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DemiTasse.ir
{
    public abstract class IR
    {
        public static void DUMP(String s) 
        {
            System_out_print(s); 
        }
        public static void DUMP(STMT s)
        {
            if (s != null) s.dump(); else DUMP(" [null]\n");
        }
        public static void DUMP(EXP e)
        {
            if (e != null) e.dump(); else DUMP(" (null)");
        }
        public abstract void dump();

        private static void System_out_print(String s) { Debug.Write(s); }
    }
}

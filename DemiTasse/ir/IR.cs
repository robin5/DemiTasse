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

        #region IrOut Event

        public delegate void IrOutEventHandler(object sender, IrOutEventArgs e);

        public static event IrOutEventHandler OnIrOutHandler;

        public static void AddOnIrOut(IrOutEventHandler handler)
        {
            OnIrOutHandler += handler;
        }

        public static void RemoveOnIrOut(IrOutEventHandler handler)
        {
            OnIrOutHandler -= handler;
        }

        protected static void OnIrOut(Object sender, IrOutEventArgs e)
        {
            if (OnIrOutHandler != null)
                OnIrOutHandler(sender, e);
        }

        #endregion IrOut Event

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

        public static void Dump(Object sender)
        {
            OnIrOut(sender, new IrOutEventArgs(text.ToString()));
        }

        public static void Clear()
        {
            text.Length = 0;
        }

        public abstract void dump();
    }
}

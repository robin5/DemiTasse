using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemiTasse.AppIDE
{
    public abstract class Command
    {
        public virtual void Execute(string txt)
        {
            Message();
        }
        public virtual void Execute()
        {
            Message();
        }

        private void Message()
        {
            MessageBox.Show("Not yet implemented.", "DemiTasse", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

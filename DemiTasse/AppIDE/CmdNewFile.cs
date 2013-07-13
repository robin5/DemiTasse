using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DemiTasse.AppIDE
{
    class CmdNewFile : Command
    {
        private IAppIDECommand _app = null;

        public CmdNewFile(IAppIDECommand app)
        {
            _app = app;
        }
        public override void Execute()
        {
            _app.NewFile();
        }
    }
}

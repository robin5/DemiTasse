using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DemiTasse.AppIDE
{
    class CmdAddFile : Command
    {
        private IAppIDECommand _app = null;

        public CmdAddFile(IAppIDECommand app)
        {
            _app = app;
        }

        public override void Execute()
        {
            _app.AddFile();
        }
    }
}

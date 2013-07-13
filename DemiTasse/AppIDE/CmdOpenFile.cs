using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemiTasse.AppIDE
{
    class CmdOpenFile : Command
    {
        private IAppIDECommand _app = null;

        public CmdOpenFile(IAppIDECommand app)
        {
            _app = app;
        }
        public override void Execute(string fileName)
        {
            _app.OpenFile(fileName);
        }
    }
}

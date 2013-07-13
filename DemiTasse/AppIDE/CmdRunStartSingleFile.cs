using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    class CmdRunStartSingleFile : Command
    {
        private IAppIDECommand _app = null;

        public CmdRunStartSingleFile(IAppIDECommand app)
        {
            _app = app;
        }
        public override void Execute(string fileName)
        {
            _app.RunStartSingleFile(fileName);
        }
    }
}

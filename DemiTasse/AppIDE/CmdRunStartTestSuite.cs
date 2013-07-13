using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    class CmdRunStartTestSuite : Command
    {
        private IAppIDECommand _app = null;

        public CmdRunStartTestSuite(IAppIDECommand app)
        {
            _app = app;
        }
        public override void Execute(string name)
        {
            _app.RunStartTestSuite(name);
        }
    }
}

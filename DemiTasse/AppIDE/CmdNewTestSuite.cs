using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemiTasse.AppIDE
{
    class CmdNewTestSuite : Command
    {
        private IAppIDECommand _app = null;

        public CmdNewTestSuite(IAppIDECommand app)
        {
            _app = app;
        }

        public override void Execute(string name)
        {
            _app.NewTestSuite(name);
        }
    }
}

// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: AppIDE.cs
// *
// * Description:  
// *
// *
// *
// **********************************************************************************
// *
// * Granting License: The MIT License (MIT)
// * 
// *   Permission is hereby granted, free of charge, to any person obtaining a copy
// *   of this software and associated documentation files (the "Software"), to deal
// *   in the Software without restriction, including without limitation the rights
// *   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// *   copies of the Software, and to permit persons to whom the Software is
// *   furnished to do so, subject to the following conditions:
// *   The above copyright notice and this permission notice shall be included in
// *   all copies or substantial portions of the Software.
// *   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// *   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// *   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// *   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// *   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// *   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// *   THE SOFTWARE.
// * 
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

using DemiTasse.ir;
using DemiTasse.interp;
using DemiTasse.irpsr;
using DemiTasse.ast;
using DemiTasse.astpsr;
using DemiTasse.symbol;
using DemiTasse.typechk;

namespace DemiTasse.AppIDE
{
    class AppIDE : IAppIDECommand
    {
        public AppIDE()
        {
            TestSuiteManager.Init("");
        }

        #region Event Definitions

        #region AppException Event
        public delegate void AppExceptionEventHandler(object sender, AppExceptionEventArgs e);

        public event AppExceptionEventHandler OnAppExceptionHandler;

        public void AddOnAppException(AppExceptionEventHandler handler)
        {
            OnAppExceptionHandler += handler;
        }

        public void RemoveOnAppException(AppExceptionEventHandler handler)
        {
            OnAppExceptionHandler -= handler;
        }

        protected void OnAppException(AppExceptionEventArgs e)
        {
            if (OnAppExceptionHandler != null)
                OnAppExceptionHandler(this, e);
        }
        #endregion AppException Event

        #region AppError Event
        public delegate void AppErrorEventHandler(object sender, AppErrorEventArgs e);

        public event AppErrorEventHandler OnAppErrorHandler;

        public void AddOnAppError(AppErrorEventHandler handler)
        {
            OnAppErrorHandler += handler;
        }

        public void RemoveOnAppError(AppErrorEventHandler handler)
        {
            OnAppErrorHandler -= handler;
        }

        protected void OnAppError(AppErrorEventArgs e)
        {
            if (OnAppErrorHandler != null)
                OnAppErrorHandler(this, e);
        }
        #endregion AppError Event

        #region OpenFile Event
        public delegate void OpenFileEventHandler(object sender, OpenFileEventArgs e);

        public event OpenFileEventHandler OnOpenFileHandler;

        public void AddOnOpenFile(OpenFileEventHandler handler)
        {
            OnOpenFileHandler += handler;
        }

        public void RemoveOnOpenFile(OpenFileEventHandler handler)
        {
            OnOpenFileHandler -= handler;
        }

        protected void OnOpenFile(OpenFileEventArgs e)
        {
            if (OnOpenFileHandler != null)
                OnOpenFileHandler(this, e);
        }
        #endregion OpenFile Event

        #region OpenTestSuiteFile Event
        public delegate void OpenTestSuiteFileEventHandler(object sender, OpenTestSuiteFileEventArgs e);

        public event OpenTestSuiteFileEventHandler OnOpenTestSuiteFileHandler;

        public void AddOnOpenTestSuiteFile(OpenTestSuiteFileEventHandler handler)
        {
            OnOpenTestSuiteFileHandler += handler;
        }

        public void RemoveOnOpenTestSuiteFile(OpenTestSuiteFileEventHandler handler)
        {
            OnOpenTestSuiteFileHandler -= handler;
        }

        protected void OnOpenTestSuiteFile(OpenTestSuiteFileEventArgs e)
        {
            if (OnOpenTestSuiteFileHandler != null)
                OnOpenTestSuiteFileHandler(this, e);
        }
        #endregion OpenTestSuiteFile Event

        #region OpenTestSuite Event
        
        public delegate void OpenTestSuiteEventHandler(object sender, OpenTestSuiteEventArgs e);

        public event OpenTestSuiteEventHandler OnOpenTestSuiteHandler;

        public void AddOnOpenTestSuite(OpenTestSuiteEventHandler handler)
        {
            OnOpenTestSuiteHandler += handler;
        }

        public void RemoveOnOpenTestSuite(OpenTestSuiteEventHandler handler)
        {
            OnOpenTestSuiteHandler -= handler;
        }

        protected void OnOpenTestSuite(OpenTestSuiteEventArgs e)
        {
            if (OnOpenTestSuiteHandler != null)
                OnOpenTestSuiteHandler(this, e);
        }
        
        #endregion OpenTestSuite Event

        #region SystemOut Event
        public delegate void SystemOutEventHandler(object sender, SystemOutEventArgs e);

        public event SystemOutEventHandler OnSystemOutHandler;

        public void AddOnSystemOut(SystemOutEventHandler handler)
        {
            OnSystemOutHandler += handler;
        }

        public void RemoveOnSystemOut(SystemOutEventHandler handler)
        {
            OnSystemOutHandler -= handler;
        }

        protected void OnSystemOut(SystemOutEventArgs e)
        {
            if (OnSystemOutHandler != null)
                OnSystemOutHandler(this, e);
        }
        #endregion SystemOut Event

        #endregion Event Definitions

        #region IAppIDECommand

        public void NewFile()
        {
            OpenFileEventArgs e = new OpenFileEventArgs("(untitled)", "");
            OnOpenFile(e);
        }
        
        public void NewTestSuite(string name)
        {
            try
            {
                OpenTestSuiteEventArgs e = new OpenTestSuiteEventArgs(TestSuiteManager.Create(name));
                OnOpenTestSuite(e);
            }
            catch (AppUserErrorException ex)
            {
                OnAppError(new AppErrorEventArgs(ex.Message));
            }
            catch (Exception ex)
            {
                OnAppException(new AppExceptionEventArgs(ex));
            }
        }

        public void OpenFile(string fileName)
        {
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(fileName, Encoding.ASCII);
                string code = sr.ReadToEnd();

                OpenFileEventArgs e = new OpenFileEventArgs(fileName, code);
                OnOpenFile(e);
            }
            catch (IOException ex)
            {
                OnAppError(new AppErrorEventArgs(ex.Message));
            }
            catch (OutOfMemoryException ex)
            {
                OnAppException(new AppExceptionEventArgs(ex));
            }
            catch (Exception ex)
            {
                OnAppException(new AppExceptionEventArgs(ex));
            }
            finally
            {
                if (sr != null)
                    sr.Close();
                    
            }
        }

        public void OpenTestSuite(string name)
        {
            try
            {
                OpenTestSuiteEventArgs e = new OpenTestSuiteEventArgs(TestSuiteManager.TestSuites[name]);
                OnOpenTestSuite(e);
            }
            catch (Exception ex)
            {
                OnAppError(new AppErrorEventArgs(ex.Message));
            }
        }

        public void OpenTestSuiteFile(string fileName)
        {
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(fileName, Encoding.ASCII);
                string code = sr.ReadToEnd();

                OpenTestSuiteFileEventArgs e = new OpenTestSuiteFileEventArgs(fileName, code);
                OnOpenTestSuiteFile(e);
            }
            catch (IOException ex)
            {
                OnAppError(new AppErrorEventArgs(ex.Message));
            }
            catch (OutOfMemoryException ex)
            {
                OnAppException(new AppExceptionEventArgs(ex));
            }
            catch (Exception ex)
            {
                OnAppException(new AppExceptionEventArgs(ex));
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        public void AddFile()
        {
            OnAppError(new AppErrorEventArgs("AddFile Not yet implemented."));
        }

        public void Close()
        {
            OnAppError(new AppErrorEventArgs("Close Not yet implemented."));
        }

        public void CloseTestSuite()
        {
            OnAppError(new AppErrorEventArgs("CloseTestSuite Not yet implemented."));
        }

        public void RunStartSingleFile(string fileName)
        {
            if (fileName.EndsWith(".ir", StringComparison.OrdinalIgnoreCase))
            {
                ExecuteIrTest(fileName);
            }
            else if (fileName.EndsWith(".ast", StringComparison.OrdinalIgnoreCase))
            {
                ExecuteAstTest(fileName);
            }
        }

        private void ExecuteIrTest(string fileName)
        {
            FileStream stream = null;
            PROG p = null;

            try
            {
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                p = (new irParser(stream)).Program();
            }
            catch (InterpException ex)
            {
                OnAppException(new AppExceptionEventArgs(ex));
            }
            catch (Exception ex)
            {
                OnAppException(new AppExceptionEventArgs(ex));
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            if (p != null)
            {
                try
                {
                    InterpVisitor iv = new InterpVisitor();
                    iv.AddOnSystemOut(Interpreter_OnSystemOut);
                    iv.visit(p);
                }
                catch (Exception ex)
                {
                    OnAppException(new AppExceptionEventArgs(ex));
                }
            }
        }

        public void ExecuteAstTest(string fileName)
        {
            try 
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                //DemiTasse.ast.Program p = (new astParser(stream)).Program();
                astParser psr = new astParser(stream);
                DemiTasse.ast.Program p = astParser.Program();

                stream.Close();
                SymbolVisitor sv = new SymbolVisitor();
                sv.visit(p);
                sv.symTable.show();
                TypeVisitor tv = new TypeVisitor(sv.symTable);
                tv.visit(p);
            }
            catch (TypeException ex) 
            {
                OnAppException(new AppExceptionEventArgs(ex));
            }
            catch (SymbolException ex)
            {
                OnAppException(new AppExceptionEventArgs(ex));
            }
            catch (Exception ex) 
            {
                OnAppException(new AppExceptionEventArgs(ex));
            }
        }

        public void ExecuteSymbolTest(string fileName)
        {
            try
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                //DemiTasse.ast.Program p = new astParser(stream).Program();
                astParser psr = new astParser(stream);
                DemiTasse.ast.Program p = astParser.Program();
                stream.Close();
                SymbolVisitor v = new SymbolVisitor();
                p.accept(v);
                v.symTable.show();
            }
            catch (SymbolException e)
            {
                Debug.WriteLine(e.ToString() + ": " + e.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString() + ": " + e.Message);
            }
        }

        public void RunPause()
        {
            OnAppError(new AppErrorEventArgs("RunPause Not yet implemented."));
        }

        public void RunContinue()
        {
            OnAppError(new AppErrorEventArgs("RunContinue Not yet implemented."));
        }

        public void RunStop()
        {
            OnAppError(new AppErrorEventArgs("RunStop Not yet implemented."));
        }

        #endregion IAppIDECommand

        public string[] TestSuiteNames
        {
            get 
            {
                string[] testSuiteNames = new string[TestSuiteManager.TestSuites.Count];

                TestSuiteManager.TestSuites.Keys.CopyTo(testSuiteNames, 0);

                return testSuiteNames;
            } 
        }

        private void Interpreter_OnSystemOut(object sender, SystemOutEventArgs e)
        {
            OnSystemOut(e);
        }
    }


}

// **********************************************************************************
// * Copyright (c) 2013 Robin Murray
// **********************************************************************************
// *
// * File: TestSuiteFileEntry.cs
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

namespace DemiTasse.AppIDE
{
    class TestSuiteFileEntry : TestSuiteEntry
    {
        public TestSuiteFileEntry(string fileName, string astRefFileName, string irRefFileName, string soRefFileName)
            : base((new FileInfo(fileName)).Name)
        {
            _fileName = fileName;
            _astRefFileName = astRefFileName;
            _irRefFileName = irRefFileName;
            _soRefFileName = soRefFileName;
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public string AstRefFileName
        {
            get { return _astRefFileName; }
        }

        public string IrRefFileName
        {
            get { return _irRefFileName; }
        }

        public string SystemOutRefFileName
        {
            get { return _soRefFileName; }
        }

        private string _fileName;
        private string _astRefFileName;
        private string _irRefFileName;
        private string _soRefFileName;
    }
}

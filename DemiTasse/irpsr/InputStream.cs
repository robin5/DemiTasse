using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DemiTasse.irpsr
{
    public class InputStream
    {
        private Stream stream = null;

        public InputStream()
        {
        }
        public InputStream(string fileName)
        {
            stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        public char readChar()
        {
            return (char) stream.ReadByte();
        }
        public void Close()
        {
            stream.Close();
        }
    }
}

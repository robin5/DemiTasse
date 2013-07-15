// Driver program for testing MINI AST parser v1.3. (Jingke Li)
//
using System;
using System.IO;
using DemiTasse.ast;

namespace DemiTasse.astpsr
{
    public class TestASTparser
    {
        public static void main(string[] args)
        {
            try
            {
                if (args.Length == 1)
                {
                    FileStream stream = new FileStream(args[0]);
                    Program p = new astParser(stream).Program();
                    stream.Close();
                    p.dump();
                }
                else
                {
                    throw new Exception("You must provide a parameter of one file name.");
                }
            }
            catch (Exception e)
            {
                System.err.println(e.toString());
            }
        }
    }
}
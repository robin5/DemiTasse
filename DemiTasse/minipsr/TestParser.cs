// Driver program for testing MINI parser v1.7. (Jingke Li)
//
package parser;
import ast.*;
import java.io.*;

public class TestParser {
  public static void main(String [] args) {
    try {
      if (args.length == 1) {
	FileInputStream stream = new FileInputStream(args[0]);
	Program p = new miniParser(stream).Program();
	stream.close();
	p.dump();
      } else {
	System.out.println("You must provide an input file name.");
      }
    } catch (Exception e) {
      System.err.println(e.toString());
    }
  }
}

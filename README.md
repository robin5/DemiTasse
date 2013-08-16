## Welcome to DemiTasse
Copyright © 2013 Robin Murray

DemiTasse is an integrated development environment for 
the [Mini-Java](http://www.cambridge.org/us/features/052182060X/index.html) 
programming language.  It has primitive features that allow
users to edit, compile, and execute Mini-Java programs within the IDE.  It also
also allows users to create, and execute suites of mini-java programs.  In future phases
of the project, DemiTasse will be more than just an IDE.  The hope is to develope a .NET control
and library which developers can use to provide scripting abilities to applications.


## Getting Started

DemiTasse is being developed with Visual Studio 2010.  No additioanl libraries are necessary outside
of those delivered with Visual Studio.  Use the GitHub tools to download the source code onto
your machine.  Once the project is downloaded, double-click the DemiTasse.sln solution file to open 
the DemiTasse project.

Mini-Java sample files can be found at the following web site: [http://www.cambridge.org/resources/052182060X/#programs](http://www.cambridge.org/resources/052182060X/#programs).
* [Factorial.java](http://www.cambridge.org/resources/052182060X/MCIIJ2e/programs/Factorial.java)
* [BinarySearch.java](http://www.cambridge.org/resources/052182060X/MCIIJ2e/programs/BinarySearch.java)
* [BubbleSort.java](http://www.cambridge.org/resources/052182060X/MCIIJ2e/programs/BubbleSort.java)
* [TreeVisitor.java](http://www.cambridge.org/resources/052182060X/MCIIJ2e/programs/TreeVisitor.java)
* [QuickSort.java](http://www.cambridge.org/resources/052182060X/MCIIJ2e/programs/QuickSort.java)
* [LinearSearch.java](http://www.cambridge.org/resources/052182060X/MCIIJ2e/programs/LinearSearch.java)
* [LinkedList.java](http://www.cambridge.org/resources/052182060X/MCIIJ2e/programs/LinkedList.java)
* [BinaryTree.java](http://www.cambridge.org/resources/052182060X/MCIIJ2e/programs/BinaryTree.java)

## Documents

User Instructions: [https://github.com/robin5/DemiTasse/blob/master/DemiTasse/doc/DemiTasse%20Instructions.pdf](https://github.com/robin5/DemiTasse/blob/master/DemiTasse/doc/DemiTasse%20Instructions.pdf)

## Code Status

The current software release is v1.0.0-rc.2: [https://github.com/robin5/DemiTasse/releases/tag/v1.0.0-rc.2](https://github.com/robin5/DemiTasse/releases/tag/v1.0.0-rc.2).

Other releases can be found here: [https://github.com/robin5/DemiTasse/releases](https://github.com/robin5/DemiTasse/releases)

## License

DemiTasse is released under the [MIT License](http://www.opensource.org/licenses/MIT).  

The MIT licesnse states the following:

   Permission is hereby granted, free of charge, to any person obtaining a copy
   of this software and associated documentation files (the "Software"), to deal
   in the Software without restriction, including without limitation the rights
   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
   copies of the Software, and to permit persons to whom the Software is
   furnished to do so, subject to the following conditions:
   The above copyright notice and this permission notice shall be included in
   all copies or substantial portions of the Software.
   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
   THE SOFTWARE.

## Issues / Enhancements

Please review issues list to view

* The Parsers generated by the [JavaCC](https://java.net/projects/javacc/) tool were defined as static classes, and they share common code.  I'm
sure a better OOD can be achieved for these classes.
* The compiling portions of the IDE code should be better encapsulated within a DLL. 
* Compiling should happen on a thread seperate from the UI thread.
* All MIni-Java source file editing functionality should be moved early into it's own control.
* Exchange test suite functionality for n-unit unit testing.

Additional Information can be found in the [Issues section...](https://github.com/robin5/DemiTasse/issues).

## Blue Sky

* Editor color coding of mini-java language.
* Auto-completion.

## Contact Information

For more information, contact Robin Murray [robin5@pdx.edu](email-to:robin5@pdx.edu)


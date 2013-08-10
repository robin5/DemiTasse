## Welcome to DemiTasse

The DemiTassi project will provide at first, an integrated development environment for 
the [Mini-Java](http://www.cambridge.org/us/features/052182060X/index.html) 
programming language.  It will have very primitive features that will allow
users to edit, compile, and execute Mini-Java programs within the IDE.  It will
also have the ability to assemble, and execute suite of mini-java programs.  But
DemiTasse will be more than just an IDE.  The hope is to develope a .NET control
and library which developers could use to provide scripting abilities to applications.


## Getting Started

DemiTasse is being developed with Visual Studio 2010.  No additioanl libraries are necessary outside
of those delivered with Visual Studio.  You can use any of the GitHub tools to create a directory
on your machine for installing the source code.  Once the project is downloaded, you simply
double-click the DemiTasse.sln solution file to open the DemiTasse project.

The tst directory contains mini-java test files that can be used to test the mini-java compiler's functionality.


## Contributing


## Code Status

The current source code has mostly been done to prove feasibility. It was first created by professor Jingke li
for the CS-321 and CS-322 compiler class sequence at Portlan tate University.  The parsers were generated by the 
JavaCC compiler tool. I have done the initial conversion to C#.  The code can execute successfully all of the 
files we used to test compiler functionality.  But there is still lots of work to be done (See below).

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

## To Do

1) The Parsers generated by the JavaCC tool were defined as static classes, and they share common code.  I'm
sure a better OOD can be achieved for these classes.

2) The compiling portions of the IDE code should be better encapsulated within a DLL. 

3) Compiling should happen on a thread seperate from the UI thread.

4) All MIni-Java source file editing functionality should be moved early into it's own control.


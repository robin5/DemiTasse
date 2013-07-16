// class vars
// (should print 1 1 2)
class test {
  public static void main(String[] a) {
    A x = new A();
    B y = new B();
    System.out.println(x.i);
    System.out.println(y.i);
    System.out.println(y.j);
  }
}

class A {
  int i=1;
}  

class B extends A {
  int j=2;
}  

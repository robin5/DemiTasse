// mutually dependent class decls
class test {
  public static void main(String[] a) {
    A x = new A();
    B y = new B();
  }
}

class A {
  B b;
}  

class B {
  A a;
}  

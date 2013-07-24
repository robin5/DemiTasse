// variable and method shared the same name
class test {
  public static void main(String[] a) {
    A x = new A();
    x.foo = x.foo(1);
    System.out.println(x.foo);
  }
}

class A {
  int foo;
  public int foo(int i) {
    return i;
  }
}

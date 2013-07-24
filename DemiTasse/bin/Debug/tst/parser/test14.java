// test method invocations
// (should print 2)
class test {
  public static void main(String[] a) {
    A a = new A();
    System.out.println(a.go());
  }
}

class A {
  public int back(int a) {
    return a;
  }

  public int go() {
    return this.back(2);
  }
}

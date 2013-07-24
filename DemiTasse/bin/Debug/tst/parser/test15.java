// test more invocations 
// (should print 2)
class test {
  public static void main(String[] a) {
    A a = new A();
    System.out.println(a.go());
  }
}

class B {
  public int back(int a) {
    return a;
  }
}

class A extends B {
  public int go() {
    return this.back(2);
  }
}

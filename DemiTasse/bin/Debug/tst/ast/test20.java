// test calls
// (should print 5 4 3 2 1 0)
class test {
  public static void main(String[] a) {
    A x;
    x = new A();
    System.out.println(x.go(5));
  }
}

class A {
  public int go(int n) {
    int i;
    i = 0;
    if (n>0) {
      System.out.println(n);
      i = this.back(n-1);
    }
    return i;
  }

  public int back(int n) {
    int i;
    i = this.go(n);
    return 0;
  }
}

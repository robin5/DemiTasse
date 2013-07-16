// class decls
// (should print 1 1)
class test {
  public static void main(String[] a) {
    body b = new body();
    int i = foo(1);
    int j = b.foo(1);
    System.out.println(i);
    System.out.println(j);
  }

  public int foo(int i) {
    int x;
    return i;
  }
}

class body {
  int i;

  public int foo(int i) {
    int y;
    return i;
  }
}  

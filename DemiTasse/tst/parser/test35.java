// test method calls
//
class test {
  public static void main(String[] a) {
    body b = new body();
    int i;
    i = b.foo(1,2);
    System.out.println(i);
  }
}

class body {
  public int foo(int x, int y) {
    return x + y;
  }
}

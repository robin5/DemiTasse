// test non-local vars
//
class test {
  public static void main(String[] a) {
    int i;
    body b;
    b = new body();
    i = b.foo();      
    System.out.println(i);
  }
}

class body {
  int x;
  public int foo() {
    x = 1;
    return x;
  }
}

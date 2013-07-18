// mutually dependent method decls
// (should print 0)
class test {
  public static void main(String[] a) {
    int i = foo(2);
    System.out.println(i);
  }

  public int foo(int i) {
    if (i>1) return bar();
    else return 3;
  }

  public int bar() {
    return foo(1);
  }
}

// method decls
// (should print 1 2)
class test {
  public static void main(String[] a) {
    int i;
    int j;
    i = foo(1);
    j = bar(1);
    System.out.println(i);
    System.out.println(j);
  }

  public int foo(int i) {
    int x;
    return i;
  }

  public int bar(int i) {
    int x=2;
    return x;
  }
}

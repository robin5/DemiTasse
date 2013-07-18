// param and local var decls
// (should print 1 3 6)
class test {
  public static void main(String[] a) {
    boolean b;
    int i;
    int j;
    b = true;
    i = foo(1,2);
    j = 2 * 3;
    System.out.println(b);
    System.out.println(i);
    System.out.println(j);
  }

  public int foo(int i, int j) {
    return i+j;
  }
}

// test simple arith
// (should print 1, 7)
class test {
  public static void main(String[] a) {
    boolean b = (1>2) || (3<4) && !false;
    int i = 2 + 2 * 4 - 9 / 3;
    System.out.println(b);
    System.out.println(i);
  }
}

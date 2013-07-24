// (should print 3 5)
class prog {
  public static void main(String[] args) {
    int[] a = new int[2];
    int i = 0;
    a[0] = 3;
    a[1] = 5;
    while (i<2) {
      System.out.println(a[i]);
      i = i+1;
    }
  }
}

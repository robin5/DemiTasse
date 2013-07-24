class testd24 {
  public static void main(String[] a) {
    int[] a = new int[5];
    int[] b;
    a[true] = 2;	// OK
    b = new int[true];  // error
  }
}

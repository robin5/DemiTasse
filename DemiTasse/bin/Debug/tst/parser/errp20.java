class testd20 {
  public static void main(String[] a) {
    int x;
    x = (1) || x();    // OK
    x = x && x.x();    // OK
    x = x.1;
  }
}

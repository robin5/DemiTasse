// array element can't be object
class testd15 {
  public static void main(String[] a) {
    a.b.c.d() = 1; // OK
    a[1].b = 2;
  }
}

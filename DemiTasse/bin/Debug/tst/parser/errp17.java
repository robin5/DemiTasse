// operator error
class testd17 {
  public static void main(String[] a) {
    x = a < b || c < d; // OK
    x = a < b < c;      // error
  }
}

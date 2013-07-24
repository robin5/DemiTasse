// operator error
class testd16 {
  public static void main(String[] a) {
    boolean b;
    a = b == 1+1; // OK
    c = d = 1+1;  // error
  }
}

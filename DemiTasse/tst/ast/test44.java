// test var init
// (should print 12)
class Body {
  int i=5;
  int j=i+2;
}
class test {
  public static void main(String[] a) {
    Body b = new Body();
    System.out.println(b.i + b.j);
  }
}

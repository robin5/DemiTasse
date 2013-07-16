// test method call and return value
// (should print 6)
class test {
  public static void main(String[] a) {
    Body b = new Body();
    System.out.println(b.go());
  }
}

class Body {
  int i;
  public int go() {
    int j;
    i = 4;
    j = i + 2;
    return j;
  }
}

// test method call
// (should print "Go!")
class test {
  public static void main(String[] a) {
    Body b = new Body();
    b.go();
  }
}

class Body {
  public void go() {
    System.out.println("Go!");
  }
}

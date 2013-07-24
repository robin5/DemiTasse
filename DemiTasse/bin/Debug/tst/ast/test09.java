// test parameter passing
// (should print 6)
class test {
  public static void main(String[] a) {
    Body b = new Body();
    System.out.println(b.go());
  }
}

class Body {
  public int go() {
    Body2 b2 = new Body2();
    return b2.value(1,2,3);
  }
}

class Body2 {
  public int value(int i, int j, int k) {
    return i+j+k;
  }
}


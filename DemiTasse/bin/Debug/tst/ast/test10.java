// test variable accesses
// (should print 5)
class test {
  public static void main(String[] a) {
    Body b = new Body();
    System.out.println(b.go());
  }
}

class Body {
  public int go() {
    Body2 b;
    b = new Body2();
    return b.value(true);
  }
}

class Body2 {
  int i;
  public int value(boolean cond) {
    int j;
    int k;
    i = 5;
    j = 6;
    if (cond)
      k = i;
    else
      k = j;
    return k;
  }
}


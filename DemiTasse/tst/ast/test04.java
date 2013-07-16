// test var decls
// (should print 5)
class test {
  public static void main(String[] a) {
    Body b = new Body();
    int i = 2;    
    b.i = 3;
    System.out.println(i + b.i);
  }
}

class Body {
  int i;
}

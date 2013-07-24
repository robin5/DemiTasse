// test relops
// (should print 1 6)
class test {
  public static void main(String[] a) {
    A a = new A();
    System.out.println(a.go());
  }
}

class A {
  public int go() {
    if (1<2) 
      System.out.println(1); 
    else { 
      if ((3*4)==10) 
	System.out.println(4); 
      else 
	System.out.println(5); 
    }
    return 6;	
  }
}

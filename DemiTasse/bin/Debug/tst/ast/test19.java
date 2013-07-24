// Testing precedence.
// (should print 1 17)
class test {
  public static void main(String[] a){
    A a = new A();
    System.out.println(a.go());
  }
}

class A {
  boolean b;
  int i;
  
  public int go() {
    int[] a = new int [4];
    b = 1 < 2 || 3 > 4 && 5 == 6 + 7 * 8 || ! true;
    i = 8 - 7 + 6 + 5 * a . length ( ) / 2;
    System.out.println(b);
    return i;
  } 
}

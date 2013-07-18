// variable, method, and class shared the same name
class test {
  public static void main(String[] a) {
    int A;
    A x = new A();
    x.A = x.A(1);
    A = x.A;
    System.out.println(A);
  }
}

class A {
  int A;
  public int A(int i) {
    return i;
  }
}

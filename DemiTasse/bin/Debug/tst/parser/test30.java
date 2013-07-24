// variable, method, and class shared the same name
class test {
  public static void main(String[] a) {
    A A = new A();
    A.A = A.A(2);
    System.out.println(A.A);
  }
}

class A {
  int A;
  public int A(int i) {
    return i;
  }
}

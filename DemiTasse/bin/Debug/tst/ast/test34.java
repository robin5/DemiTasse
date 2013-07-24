// test arrays
//
class test {
  public static void main(String[] dummy) {
    int[] a;
    int b;
    int i;
    a = new int[2];
    a[0] = 2;
    a[1] = 4;
    i = 0;
    b = a[i] + a[i+1];
    System.out.println(b);
  }
}

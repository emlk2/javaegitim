import java.util.Scanner;

public class EbobEkok {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("1. sayıyı giriniz: ");
        int n1 = input.nextInt();

        System.out.print("2. sayıyı giriniz: ");
        int n2 = input.nextInt();

        int ebob = 1;
        int i = n1;

        if (n2 < n1) {
            i = n2;
        }

        while (i >= 1) {
            if (n1 % i == 0 && n2 % i == 0) {
                ebob = i;
                break;
            }
            i--;
        }

        int ekok = (n1 * n2) / ebob;

        System.out.println("EBOB: " + ebob);
        System.out.println("EKOK: " + ekok);
    }
}

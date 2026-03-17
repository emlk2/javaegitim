import java.util.Scanner;

public class MukemmelSayi {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("Bir sayı giriniz: ");
        int n = input.nextInt();

        int sum = 0;

        for (int i = 1; i <= n / 2; i++) {
            if (n % i == 0) {
                sum += i;
            }
        }

        if (sum == n && n > 0) {
            System.out.println(n + " Mükemmel sayıdır.");
        } else {
            System.out.println(n + " Mükemmel sayı değildir.");
        }
    }
}

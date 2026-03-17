import java.util.Scanner;

public class MinMaxBulma {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("Kaç tane sayı gireceksiniz: ");
        int n = input.nextInt();

        int min = 0;
        int max = 0;

        for (int i = 1; i <= n; i++) {
            System.out.print(i + ". Sayıyı giriniz: ");
            int number = input.nextInt();

            if (i == 1) {
                min = number;
                max = number;
            } else {
                if (number > max) {
                    max = number;
                }
                if (number < min) {
                    min = number;
                }
            }
        }

        System.out.println("En büyük sayı: " + max);
        System.out.println("En küçük sayı: " + min);
    }
}

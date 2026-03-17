import java.util.Scanner;

public class BasamakToplami {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("Bir sayı giriniz: ");
        int number = input.nextInt();

        int tempNumber = number;
        int result = 0;

        while (tempNumber != 0) {
            result += tempNumber % 10;
            tempNumber /= 10;
        }

        System.out.println(number + " sayısının basamakları toplamı: " + result);
    }
}

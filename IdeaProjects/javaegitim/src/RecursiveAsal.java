import java.util.Scanner;

public class RecursiveAsal{

    static boolean isPrime(int num, int i) {
        if (num < 2) {
            return false;
        }
        if (num == 2 || i == 1) {
            return true;
        }
        if (num % i == 0) {
            return false;
        }
        return isPrime(num, i - 1);
    }

    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("Sayı Giriniz : ");
        int num = input.nextInt();

        if (isPrime(num, num / 2)) {
            System.out.println(num + " sayısı ASALDIR !");
        } else {
            System.out.println(num + " sayısı ASAL değildir !");
        }
    }
}

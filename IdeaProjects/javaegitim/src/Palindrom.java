import java.util.Scanner;

public class Palindrom {

    static boolean isPalindrom(int number) {
        int temp = number, reverseNumber = 0, lastNumber;

        while (temp != 0) {
            lastNumber = temp % 10;
            reverseNumber = (reverseNumber * 10) + lastNumber;
            temp /= 10;
        }

        return number == reverseNumber;
    }

    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("Bir sayı giriniz: ");
        int n = input.nextInt();

        if (isPalindrom(n)) {
            System.out.println(n + " bir Palindrom Sayıdır.");
        } else {
            System.out.println(n + " bir Palindrom Sayı değildir.");
        }
    }
}

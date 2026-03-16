import java.util.Scanner;

public class DeseneGöreMetot {

    static void printPattern(int n) {
        System.out.print(n + " ");

        if (n > 0) {
            printPattern(n - 5);
            System.out.print(n + " ");
        }
    }

    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("N Sayısı : ");
        int n = input.nextInt();

        System.out.print("Çıktısı : ");
        printPattern(n);
    }
}
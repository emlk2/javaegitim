
import java.util.Scanner;

public class Kombinasyon {

    static long factorial(int num) {
        long result = 1;
        for (int i = 1; i <= num; i++) {
            result *= i;
        }
        return result;
    }

    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("N eleman sayısını giriniz: ");
        int n = input.nextInt();

        System.out.print("R seçim sayısını giriniz: ");
        int r = input.nextInt();

        if (n >= r && n >= 0 && r >= 0) {
            long combination = factorial(n) / (factorial(r) * factorial(n - r));
            System.out.println("C(" + n + "," + r + ") = " + combination);
        } else {
            System.out.println("Hatalı giriş! N değeri R'den büyük veya eşit olmalı, sayılar negatif olmamalıdır.");
        }
    }
}
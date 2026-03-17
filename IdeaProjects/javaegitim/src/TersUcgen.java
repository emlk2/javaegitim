import java.util.Scanner;

public class TersUcgen {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        // Kullanıcıdan basamak sayısını alıyoruz
        System.out.print("Basamak Sayısı : ");
        int n = input.nextInt();

        // Dış döngü: Satır sayısını kontrol eder (n'den 1'e kadar azalır)
        for (int i = n; i >= 1; i--) {

            // 1. İç döngü: Boşlukları yazdırır
            for (int j = 1; j <= (n - i); j++) {
                System.out.print(" ");
            }

            // 2. İç döngü: Yıldızları (*) yazdırır
            // Yıldız sayısı formülü: (2 * i) - 1
            for (int k = 1; k <= (2 * i - 1); k++) {
                System.out.print("*");
            }

            // Bir alt satıra geçiş
            System.out.println();
        }
    }
}

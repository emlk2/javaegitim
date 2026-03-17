import java.util.Scanner;

public class TekSayiToplami {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);
        int sayi;
        int toplam = 0;

        do {
            System.out.print("Sayı giriniz: ");
            sayi = input.nextInt();

            if (sayi % 4 == 0) {
                toplam += sayi;
            }

        } while (sayi % 2 == 0);

        System.out.println("Toplam: " + toplam);
    }
}
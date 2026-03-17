import java.util.Scanner;

public class OrtalamaHesaplama {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("Bir sayı giriniz: ");
        int sayi = input.nextInt();

        int toplam = 0;
        int adet = 0;

        for (int i = 0; i <= sayi; i++) {
            if (i % 3 == 0 && i % 4 == 0 && i != 0) {
                toplam += i;
                adet++;
            }
        }

        if (adet > 0) {
            double ortalama = (double) toplam / adet;
            System.out.println("3 ve 4'e tam bölünen sayıların ortalaması: " + ortalama);
        } else {
            System.out.println("Belirttiğiniz aralıkta 3 ve 4'e tam bölünen bir sayı bulunamadı.");
        }
    }
}
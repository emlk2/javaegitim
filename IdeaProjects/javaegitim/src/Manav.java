 import java.util.Scanner;

public class Manav {
    public static void main(String[] args) {
        Scanner giris = new Scanner(System.in);

        // Meyve ve sebzelerin KG fiyatlarını (sabit değerler) tanımlıyoruz
        double armutFiyat = 2.14;
        double elmaFiyat = 3.67;
        double domatesFiyat = 1.11;
        double muzFiyat = 0.95;
        double patlicanFiyat = 5.00;

        // Kullanıcıdan KG bilgilerini alıyoruz
        System.out.print("Armut Kaç Kilo ? :");
        double armutKg = giris.nextDouble();

        System.out.print("Elma Kaç Kilo ? :");
        double elmaKg = giris.nextDouble();

        System.out.print("Domates Kaç Kilo ? :");
        double domatesKg = giris.nextDouble();

        System.out.print("Muz Kaç Kilo ? :");
        double muzKg = giris.nextDouble();

        System.out.print("Patlıcan Kaç Kilo ? :");
        double patlicanKg = giris.nextDouble();

        // Her bir ürünün tutarını hesaplayıp genel toplama ekliyoruz
        double toplamTutar = (armutKg * armutFiyat) +
                (elmaKg * elmaFiyat) +
                (domatesKg * domatesFiyat) +
                (muzKg * muzFiyat) +
                (patlicanKg * patlicanFiyat);

        // Sonucu ekrana bastırıyoruz
        System.out.println("Toplam Tutar : " + toplamTutar + " TL");
    }

}

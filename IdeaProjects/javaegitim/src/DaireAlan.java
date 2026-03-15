import java.util.Scanner;

public class DaireHesaplama {
    public static void main(String[] args) {
        Scanner giris = new Scanner(System.in);

        // Soruda verildiği gibi Pi sayısını sabit olarak tanımlıyoruz
        double pi = 3.14;

        System.out.print("Dairenin yarıçapını (r) giriniz: ");
        double r = giris.nextDouble();

        System.out.print("Merkez açısının ölçüsünü (𝛼) giriniz: ");
        double alpha = giris.nextDouble();

        // 1. Kısım: Normal Dairenin Çevresi ve Alanı
        double cevre = 2 * pi * r;
        double alan = pi * r * r;

        // 2. Kısım: Ödev (Daire Diliminin Alanı)
        // Formül: (pi * (r * r) * alpha) / 360
        double dilimAlani = (pi * (r * r) * alpha) / 360;

        // Sonuçları ekrana yazdırma
        System.out.println("Dairenin Çevresi: " + cevre);
        System.out.println("Dairenin Tüm Alanı: " + alan);
        System.out.println("Daire Diliminin Alanı: " + dilimAlani);
    }
}

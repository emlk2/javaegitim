import java.util.Arrays;
import java.util.Random;
import java.util.Scanner;

public class SayiTahmin {
    public static void main(String[] args) {
        // Rastgele sayı üretimi (0-99 arası)
        Random rand = new Random();
        int number = rand.nextInt(100);

        Scanner input = new Scanner(System.in);
        int right = 0; // Kullanıcın mevcut deneme sayısı
        int selected;
        int[] wrong = new int[5]; // Hatalı tahminleri tutacak dizi
        boolean isWin = false;
        boolean isWrong = false;

        // Test amaçlı gizli sayıyı görmek istersen aşağıdaki satırı aktif edebilirsin
        // System.out.println("Gizli Sayı: " + number);

        System.out.println("0 ile 100 arasında bir sayı tuttum. Bakalım bulabilecek misin?");

        while (right < 5) {
            System.out.print((right + 1) + ". Tahmininizi giriniz: ");
            selected = input.nextInt();

            // 1. Durum: Geçersiz aralık kontrolü
            if (selected < 0 || selected > 99) {
                System.out.println("Hata! Lütfen 0-100 arasında bir değer giriniz.");
                if (isWrong) {
                    right++;
                    System.out.println("Çok fazla hatalı giriş yaptınız. Kalan hak: " + (5 - right));
                } else {
                    isWrong = true;
                    System.out.println("Bir sonraki hatalı girişinizde hakkınızdan düşülecektir.");
                }
                continue; // Döngünün başına dön, aşağıdaki kodlara bakma
            }

            // 2. Durum: Tahmin doğru mu?
            if (selected == number) {
                System.out.println("Tebrikler, doğru tahmin! Gizli sayı: " + number);
                isWin = true;
                break;
            } else {
                // 3. Durum: Tahmin yanlışsa yönlendirme yap
                System.out.println("Hatalı bir sayı girdiniz!");
                if (selected > number) {
                    System.out.println(selected + " sayısı, gizli sayıdan büyüktür.");
                } else {
                    System.out.println(selected + " sayısı, gizli sayıdan küçüktür.");
                }

                wrong[right] = selected; // Yanlış tahmini diziye kaydet
                right++;
                System.out.println("Kalan hakkınız: " + (5 - right));
            }
            System.out.println("------------------------------------");
        }

        // Oyun bittiğinde kazanma/kaybetme kontrolü
        if (!isWin) {
            System.out.println("\nMaalesef kaybettiniz!");
            System.out.println("Gizli sayı şuydu: " + number);

            // Sadece girilen geçerli tahminleri göster (0 olmayanları)
            System.out.print("Tahminleriniz: ");
            for (int value : wrong) {
                if (value != 0) {
                    System.out.print(value + " ");
                }
            }
        }

        input.close(); // Scanner'ı kapatmak iyi bir alışkanlıktır
    }
}

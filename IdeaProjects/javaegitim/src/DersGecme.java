import java.util.Scanner;

public class DersGecme {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        int mat, fizik, turkce, kimya, muzik;
        int toplamNot = 0;
        int gecerliDersSayisi = 0;

        // Matematik
        System.out.print("Matematik notunuzu giriniz: ");
        mat = input.nextInt();
        if (mat >= 0 && mat <= 100) {
            toplamNot += mat;
            gecerliDersSayisi++;
        }

        // Fizik
        System.out.print("Fizik notunuzu giriniz: ");
        fizik = input.nextInt();
        if (fizik >= 0 && fizik <= 100) {
            toplamNot += fizik;
            gecerliDersSayisi++;
        }

        // Türkçe
        System.out.print("Türkçe notunuzu giriniz: ");
        turkce = input.nextInt();
        if (turkce >= 0 && turkce <= 100) {
            toplamNot += turkce;
            gecerliDersSayisi++;
        }

        // Kimya
        System.out.print("Kimya notunuzu giriniz: ");
        kimya = input.nextInt();
        if (kimya >= 0 && kimya <= 100) {
            toplamNot += kimya;
            gecerliDersSayisi++;
        }

        // Müzik
        System.out.print("Müzik notunuzu giriniz: ");
        muzik = input.nextInt();
        if (muzik >= 0 && muzik <= 100) {
            toplamNot += muzik;
            gecerliDersSayisi++;
        }

        // Eğer en az 1 tane geçerli not girildiyse ortalamayı hesapla
        if (gecerliDersSayisi > 0) {
            double ortalama = (double) toplamNot / gecerliDersSayisi;
            System.out.println("Ortalamanız : " + ortalama);

            if (ortalama >= 55) {
                System.out.println("Tebrikler, sınıfı geçtiniz!");
            } else {
                System.out.println("Sınıfta kaldınız, seneye tekrar görüşmek üzere.");
            }
        } else {
            System.out.println("Geçerli bir not girmediniz, ortalama hesaplanamadı.");
        }
    }
}
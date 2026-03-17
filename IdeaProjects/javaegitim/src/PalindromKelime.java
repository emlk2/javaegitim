import java.util.Scanner;

public class PalindromKelime {

    /**
     * 1. Yöntem: İki İşaretçi (Two Pointers) Mantığı
     * Kelimenin başından ve sonundan merkeze doğru kontrol eder.
     * En performanslı yöntemdir çünkü yeni bir String oluşturmaz.
     */
    static boolean isPalindrome(String str) {
        int i = 0;
        int j = str.length() - 1;

        while (i < j) {
            if (str.charAt(i) != str.charAt(j)) {
                return false; // Karakterler eşleşmediği anda false döner
            }
            i++;
            j--;
        }
        return true;
    }

    /**
     * 2. Yöntem: StringBuilder ile Ters Çevirme
     * Modern Java özelliklerini kullanarak kelimeyi ters çevirir ve karşılaştırır.
     */
    static boolean isPalindrome2(String str) {
        String reverse = new StringBuilder(str).reverse().toString();
        return str.equals(reverse);
    }

    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.println("=== Palindrom Kelime Bulucu ===");
        System.out.print("Bir kelime veya cümle giriniz: ");

        // Kullanıcıdan girişi alıyoruz
        String original = input.nextLine();

        // Programın hata yapmaması için:
        // 1. Tüm harfleri küçük yapıyoruz (toLowerCase)
        // 2. Boşlukları temizliyoruz (replace) -> "Ey Edip" gibi durumlar için
        String processed = original.toLowerCase().replace(" ", "");

        System.out.println("-------------------------------");

        if (isPalindrome(processed)) {
            System.out.println("Sonuç: '" + original + "' bir PALİNDROMİK ifadedir.");
        } else {
            System.out.println("Sonuç: '" + original + "' bir palindromik ifade değildir.");
        }

        System.out.println("-------------------------------");
        input.close();
    }
}
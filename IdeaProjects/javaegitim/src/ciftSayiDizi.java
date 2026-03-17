import java.util.Arrays;

public class ciftSayiDizi{
    // Dizide bir elemanın daha önce olup olmadığını kontrol eden yardımcı metod
    static boolean isFind(int[] arr, int value) {
        for (int i : arr) {
            if (i == value) {
                return true;
            }
        }
        return false;
    }

    public static void main(String[] args) {
        int[] list = {3, 7, 3, 3, 2, 9, 10, 21, 1, 33, 9, 1};
        // Ödev için çift sayılar içeren bir dizi:
        int[] numbers = {10, 20, 20, 10, 4, 34, 5, 4, 12, 10, 2, 2};

        int[] duplicateEvens = new int[numbers.length];
        int startIndex = 0;

        for (int i = 0; i < numbers.length; i++) {
            for (int j = 0; j < numbers.length; j++) {
                // Kendisi hariç (i != j) aynı değerde bir sayı var mı?
                // Ve bu sayı çift mi? (% 2 == 0)
                if ((i != j) && (numbers[i] == numbers[j]) && (numbers[i] % 2 == 0)) {
                    // Eğer bu çift sayı daha önce "bulunanlar" dizisine eklenmediyse ekle
                    if (!isFind(duplicateEvens, numbers[i])) {
                        duplicateEvens[startIndex++] = numbers[i];
                    }
                    break;
                }
            }
        }

        System.out.println("Dizi : " + Arrays.toString(numbers));
        System.out.print("Tekrar Eden Çift Sayılar : ");

        // Sadece içi dolan (0 olmayan veya geçerli olan) kısımları yazdırıyoruz
        for (int value : duplicateEvens) {
            if (value != 0) {
                System.out.print(value + " ");
            }
        }
    }
}

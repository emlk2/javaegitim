import java.util.Scanner;
import java.util.Arrays;

public class Siralama {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        // 1. Adım: Dizinin boyutunu kullanıcıdan alıyoruz
        System.out.print("Dizinin boyutu n : ");
        int n = input.nextInt();

        // Boyuta göre diziyi tanımlıyoruz
        int[] list = new int[n];

        // 2. Adım: Dizinin elemanlarını kullanıcıdan tek tek alıyoruz
        System.out.println("Dizinin elemanlarını giriniz :");
        for (int i = 0; i < n; i++) {
            System.out.print((i + 1) + ". Elemanı : ");
            list[i] = input.nextInt();
        }

        // 3. Adım: Java'nın hazır Arrays sınıfını kullanarak sıralama yapıyoruz
        // Bu metod arka planda oldukça hızlı bir sıralama algoritması (Dual-Pivot Quicksort) çalıştırır.
        Arrays.sort(list);

        // 4. Adım: Sıralanmış diziyi ekrana yazdırıyoruz
        System.out.print("Sıralama : ");
        for (int number : list) {
            System.out.print(number + " ");
        }

        input.close();
    }
}
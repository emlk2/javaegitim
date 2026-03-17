import java.util.Arrays;
import java.util.Scanner;

public class MinMaxDizi {
    public static void main(String[] args) {
        int[] list = {15, 12, 788, 1, -1, -778, 2, 0};

        Scanner sc = new Scanner(System.in);
        System.out.print("Girilen Sayı : ");
        int input = sc.nextInt();

        // 1. Adım: Diziyi küçükten büyüğe sıralıyoruz
        // Sıralanmış dizi: [-778, -1, 0, 1, 2, 12, 15, 788]
        Arrays.sort(list);

        int minNearest = list[0]; // Başlangıçta dizinin en küçüğünü verelim
        int maxNearest = list[list.length - 1]; // Başlangıçta dizinin en büyüğünü verelim

        // 2. Adım: Döngü ile en yakınları bulalım
        for (int i : list) {
            // Eğer sayı inputtan küçükse, küçüklerin en büyüğü olmaya adaydır
            if (i < input) {
                minNearest = i;
            }
            // Eğer sayı inputtan büyükse, büyüklerin en küçüğü olmaya adaydır
            if (i > input) {
                maxNearest = i;
                break; // İlk büyük sayıyı bulduğumuzda döngüden çıkabiliriz (çünkü dizi sıralı)
            }
        }

        System.out.println("Girilen sayıdan küçük en yakın sayı : " + minNearest);
        System.out.println("Girilen sayıdan büyük en yakın sayı : " + maxNearest);

        sc.close();
    }
}

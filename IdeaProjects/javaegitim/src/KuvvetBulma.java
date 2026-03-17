import java.util.Scanner;

public class KuvvetBulma {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("Sınır sayısını giriniz: ");
        int n = input.nextInt();

        System.out.println("4'ün kuvvetleri:");
        for (int i = 1; i <= n; i *= 4) {
            System.out.print(i + " ");
        }

        System.out.println("\n5'in kuvvetleri:");
        for (int i = 1; i <= n; i *= 5) {
            System.out.print(i + " ");
        }
    }
}
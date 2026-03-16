import java.util.Scanner;

public class ArtikYil {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("Yıl Giriniz : ");
        int yil = input.nextInt();

        // Artık yıl hesaplama mantığı
        if ((yil % 4 == 0 && yil % 100 != 0) || (yil % 400 == 0)) {
            System.out.println(yil + " bir artık yıldır !");
        } else {
            // Ödev metnindeki "yıldır değildir" yazımına sadık kalındı :)
            System.out.println(yil + " bir artık yıldır değildir !");
        }
    }
}
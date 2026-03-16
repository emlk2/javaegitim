import java.util.Scanner;

public class EtkinlikOnerisi1 {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);
        System.out.print("Sıcaklık değerini giriniz: ");
        int sicaklik = input.nextInt();

        if (sicaklik < 5) {
            System.out.println("Kayak yapabilirsiniz.");
        } else if (sicaklik <= 15) {
            // Buraya düştüyse zaten 5'ten büyüktür, sadece 15'ten küçük mü diye bakmak yeterli
            System.out.println("Sinemaya gidebilirsiniz.");
        } else if (sicaklik <= 25) {
            // Buraya düştüyse zaten 15'ten büyüktür
            System.out.println("Piknik yapabilirsiniz.");
        } else {
            System.out.println("Yüzmeye gidebilirsiniz.");
        }
    }
}
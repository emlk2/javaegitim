import java.util.Scanner;

public class UcakBileti {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        System.out.print("Mesafeyi km türünden giriniz : ");
        int mesafe = input.nextInt();

        System.out.print("Yaşınızı giriniz : ");
        int yas = input.nextInt();

        System.out.print("Yolculuk tipini giriniz (1 => Tek Yön , 2 => Gidiş Dönüş ): ");
        int yolculukTipi = input.nextInt();

        if (mesafe > 0 && yas > 0 && (yolculukTipi == 1 || yolculukTipi == 2)) {
            double normalTutar = mesafe * 0.10;
            double yasIndirimiOrani = 0;

            if (yas < 12) {
                yasIndirimiOrani = 0.50;
            } else if (yas >= 12 && yas <= 24) {
                yasIndirimiOrani = 0.10;
            } else if (yas > 65) {
                yasIndirimiOrani = 0.30;
            }

            double yasIndirimi = normalTutar * yasIndirimiOrani;
            double indirimliTutar = normalTutar - yasIndirimi;
            double toplamTutar;

            if (yolculukTipi == 2) {
                double gidisDonusIndirimi = indirimliTutar * 0.20;
                toplamTutar = (indirimliTutar - gidisDonusIndirimi) * 2;
            } else {
                toplamTutar = indirimliTutar;
            }

            System.out.println("Toplam Tutar = " + toplamTutar + " TL");

        } else {
            System.out.println("Hatalı Veri Girdiniz !");
        }
    }
}

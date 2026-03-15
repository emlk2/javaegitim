import java.util.Scanner;
public class Taksimetre {
    public static void main(String[] args) {
        Scanner giris = new Scanner(System.in);
        System.out.print("Gidilen mesafeyi (km) giriniz: ");
        double km = giris.nextDouble();

        double acilisucreti = 10.0;
        double kmbasiucret = 2.20;
        double minimumodeme = 20.0;

        double toplamtutar = acilisucreti + (km * kmbasiucret);
        if (toplamtutar < minimumodeme) {
            toplamtutar = minimumodeme;
        }
        System.out.println("Taksimetre tutarı " + toplamtutar + "TL");
    }



}

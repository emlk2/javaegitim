import java.util.Scanner;

public class UcgenAlani {
    public static void main(String[] args){
        Scanner giris=new Scanner(System.in);
        System.out.println("--- Üçgen Alanı Hesaplama ---");

        System.out.print("Birinci kenar uzunluğunu giriniz: ");
        double kenar1=giris.nextDouble();

        System.out.print("İkinci kenar uzunluğunu giriniz: ");
        double kenar2=giris.nextDouble();

        System.out.print("Hipotenüs uzunluğunu giriniz : ");
        double hipotenus= giris.nextDouble();

        double u = (kenar1+ kenar2 + hipotenus) / 2;
        double cevre = 2 * u;
        double alanKaresi = u * (u - kenar1) * (u - kenar2) * (u - hipotenus);
        double alan=Math.sqrt(alanKaresi);

        System.out.println("Üçgenin Çevresi: " + cevre);
        System.out.println("Üçgenin Alanı: " + alan);


    }
}

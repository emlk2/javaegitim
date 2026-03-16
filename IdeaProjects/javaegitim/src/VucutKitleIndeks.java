import java.util.Scanner;
public class VucutKitleIndeks {
    public static void main(String[] args){
        Scanner giris=new Scanner(System.in);
        System.out.println("Lütfen boyunuzu giriniz: ");
        double boy=giris.nextDouble();
        System.out.println("Lütfen kilonuzu giriniz: ");
        double kilo=giris.nextDouble();

        double vki=kilo/(boy*boy);
        System.out.println(vki);

    }

}

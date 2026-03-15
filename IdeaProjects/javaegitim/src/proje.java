import java.util.Scanner;
public class proje
{ public static void main (String[] args){
//değişkenleri oluştur
    int mat , fizik ,kimya , tarih ;
    Scanner giris=new Scanner(System.in);
    System.out.print("Matematik notunuz: ");
    mat=giris.nextInt();

    System.out.print("Fizik notunuz: ");
    fizik=giris.nextInt();

    System.out.print("Kimya notunuz: ");
    kimya=giris.nextInt();

    System.out.print("Tarih notunuz: ");
    tarih= giris.nextInt();

    int toplam=(mat+fizik+kimya+tarih);
    double sonuc=toplam/4;
    System.out.print(sonuc);




}
}

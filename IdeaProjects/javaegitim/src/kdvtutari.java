import java.util.Scanner;
public class kdvtutari {

   public  static void main(String[] args) {
       Scanner giris=new Scanner(System.in);
       System.out.print("Lütfen kdv tutarını giriniz: ");
       double tutar=giris.nextDouble();
       double kdvOrani=0;
       if(tutar>=0 &&tutar<=1000) {
           kdvOrani = 0.18;
       }else if(tutar<1000){
           kdvOrani=0.08;
           }
       else{ System.out.println("Geçersiz bir tutar girdiniz.");}

       double kdvTutari= tutar* kdvOrani;
       double kdvliFiyat=tutar+kdvTutari;

       System.out.println("KDV'siz Fiyat: " + tutar);
       System.out.println("KDV Oranı: " + kdvOrani);
       System.out.println("KDV Tutarı: " + kdvTutari);
       System.out.println("KDV'li Fiyat: " + kdvliFiyat);





    }
}

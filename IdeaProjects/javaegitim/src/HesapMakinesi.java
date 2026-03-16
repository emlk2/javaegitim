import java.util.Scanner;
public class HesapMakinesi {
    public static void main (String[] args){
        int n1,n2,select;
        Scanner giris=new Scanner(System.in);
        System.out.println("İlk sayı: ");
        n1=input.nextInt();
        System.out.println("İkinci sayı: ");
        n2=input.nextInt();

        switch(select){
            case 1:
                System.out.println("Toplam: " + (n1 + n2));
                break;
            case 2:
                System.out.println("Çıkarma: " + (n1 - n2));
                break;
            case 3:
                System.out.println("Çarpma: " + (n1 * n2));
                break;
            case 4:
                // Bölme işleminde 0'a bölünme hatasını (ArithmeticException) önlemek için ufak bir kontrol ekliyoruz
                if (n2 != 0) {
                    System.out.println("Bölme: " + (n1 / n2));
                } else {
                    System.out.println("Hata: Bir sayı 0'a bölünemez!");
                }
                break;
            default:
                System.out.println("Yanlış seçim yaptınız. Lütfen tekrar deneyin.");
                break;
        }

    }
}

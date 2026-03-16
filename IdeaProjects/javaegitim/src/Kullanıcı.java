import java.util.Scanner;

public class Kullanıcı {
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);

        // Sistemde kayıtlı olduğunu varsaydığımız doğru şifre
        String dogruSifre = "patika123";

        System.out.print("Lütfen şifrenizi giriniz: ");
        String girilenSifre = input.nextLine();

        // Şifre doğruysa
        if (girilenSifre.equals(dogruSifre)) {
            System.out.println("Giriş başarılı! Hoş geldiniz.");
        }
        // Şifre yanlışsa
        else {
            System.out.println("Şifreniz yanlış!");
            System.out.print("Şifrenizi sıfırlamak ister misiniz? (Evet / Hayır): ");
            String secim = input.nextLine();

            // Kullanıcı Evet yazarsa (Büyük/küçük harf duyarsız olması için equalsIgnoreCase kullandık)
            if (secim.equalsIgnoreCase("Evet")) {
                System.out.print("Yeni şifrenizi giriniz: ");
                String yeniSifre = input.nextLine();

                // Yeni girilen şifre, unutulan (sistemdeki eski) şifreyle aynı mı kontrolü
                if (yeniSifre.equals(dogruSifre)) {
                    System.out.println("Şifre oluşturulamadı, lütfen başka şifre giriniz.");
                } else {
                    System.out.println("Şifre oluşturuldu.");
                }
            }
            // Kullanıcı Hayır derse veya başka bir şey yazarsa
            else {
                System.out.println("Şifre sıfırlama işlemi iptal edildi.");
            }
        }
    }

}

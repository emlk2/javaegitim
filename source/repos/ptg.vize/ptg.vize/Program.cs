using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ptg.vize
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Merhaba Dünya");

            // Console.WriteLine("adınız:");
            //string ad = Console.ReadLine();
            //Console.WriteLine("merhaba"+ ad);


            //Console.WriteLine("Sayı giriniz:");
            //int sayi = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine(sayi);

            //Console.WriteLine("1.sayi:");
            //int birincisayi=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("2.sayi:");
            //int ikincisayi = Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine("dikdörtgenin uzun kenarı:");
            //int uzunkenar=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("kısa kenar:");
            //int kisakenar = Convert.ToInt32(Console.ReadLine());
            //int alan = uzunkenar * kisakenar;
            //int cevre = 2 * (uzunkenar + kisakenar);
            //Console.WriteLine("alan:"+alan +","+"çevre:"+cevre);


            //Console.WriteLine("dogum yılınız:");
            //int dogumyili=Convert.ToInt32(Console.ReadLine());
            //int yas = 2025 - dogumyili;
            //Console.WriteLine(yas);

            //Console.WriteLine("yaşınız:");
            //int yas=Convert.ToInt32(Console.ReadLine());
            //int dogumyili = 2025 - yas;
            //Console.WriteLine(dogumyili);

            //Console.WriteLine("1.sayı:");
            //double birincisayi=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("2.sayı:");
            //double ikincisayi=Convert.ToInt32(Console.ReadLine());

            //if (birincisayi > ikincisayi)
            //{
            //    double fark = birincisayi - ikincisayi;
            //    Console.WriteLine(fark);
            //}
            //else 
            //{
            //    double fark = ikincisayi - birincisayi;
            //    Console.WriteLine(fark);
            //}

            //double carpim = birincisayi * ikincisayi;


            //double bolum= birincisayi / ikincisayi;
            //Console.WriteLine(bolum+" "+carpim);

            //Console.WriteLine("yarıçap:");
            //int r=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("pi sayısı:");
            //int pi=Convert.ToInt32(Console.ReadLine());
            //int alan = pi * r ^ 2;
            //Console.WriteLine(alan);,

            //Console.WriteLine("çalışma saati:");
            //int calismasaati=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("saat ücreti:");
            //int saatücret=Convert.ToInt32(Console.ReadLine());
            //int aylıkmaas = calismasaati * saatücret * 30;
            //Console.WriteLine(aylıkmaas); 

            //Console.WriteLine("birinci ürün adı:");
            //string birinciürün = Console.ReadLine();
            //Console.WriteLine("ikinci ürün adı:");
            //string ikinciürün= Console.ReadLine();
            //Console.WriteLine("üçüncü ürün adı:");
            //string ücüncüürün=Console.ReadLine();

            //Console.WriteLine("birinci ürün fiyatı:");
            //double birincifiyat=Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("ikinci ürün fiyatı:");
            //double ikincifiyat = Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("üçüncü ürün fiyatı:");
            //double ücüncüfiyat=Convert.ToDouble(Console.ReadLine());

            //Console.WriteLine("birinci adet:");
            //int birinciadet=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("ikinci adet:");
            //int ikinciadet=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("üçüncü adet:");
            //int ücüncüadet=Convert.ToInt32(Console.ReadLine());

            //Console.WriteLine(birinciürün+birincifiyat+birinciadet+", "+ ikinciürün+ikincifiyat+ikinciadet+" ,"+ücüncüürün+ücüncüfiyat+ücüncüadet);

            //-----------------------------------------------------------

            //ifelse

            //NOTHESAPLAMA

            //int ortalama=Convert.ToInt32(Console.ReadLine());
            //if (ortalama >= 85)
            //{
            //    Console.WriteLine("AA");
            //}
            //else if (ortalama >= 75)
            //{
            //    Console.WriteLine("BA");
            //}
            //else if (ortalama >= 65)
            //{
            //    Console.WriteLine("BB");
            //}
            //else if (ortalama >= 50)
            //{
            //    Console.WriteLine("CC");
            //}
            //else
            //{
            //    Console.WriteLine("FF");
            //}



            //VİZE FİNAL

            //Console.WriteLine("vize notu gir:");
            //int vize=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("final:");
            //int final=Convert.ToInt32(Console.ReadLine());
            //double ortalama = (vize * 0.4) + (final * 0.6);
            //if (ortalama >= 50 && final >= 50)
            //{
            //    Console.WriteLine("geçti");
            //}
            //else
            //{
            //    Console.WriteLine("kaldı");
            //}


            //kULLANICI ADI ŞİFRE KONTROLÜ

            //Console.WriteLine("kullanıcı adı giriniz:");
            //string username = Console.ReadLine();
            //Console.WriteLine("şifre giriniz");
            //string password = Console.ReadLine();

            //if (username == "admin" && password == "12345")
            //{
            //    Console.WriteLine("giriş başarılı");
            //}
            //else
            //{
            //    Console.WriteLine("giriş başarısız");
            //}

            //KULLANICIDAN ALINAN SAYI TEK Mİ ÇİFT Mİ?
            //Console.WriteLine("sayı giriniz");
            //int sayi=Convert.ToInt32(Console.ReadLine());

            //if (sayi % 2 == 0)
            //{
            //    Console.WriteLine("çift");
            //}
            //else
            //{
            //    Console.WriteLine("tek");
            //}


            //kullanıcıdan alınan sayı pozitif mi negatif mi sıfır mı?
            //Console.WriteLine("sayı gir:");
            //int sayi=Convert.ToInt32(Console.ReadLine());
            //if (sayi == 0)
            //{
            //    Console.WriteLine("sıfır");
            //}
            //else if (sayi > 0)
            //{
            //    Console.WriteLine("pozitif");
            //}
            //else
            //{
            //    Console.WriteLine("negatif");
            //}
            //double sonuc = 0;
            //Console.WriteLine("1.sayı:");
            //int sayi1=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("2.sayı:");
            //int sayi2=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("işlem giriniz:");
            // !!//char islem = char.Parse(Console.ReadLine());
            //if (islem == 't')
            //{ sonuc = sayi1 + sayi2; }
            //else if (islem == 'c')
            //{  sonuc = sayi1 * sayi2; }
            //else if(islem =='b')
            //{ sonuc= sayi1 / sayi2; }
            //else if(islem=='ç')
            //{ sonuc = sayi1 - sayi2; }
            //else
            //{ Console.WriteLine("tanımsız işlem"); }

            //kullanıcıdan 2 ürün fiyatı alan toplam fiyat 2000den büyükse 2.ürüne %25 indirim yapan ve toplam
            //tutarı ekrana yazan:

            //double tutar = 0;
            //Console.WriteLine("birinci ürünün fiyatı:");
            //double birincifiyat=Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("ikinci ürünün fiyatını gir:");
            //double ikincifiyat = Convert.ToDouble(Console.ReadLine());

            //if (birincifiyat + ikincifiyat > 2000)
            //{
            //    tutar = birincifiyat + (ikincifiyat - ikincifiyat * 0.4);
            //}
            //else
            //{
            //    tutar = birincifiyat + ikincifiyat;
            //}
            //Console.WriteLine(tutar);

            //ONLİNE DERS

            //iç içe for yazılabilir mi?

            //for (int i =0; i<=10;i ++)
            //{
            //    for(int j = 1; j <= 10; j++)
            //    {
            //        Console.WriteLine(i+"x"+j+"="+ i*j); //çarpım tablosu
            //    }
            //}
            //Console.WriteLine("tamamlandı");

            //for(int i = 0; i <= 10; i++)
            //{
            //    if (i == 5)
            //    {
            //        continue; //devam et
            //    }
            //    if (i == 8)
            //    {
            //        break; //döngü kırılır.
            //    }
            //    Console.WriteLine(i);
            //}

            //for(int i = 1; i <= 100; i++)
            //{
            //    Console.WriteLine(i+"sayının karesi: "+(i*i));
            //}

            //int toplam = 0;
            //Console.WriteLine("sayı giriniz:");
            //int sayi=Convert.ToInt32(Console.ReadLine());
            //for (int i = 1; i <= sayi; i++)
            //{
            //    toplam += i;
            //}
            //Console.WriteLine("toplam: "+toplam);

            //int toplam = 0;
            //Console.WriteLine("başlangıc sayısını giriniz: ");
            //int bas = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("bitis? ");
            //int bit = Convert.ToInt32(Console.ReadLine());
            //for (int i = bas; i < bit; i++)
            //{
            //    toplam += i;
            //}
            //Console.WriteLine(toplam);

            //int toplam2 = 0;
            //int x1 = Convert.ToInt32(Console.ReadLine());
            //int x2 = Convert.ToInt32(Console.ReadLine());
            //for (x1 = 1; x1 < x2; x1++)
            //{
            //    toplam2 += x1;
            //}
            //Console.WriteLine(toplam2);

            //üs alma
            //Console.WriteLine("taban:");
            //int taban=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("üs: ");
            //int üs=Convert.ToInt32(Console.ReadLine());
            //int sonuc = 1;
            //for (int i = 1; i <= üs; i++)
            //{
            //    sonuc *= taban;
            //}
            //Console.WriteLine("sonuç: "+sonuc);

            //int toplam = 0;
            //for(int i = 1; i < 1000; i++)
            //{
            //    if (i % 3 == 0||i%5==0)
            //    {
            //        Console.WriteLine(i);
            //    }

            //}

            ////yıldız piramit
            //for (int i = 1; i <= 5; i++)
            //{
            //    for (int j = 5; j > i; j--)
            //    {
            //        Console.Write(" ");
            //    }
            //    for (int k = 1; k <= (2 * i - 1); k++)
            //    {
            //        Console.Write("*");

            //    }

            //    Console.WriteLine();
            //}
            //}

            //***SWİTCH CASE YOUTUBE***

            //iflerle işi uzatmamak için 
            //switch (kosul)
            //{
            //    case v1:
            //        //v1 ise bu kod calıssın
            //        break;
            //    case v2:
            //        //v2 ise
            //        break;
            //        ....
            //            default:
            //            //hicbir case saglanmazsa burası calıssın.

            //}
            // Console.WriteLine("gün:")
            //int gün = Convert.ToInt32(Console.ReadLine();
            //if (gün == 1)
            //{
            //    Console.WriteLine("pazartesi");
            //}
            //else if (gün == 2)
            //{
            //    Console.WriteLine("salı");
            //}
            //else if(gün==3) Console.WriteLine("çarşamba");
            //else if(gün==4) Console.WriteLine("perşembe");
            // else Console.WriteLine("geçersiz sayı);

            //Switch case hali:

            //Console.WriteLine("gün: ");
            //int gün=Convert.ToInt32(Console.ReadLine());
            //switch (gün)
            //{
            //    case 1:
            //        Console.WriteLine("pazartesi");
            //        break;
            //    case 2:
            //        Console.WriteLine("salı");
            //        break;
            //    case 3:
            //        Console.WriteLine("çarşamba");
            //        break;
            //    case 4:
            //        Console.WriteLine("perşembe");
            //        break;
            //    case 5:
            //        Console.WriteLine("cuma");
            //        break;
            //    case 6:
            //        Console.WriteLine("cumartesi");
            //        break;
            //    case 7:
            //        Console.WriteLine("pazar");
            //        break;
            //    default:
            //        Console.WriteLine("geçersiz sayı");
            //        break;


            //}
            //switch (gün)
            //{
            //    case 1:
            //    case 2:
            //    case 3:
            //    case 4:
            //    case 5:
            //        Console.WriteLine("hafta içi");
            //        break;
            //    case 6:
            //    case 7:
            //        Console.WriteLine("hafta sonu");
            //        break;
            //    default:
            //        Console.WriteLine("geçersiz");
            //        break;
            //}

            //****LAB ONLİNE***

            //not ortalaması hesaplayıp harf notu ver:
            //Console.WriteLine(" birinci not girin:");
            //int not1=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("ikinci not: ");
            //int not2=Convert.ToInt32(Console.ReadLine());

            //double ort = (not1+not2)/2;
            //if (ort >= 90)
            //{
            //    Console.WriteLine("aa");
            //}
            //else if (ort >= 80) Console.WriteLine("ba");
            //else if (ort >= 70) Console.WriteLine("bb");
            //else if (ort >= 60) Console.WriteLine("bc");
            //else if (ort >= 50) Console.WriteLine("cc");
            //else Console.WriteLine("ff");

            //HESAP MAKİNESİ

            //2 sayı al hangi işlemi yaptırmak istediğini sec, switch case kullan, sonuc yazdır

            //Console.WriteLine("birinci sayı?");
            //int sayi1=  Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("ikinci sayi? ");
            //int sayi2 = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("işlem seçin= +,-,/,* ");
            //char islem= Convert.ToChar(Console.ReadLine());  //string de olur.
            //switch (islem)
            //{
            //    case '+':
            //        Console.WriteLine(sayi1+sayi2);
            //        break;
            //    case '-':
            //        Console.WriteLine(sayi1 - sayi2);
            //        break;
            //    case '/':
            //        Console.WriteLine(sayi2 / sayi2);
            //        break;
            //    case '*':
            //        Console.WriteLine(sayi1*sayi2);
            //        break;
            //    default:
            //        Console.WriteLine("geçersiz terim");
            //        break;
            //}


            //**girilen sayının çift mi tek mi , pozitif - negatif oldugunu if else blokları ile:
            // Console.WriteLine("sayı giriniz:");
            // int sayi=Convert.ToInt32(Console.ReadLine());
            // string durum = ""; //null
            // if (sayi == 0)
            // {
            //     Console.WriteLine("sayı işaretsiz ve çift sayıdır.");
            // }
            //else if (sayi > 0)
            //{
            //     durum += "pozitiftir";
            //}
            // else
            // {
            //     durum += "negatiftir";


            // }
            // if (sayi % 2 == 0)
            // {
            //     durum += "ve çifttir";
            // }
            // else
            // {
            //     durum += "ve tektir";
            // }
            // Console.WriteLine("girilen sayi {0} {1} ", sayi , durum);


            //    int ay=Convert.ToInt32(Console.ReadLine());
            //string mevsim = null;

            //switch (ay)
            //{
            //    case 12:
            //    case 1:
            //    case 2:
            //        mevsim = "kış";
            //        break;
            //    case 3:
            //    case 4:
            //    case 5:
            //        mevsim = "ilkbahar";
            //        break;

            //    case 6:
            //    case 7:
            //    case 8:
            //        mevsim = "yaz";
            //        break;
            //    case 9:
            //    case 10:
            //    case 11:
            //        mevsim = "sonbahar";
            //        break;
            //    default:
            //        Console.WriteLine("hatalı giriş");
            //        break;


            //}
            //Console.WriteLine(mevsim);



            //7 KASIM
            //kullanıcının girdiği sayının basamaklar toplamını bulun

            //Console.WriteLine("sayı gir:");
            //int sayi=Convert.ToInt32(Console.ReadLine());
            //int toplam = 0;
            //while (sayi > 0)
            //{
            //    int birler = sayi % 10;
            //    toplam += birler;
            //    sayi = sayi / 10;

            //}
            //Console.WriteLine("toplam : {0}", toplam);
            //************************************************************

            ////sayının tersini alma (356->653)
            //Console.WriteLine("sayı gir: ");
            //int sayi = Convert.ToInt32(Console.ReadLine());
            //int ters = 0;
            //while (sayi > 0)
            //{
            //    int birler = sayi % 10;
            //    ters = ters * 10 + birler;
            //    sayi = sayi / 10;
            //}
            //Console.WriteLine("ters :" + ters);


            ////Kullanıcıdan sürekli sayı girişi al 0 girilene kadar o sayıları topla
            //Console.WriteLine("sayı giriniz ve bitimek istiyorsanız 0 girerek sonlandırınız: ");
            //int sayi=Convert.ToInt32(Console.ReadLine());
            //int toplam = 0;
            //while (sayi != 0)
            //{
            //    toplam += sayi;


            //}

            ////1 ile 100 arasında rastgele sayı tahmini do while ile çöz  
            //Random olusturdugumnesne = new Random();
            //int hedef = olusturdugumnesne.Next(1, 100);  //(1,100) 1 dahil 100 dahil değil.
            //int tahmin = 0;
            //do
            //{
            //    Console.WriteLine("sayı tahmin ediniz : (1-100)");
            //    tahmin = Convert.ToInt32(Console.ReadLine());
            //    if (tahmin > hedef)
            //    {
            //        Console.WriteLine("daha küçük bir sayı giriniz.");
            //    }
            //    else if (tahmin < hedef)
            //    {
            //        Console.WriteLine("daha büyük bir sayı giriniz.");
            //    }

            //}
            //while (tahmin != hedef);
            //Console.WriteLine("dogru bildiniz. dogru tahmin : " + hedef);

            //bir şifre belirle, kullanıcıdan sürekli şifre iste, doğruysa başarılı değilse yanlış tekrar deneyin yaz.

            //string dogruSifre = "emel"; // deve notasyonu değişkenler birden falza kelimeden oluşuyorsa ilk kelimenin ilk harfi küçük diğer kelimelerin ilk harfi büyük.

            //!!!!!pascal notasyonu =metod isimlerinde her kelimenin ilk harfinin büyük olmasıdır.



            //çıkmış vize
            //Rulet şeklinde,  iki renk ve 1-36 arası sayılar. oyuncuya her oyun başında renk ve sayı sorulur.
            //oyun sonunda tekrar oynayıp oynamayacagı sorulur. sonuc tablosu oluşturulur, kaç oyun oynadı kac oyun kazandı seklinde.

            //int oyun = 0;
            //int kayip = 0;
            //int kazanc = 0;
            //bool n = true;
            //while (n == true)
            //{ 
            //    Console.WriteLine("sayı oyunu için 1 , renk oyunu için 2 giriniz:");
            //    int secim = Convert.ToInt32(Console.ReadLine());
            //    if (secim == 1)
            //    {
            //        Console.WriteLine("sayı younuna hosgeldiniz: ");
            //        Console.WriteLine("bir sayı giriniz: (1-36");
            //        int hedefsayi=Convert.ToInt32(Console.ReadLine());
            //        Random rnd1 = new Random();
            //        int tahmin = rnd1.Next(0, 37);
            //        if (tahmin == hedefsayi)
            //        {
            //            Console.WriteLine("tebrikler bildiniz");
            //            kazanc++;
            //        }
            //        else
            //        {
            //            Console.WriteLine("bilemediniz");
            //            kayip++;
            //        }
            //        oyun++;


            //    }
            //    if (secim == 2)
            //    {
            //        Console.WriteLine("renk oyununa hoş geldiniz.");
            //        Console.WriteLine("kırmızı için 1 pembe için 2 giriniz: ");
            //        int renksayisi=Convert.ToInt32(Console.ReadLine());
            //        Random rnd2 = new Random();
            //        int tahminrenk = rnd2.Next(1, 3);
            //        if (tahminrenk == renksayisi)
            //        {
            //            kazanc++;
            //        }
            //        else
            //        {
            //            Console.WriteLine("kaybettiniz");
            //            kayip++;
            //        }
            //        oyun++;
            //        Console.WriteLine("çıkmak istiyor musunuz? ");
            //        string cikis = Console.ReadLine();
            //        if (cikis == "evet"||cikis=="EVET")
            //        {
            //            Console.WriteLine("**ÇIKIŞ YAPILIYOR** TEKRAR BEKLERİZ");
            //            n = false;
            //        }
            //        else
            //        {
            //            Console.WriteLine("oyuna devam ediliyor.");
            //            n = true;
            //        }
            //    }
            //    Console.WriteLine("Şu ana kadar " + oyun + "kez oynadınız.");
            //    Console.WriteLine("Şu ana kadar" + kazanc+ "kez kazandınız.");
            //    Console.WriteLine("Şu ana kadar"+  kayip+"kez kaybettiniz.");
            //}

            //TAŞ KAĞIT MAKAS OYUNU
            //Console.WriteLine("taş kağıt makas oyununa hoş geldiniz");
            //int kullanici = 0;
            //int kazancrobot = 0;
            //int kazanckullanici = 0;

            //for(int i = 0; i <= 2; i++)
            //{
            //    Random rnd=new Random();
            //    int robot = rnd.Next(1, 4);

            //    Console.WriteLine("1)TAŞ 2)KAĞIT 3)MAKAS");
            //    kullanici=Convert.ToInt32(Console.ReadLine());

            //    if (robot == 1)
            //    {
            //        if (kullanici == 1)
            //        {
            //            Console.WriteLine("berabere");
            //            kazancrobot++;
            //            kazanckullanici++;

            //        }
            //        if (kullanici == 2)
            //        {
            //            Console.WriteLine("kazandın robot taş seçti ";
            //            kazanckullanici++;
            //        }
            //        if (kullanici == 3)
            //        {
            //            Console.WriteLine("robot kazandı ");
            //            kazancrobot++;
            //        }
            //    }
            //    else if (robot == 2)
            //    {
            //        if (kullanici == 1)
            //        {
            //            Console.WriteLine("kazandınız ");
            //            kazanckullanici++;
            //        }
            //        if (kullanici == 2)
            //        {
            //            Console.WriteLine("berabere");
            //            kazanckullanici++;
            //            kazancrobot++;
            //        }
            //        if (kullanici == 3)
            //        {
            //            Console.WriteLine("kazandın ");
            //            kazanckullanici++;
            //        }

            //    }
            //    else if (robot == 3) // Robot: MAKAS
            //    {
            //        if (kullanici == 1)
            //        { // K: TAŞ
            //            kazanckullanici++;
            //            Console.WriteLine("KAZANDIN (Robot MAKAS seçti)");
            //        }
            //        else if (kullanici == 2)
            //        { // K: KAĞIT
            //            kazancrobot++;
            //            Console.WriteLine("Kaybettin :( (Robot MAKAS seçti)");
            //        }
            //        else if (kullanici == 3)
            //        { // K: MAKAS
            //            kazancrobot++;
            //            kazanckullanici++;
            //            Console.WriteLine("BERABERE (Robot MAKAS seçti)");
            //        }
            //        else
            //        {
            //            Console.WriteLine("GEÇERSİZ DEĞER");
            //        }
            //    }
            //    if (i == 2)//1.tur bitti
            //    {
            //        Console.WriteLine("devam etmek istiyor musunuz?");
            //        int devam = Convert.ToInt32(Console.ReadLine());

            //        if (devam == 2) // Hayır (2) derse
            //        {
            //            Console.WriteLine("Sonuç : ROBOT - Kullanıcı  " + kazancrobot + " - " + kazanckullanici);
            //            // Döngü 'i=2' olduğu için zaten doğal olarak sonlanacak. 
            //            // Eğer 'while(true)' olsaydı burada 'break;' komutu gerekirdi.
            //        }
            //        else // Evet (1) derse
            //        {
            //            // Döngüyü tekrar başlatmak için 'i' sıfırlanır.
            //            // Döngü başına dönünce 'i++' çalışacağı için 'i' tekrar 0 olur.
            //            i = -1;
            //        }

            //    } 

            //}

            //MİNİ SLOT MACHİNE OYUNU 
            //Kullanıcı 10 puan ile başlar
            //3 rastgele sayıdan üçü de eşitse 15 puan kazanır, değilse 5 puan kaybeder.
            //puan 0 olunca oyun biter

            Random rnd = new Random();
            int puan = 10;
            bool oyun= true;

            while(oyun&&puan>70)
            {
                int a = rnd.Next(1, 6);
                int b = rnd.Next(1, 6);
                int c = rnd.Next(1, 6);

                Console.WriteLine("makine : " + a+b+c);

                if(a==b && b == c)
                {
                    puan += 15;
                    Console.WriteLine("jackpot!! tebrikler 15 paun");
                }
                else
                {
                    puan = -5;
                    Console.WriteLine("-5 kaybettin");
                }
                Console.WriteLine("puan " + puan);
                Console.WriteLine("devam etmek istiyor musun? (E/H");
                string cevap=Console.ReadLine().ToUpper();
                if (cevap == "H")
                {
                    Console.WriteLine("oyun bitti");
                }
            }
        }
    }
}
    
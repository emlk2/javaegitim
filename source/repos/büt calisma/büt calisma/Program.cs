using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace büt_calisma
{
    internal class Program
    {
        //static void Main(string[] args)
        //{

        //ARMSTRONG SAYISI
        //math.pow ile
        //n basamaklı sayının n.üstlerinin toplamı kendisine eşit
        //Console.WriteLine("sayı gir: ");
        //int sayi=Int32.Parse(Console.ReadLine());
        //int kopyasayi = sayi;
        //int basamaksayisi = 4;
        //int toplam = 0;
        //while(kopyasayi > 0)
        //{
        //    int bas = kopyasayi % 10;
        //    toplam = toplam + (int)Math.Pow(bas, basamaksayisi);
        //    kopyasayi = kopyasayi / 10;
        //}

        //if (toplam == sayi)
        //{
        //    Console.WriteLine("armstrong sayısıdır");
        //}
        //else
        //{
        //    Console.WriteLine("arstrong sayısı değil");
        //}




        //metotsuz

        //cok boyutlu diziler
        //int[,] dizi= new int[2, 2];  //2 for birinci satır,ikincisi sutun için
        //dizi[0, 0]= 25;
        //dizi[0, 1] = 35;
        //dizi[1, 0] = 45;
        //dizi[1, 1] = 55;

        //for(int i=0;i<2;i++)
        //{
        //    for(int j = 0; j < 2; j++)
        //    {
        //        Console.Write(" "+dizi[i, j]);  //hepsini yan yana yazdırdı.
        //    }
        //    Console.WriteLine( );

        //2x3 matris

        //int[,] dizi = new int[2, 3];
        //dizi[0, 0] = 1;
        //dizi[0, 1] = 2;
        //dizi[0, 2] = 3;
        //dizi[1, 0] = 4;
        //dizi[1, 1] = 5;
        //dizi[1, 2] = 6;
        //for(int i=0;i<2;i++)
        //{
        //    for(int j = 0; j < 3; j++)
        //    {
        //        Console.Write(" " + dizi[i, j]);
        //    }
        //    Console.WriteLine();
        //}

        //cok boyutlu dizilerde toplama

        //int[,] matris1 = { { 1, 2 }, { 3, 4 } };
        //int[,] matris2 = { { 5,6 }, { 7, 8 } };
        //int[,] toplam = new int[2, 2];
        //for(int i=0;i<2;i++)
        //{
        //    for(int j = 0; j < 2; j++)
        //    {
        //        toplam[i,j] = matris1[i, j] + matris2[i,j];
        //    }
        //}
        //for(int k = 0; k < 2; k++)
        //{
        //    for(int m = 0; m< 2; m++)
        //    {
        //        Console.Write(" "+toplam[k,m]);
        //    }
        //    Console.WriteLine();
        //}

        //metotlar
        //public static int Ebob(int a, int b)
        //{
        //    int min = a <= b ? a : b;
        //    for (int i = min; i > 0; i--)
        //    {
        //        if (a % i == 0 && b % 1 == 0)
        //        {
        //            min = i;
        //            break;
        //        }
        //    }
        //    return min;
        //}
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("1. sayıyı giriniz");
        //    int a = Int32.Parse(Console.ReadLine());
        //    Console.WriteLine("2. sayıyı giriniz");
        //    int b = Int32.Parse(Console.ReadLine());
        //    int cikti = Ebob(a, b);
        //    Console.WriteLine("Sayıların ebob'u: " + cikti);


        //   // int min = Ebob(12, 4);
        //    //Console.WriteLine(min);
        //    Console.ReadLine();
        //}



        //bir metinde aranan kelime kaç kere var?
        //public static int KacKere(string metin,string aranan)
        //{
        //    int sayac = 0;
        //    for (int i = 0; i < metin.Length - aranan.Length; i++)
        //    {
        //        bool bayrak = true;
        //        if (metin[i] == aranan[0])
        //        {
        //            for(int j = 0;j < aranan.Length; j++)
        //            {
        //                if(metin[i+j] != aranan[j])
        //                {
        //                    bayrak = false;
        //                    break;
        //                }

        //            }
        //            if (bayrak)
        //            {
        //                sayac++;
        //                i = i + aranan.Length - 1;
        //            }
        //        }
        //    }
        //    return sayac;
        //}
        //static void Main(string[] args)
        //{
        //    string metin = "kalem kağıt silgi kağıt silgi defter kalem silgi";
        //    string aranan = "silgi";
        //    int sayac = KacKere(metin, aranan);
        //    Console.WriteLine(sayac);
        //    Console.ReadLine();
        //}


        //public static int KacKere(string metin, string aranan)
        //{
        //    int sayac = 0;
        //    for (int i = 0; i < metin.Length - aranan.Length; i++)
        //    {
        //        bool bayrak = true;
        //        if (metin[i] == aranan[0])
        //        {
        //            for (int j = 0; j < aranan.Length; j++)
        //            {
        //                if (metin[i + j] != aranan[j])
        //                {
        //                    bayrak = false;
        //                    break;
        //                }

        //            }
        //            if (bayrak)
        //            {
        //                sayac++;
        //                i = i + aranan.Length + 1;
        //            }
        //        }
        //    }
        //    return sayac;
        //}
        //static void Main(string[] args)
        //{
        //    string metin = "elma armut muz armut";
        //    string aranan = "armut";
        //    int sayac = KacKere(metin, aranan);
        //    Console.WriteLine(sayac);
        //    Console.ReadLine();
        //}

        //diziyi n adım sağa kaydıran metot    
        //eleman sayısı =5
        //n=5   //ikisi aynı olmamalı
        //public static void NSagaKaydir(int[] dizi,int n)
        //{
        //    if (dizi.Length <= 1)
        //    {
        //        return;
        //    }
        //    n = n % dizi.Length;
        //    for(int i = 0; i < n; i++)
        //    {
        //        int sonEleman = dizi[dizi.Length - 1];
        //        for(int j = dizi.Length - 1; j >= 1; j--)
        //        {
        //            dizi[j] = dizi[j - 1];
        //            //2,2,3,4,5,
        //        }
        //        dizi[0] = sonEleman;
        //    }
        //}
        //static void Main(string[] args)
        //{
        //    int[] dizi = { 2, 3, 4, 5, 6, };//45623
        //    NSagaKaydir(dizi, 3);
        //    for (int i = 0; i < dizi.Length; i++)
        //    {
        //        Console.WriteLine(dizi[i] + ",");
        //    }
        //    Console.ReadLine();
        //}


        //kullanıcıdan 2 string al,anagrama mı kontrol et
        //anagram=aynı harfler ama yerleri farklı.

        //public static bool AnagramMı(string k1, string k2)
        //{
        //    bool bayrak = true;
        //    if (k1.Length != k2.Length)
        //    {
        //        bayrak = false;
        //    }
        //    //abcçdefgğhıijklm...
        //    int[] karakterDizisi = new int[256];

        //    for (int i = 0; i < k1.Length; i++)
        //    {
        //        karakterDizisi[k1[i]]++;
        //    }
        //    //karakterlerDizisi=[0-----256]
        //    //a-->asci=33
        //    //karakterler[33]++;

        //    for (int i = 0; i < k2.Length; i++)
        //    {
        //        karakterDizisi[k2[i]]--;
        //    }
        //    for (int i = 0; i < karakterDizisi.Length; i++)
        //    {
        //        if (karakterDizisi[i] != 0)
        //        {
        //            bayrak = false;
        //            break;
        //        }
        //    }
        //    return bayrak;
        //}
        //static void Main(string[] args)
        //{
        //    string kelime1 = "anne";
        //    string kelime2 = "nane";
        //    bool sonuc = AnagramMı(kelime1, kelime2);
        //    if (sonuc)
        //    {
        //        Console.WriteLine("evet,anagram");
        //    }
        //    else
        //    {
        //        Console.WriteLine("hayır,anagram değil");
        //    }
        //    Console.ReadLine();
        //}


        //public static bool AnagramMı(string k1,string k2)
        //{
        //    bool flag = true;
        //    if (k1.Length != k2.Length)
        //    {
        //        flag = false;
        //    }
        //    int[] harflerdizisi = new int[256];
        //    for(int i=0;i<k1.Length;i++)
        //    {
        //        harflerdizisi[k1[i]]++;
        //    }
        //    for(int i = 0; i < k2.Length; i++)
        //    {
        //        harflerdizisi[k2[i]]--;
        //    }
        //    for(int i=0;i<harflerdizisi.Length;i++)
        //    {
        //        if (harflerdizisi[i] != 0)
        //        {
        //            flag = false;
        //            break;
        //        }
        //    }
        //    return flag;
        //}
        //static void Main(string[] args)
        //{
        //    string kelime1 = "anne";
        //    string kelime2 = "nane";
        //    bool sonuc=AnagramMı(kelime1,kelime2);
        //    if (sonuc == true)
        //    {
        //        Console.WriteLine("anagram");
        //    }
        //    else
        //    {
        //        Console.WriteLine("anagram değil");
        //    }
        //    Console.ReadLine(); 

        //}




        //İKİ SONUCU TOPLAYIP GERİ DÖNDÜREN METOT

        //public static int Toplama(int x,int y)
        //{
        //    int toplam = x + y;
        //    return toplam;

        //}
        //static void Main(string[] args)
        //{
        //    int sonuc = Toplama(5, 3);
        //    Console.WriteLine("toplam: " + sonuc);
        //    Console.ReadLine();
        //}

        //kendisine int bir diziyi parametre alan ve bu dizinin en büyük elemanını döndüren
        //public static int[] EnBüyük(int[] dizi)
        //{
        //    int enBüyük = int.MinValue;
        //    for(int i = 0; i < dizi.Length; i++)
        //    {
        //        if (dizi[i] > enBüyük)
        //        {
        //            enBüyük = dizi[i];

        //        }
        //    }
        //    return dizi;
        //}
        //static void Main(string[] args)
        //{
        //    int[] dizi = { 1, 5, 6, 4, 2, 8, 3, 9, 7 };
        //    int enBuyukDeger = EnBüyük(dizi);
        //    Console.WriteLine(enBuyukDeger);
        //}

        //3) İki sayı arasındaki çift sayıları yazdıran 
        //public static void Cift(int bas, int bit)
        //{
        //    for(int i=bas;i<=bit;i++)
        //    {
        //        if (i % 2 == 0)
        //        {
        //            Console.WriteLine(i);
        //        }
        //    }
        //}
        //static void Main(string[] args)
        //{
        //    Cift(3, 16);
        //}

        //4) iki sayı arasındaki çift sayıları alıp toplamı geri döndüren:

        //public static int Cifttopla(int x,int y)
        //{
        //     int toplam = 0;
        //     if (x > y)
        //     {
        //         for(int i=y+1;i<x;i++)
        //         {
        //             if (i % 2 == 0)
        //             {
        //                 toplam += i;
        //             }
        //         }
        //     }
        //     else if (y > x)
        //     {
        //         for(int i = x + 1; i < y; i++)
        //         {
        //             if(i%2==0)
        //             {
        //                 toplam += i;
        //             }
        //         }
        //     }
        //     else
        //     {
        //         toplam = 0;
        //     }
        //     return toplam;

        //}



        // static void Main(string[] args)
        // {
        //     int tplm = Cifttopla(1, 10);
        //     Console.WriteLine(tplm);

        // }


        //5)Bir diziyi paramtere alan ve bu dizideki pozitif ,negatif,ve sıfır adetlerini yazdıran:

        //public static void yazdır(int[] dizi)
        //{
        //    int sıfır = 0;
        //    int pozitif = 0;
        //    int negatif = 0;
        //    for(int i=0;i<dizi.Length; i++)
        //    {
        //        if (dizi[i]==0)
        //        {
        //            sıfır++;
        //        }
        //        else if (dizi[i] < 0)
        //        {
        //            pozitif++;
        //        }
        //        else
        //        {
        //            negatif++;
        //        }
        //    }

        //    Console.WriteLine(sıfır+ "tane sıfır");
        //    Console.WriteLine(pozitif + "tane +");

        //    Console.WriteLine(negatif + "tane -");
        //}

        //static void Main(string[] args)
        //{
        //    int[] dizi = { 0, -5, 6, -8, -4, 7 };
        //    yazdır(dizi);
        //}


        //6)kendiisne bir n parametresi alan ve n'inci fibonacciyi yazdıran 

        //public static int Fibonacciyaz(int n)
        //{
        //    if (n <= 1)
        //    {
        //        return n;
        //    }
        //    int birinci = 0;
        //    int ikinci = 1;
        //    int toplam = 0;
        //    for(int i=2;i<=n;i++)
        //    {
        //        toplam = birinci + ikinci;
        //        birinci = ikinci;
        //        ikinci = toplam;

        //    }
        //    return toplam;
        //}
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("kaçıncı fibonacciyi görmek istiyorsunuz? ");
        //    int sayı=Int32.Parse(Console.ReadLine());
        //    int fibo = Fibonacciyaz(sayı);
        //    Console.WriteLine(fibo);
        //}


        ///) Kendisine bir n parametresi alan ve fibonacciyi ilk n terime açan 

        //public static void Ntanefibonacci(int n)
        //{
        //    int birinci = 0;
        //    int ikinci = 1;
        //    int toplam = 0;
        //    if (n == 1)
        //    {
        //        Console.WriteLine(birinci);
        //    }
        //    else if (n == 2)
        //    {
        //        Console.WriteLine(birinci);
        //        Console.WriteLine(ikinci);
        //    }
        //    else
        //    {
        //        Console.WriteLine(birinci);
        //        Console.WriteLine(ikinci);
        //        for(int i=2;i<=n;i++)
        //        {
        //            toplam = birinci + ikinci;
        //            Console.WriteLine(toplam);
        //            birinci = ikinci;
        //            ikinci = toplam;


        //        }
        //    }
        //}
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("kaç tane terim girmek istiyorsunuz? ");
        //    int sayı=Int32.Parse(Console.ReadLine());
        //    Ntanefibonacci(sayı);

        //}



        //----------FİNAL SORULARI-------------
        //2.SORU
        //abc metodu yaz, girilen metini tarıyor kullanıcı metinin kendisini ya da bir kismini girerse true

        //public static bool ABC(string metin, string a)
        //{
        //    bool bayrak= true;
        //    if (metin.Length < a.Length)
        //    {
        //        return false;
        //    }
        //    for (int i = 0; i < a.Length; i++)
        //    {
        //        if (metin[i] == a[i])
        //        {
        //              bayrak= true;
        //        }

        //    }
        //    for(int i = 0; i < a.Length; i++)
        //    {
        //        if (metin[i] != a[i])
        //        {
        //            if (metin[i] != a[i])
        //            {
        //                bayrak= false;
        //            }
        //        }
        //    }
        //    return false;

        //}
        //static void Main(string[] args)
        //{
        //    string b = "emel ptgyi gecti";
        //    string e = "ptg";
        //    bool dogrumu = ABC(b, e);
        //    if (dogrumu == true)
        //    {
        //        Console.WriteLine("doğru");
        //    }
        //    else
        //    {
        //        Console.WriteLine("yanlış");
        //    }
        //    Console.ReadLine();
        //}


        //3.SORU

        //paragrafta XYZ görülen yerlere sırasıyla yazıyor

        //public static string AtifYerlestir(string metin, string[] atif)
        //{
        //    string metin2 = null;
        //    int index = 0;
        //    for (int i = 0; i < metin.Length - 2; i++)
        //    {
        //        if (metin[i] == 'X' && metin[i + 1] == 'Y' && metin[i + 2] == 'Z')
        //        {
        //            for (int j = 0; j < atif[index].Length; j++)
        //            {
        //                metin2 += atif[index[j]];
        //            }
        //            index++;
        //            i += 2;

        //        }
        //        else
        //        {
        //            metin2 += metin[i];
        //        }
        //    }
        //    metin2 += metin[metin.Length - 2];
        //    metin2 += metin[metin.Length - 1];
        //    return metin2;

        //}

        //    namespace IndiabixConsoleApplication
        //{
        //    class SampleProgram
        //    {
        //        static void Main(string[] args)
        //        {
        //            int i, j;
        //            int[,] arr = new int[2, 2];
        //            for (i = 0; i < 2; ++i)
        //            {
        //                for (j = 0; j < 2; ++j)
        //                {
        //                    arr[i, j] = i * 17 + i * 17;
        //                    Console.Write(arr[i, j] + " ");
        //                }
        //            }
        //        }
        //    }
        //}                              -------------- çıktı=  0 0 34 34------------

        //LASA SAYISI
        //static void Main(string[] args)
        //{
        //    bool flag = true;
        //    int sayı1=Int32.Parse(Console.ReadLine());
        //    for(int i = 2; i < sayı1; i++)
        //    {
        //        if(sayı1%i==0)
        //        {
        //            flag = false;
        //            break;
        //        }
        //        else
        //        {
        //            flag = true;
        //        }
        //    }
        //    int[] dizi = new int[10];
        //   int sayac = 0;
        //    while(sayı1>0)
        //    {
        //        dizi[sayac] = sayı1 % 10;

        //        sayı1 = sayı1 / 10;
        //        sayac++;
        //    }
        //    bool flag2 = true;
        //    int yenisayi = 0;
        //    for(int i=0;i<sayac;i++)
        //    {
        //        yenisayi = yenisayi * 10 + dizi[i];
        //    }
        //    for(int i = 2; i < yenisayi; i++)
        //    {
        //        if(yenisayi%i==0) {
        //            flag2 = false;
        //        }
        //    }
        //    if (flag2 == true && flag == true)
        //    {
        //        Console.WriteLine("lasa");
        //    }
        //    else
        //    {
        //        Console.WriteLine("lasa değil");
        //    }
        //    Console.ReadLine();
        //}
        //public static int alanh(int k, int m)
        //{
        //    int alan = k * m;
        //    return (alan); 


        //}
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("kısa gir: ");
        //    int kısa=int.Parse(Console.ReadLine());
        //    Console.WriteLine("uzun gir: ");
        //    int uzun=int.Parse(Console.ReadLine());
        //   int  alan= alanh(kısa,uzun);
        //    Console.WriteLine(alan);
        //    Console.ReadLine();
        //}


        //final xyz
        //public static string AtifYerlestir(string metin, string[] atif)
        //{
        //     string metin2 = "";
        //     int index = 0;
        //     for(int i = 0;i<metin.Length;i++) 
        //     {
        //         if (metin[i] == 'X' && metin[i + 1] == 'Y' && metin[i + 2] == 'Z')
        //         {
        //             for(int j = 0; j <atif[index].Length; j++)
        //             {
        //                 metin2 += metin[i];
        //             }
        //             index++;
        //             i += 2;
        //         }
        //         else
        //         {
        //             metin2 += metin[i];
        //         }
        //     }
        //     metin2 += metin[metin.Length - 2];
        //     metin2 += metin[metin.Length - 1];
        //     return metin2;

        //}
        //______ replace fonksiyonu elimizdeki değerleri belirtilen değerleri diğer değerlerle değiştirir.




        // split(" ")

        //public static string[] Ayrac (string metin,char ayrac)
        //{
        //    char[] list =new char[metin.Length];
        //    int sayac = 0;
        //    for(int i=0;i<metin.Length;i++)
        //    {
        //        list[i] = metin[i];
        //    }
        //    for(int i=0;i<metin.Length; i++)
        //    {
        //        if (list[i] == ayrac) 
        //        {
        //        sayac++;
        //        }
        //    }

        //    string[] list2 = new string[sayac + 1];
        //    int sayac2 = 0;
        //    string kelime = "";
        //    for(int i=0;i<metin.Length;i++)
        //    {
        //        if (list[i] == ayrac)
        //        {
        //            list2[sayac2] = kelime;
        //            sayac2++;
        //            kelime = "";

        //        }
        //        else if(i==metin.Length-1)
        //        {
        //            kelime += list[i];
        //            list2[sayac2] = kelime;
        //        }
        //        else
        //        {
        //            kelime += list[i];
        //        }
        //    }
        //    return list2;
        //}
        //static void Main(string[] args)
        //{
        //    string metin = "c# dersimize hosgeldiniz";
        //    char ayrac = ' ';

        //    string[] list = Ayrac(metin, ayrac);
        //    for(int i=0;i<list.Length;i++)
        //    {
        //        Console.WriteLine(list[i]);
        //    }
        //    Console.ReadLine();
        //}


        //public static void BubbleSort(int[] dizi)
        //{

        //    for(int i=0;i<dizi.Length;i++)//Bu döngü, sıralama işlemini yapacak olan dış döngüdür.
        //                                  //Dizinin her bir elemanı üzerinde işlem yapılacak ve bu işlem n kez tekrarlanacak.
        //                                  //Yani, dış döngü n kere çalışır.
        //    {
        //        for(int j = 0; j < dizi.Length- 1 ; j++)//u iç döngü, dizideki ardışık elemanları karşılaştırır ve gerekirse yer değiştirir.
        //                                                //İç döngü, dış döngü her çalıştığında bir eksik olur
        //                                                //çünkü her dış döngüde en büyük öğe sona yerleşmiştir.
        //        {
        //            if (dizi[j] > dizi[j + 1])
        //            {
        //                //elemanları yer değiştir
        //                int gecici = dizi[j];//Burada, gecici adında geçici bir değişken oluşturuluyor. Bu değişken, yer değiştiren iki elemandan birinin değerini geçici olarak saklar.
        //                                     //Bu, iki değerin takas yapılabilmesi için gereklidir.
        //                dizi[j] = dizi[j+1];
        //                dizi[j+1] = gecici; //bu satırda, dizi[j + 1] öğesine, geçici değişkende saklanan dizi[j] öğesinin değeri atanır.
        //                                    //Bu, yer değiştiren öğenin doğru pozisyona yerleştirilmesini sağlar.
        //            }
        //        }
        //    }
        //}
        //static void Main(string[] args)
        //{
        //    int []dizi = { 64, 34, 25, 22, 11, 90 };
        //    Console.WriteLine("başlangıç dizisi: ");
        //    for(int i=0;i<dizi.Length; i++)//Buradaki döngü, diziyi başlangıçta olduğu gibi ekrana yazdırır:
        //    {
        //        Console.WriteLine(dizi[i] + " ");
        //    }
        //    Console.WriteLine();
        //    BubbleSort(dizi);//Bu satırda, BubbleSort metodu çağrılır ve diziyi sıralamak için kullanılır. Yani burada diziyi sıralıyoruz.

        //    Console.WriteLine("sıralanmış dizi: ");
        //    for(int i=0;i<dizi.Length; i++)
        //    {
        //        Console.WriteLine(dizi[i]+" ");
        //    }
        //    Console.ReadLine();
        //}


        //public static void BubbleSort(int[] dizi)
        //{
        //    for(int i=0;i<dizi.Length;i++)
        //    {
        //        for(int j=0;j<dizi.Length-1;j++)
        //        {
        //            if (dizi[j] > dizi[j + 1])
        //            {
        //                int gecici = dizi[j];
        //                dizi[j] = dizi[j+1];
        //                dizi[j + 1] = gecici;
        //            }
        //        }
        //    }
        //}
        //static void Main(string[] args)
        //{
        //    int[] dizi = { 81, 74, 2, 54, 63, 1, 45, };
        //    for(int i = 0; i < dizi.Length; i++)
        //    {
        //        Console.WriteLine("elimizdeki dizi: ");
        //        Console.WriteLine(dizi[i]+" ");
        //    }
        //    Console.WriteLine();
        //    BubbleSort(dizi);
        //    for(int i=0; i<dizi.Length; i++)
        //    {
        //        Console.WriteLine("sıralanmış dizi: ");
        //        Console.WriteLine(dizi[i]+" ");
        //    }
        //    Console.WriteLine();
        //    Console.ReadLine();
        //}

        //int[] dizi = new int[5];
        //dizi[1]=5;
        //

            

        
    }
}

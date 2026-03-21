using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ptg
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //1 den n e kadar asal sayıları yazdırma
            //bool asalmi = false;
            //int sayac = 1;
            //while (sayac < 10)
            //{
            //    asalmi = false;
            //    sayac++;
            //    if (sayac == 2)
            //    {
            //        asalmi = true;
            //    }
            //    else
            //    {
            //        for(int i = 2; i<=sayac/2;i++)
            //        {
            //            if (sayac % i == 0)
            //            {
            //                asalmi = false;
            //            }
            //            else
            //            {
            //                asalmi = true;
            //            }
            //        }

            //    }
            //    if (asalmi == true)
            //    {
            //        Console.WriteLine(sayac);
            //    }
            //}


            //kendisine parametre olarak aldığı bir n sayısının asal olup olmadıgını belirleyen program:
            //Console.WriteLine("bir sayı giriniz");
            //int sayi=Convert.ToInt32(Console.ReadLine());
            //bool asalmi = true;
            //if (sayi <= 1)
            //{
            //    asalmi = false;
            //}
            //else if (sayi == 2)
            //{
            //    asalmi = true;
            //}
            //else
            //{
            //    for(int i = 2; i <= sayi; i++)
            //    {
            //        if (sayi % i == 0)
            //        {
            //            asalmi = false;
            //            break;
            //        }
            //    }
            //}
            //if(asalmi)
            //{
            //    Console.WriteLine("bu sayı asaldır");
            //}
            //else
            //    Console.WriteLine("asal değildir");


            //bir sayının asal çarpanlarını ekrana yazdıran program


            //klavyeden girilen sayı çifse ekrana yazdıran tekse hayır yazdıran program:
            //Console.WriteLine("sayı giriniz");
            //int sayi=Convert.ToInt32(Console.ReadLine());
            //if (sayi % 2 == 0)
            //{
            //    Console.WriteLine(sayi);
            //}
            //else
            //{
            //    Console.WriteLine("hayır");
            //}

            //vize final
            //Console.WriteLine("vize giriniz");
            //int vize=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("final giriniz");
            //int final=Convert.ToInt32(Console.ReadLine());
            //double puan = (vize * 0.4) + (final * 0.6);
            //if (puan > 50 && final > 50)
            //{
            //    Console.WriteLine("geçti");
            //}
            //else
            //{
            //    Console.WriteLine("kaldı");
            //}

            //1 ile 500 arasındaki sayıları toplayıp ekrana yazdıran
            //int toplam = 0;
            //for(int i = 0; i < 500; i++)
            //{
            //    toplam = toplam + i;
            //    Console.WriteLine(toplam);
            //}

            //klavyeden 10dan küçük bir sayı alan ve faktoriyel hesaplayan
            //Console.WriteLine("10dan küçük bir sayı giriniz");
            //int sayi=int.Parse(Console.ReadLine());
            //int fak = 1;
            //if (sayi < 10)
            //{
            //    for (int i = 1; i <= sayi; i++)
            //    {
            //        fak = fak * i;
            //        Console.WriteLine(fak);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("başka sayı giriniz");
            //}

            //1 den 9 a kadar çarpım tablosu yazan
            //int carp = 1;
            //for (int i = 1; i <= 9; i++)
            //{
            //    for (int j = 1; j <= 9; j++)
            //        carp = i * j;
            //    Console.WriteLine(carp);
            //}


            // pozitif tam bölenlerinin kendisi hariç toplamı kendisine eşit olan sayılar mükemmel sayıdır.
            //Console.WriteLine("bir sayı giriniz");
            //int top = 0;
            //int n=int.Parse(Console.ReadLine());
            //for (int i = 1; i < n; i++)
            //{
            //    if (n % i == 0)
            //    {
            //        top = top + i;
            //    }

            //}    
            //    if (top == n)
            //    {
            //        Console.WriteLine("mükemmel sayı");
            //    }
            //    else
            //    {
            //        Console.WriteLine("mükemmel sayı değil");
            //    }

            //girilen sayı çift ise yarısını tek ise 2 katını alarak yazdıran program:
            //Console.WriteLine("sayı giriniz");
            //int sayi=int.Parse(Console.ReadLine());
            //if (sayi % 2 == 0)
            //{
            //    sayi = sayi / 2;
            //    Console.WriteLine(sayi);
            //}
            //else
            //{
            //    sayi = sayi * 2;
            //    Console.WriteLine(sayi);
            //}


            //ikiz asallar(arasında 2 fark olup her ikisi de asal olan 2 sayı). 100 e kadar olan ikiz asallar
            //Console.WriteLine("ikiz asallar:");
            //for(int i = 2; i<100; i++)
            //{
            //    bool asalmi1 = true;
            //    bool asalmi2 = true;
            //    for(int j = 2; j <= i / 2; j++)
            //    {
            //        if (i % j == 0 )
            //        {
            //            asalmi1 = false;
            //            break;
            //        }
            //    }
            //    for(int j = 2; j <= (i + 2) / 2; j++)
            //    {
            //        if ((i + 2) % j == 0)
            //        {
            //            asalmi2= false;
            //            break;

            //        }
            //    }
            //    if (asalmi1 && asalmi2)
            //    {
            //        Console.WriteLine(i + "ve" + (i + 2) + "ikiz asallar");
            //    }
            //}

            //kullanıcının girdiği sayılar ikiz asal mi?
            //Console.WriteLine("birinci sayıyı giriniz");
            //int sayi1 = Int32.Parse(Console.ReadLine());
            //Console.WriteLine("ikinci sayıyı giriniz");
            //int sayi2 = Int32.Parse(Console.ReadLine());
            //bool asalmi1 = true;
            //bool asalmi2 = true;
            //for (int i = 2; i < sayi1; i++)
            //{
            //    if (sayi1 % i == 0)
            //    {
            //        asalmi1 = false;
            //    }
            //}
            //for (int i = 2; i < sayi2; i++)
            //{
            //    if (sayi2 % i == 0)
            //    {
            //        asalmi2 = false;
            //    }
            //}
            //if ((sayi1 == sayi2 + 2) || (sayi2 == sayi1 + 2) && asalmi1 && asalmi2)
            //{

            //    Console.WriteLine("ikiz asallar");
            //}



            //iki basamaklı asal sayıları ekrana yazdıran
            //Console.WriteLine("iki basamaklı asal sayılar:");
            //for(int i = 10; i < 100; i++)
            //{
            //    bool asalmi = true;
            //    for(int j = 2; j < i / 2; j++)
            //    {
            //        if (i % j == 0)
            //        {
            //            asalmi = false;
            //            break;
            //        }
            //    }
            //    if (asalmi)
            //    {
            //        Console.WriteLine(i+"");
            //    }


            //bir sayının asal çarpanlarını ekrana bastıran program:
            //Console.WriteLine("bir sayı giriniz");
            //int sayi=Int32.Parse(Console.ReadLine());
            //bool asalmi = true; 
            //for(int i=2;i<=sayi/2;i++)
            //{
            //    if (sayi % i == 0)
            //    {
            //          asalmi = true;
            //    }
            //    if (i == 2)
            //    {
            //        asalmi = true;
            //        Console.WriteLine(2);
            //    }
            //    else
            //    {
            //        for(int j = 2; j <= i / 2; j++)
            //        {
            //            if(i % j == 0)
            //            {
            //                 asalmi = false;
            //                break;
            //            }
            //            if (asalmi)
            //            {
            //                Console.WriteLine(i+"");
            //            }
            //        }
            //    }

            //}



            //kendisine parametre olarak alduğı bir n sayısının asal olup olmadıgını belirleyen program
            //Console.WriteLine("bir sayı giriniz");
            //int sayi = Int32.Parse(Console.ReadLine());
            //bool asalmi = true;
            //if (sayi <= 1)
            //{
            //    asalmi = false;
            //}
            //if (sayi == 2)
            //{
            //     asalmi = true;
            //}
            //for(int i = 2; i < sayi / 2; i++)
            //{
            //    if (sayi % i == 0)
            //    {
            //         asalmi = false;
            //        break;
            //    }
            //}
            //if (asalmi)
            //{
            //    Console.WriteLine("asal");
            //}
            //else
            //{
            //    Console.WriteLine("asal değil");
            //}


            //belirli bir aralıktaki asalları bulma(Alt sınır,üst sınır)
            //Console.WriteLine("alt sınır giriniz");
            //int alt = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("üst sınır giriniz");
            //int üst= Convert.ToInt32(Console.ReadLine());
            //for(int i = alt; i < üst; i++)
            //{
            //    bool asalmi = true;
            //    if (i <= 1)
            //    {
            //        asalmi = false;
            //    }
            //    if (i == 2)
            //    {
            //        asalmi = true;
            //    }
            //    for(int j = 2; j < i; j++)
            //    {
            //        if (i % j == 0)
            //        {
            //            asalmi = false;
            //            break;
            //        }
            //    }
            //    if (asalmi)
            //    {
            //        Console.WriteLine("{0}" + i);
            //    }



            //}




            //n basamaklı bir t Kaprekar sayısının karesi alınıp sağdaki n basamağı solda kalan n-1 basamağa eklendiğinde sonuç yine t sayısını verir.Örnek: 55, iki basamaklı bir sayıdır.



            //fibonacci dizisi
            //int a = 1;
            //int b = 1;
            //int c;

            //Console.WriteLine(a); /* 1 yazar */  /* aşağıdaki yorum satırında alt alta bir yazar dememin sebebi bu kısımdır */
            //Console.WriteLine(b); /* 1 yazar */

            //for (int i = 1; i <= 8; i++) /* 8 adet sayıyı üst üste toplayarak gidecektir. Eğer burada sekiz yerine faklı rakam yazarsak o kadar toplama yapacaktır. */
            /* ekran çıktısında başta alt alta 1 1 yazacaktır a ve b ye 1 değerlerine 1 verdiğimiz için */
            /* 1+1= 2 daha sonra her çıkan sonuçlar bir önceki değeri toplayarak gidecektir. */
            //{
            //    c = a + b;
            //    a = b;
            //    b = c;

            //    Console.WriteLine(c);
            //}

            //arkadas/dost sayı:iki sayının  kendisi hariç bölenleri toplamı birbirine eşitse.
            //Console.WriteLine("birinci sayıyı giriniz");
            //int sayi1=Int32.Parse(Console.ReadLine());
            //Console.WriteLine("ikinci sayıyı giriniz");
            //int sayi2=Int32.Parse(Console.ReadLine());
            //int  toplam1 = 0;
            //int toplam2 = 0;
            //for (int i = 1; i < sayi1; i++)
            //{
            //    if (sayi1 % i == 0)
            //    {
            //        toplam1 = toplam1 + i;
            //    }
            //}
            //for(int i = 1; i < sayi2; i++)
            //{
            //    if (sayi2 % i == 0)
            //    {
            //        toplam2 = toplam2 + i;
            //    }
            //}
            //if (toplam1 == toplam2)
            //{
            //    Console.WriteLine("dost sayı");
            //}
            //else
            //{
            //    Console.WriteLine("dost sayı değil");
            //}



            
               









            Console.ReadLine();





        }
    }
}


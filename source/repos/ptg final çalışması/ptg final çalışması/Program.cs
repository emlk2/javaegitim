using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ptg_final_çalışması
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        // diziler
        //random
        //cok boyutlu dizi
        //metotlar
        //string operasyonları
        //dosya açma kapama
        //hazır string metotları substring, split , equals, getlength

        //-----------------------------------------
        //**DİZİLER**
        //aynı tipte birden fazla veriyi tuttugumuz veri kümesi

        ////int[] yaslar = new int[25];
        //int[] x = new int[8] { 1, 2, 3, 5, 6, 7, 4, 99 };
        //int[] x = { 2, 4, 7, 8, 9, };


        //////string[] isimler = new string[25];

        ////yaslar[3] = 18;
        ////yaslar[4] = 19; //her elemana değer atamak zorunda değilim , ben atamazsam varsayılan değer atanır(0)
        //                //elemanlar 0,1,....24
        //Console.WriteLine(yaslar[3]); //18

        //for (int i = 0; i < 25; i++) 
        //{
        //    yaslar[i] =i *2;
        //}
        //for (int i = 0;i < 25; i++)
        //{
        //    Console.WriteLine(yaslar[i]);
        //}
        //birinci forda elemanları doldurdu, ikinci forda hepsini ekrana yazdırdı

        //***foreach döngüsü
        //int[] sayilar = { 7, 8, 9, 2, 5, 6, 3, };
        //foreach(var item in sayilar)
        //{
        //    Console.WriteLine(item);
        //}
        //item=emel de olabilir.


        //** try-catch

        //try
        //{
        //    Console.WriteLine("lütfen sayı girin: ");
        //    int sayi = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine(sayi+"girdiniz");
        //}
        //catch
        //{
        //    Console.WriteLine("hata alındı , lütfen sayısal veri girin");
        //}

        //try
        //{

        //        Console.WriteLine("lütfen sayı girin: ");
        //        int sayi = Convert.ToInt32(Console.ReadLine());
        //        Console.WriteLine(sayi+"girdiniz");
        //}
        //catch(Exception ex) 
        //{
        //    Console.WriteLine(ex.Message);
        //}

        //try
        //{

        //}
        //catch(FormatException )
        //{
        //    throw;
        //}
        //catch (NullReferenceException)
        //{
        //    throw;
        //}
        //catch(Exception ex)
        //{
        //    throw;
        //}



        //ÇOK BOYUTLU DİZİLER

        //int[,] dizi1 = new int[2, 2];
        //dizi1[0, 0] = 25;
        //dizi1[0, 1] = 35;
        //dizi1[1, 0] = 18;
        //dizi1[1, 1] = 21;

        //for (int i = 0; i < 2; i++)
        //{
        //    for (int j = 0; j < 2; j++)


        //        Console.Write(" " +dizi1[i, j]);
        //        Console.WriteLine();
        //    //buraya bak nasıl matris şeklinde yazılır
        //}


        //***METOTLAR***

        //Aynı kodu sürekli yazmaktansa bir kere yazıp her yerde o metotu cagırabiliriz.
        //Erişim belirleyici+ dönüş tipi+ metot adı+(parametre listesi) {}
        //mutlaka class içinde olmalılar.

        //erişim b-belirleyici=public , private, static 
        //dönüş tipi=
        //return olmalı

        //geriye değer döndürmeyen ve parametre almayan metot:

        //private static void metotadi()
        //{ }











        //    Console.ReadLine();

        //}


        //metot örnek:

        //      private static void alan()
        //     {
        //        int uk, kk, alann;
        //        Console.WriteLine("uzun  kenarı giriniz: ");
        //        uk=Convert.ToInt32(Console.ReadLine());
        //        Console.WriteLine("kısa kenarı giriniz");
        //        kk=Convert.ToInt32(Console.ReadLine());
        //        alann = kk * uk;
        //        Console.WriteLine(alann);


        //     }
        //    private static void cevre()
        //    {
        //        int uk, kk, cevre;
        //        Console.WriteLine("uzun  kenarı giriniz: ");
        //        uk = Convert.ToInt32(Console.ReadLine());
        //        Console.WriteLine("kısa kenarı giriniz");
        //        kk = Convert.ToInt32(Console.ReadLine());
        //        cevre = 2 * (uk + kk);

        //    }

        //    static void main(string[] args) 
        //    {
        //        Console.WriteLine("ne hesaplamak istersiniz? (alan=1/cevre=2");
        //        int istenenislem;
        //        istenenislem = Convert.ToInt32(Console.ReadLine());
        //        if (istenenislem == 1)
        //        {
        //            alan();
        //        }
        //        else if(istenenislem == 2)
        //        {
        //            cevre();

        //        }
        //        else
        //        {
        //            Console.WriteLine("1 veya 2 giriniz");
        //        }
        //    }

        //geriye değer döndürmeyen, parametre alan:
        //geriye değer döndürmeyen=void

        //private static void Toplam(int a, int b)
        //{
        //    int topla = a + b;
        //    Console.WriteLine("sayıların toplamı: "+ topla);
        //}

        //static void Main(string[] args)
        //{
        //    int s1, s2;
        //    Console.WriteLine("1.sayıyı gir: ");
        //    s1=Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine("2.sayıyı gir: ");
        //    s2=Convert.ToInt32(Console.ReadLine());
        //    Toplam(s1, s2);

        //}

        //3)geriye değer döndüren ve parametre almayan

        //public static int benimmetodum()
        //{

        //    //return şart(void olmadığı sürece)
        //    int a, b;
        //    a=Convert.ToInt32(Console.ReadLine());
        //    b=Convert.ToInt32(Console.ReadLine());
        //    int sonuc = a + b;
        //    return sonuc;

        //}

        //4) geriye değer döndüren ve parametre alan:

        //public int emel(int a, int b)
        //{
        //    int sonuc = a * b;
        //    return sonuc;

        //}

        //-------------------------------------------------

        //  STRİNG METOTLARI

        //static void Main(string[] args)
        //{
        //    string abc = "Programlama Tekniklerine Giriş";
        //    string[] metin = abc.Split(',');
        //    char[] karakter=abc.ToCharArray();
        //    Console.WriteLine(karakter[0]);
        //    //"Split" içine girilen sembolü yazının içinde gördüğü an orayı ayırır, parçalar.

           // Console.WriteLine(abc.Substring(4,4));
            //Substring (x,y) x kadar karakteri silip , y kadarını ekrana yazdırır.

            //string x = "abc";
            //string y = "abc";
            //if (x.Equals(y))
            //{
            //    Console.WriteLine("aynı string");
            //}
            //else
            //{
            //    Console.WriteLine("farklı string");
            //}



            //**** Kullanıcıdan bir string ve bir int alan, 0'dan başlayıp girilen sayıya kadar elemanları
            //çöpe atıp gerisini yazan?

            //Console.WriteLine("bir metin  giriniz: ");
            //string girilen = Console.ReadLine();
            //Console.WriteLine("x gir");
            //int x=Int32.Parse(Console.ReadLine());
            //Console.WriteLine("y gir");
            //int y=Int32.Parse(Console.ReadLine());

            //string dizi=Sub2(girilen, x, y);


            // (x,y) kadar silen ve yazan
            // x.Equal metodu






        //}


        //public static string Cop(String abc)
        //{
        //    Console.WriteLine("kaç tane elaman silmek istiyorsunuz? ");
        //    int sayi=Convert.ToInt32(Console.ReadLine());
        //    char[] karakterler=new char[abc.Length];
        //    for (int i = 0; i < abc.Length; i++)
        //    {
        //        if (i >= sayi)
        //        {
        //            karakterler[i] = abc[i];

        //            Console.WriteLine(karakterler[i]);
        //        }
        //    }
        //    Console.WriteLine(" ");
        //    return abc;
        //}

        //public static string Sub2 (string metin1, int a, int b)
        //{
        //    Console.WriteLine("x, y gir: ");
        //    string metin = Console.ReadLine();
        //    char[] karakterler=new char[metin.Length];
        //    for (int i = 0; i < metin.Length; i++)
        //    {
        //        karakterler[i] = metin[i];
        //    }
        //    int x=Convert.ToInt32(Console.ReadLine());
        //    int y=Convert.ToInt32(Console.ReadLine());
        //    for (int i = a; i < a+b; i++)
        //    {
        //        Console.WriteLine(metin1[i]);
        //    }
        //    Console.WriteLine( " ");
        //    return metin1;
        //}


}
}

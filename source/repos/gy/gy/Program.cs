 using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gy
{
     class Program
    {
        static void Main(string[] args)
        {

            //string Ad;
            //int Yas;
            // char BasHarf;
            //bool EvliMi;


            // Değişken Tanımlama Varyasyonları
            //string Ad; 
            //Ad = "emel";
            //string Soyad="kılıç";


            //string Ad, Soayd, Memleket;
            //Ad = "emel";
            //Soyad = "kılıç";
            //Memleket = "samsun";

            //int x = 5;
            //int y = 15;
            //int z = x + y;

            //tip çevrimleri
            //string x = "123";
            //int y=Convert.ToInt32(x);

            //Console.WriteLine(y);
            //Console.Read();

            //double x = 123.2;
            //string y=Convert.ToString(x);
            //Console.WriteLine(y);

            //bool x = true;
            ///string y=Convert.ToString(x);
            //Console.WriteLine(y);

            //string x = "true";
            //bool y=Convert.ToBoolean(x);
            //Console.WriteLine(y);
            //Console.Read();

            //string x = "546";
            //int y =int.Parse(x);
            //Console.WriteLine(y);
            //Console.Read();

            //int x = 125;
            //char y=Convert.ToChar(x);
            //Console.WriteLine(y);

            //int x = 15;
            //--x;
            //x--;
            //Console.WriteLine(x);

            //int x = 15;
            //int y = 25;
            //bool sonuc = x < y;

            //int sayi1 = 10; 
            //int sayi2 = 20;
            //bool sonuc = (sayi1 > sayi2) && (sayi2 %5 == 3);
            //-----------------------------------------------------------------------------
            //true&&true=true
            //true&&false=false
            //false&&true=false
            //false&&false=false

            //true||true=true
            //true||false=true
            //false||true=true
            //false||false=false

            //Console.WriteLine(sonuc);
            //Console.Read();
            //---------------------------------------------------------------------------
            //switch-case

            /*int x = 10;
            switch(x) (parantez ici string ise case blokları tırnak icinde)
            {
                case 3:
                    Console.WriteLine("deger")
                    break;

                case 10:
                    Console.WriteLine("değer:10");
                    break;

                    //hicbiri dogru degise default
                default:
                    Console.WriteLine("her durumda");
                    break;
                
                case 10:go to case3
                     

           */ //}

            //int x = 5;
            //int y = 15;
            //-----------------------------------------------------
            //if(x>y)
            //{
            //    //eğer koşul doğruysa burası çalışır.
            //}
            //else
            //{

            //    //eğer koşul yanlışsa burası çalışır.
            //}

            //Console.Read();
            //-----------------------------------------
            //bool yağmur = true;
            //if (!yağmur)
            //{
            //    Console.WriteLine("şemsiye al");
            //}
            //else
            //{
            //    Console.WriteLine("şemsiye alma");
            //}
            //Console.Read();
            //------------------------------------------------------------------
            //if(true)
            //{

            //}
            //else if(true)
            //{

            //}
            //else if(true)
            //{

            //}
            //else
            //{

            //}
            //-----------------------------------------------------

            //for döngüsü
            //for(int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(i);
            //}

            //int i = 0;
            //for (i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(i);
            //}
            //for(int i = 0; true; i++) //sonsuz döngü
            //{
            //    Console.WriteLine(i);
            //}
            //Console.Read();
            //for(; ; ) //sonsuz döngü
            //{
            //    Console.WriteLine("emel");


            //} 
            //--------------------------------------------------------------------------------
            ////while döngüsü
            //bool kosul = true;
            //int sayac = 1;
            //while (kosul)
            //{
            //    if (sayac <= 100)
            //    {
            //        Console.WriteLine("emel");
            //    }
            //    else
            //        kosul = false;
            //    sayac++;

            //}
            //Console.Read
            //--------------------------------------------------------------------------------
            //do while

            //bool kosul = true;                 //(önce şarta bakıp sonra yazdırır)
            //while (kosul)
            //{
            //    Console.WriteLine("emel");
            //    kosul = false;
            //}

            //bool kosul = false;           //(şarta bakmadan çalıştırır)
            //do
            //{
            //    Console.WriteLine("emel");
            //}
            //while (kosul);
            //-------------------------------------------------------------------

            //breakcontinuereturn

            //break komutu switch ve döngülerde kullanılmaktadır.
            //for(int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine(i);
            //    if(i==5)
            //    break;
            //}
            //for(int i = 0; i < 10; i++)
            //{
            //    if (true)
            //    {
            //        if (true)
            //        {
            //            if (true)
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}

            //for(int i = 0; i < 10; i++)
            //{
            //    for(int j = 0; j < 10; j++)
            //    {
            //        break;
            //        Console.WriteLine(j+"-j");
            //    }
            //    Console.WriteLine(i+ "-i");
            //}
            //---------------------------------------------------------------------

            //continue
            //bool kosul = true;
            //int sayac = 1;
            //while (kosul)
            //{
            //    if (sayac == 6)
            //    {
            //        sayac++;
            //        continue;
            //    }
            //    Console.WriteLine(sayac);
            //    sayac++;
            //        if (sayac == 15)
            //    {
            //        kosul = false;
            //    }
            //}
            //-------------------------------------------------------------------------------

            //array sınıfı

            int[] sayilar = new int[5];
            sayilar[0] = 21;
            sayilar[1] = 12;
            sayilar[2] = 13;
            sayilar[3] = 25;
            sayilar[4] = 16;

            //özellikler
            //IsFixedSize=dizideki eleman sayısının sabit olup olmadığını bilditrir.
            //Console.WriteLine(sayilar.IsFixedSize);

            //IsReadOnly=dizideki elemanların sadece okunabilir olup olmadığını belirtir.
            //Console.WriteLine(sayilar.IsReadOnly);

            //Length=dizinin eleman sayısını verir.
            //Console.WriteLine(sayilar.Length);

            //Rank=dizinin boyutunu verir.
            //Conaole.WriteLine(sayilar.Rank);

            //metodlar:
            //Clear=dizinin elemanlarının değerini varsayılan yapar.
            for(int i = 0; i < sayilar.Length; i++)
            {
                Console.WriteLine(sayilar[i]);
            }
            Array.Clear(sayilar, 0, sayilar.Length);
            for(int i=0;i<sayilar.Length; i++)
            {
                Console.WriteLine(sayilar[i]);
            }

            Console.Read();























        }
    }
}

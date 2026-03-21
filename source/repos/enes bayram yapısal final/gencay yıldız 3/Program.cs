using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gencay_yıldız_3
{
    //27) Destructor ve Garbage Collector
    //: nesne bellekten silinirken son kez tetiklenen metot.

    //internal class Program
    //{

    //    static void Main(string[] args)
    //    {
    //        27)
    //            for (int i = 0; i < 10; i++)
    //        {
    //            new Ornek(i);
    //        }
    //        Console.WriteLine("***");
    //        GC.Collect(); //referanssız nesneleri siler.
    //        Console.ReadLine();
    //        --------------------------------------------------------------

    //        Ornek referans1 = new Ornek();
    //        referans1.değer = 123;
    //        Console.WriteLine(referans1.Gonder().değer;

    //        if (referans1.Equals(referans1.Gonder()))
    //        {
    //            Console.WriteLine("nesneler aynı");
    //        }
    //        else
    //        {
    //            Console.WriteLine("farklı");
    //        }
    //        Console.ReadLine();


    //    }
    //}

    //public class Ornek
    //{

        //int numara;
        //public Ornek(int id) //yapıcı metot /constructer
        //{
        //    numara = id;
        //    Console.WriteLine(numara+"nesne üretildi.");
        //}
        //~Ornek()//desructor /yıkıcı metot.
        //{
        //    Console.WriteLine(numara+"nesne siliniyor.");
        //}
        //-------------------------------------------------------------------------------------

        //    //28)THİS ANAHTAR SÖZCÜĞÜ
        //    //: ilgili sınıfta o sınıftan türet,lmiş sözcükleri o sınıfta temsil etmemizi sağlayan bir anahtar sözcük.

        //    public int değer { get; set; }
        //    int x;
        //    public Ornek Gonder(int  x)
        //    {
        //        this.x = x;
        //        //return new Ornek();= aynısı aşagıda.
        //        return this;

        //    }
        //}
        //------------------------------------------------
        //28)INDEXER
        //[]
        //publiv ya da private olmalı.

        class Ornek
    {
        #region
        string[] Isımler = new string[10];
        public string this[int i]//parantez içinde bir parametre olmalı.
        {
            get
            {
                return Isımler[i];
            }
            set
            {
                Isımler[i] = value;
            }
        }
        #endregion
    }
    class pp
    {
        #region
        static void Main(string[] args)
        {
            Ornek örnek1=new Ornek();
            örnek1[5] = "emel";
            örnek1[2] = "alperen";

            Console.WriteLine(örnek1[5]);
            Console.WriteLine(örnek1[2]);
            #endregion

            Console.ReadLine();
        }


    }
    //--------------------------------------------------------------------------------------
    //30KOLEKSİYON MANTIĞI VE ARRAY LİST

    //KOLEKSİYON: koleksiyon ve dizi arasında teknik farklar var.

    //dizilerde olumsuz noktalar :  * Eleman sayısını önceden belirtmemiz gerekiyor.
    //                              *Elemanları atamasak dahi bellekte yerleri ayrılıyor.
    //                              * Performans açısından zayıf.
    //                              * Eleman atarken yahut okurken kod maaliyeti...
    //Bu olumsuzları yoktur koleksiyonların.

    //İlk oluşturulan koleksiyon yapısı=ARRAY LİST

    class video30
    {
        static void Main(string[] args)
        {
            int[] Sayilar = new int[25];

            ArrayList liste = new ArrayList();
            liste.Add("emel");//Add koleksiyona eleman atamayı sağlayan bir hazır metot.
            liste.Add("alperen");

            //dizilerde değer atarken de değeri çağırırken de [](indexer kullanırız.Ama
            //koleksiyonlarda değer atarken-> Add() , değeri çağırırken -> []


            liste.Add(123);
            liste.Add(true);
            liste.Add('b');
            foreach (var item in liste)
            {
                if (item is int)//item int ise:
                {
                    Console.WriteLine((int)item + 120);
                }
            }
            //tek tek tüm konrtolleri sağlamak zorunda kalııyoruz bunları aşabileceğimiz =LİST., Generic List.
            //dizi gibi ama dizinin olumsuz yanlarını olumluya ceviriyor.

            //31)LİST  :İçerisine sadece belirlediğimiz tipte değerler alır.

            List<int> liste2 = new List<int>();
            liste2.Add(34);
        }
    }
        
}

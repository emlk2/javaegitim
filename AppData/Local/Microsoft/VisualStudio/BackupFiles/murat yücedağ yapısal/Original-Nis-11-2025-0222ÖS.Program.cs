using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace murat_yücedağ_yapısal
{
    internal class Program
    {
        //1) public class Araba
        //{
        //    public string renk;
        //    public int modelyili;
        //    public string vites;
        //    public int plaka;
        //}
        //static void Main(string[] args)
        //{
        //    Araba arabasinifi = new Araba();
        //    arabasinifi.renk = "mavi";
        //    arabasinifi.modelyili = 2018;
        //    arabasinifi.vites = "otomatik";
        //    arabasinifi.plaka = 55;

        //    Console.WriteLine("Araba rengi: " + arabasinifi.renk);
        //    Console.WriteLine("Araba model yılı: " +arabasinifi.modelyili);
        //    Console.WriteLine("Araba vitesi: "+ arabasinifi.vites);
        //    Console.WriteLine("Plaka: "+ arabasinifi.plaka);

        /*get veriyi alır,set üzerinde değişiklik yapar.
         * amaç dogrudan erişimi engellemek.
       */
        //---------------------------------------------------------------------------------
        //2)private class Ogrenci
        //{
        //      private string ad;
        //      private string soyad;
        //      private int yas;
        //      private string alan;

        //      public string ADI
        //      {
        //          get { return ad; } // klavyeden girilecek olan ad değerini döndür.
        //          set { ad=value; }  //klavyeden girilen ad değerini aktardı.

        //      }
        //      public string SOYADI
        //      {
        //          get { return soyad; }
        //          set { soyad = value; }
        //      }
        //      public string ALANI
        //      {
        //          get { return alan; }
        //          set { alan=value; }
        //      }
        //      //yası 18den küçük olanları 18 alsın.
        //      public int YASI
        //      {
        //          get { return yas; }
        //          set
        //          {
        //              if (value < 18)
        //              {
        //                  yas = 18;
        //              }
        //              else
        //              {

        //                  yas = value;  //klavyeden girilen yas değer olmus oldu.
        //              }
        //          }
        //      }





        //}

        //  static void Main(string[] args)
        //  {
        //      Ogrenci ogrsinif = new Ogrenci();
        //      ogrsinif.ADI = "emel";
        //      ogrsinif.SOYADI = "kılıç";
        //      ogrsinif.YASI = 17;
        //      ogrsinif.ALANI = "sayısal";

        //      Console.WriteLine("öğrencinin adı: " +ogrsinif.ADI);
        //      Console.WriteLine("öğrencinin soyadı: "+ogrsinif.SOYADI);
        //      Console.WriteLine("öğrencinin yaşı: "+ogrsinif.YASI);
        //      Console.WriteLine("öğrencinin alanı: "+ogrsinif.ALANI);
        //      Console.ReadLine();
        //  }

        public class Emlak
        {
            private string semt;
            private string renk;
            private int odasayi;
            private int katno;
            private double alan;

            public string SEMTİ
            {
                get { return semt; }
                set { semt = value.ToUpper(); } //büyük harf
            }
            public string RENGİ
            {
                get { return renk; }
                set { renk=value.ToLower(); } //küçük harf
            }
            public int ODASAYİSİ
            {
                get { return odasayi; }
                set { odasayi=Math.Abs(value); }//mutlak değerle yaz.
            }
            public int KATNO
            {
                get { return katno; }
                set { katno = Math.Abs(value); }
            }

            public double ALAN
            {
                get { return alan; }
                set { alan= Math.Abs(value); } 
            }

        }

        static void Main(string[] args)
        {
            Emlak emlak = new Emlak();

            emlak.SEMTİ = "atakum";
            emlak.RENGİ = "beyaz";
            emlak.KATNO = 5;
            emlak.ODASAYİSİ = 6;
            emlak.ALAN = 450;

            Console.WriteLine("semt: "+ emlak.SEMTİ);
            Console.WriteLine("renk: " + emlak.RENGİ);

            Console.WriteLine("alan : " + emlak.ALAN);
            Console.WriteLine("oda sayısı : " + emlak.ODASAYİSİ);
            Console.WriteLine("katno : " + emlak.KATNO);

            Console.ReadLine();
        }
    }
}

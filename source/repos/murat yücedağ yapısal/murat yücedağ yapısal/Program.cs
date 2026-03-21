using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace murat_yücedağ_yapısal
{
    //internal class Program
    //{
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
    //-----------------------------------------------------------------------
    //3) public class Emlak
    //{
    //    private string semt;
    //    private string renk;
    //    private int odasayi;
    //    private int katno;
    //    private double alan;

    //    public string SEMTİ
    //    {
    //        get { return semt; }
    //        set { semt = value.ToUpper(); } //büyük harf
    //    }
    //    public string RENGİ
    //    {
    //        get { return renk; }
    //        set { renk=value.ToLower(); } //küçük harf
    //    }
    //    public int ODASAYİSİ
    //    {
    //        get { return odasayi; }
    //        set { odasayi=Math.Abs(value); }//mutlak değerle yaz.
    //    }
    //    public int KATNO
    //    {
    //        get { return katno; }
    //        set { katno = Math.Abs(value); }
    //    }

    //    public double ALAN
    //    {
    //        get { return alan; }
    //        set { alan= Math.Abs(value); } 
    //    }

    //}

    //static void Main(string[] args)
    //{
    //    Emlak emlak = new Emlak();

    //    emlak.SEMTİ = "atakum";
    //    emlak.RENGİ = "beyaz";
    //    emlak.KATNO = 5;
    //    emlak.ODASAYİSİ = 6;
    //    emlak.ALAN = 450;

    //    Console.WriteLine("semt: "+ emlak.SEMTİ);
    //    Console.WriteLine("renk: " + emlak.RENGİ);

    //    Console.WriteLine("alan : " + emlak.ALAN);
    //    Console.WriteLine("oda sayısı : " + emlak.ODASAYİSİ);
    //    Console.WriteLine("katno : " + emlak.KATNO);

    //    Console.ReadLine();
    //}
    //----------------------------------------------------------
    //4) KALITIM
    //Yolcunun kendi bilgilerini doldurduktan sonra hangi ucusu seçmis ise o bilgileri de altına ekleyecegiz.

    //yolcu , uçak sınıfı

    //public class Yolcu
    //{
    //    public string ad { get; set; }
    //    public string soyad { get; set; }
    //    public int yas { get; set; }
    //    public string cinsiyet { get; set; }
    //}

    //public class ucak : Yolcu
    //{

    //    public string marka { get; set; }
    //    public string kalkis { get; set; }
    //    public string varis { get; set; }


    //}
    //static void Main(string[] args)
    //{
    //    ucak UCAK = new ucak();
    //    UCAK.marka = "thy";
    //    UCAK.varis = "samsun";
    //    UCAK.kalkis = "bursa";
    //    UCAK.ad = "emel";
    //    UCAK.soyad = "kılıç";
    //    UCAK.yas = 20;
    //    UCAK.cinsiyet = "kadın";

    //    Console.WriteLine("ucagın markası= " + UCAK.marka);
    //    Console.WriteLine("varis: "+ UCAK.varis);
    //    Console.WriteLine("kalkis: "+UCAK.kalkis);
    //    Console.WriteLine("yolcunun adı: "+UCAK.ad);
    //    Console.WriteLine("yolcunun soyadı: "+UCAK.soyad);
    //    Console.WriteLine("yolcunun yaşı: "+UCAK.yas);
    //    Console.WriteLine("yolcunun cinsiyeti: " + UCAK.cinsiyet);

    //    Console.ReadLine();
    //}

    //----------------------------------------------------------------------------------Ğ
    //ÇOK BİÇİMLİLİK

    //public class İnsan
    //{
    //    public virtual void selamver() //virtual, metotlara uygulanan bir anahtar sözcük.  void =boş

    //    {
    //        Console.BackgroundColor = ConsoleColor.Green;
    //        Console.Title = "11.04.2025";
    //        Console.WriteLine("merhaba");
    //    }
    //}

    //    public class Türk : İnsan
    //    {
    //        public override void selamver()   //override selamveri gecersiz kıldı , artık selamvere insan sınfıında yazdıgımız değil burada
    //                                          //yazdıgımız calısacak. kalıtımla birleştirdik.
    //        {
    //            Console.WriteLine("merhaba");
    //        }
    //    }
    //    public class Fransız : İnsan
    //    {
    //        public override void selamver()
    //        {
    //            Console.WriteLine("Bonjour");
    //        }
    //    }

    //    public class İngiliz : İnsan
    //    {
    //        public override void selamver()
    //        {
    //            Console.WriteLine("Hello");
    //        }
    //    }

    //    static void Main(string[] args)
    //    {
    //        Fransız fr = new Fransız();
    //        fr.selamver();
    //    Türk tr = new Türk();
    //    tr.selamver();                 //bonjour ve merhaba yazdırır.





    //        Console.ReadLine();
    //    }




    //}


    //------------------------------------------------------------------------------
    //REF-OUT 


    //* Main metodu içerisinde tanımlayarak 

    //A YI 1 ARTIRDI
    // internal class Program 
    //{ 
    //    static void arttir (ref int s)
    //    {
    //        s++;
    //    }

    //    static void Main(string[] args)
    //    {
    //        int a = 10;
    //        arttir( ref a);
    //        Console.WriteLine(a);

    //        Console.ReadLine();
    //    }
    //----------------------------------
    // static void arttir (out  int s)        //SYİ 1 ARTIRDI
    //{
    //    s = 20;
    //    s++;
    //}

    //static void Main(string[] args)
    //{
    //    int a = 10;
    //    arttir (out a);
    //    Console.WriteLine(a);

    //    Console.ReadLine();
    //}

    // }


    //---------------------------------------
    //overload
    //amaç: kod daha esnek daha düzgün ,profesyonel olsun diye.
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Matematik a = new Matematik();
    //    }
    //}

    //class Matematik
    //{
    /*metot isimleri aynı olmalıdır.
     * metotların parametre sayıları farklı olmalıdır.
     * yoksa:
     * parametre tipleri farklı olmalıdır.
     * yoksa:
     *parametre sıraları farklı olmalıdır.
     */
    //    public double Topla(int a,double b)
    //    {
    //        return a + b;
    //    }
    //    public double Topla(double b ,int  a)
    //    {
    //        return a + b;
    //    }
    //}

    // metot override (metot ezme)

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Kedi siyam=new Kedi();
    //        siyam.Ses();
    //        Kopek pitbul=new Kopek();
    //        pitbul.Ses();
    //        Console.ReadLine();
    //    }
    //}

    //class Canli
    //{
    //    public  virtual void Ses()//(virtual : asıl metot)
    //    {
    //        Console.WriteLine("***********");
    //    }
    //}
    //class Kopek:Canli
    //{
    //    public override void Ses()
    //    {
    //        Console.WriteLine("HAV");
    //    }
    //}
    //class Kedi:Canli
    //{
    //    public  override void Ses()
    //    {
    //        Console.WriteLine("MİYAV");
    //    }
    //}

   
    

   




}

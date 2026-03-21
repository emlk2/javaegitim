using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace generic_class
{
    //normal classtan daha da genel class tüm yapılar aynı.

    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        MyClass <Ornek,Emel> m1=new MyClass<Ornek,Emel>();
    //    }
    //}

    //class MyClass<T,A>//bu sınıftan oluşturulacak nesneelrde farklı bir sınıf tipini temsil edecek.
    //{
    //    T t;
    //    A a;
    //    public void x()
    //    {
    //        //a=new A();//A tipinde nesne oluşturulamama ihtimali var ,Adan nesne üretmeyi engelledi,kesinlikle public mi?

    //    }

    //}
    //class Ornek
    //{

    //}
    //class Emel
    //{

    //}
    //----------------------------------------------------------------------------
    //GENERİC BASE CLASS CONNSTRAİT


    //T parametresi B veya B den türeyen sınıfları refere edebilsin gibi gibi..
    //bir koşul varsa where komutu
    //class programm
    //{
    //    static void Main(string[] args)
    //    //{
    //    //    MyClass<A> m1 = new MyClass<A>();
    //    //    MyClass<B>m2= new MyClass<B>();
    //    //    MyClass<C>m3= new MyClass<C>();

    //    //    MyClass<D>m4= new MyClass<D>();//Hata alırız çünkü ne A ne de Adan kalıtım alıyor. Base class c.T parametresinin refere edebileceği tipleri Hiyerarşik olarak belirler 
    //    }
    //}
    //class MyClass<T> where T:A //T tipi A veya A dan türeyen sınıfları temsil edebilir dedik.
    //{
    //    T t;
    //    public void x()
    //    {

    //    }

    //}
    //class A
    //{

    //}
    //class B : A
    //{

    //}
    //class C : B
    //{

    //}
    //class D
    //{

    //}
    //---------------------
    //GENERİC CLASS NEW CONSTRAİT
    //Generic classtaki parametreye gelen sınıf oluşturup oluşturamayacağımızın kısıtlamasıdı.

    //class MyClass<T> where T : new()//T tipi A veya A dan türeyen sınıfları temsil edebilir dedik.
    //{                //yaparsak hata kalkar.

    //    public void x()
    //    {
    //        //T t1 = new T();//hata verir, bu tip static(nesnne üretilemeyen9,constructer.. olabilir o yüzden engelliyor
    //                       //new constrait ile bunun nesne alabilen olduğunu kanıtlayacağız!



    //    }

    //}

    ////not 
    //class sınıf <T>where T:class,new() //hem base class constrait hem new class c. yapabiliriz.
    //{

    //}
    //-------------------------------------------------------------------------------------------

    //ÖRNEKLER:
    //class örnekler 
    //{ 
    //    static void Main (string[] args)
    //    {
    //        string s1 = "merhaba";
    //        string s2 = "merhaba";

    //        Console.WriteLine(  s1==s2);

    //        Console.ReadLine();
    //    }
    //}


    //class örnekler
    //{
    //    static void Main(string[] args)
    //    {
    //        string degistirilecek = ".";
    //        string metin = "bugun.güzel.bir.gün";
    //        string degistir = "";
    //        string sonuc;
    //        sonuc = metin.Replace(degistirilecek, degistir);
    //        Console.WriteLine(sonuc);




    //        Console.ReadLine();
    //    }




    //class örnekler
    //{
    //    public static bool BosMu(string s)
    //    {
    //        if (s == null)
    //            return true;

    //        try
    //        {
    //            char temsil = s[0];
    //        }
    //        catch (IndexOutOfRangeException)
    //        {
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //        return false;
    //    }

    //    static void Main(string[] args)
    //    {
    //        string metin = "";
    //        Console.WriteLine(BosMu(metin));
    //        Console.ReadLine();
    //    }

    //}
    //public static bool MyEquals(string s1, string s2)
    //{
    //    // İkisi de null ise eşittir
    //    if (s1 == null && s2 == null)
    //        return true;

    //    // Biri null, diğeri değilse eşit değildir
    //    if (s1 == null || s2 == null)
    //        return false;

    //    int i = 0;
    //    try
    //    {
    //        while (true)
    //        {
    //            char c1 = s1[i];
    //            char c2 = s2[i];
    //            if (c1 != c2)
    //                return false;
    //            i++;
    //        }
    //    }
    //    catch (IndexOutOfRangeException)
    //    {
    //        // Her iki string de aynı anda biterse eşittir
    //        try
    //        {
    //            char test = s1[i];
    //            return false; // s1 daha uzun
    //        }
    //        catch (IndexOutOfRangeException)
    //        {
    //            try
    //            {
    //                char test = s2[i];
    //                return false; // s2 daha uzun
    //            }
    //            catch (IndexOutOfRangeException)
    //            {
    //                return true; // ikisi de aynı anda bitti, eşit
    //            }
    //        }
    //    }
    //}

    //    class örnekler
    //{
    //    public bool MyEquals(string s1, string s2)
    //    {
    //        // İkisi de null ise eşittir
    //        if (s1 == null && s2 == null)
    //            return true;

    //        // Biri null, diğeri değilse eşit değildir
    //        if (s1 == null || s2 == null)
    //            return false;

    //        int i = 0;
    //        try
    //        {
    //            while (true)
    //            {
    //                char c1 = s1[i];
    //                char c2 = s2[i];
    //                if (c1 != c2)
    //                    return false;
    //                i++;
    //            }
    //        }
    //        catch (IndexOutOfRangeException)
    //        {
    //            // Her iki string de aynı anda biterse eşittir
    //            try
    //            {
    //                char test = s1[i];
    //                return false; // s1 daha uzun
    //            }
    //            catch (IndexOutOfRangeException)
    //            {
    //                try
    //                {
    //                    char test = s2[i];
    //                    return false; // s2 daha uzun
    //                }
    //                catch (IndexOutOfRangeException)
    //                {
    //                    return true; // ikisi de aynı anda bitti, eşit
    //                }
    //            }
    //        }
    //    }

    //    static void Main(string[] args)
    //    {
    //        örnekler o = new örnekler();
    //        Console.WriteLine(o.MyEquals("abc", "abc")); // true
    //        Console.WriteLine(o.MyEquals("abc", "ab"));  // false
    //        Console.WriteLine(o.MyEquals(null, null));   // true
    //        Console.WriteLine(o.MyEquals("a", null));    // false
    //        Console.ReadLine();
    //    }
    //}





    //class pp
    //{
    //    static void Main(string[] args)
    //    {
    //        string metin="Bilişim Teknolojileri";
    //        Console.WriteLine(metin.IndexOf("loji"));
    //        Console.WriteLine(metin.IndexOf("Bilişim"));
    //        Console.WriteLine(metin.IndexOf("bilisimm"));


    //        Console.ReadLine();
    //    }




    //    public static int MyIndexOf(string metin, string aranan)
    //    {
    //        if (metin == null || aranan == null)
    //            return -1;

    //        // aranan boşsa 0 döndür (C#'ta IndexOf böyle çalışır)
    //        try { char dummy = aranan[0]; }
    //        catch (IndexOutOfRangeException) { return 0; }

    //        int i = 0;
    //        while (true)
    //        {
    //            // metin[i] ile başla, arananın tüm karakterlerini sırayla karşılaştır
    //            int j = 0;
    //            while (true)
    //            {
    //                try
    //                {
    //                    char c1 = metin[i + j];
    //                    char c2 = aranan[j];
    //                    if (c1 != c2)
    //                        break; // eşleşme yok, bir sonraki i'ye geç
    //                    j++;
    //                }
    //                catch (IndexOutOfRangeException)
    //                {
    //                    // metin biterse ve aranan hala bitmediyse eşleşme yok
    //                    try { char test = aranan[j]; return -1; }
    //                    catch (IndexOutOfRangeException) { return i; } // aranan da bittiyse eşleşme var
    //                }
    //            }
    //            // arananın tüm karakterleri eşleşti mi kontrolü
    //            try { char test = aranan[j]; }
    //            catch (IndexOutOfRangeException) { return i; } // aranan bitti, eşleşme var

    //            // metin bitti mi kontrolü
    //            try { char test = metin[i + 1]; }
    //            catch (IndexOutOfRangeException) { return -1; }

    //            i++;
    //        }
    //    }
    //}

    //class p
    //{
    //    static void Main(string[] args)
    //    {
    //        string[] kelime = { "e", "merhaba", "dünya", "merhaba dünya", "merhaba dünya merhaba dünya" };
    //        string sonuc = String.Join(".", kelime);
    //        Console.WriteLine(sonuc);
    //        Console.ReadLine();
    //    }

    //}


    //public static string M(string ayrac, string[] dizi)
    //{
    //        // Null veya boş dizi kontrolü
    //        if (dizi == null)
    //            return null;

    //        // Sonuç için karakter dizisi oluştur
    //        char[] sonuc = new char[1000]; // Yeterince büyük bir dizi (pratikte dinamik olmalı)
    //        int pos = 0;

    //        for (int i = 0; ; i++)
    //        {
    //            // Dizi bitti mi kontrolü
    //            try { string kelimeler = dizi[i]; }
    //            catch (IndexOutOfRangeException) { break; }

    //            // Null string için boş ekle
    //            string kelime = dizi[i];
    //            int j = 0;
    //            if (kelime != null)
    //            {
    //                while (true)
    //                {
    //                    try
    //                    {
    //                        char c = kelime[j];
    //                        sonuc[pos++] = c;
    //                        j++;
    //                    }
    //                    catch (IndexOutOfRangeException)
    //                    {
    //                        break;
    //                    }
    //                }
    //            }

    //            // Son elemana gelmediysek ayraç ekle
    //            try { string test = dizi[i + 1]; }
    //            catch (IndexOutOfRangeException) { break; }

    //            int k = 0;
    //            while (true)
    //            {
    //                try
    //                {
    //                    char c = ayrac[k];
    //                    sonuc[pos++] = c;
    //                    k++;
    //                }
    //                catch (IndexOutOfRangeException)
    //                {
    //                    break;
    //                }
    //            }
    //        }

    //        // Sonucu stringe çevir (hazır metot kullanmadan)
    //        string sonucStr = "";
    //        int s = 0;
    //        while (s < pos)
    //        {
    //            sonucStr += sonuc[s];
    //            s++;
    //        }
    //        return sonucStr;



    //}
    //using System;

    //namespace generic_class
    //    {
    //        class Program
    //        {
    //            public string MyJoin(string ayrac, string[] dizi)
    //            {
    //                if (dizi == null)
    //                    return null;

    //                string sonuc = "";
    //                int i = 0;
    //                while (true)
    //                {
    //                    // Dizi bitti mi kontrolü
    //                    try { string kelimeler = dizi[i]; }
    //                    catch (IndexOutOfRangeException) { break; }

    //                    string kelime = dizi[i];
    //                    int j = 0;
    //                    if (kelime != null)
    //                    {
    //                        while (true)
    //                        {
    //                            try
    //                            {
    //                                char c = kelime[j];
    //                                sonuc += c;
    //                                j++;
    //                            }
    //                            catch (IndexOutOfRangeException)
    //                            {
    //                                break;
    //                            }
    //                        }
    //                    }

    //                    // Son elemana gelmediysek ayraç ekle
    //                    try { string test = dizi[i + 1]; }
    //                    catch (IndexOutOfRangeException) { break; }

    //                    int k = 0;
    //                    while (true)
    //                    {
    //                        try
    //                        {
    //                            char c = ayrac[k];
    //                            sonuc += c;
    //                            k++;
    //                        }
    //                        catch (IndexOutOfRangeException)
    //                        {
    //                            break;
    //                        }
    //                    }
    //                    i++;
    //                }
    //                return sonuc;
    //            }

    //            static void Main(string[] args)
    //            {
    //                string[] kelime = { "e", "merhaba", "dünya", "merhaba dünya", "merhaba dünya merhaba dünya" };
    //                Program p = new Program();
    //                string sonuc = p.MyJoin(".", kelime);
    //                Console.WriteLine(sonuc);

    //                Console.ReadLine();
    //            }
    //        }
    //    }


    //class p
    //{
    //    static void Main(string[] args)
    //    {
    //        string e = "selammmmmmmcnmmm";
    //        string yeni = e.Remove(2, 5);
    //        Console.WriteLine(yeni);
    //        Console.ReadLine();
    //    }
    //}


    //class pp
    //{
    //    public string MyRemove(string metin, int baslangic, int adet)
    //    {
    //        if (metin == null)
    //            return null;
    //        if (baslangic < 0 || adet < 0)
    //            return metin;

    //        // metnin uzunluğunu bul
    //        int uzunluk = 0;
    //        try { while (true) { char c = metin[uzunluk]; uzunluk++; } }
    //        catch (IndexOutOfRangeException) { }

    //        if (baslangic >= uzunluk)
    //            return metin;

    //        string sonuc = "";
    //        int i = 0;
    //        while (i < uzunluk)
    //        {
    //            if (i >= baslangic && i < baslangic + adet)
    //            {
    //                i++;
    //                continue;
    //            }
    //            try
    //            {
    //                char c = metin[i];
    //                sonuc += c;
    //            }
    //            catch (IndexOutOfRangeException)
    //            {
    //                break;
    //            }
    //            i++;
    //        }
    //        return sonuc;
    //    }

    //    static void Main(string[] args)
    //    {
    //        pp nesne = new pp();
    //        string e = "selammmmmmmcnmmm";
    //        string yeni = nesne.MyRemove(e, 2, 5);
    //        Console.WriteLine(yeni); // Çıktı: semmmmmcnmmm
    //        Console.ReadLine();
    //    }
    //}
    //---------------------------------------------------------------------------------------



    //class e
    //{

    //    public string MyPadLeft(string metin, int toplamUzunluk, char karakter)
    //    {
    //        if (metin == null)
    //            return null;

    //        // metnin uzunluğunu bul
    //        int uzunluk = 0;
    //        try { while (true) { char c = metin[uzunluk]; uzunluk++; } }
    //        catch (IndexOutOfRangeException) { }

    //        // Eğer zaten istenen uzunlukta veya daha uzunsa, aynen döndür
    //        if (uzunluk >= toplamUzunluk)
    //            return metin;

    //        // Kaç tane karakter eklenecek?
    //        int eklenecek = toplamUzunluk - uzunluk;
    //        string sonuc = "";

    //        // Soldan karakter ekle
    //        int i = 0;
    //        while (i < eklenecek)
    //        {
    //            sonuc += karakter;
    //            i++;
    //        }

    //        // Sonra metni ekle
    //        i = 0;
    //        while (true)
    //        {
    //            try
    //            {
    //                char c = metin[i];
    //                sonuc += c;
    //                i++;
    //            }
    //            catch (IndexOutOfRangeException)
    //            {
    //                break;
    //            }
    //        }

    //        return sonuc;
    //    }
    //}



    //-----------------------------------------------------------------------------------
    //örnekler

    //class program
    // {
    //     static void Main(string[] args)
    //     {
    //         int sayi = 42;
    //         DateTime tarih = DateTime.Now; //bugünün tarihini stringe çevirdi.
    //         Console.WriteLine(tarih.ToString());

    //         Console.ReadLine();
    //     }
    // }
    //kendi sınıfında tostring override etmek:

    //class ögrenci
    //{
    //    public string ad { get; set; }
    //    public int yas { get; set; }
    //    public override string ToString()
    //    {
    //        return $"ad:{ad},yas:{yas}";
    //    }


    //}

    //class program2
    //{
    //    static void Main(string[] args)
    //    {
    //        ögrenci ogr = new ögrenci();
    //        ogr.ad = "ali";
    //        ogr.yas = 30;
    //        ogr.ToString();
    //        Console.WriteLine(ogr.ToString());
    //        Console.ReadLine();
    //    }
    //}
    //---------------------------------------------------------------------------------------

    //    using System;
    //using System.Collections.Generic;

    //class Program
    //    {
    //        static void Main()
    //        {
    //            // 1) Dizi ile foreach
    //            string[] meyveler = { "Elma", "Armut", "Kiraz" };
    //            foreach (var meyve in meyveler)
    //                Console.WriteLine(meyve);

    //            Console.WriteLine();

    //            // 2) List ile foreach ve koşullu atlama
    //            var sayilar = new List<int> { 1, 2, 3, 4, 5 };
    //            foreach (int s in sayilar)
    //            {
    //                if (s % 2 == 0) continue;   // çiftleri atla
    //                Console.WriteLine(s);      // sadece tek sayıları yazdır
    //            }

    //            Console.WriteLine();

    //            // 3) Dictionary ile foreach
    //            var sozluk = new Dictionary<string, string>
    //            {
    //            ["TR"] = "Türkiye",
    //            ["US"] = "Amerika",
    //            ["DE"] = "Almanya"
    //        };
    //        foreach (var kv in sozluk)
    //            Console.WriteLine($"{kv.Key} → {kv.Value}");
    //    }
    //}

    class dizi
    {
        

        public  bool ArrayArtanSıradaMI(int[]e)
        {

            if (e == null) 
            {
                return false;
            }
            if (e.Length <= 1)
            {
                return true;
            }
            for(int i = 0; i < e.Length; i++)
            {
                if (e[i] < e[i + 1])
                {
                    return true;
                }

            }
            return false;

        }
    }
class p
    {
        static void Main(string[] args)
        {
            int[]dizi1= { 1, 2, 3, 4, 5 };
            dizi kontrol= new dizi();
            bool resultmyArray1 = kontrol.ArrayArtanSıradaMI(dizi1);
            Console.WriteLine(resultmyArray1);

            Console.ReadLine();
        }
    }
}

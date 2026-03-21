using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gencay_yıldız_2
{
    //21)nesneler üzerinden metotlara erişim

    //public class Matematik
    //{
    //    public int Topla(int a , int b)
    //    {
    //        return a + b;
    //    }
    //    public double böl (double c,double d)
    //    {
    //        return c / d;
    //    }
    //}
    //internal class Program
    //{


    //static void Main(string[] args)
    //{
    //    Matematik m = new Matematik();
    //    int sonuc = m.Topla(2, 3);
    //    Console.WriteLine("sonuç:"+sonuc);

    //    Console.Read();
    //}
    // }
    //metotlar ,referanslar üzerinden çağırıldığı zaman bir nesneye ihtiyaç vardır.
    //-------------------------------------------------------------------------------------

    //22) METOTLARDA OVERLOAD/AŞIRI YÜKLEME
    //Bir sınıfta aynı isimle birden fazla metot olması.

    //küçük farklarla aynı işleri yapan metotları aynı isimlerle yazarız , titiz bir çalışma olması için.
    //verdiğimiz parametre farkına göre çağırınca çalışır.

    //!!!KURALLAR !!!
    //1)Metot isimleri aynı olmalıdır.
    //2)Metotların parametre sayıları farklı olmalıdır.
    //3)eğer parametre sayıları aynıysa parametre TİPLERİ farklı olmalı.
    //4)eğer parametre tipleri de farklı değilse yazılma SIRALARI farklı olmalı.(int a ,double b/double b ,int a)

    //        class Mat
    //      {

    //        public double Topla(int a,double b)
    //        {
    //            return a + b;
    //        }
    //        public decimal Topla(double b,int a)
    //        {
    //            return 3;
    //        }
    //        public bool Topla (double b, int a, char c)
    //        {
    //            return false;
    //        }
    //    }

    //23) CLASS NEDİR?

    //Bir class oluşturduğumuzda içerisinde metot, field property, constructer barındırabilir.
    //class içerisinde class olabilir.
    //başına public koymadan direkt yazdığımız class private

    // public class Okul
    //{

    //    int x; //! x değişkeni class içerisinde tanımlandığından dolayı FİELD , yani alan
    //           //tanımlanan değişkenin tipine göre varsayılan değerleri otomatik olarak atılır.(int =0, string=null,bool=false..)
    //           //global değişken
    //           //organize veri tutmamızı sağlayan yapılar.
    //     string adi;//(field)
    //    //sınıf içindeki fieldları dışarı kontrollü bir şekilde açmamızı sağlayan yapı=PROPERTY
    //    //Property:
    //    public string Adi //parantez koyarsak metot olur.
    //    {
    //        get //return
    //        {
    //            return adi;
    //            return adi.Substring(0, 2);//ilk 2 karakterini geri göndermek isteyebilirim.
    //        }
    //        set //dışarıdan propertye gönderilen değer nereye gönderilecel, ne yapılacak??
    //        {
    //            adi=value;
    //            adi=value.ToUpper();//gönderilen verinin hepsini büyük harfle tutmak isteyebilirim
    //        }

    //         //CTRL+R+E  tıkladıgın fielda özel oluşturulacak property
    //    }


    //    public int emel { get; set; }
    //    public class Ogretmen
    //    {

    //    }
    //}
    //public class progrmm
    //{
    //    static void Main(string[] args)
    //    {
    //        Okul o=new Okul();
    //        Okul.Ogretmen ö = new Okul.Ogretmen();//class içinde class

    //        //property
    //        o.Adi = "emel"; //set çalışır , value ile yakalar
    //        Console.WriteLine(o.Adi);//get çalışır.
    //        o.emel = 20;//hazır propertye değer atadım.

    //    }
    //}
    //-----------------------------------------------------------------------------------------

    //26)CONSTRUCTER YAPISI/YAPICI METOT)

    //Bulunduğu sınıfı yapılandırır.,nesne üretirken o nesneyi yapılandırmamızı saglayan metottur.
    // constructer metot PUBLİC olmalı.
    //Geri dönüş değeri olmaz!
    //ismi sınıf ismi ile AYNI olmalı.

    //bir sınıftan nesne üretirken her şeyden önce tetiklenir. Yapılandırmqa işlemlerine hizmet eder.
    #region
    class Ornek
    {
        public Ornek()
        {
        
        }
        public Ornek(int a)//overload edebiliriz.
        {
            Console.WriteLine("nesne oluşturuldu.");
        }

        //STATİC Comstructer
        //normal constructerdan da önce çalışan metot.
        //ilgili sınıftan yapılan nesne taleplerinden ilkinde tetiklenir, sonrakilerde teiklenmez.
        //bir sınıfta constructerdan da once çalışan ama o sınıftan yapılan nesne telebinde 1 kere çalışan metot.
        static Ornek()
        {
            Console.WriteLine("normal consturcterdan önce çalıştı");
        }

    }
    class pp
    {
        static void Main(string[] args)
        {
            new Ornek(); new Ornek();
        }
    }

    #endregion

    
}


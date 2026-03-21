using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gencay_yıldız
{



    //bir class içinde metotlar , constructurlar,global değişkenler,referanslar tutabiliriz
    //algoritmik kodlar yazamayız., metot , property sadece classlarda tanımlanır.
    //-------------------------------------------------------------------------------------------------

    //metotlar nedir?:  işlem parçacıklarıdır
    //belirli bir amaca dönük yöntem oluşturuyoruz.
    //bubüne kadar main bir metottu.
    //2 türlü: 1)önceden hazır 2) kendi yazdığımız metotlar., hazırsa kullanırız, değilse tanımlarız.
    //dışardan girdisi varsa parametre alan, çıktısı varsa geriye değer döndüren tipine bakacağız.=yünü verdim kazak çıktı geriye değer gönderen(kazak) parametre alan(yün)    , kazak cıkmasaydı sadece parametre alan.

    /*1 Geriye değer göndermeyen,Parametre almayan metot
     *2  Geriye dğeer göndermeyen,Parametre alan metot
     *3  Geriye değer gönderen,Parametre almayan metot
     *4  Geriye değer gönderen ,Parametre alan metot
     */

    //[erişim belirleyicisi][ geri dönüş tipi][adı]
    //{
    //işlemler
    //}

    //Erişim belirleyiciler=1)public: dışardan erişilebilen 2)private:sadece tanımlandığı sınıf içerisinde kullanılabilir.



    //metot oluşturalım:
    //
    //geriye değer göndermiyorsa geri dönüş tipi:void(boş)
    //parametreler parantez içine
    //class metotlar
    //{


    //    public void Metot1()
    //    {
    //        Console.WriteLine("metot1 ");
    //    }
    //    public void Metot2( bool emel)
    //    {
    //        Console.WriteLine("metot2");
    //    }
    //    //ekrana bir şey yazdırmak gwriye değer göndermek değildir.
    //   //eğer metodumuz geriye değer döndürecek tipte ise o tipte bir değeri 
    //   //göndermek zorundadır!!!!! yoksa hata.
    //    private string Metot3()
    //    {
    //        Console.WriteLine("metot3");
    //        return "emel"; 

    //    }
    //    public string Metot4( bool b , string x ,int y)
    //    {
    //        return "abc";
    //       //int 5 olmaz çünkü o değer değişken adı değil ,parametrede isim olmalı.

    //    }-----------------------------------------------------------------------------------------------------------------------------

    //19)METOTLARIN KULLANIMI 

    //bir metodu sınıf içinde tanımlarız , kullanırken başka bir metot ya da kendi içinde kullanırız.

    //aynı sınıfta tanımlanmış metotların birbirlerine erişimleri:

    //class Program 
    //{
    //    static void Main(string[] args)
    //    {

    //        int sonuc=Topla(32, 13);
    //        double bölüm = Böl(18 , 2);
    //    }
    //   static  public int Topla(int sayı1,int sayı2)
    //   {
    //        return sayı1+sayı2 ;
    //   }
    //    static public double Böl(double sayi1,double sayi2)
    //    {
    //        return sayi1 / sayi2;
    //    }

    //}------------------------------------------------------------------------------------------


    //20) new operatörüyle nesne oluşturma ve referans mantığı:

    //nesne dediysek akla class gelecek.nesneleri sınıflardan oluştururuz.Sınıflar 
    //nesne oluşturmamıza izin veriyorlarsa belirli bir tip karşılığında verirler.


    //değer tipli değişken 1 tane tutar, referans tipli (nesne) birçok değer tutar.



    //   class Ornek
    //{

    //}
    class Programm
    {
    static void Main(string[] args)
    {
    //int sayi = 0;
    //Ornek abc = new Ornek();
    //new Ornek();

    //Ornek x=null;
    //x = new Ornek();

    //referans , işaretleme
    //Ornek x = new Ornek();
    //Ornek y = x;     //y değişkeni, x ile aynı nesneyi paylaşır. İkisi de aynı bellek adresine işaret eder. Bu sayede biri üzerinden yapılan değişiklik, diğerinde de görünür.
    //if (x.Equals(y))
    //{
    //    Console.WriteLine("evet eşit ");
    //}
    //else
    //{
    //    Console.WriteLine( "eşit değil");
    //}


    

    //değer , değeri kopyalama
    //int x = 3;
    //int y = x;




       }
    }












}





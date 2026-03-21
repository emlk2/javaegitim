using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polimorfizm
{
    //POLİMORFİZM-ÇOK BİÇİMLİLİK
    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {


    //        B bb=new B();
    //        A aa = new A();
    //        A a = new B();//mümkün değil ama polimorfizmle mümkün olur.
    //        //A tipinde bir referansa B Tipinde bir nesneyi baglamak.
    //        //!! ama B bbb=new A(); OLMAZ.!
    //        B bbb = (B)a;
    //    }
    //}
    //class A
    //{
    //    public int x { get; set; }

    //}

    //class B :A
    //{
    //    public int y { get; set; }

    //}
    //-----------------------------------------------------------------------ğ
    //36) ABSTRACT  CLASS
    //: Bir kalıtım olmalı gerekiyor. Base classta var olan metot ya da propertynin işleyişi.
    //virtual override zorunlu değil ama abstract class zorunlu yapar.

    //abstract class Ornek//bu sınıfta abstract olan metot veya propertyler abstracttan kalıtım alan her sınıfta işlenmek zorunda.
    //                     //abstract metot ya da propertylerin gövdeleri oluşturuldukları sınıf içinde yazılmazlar.Sadece geri dönüş tipleri, isimleri erişim belirleyicileri
    //                     //gövde override ile yazılır.
    //                     //abstract elemanlar private olamaz.
    //                     //içinde abstract olmayan yapılar da bulunabilir.
    //                     //!!!!ABSTRACT CLASSLARDAN NESNE OLUŞTURULMAZ.REFERANS NOKTASI ALINABİLİR.
    //{
    //    public abstract void X();//gövdesi YOK.
    //  abstract public int MyProperty { get; set; }
    //}
    //class Calisma: Ornek
    //{

    //}
    //------------------------------------------------------------------------------------------------------------

    //37)  INTERFACE(ARA YÜZ)
    //Kendisinden kalıtım alan sınıfların içinde olması zorunlu olan yapıları tanımlayan yapı.(abstract classa benzer ama abstractta zorunlu olmayanlar da tanımlanır,interface sadece imza.
    //Kendisinden kalıtım alan derived classlarda zorla uygulattıracağı elemanların imzasını kendi içinde barındıran bir yapı.Tek amacı kendisinden kalıtım alacak sınıflara imza teşkil etmek.
    //interface duyunca akla ilk kalıtım gelmeli.
    //Nesne oluşturulamaz!yani constructer yapısı yok.
    //Ama referans noktası oluşturulabilir.
    //Interfacelerde bir sınıfn bir veya birden fazla sınıftan kalıtım alabilir.
    //Interafece içerisinde tanımlanmıs yapıların gövdeleri interfaceten kalıtım alan classlarda oluşturulacaktır.
    //İçinde static yapı olamaz.
    //İmzalarda public,private protected vs.gibi erisim belirleyicileri olmaz!
    //Interfacein içindeki herhangi bir şey erişim belirleyicisi alamaz.(public,private)



    interface IOrnek //->başına I koyarız.
    {
        int X(); //bir metot parantez var.
        void Y();

        int OrnekProperty { get; set; }
    }
    class Ornek : IOrnek
    {
        public int OrnekProperty
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int X()
        {
            throw new NotImplementedException();
        }

        public void Y()
        {
            throw new NotImplementedException();
        }
    }
    interface IA
    {
        void x();
        void y();
        void z();
    }
    interface IB
    {
        void x();
        void M();
        void N();
    }
    class C
    {

    }
    class Ornek2 :  C,IA, IB
    {
        void IB.M()
        {
            throw new NotImplementedException();
        }

        void IB.N()
        {
            throw new NotImplementedException();
        }

        //public void x()//HANGİSİNDEN GELDİ???  NAME HİDİNG Hatası(isim saklama) //eğer böyle yaparsak , bu yüzden böylw yapmayız burda
        //{
        //    throw new NotImplementedException();
        //}
        void IA.x()
        {
            throw new NotImplementedException();
        }

        void IB.x()
        {
            throw new NotImplementedException();
        }

        void IA.y()
        {
            throw new NotImplementedException();
        }

        void IA.z()
        {
            throw new NotImplementedException();
        }
    }

    class program
    {
        static void Main(string[] args)
        {
            //şimdi interfacein içindekilerden nesne olıuşturamayız direkt cünkü publc yok, şöyle yaparız:
            IA a = new Ornek2();
            a.x();

        }
    }
}



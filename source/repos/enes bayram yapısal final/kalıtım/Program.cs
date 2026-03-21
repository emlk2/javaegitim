using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kalıtım
{
    //KALITIM (INHERİTANCE)
    //Çalıştıgımız bir projede oluşturdugumuz yapıları hiyerarsik bir şekilde parçalara bölmemizi sağlar.

    //Erişim belirleyiciler: public -private-protected-internal
    //Protected: Eger bir class içinde eleman ya da metot protected ise o class için private ve o classtan türeyen classlar için 
    //public davranışlar sergiler.Yani sadece o class ve o classtan türeyen classlarda kullanılabilirler.O classtan türemeyenlerde kullanılamazlar.

    //Internal: eğer bir class içindeki ifade internal ise ssadece o assembly içinde kullanılabilirler.(Kalıtım alsın ya da almasın.)

    //Protected Internal : Başka bir assemblydeki bir classan türemişse , o class içindeki protected internal ifadeler protected olmalarından dolayı kullanılabilirler.
    //ama türemeyen classlar içinde bu elemanlar aynı zamanda internal oldukları için kullanılamazlar.
    //türeten sınıf: base class, türeyen: derived class
    //bir sınıf sadece tek bir sınıftan kalıtım alabilir ama 1 ve 1den falza sınıfa kalıtım verebilir.
    //hiyerarşide en alttaki sınıfın nesnesini üreteceksek en üsttekinden başlar , ne kadar bağ varsa hepsinin nesnesi üretilecektir.
    //A    B:A     C:B  ---> A B nin base classoı, B Cnin base classı.




    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {

    //        C c=new C();  // sadece c den nesne üretsek bile bütün sınıflar calısır ve hepsinden üretilir.
    //        Console.ReadLine();

    //    }
    //}

    //public class A
    //{
    //    public A()
    //    {
    //        Console.WriteLine("A nesnesi üretildi.");
    //    }
    //    public void X()
    //    {

    //    }
    //    public int Y { get; set; }
    //    public bool z;
    //}
    //public class B:A  // B Sınıfı A sınıfından kalıtım almıştır.
    //{
    //    public B()
    //    {
    //        Console.WriteLine("B nesnesi üretildi.");
    //    }

    //}
    //public class C : B
    //{
    //    public C()
    //    {
    //        Console.WriteLine("C nesnesi üretildi.");
    //    }

    //}
    //------------------------------------------------------------------------------------------------


    //public class Insan    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        //Erkek e = new Erkek();
    //        //e.adi = "alperen";
    //        //e.soyadi = "manas";
    //        //e.meslegi = "mühendis";
    //        //e.sakal = "kumral";
    //        //e.biyik = "kumral";
    //        //Console.WriteLine(e.adi+e.soyadi);

    //        //Kadın k = new Kadın();
    //        //k.adi = "emel";
    //        //k.soyadi = "kılıç";
    //        //k.meslegi = "mühendis";
    //        //k.makyaj = "renkli";
    //        //k.oje = "pembe";


    //        Console.ReadLine();

    //    }
    //}
    //{
    //    public string adi { get; set; }
    //    public string soyadi { get; set; }

    //    public string meslegi { get; set; }

    //}
    //public class Erkek : Insan
    //{
    //    public string sakal { get; set; }
    //    public string biyik { get; set; }

    //}

    //public class Kadın : Insan
    //{
    //    public string makyaj { get; set; }
    //    public string oje { get; set; }




    //}-----------------------------------------------------------------------------------------------------


    //33) VİRTUAL VE OVERRİDE

    //Virtual: base classtaki işlev derivedde farklı bir sekilde kullanılacaksa virtual yazılır.
    //override : basete virtual olarak kullanılanı derivedde yeniden şekillendirmemizi saglar.

    
    class A
    {
        public virtual void OrnekMetod()
        {
            Console.WriteLine("bu bir örnek");
        }
    }
    class B : A
    {
        public override void OrnekMetod()
        {
            Console.WriteLine("bu ezilmiş");
        }



    }



}

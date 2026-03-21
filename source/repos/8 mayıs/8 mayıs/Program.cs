using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _8_mayıs
{
    //internal class Program
    //operatör overloading   SINAVDA ÇIKAR
    //{
    //eşit eşit varsa eşit değil kodlanmak zorundadır!.   , küçük kodlandıysa büyük kodlanmak zorunda.!!
    //override


    // static void Main(string[] args)
    //{


    //Exeption Handling 

    //string a = "5";      //amaç bu hatadaki mesajı kullanıcıya cıkartmamak ,daha basit cıkarmak.
    //int x=Int32.Parse(a);
    //Console.WriteLine(  x);

    //out :bir metottaan birden  farklı tipte geri dönüş tipi mümkün.
    //bool result Int32.TryParse("5", out a);
    //true                          //5
    //    false                           "a5",out a);        

    //Int32.TryParse
    // int x = 0;
    //bool result =Int32.TryParse("5", out x);
    //if(result )
    //{
    //    Console.WriteLine("dönüşüm başarılı");
    //}
    //else
    //{
    //    Console.WriteLine("dönüşüm başarısız");
    //}

    //Console.ReadLine();

    /*
     * try
     *{
     *hata muhtemel
     *}
     *catch(N tane)
     *{
     *hata olursa ne yapıyoruz
     *}
     *finally 
     *{
     *}
     */
    //try
    //{
    //    int a = 10 / Int32.Parse(Console.ReadLine()); //kullanıcıdan alınan değeri paydaya yazıyor 10 a bölüyor
    //}
    //catch (FormatException f1) 
    //{
    //    Console.WriteLine("lütfen sayısal veri giriniz. "+f1.Message);
    //}

    //catch (DivideByZeroException f2)
    //{
    //    Console.WriteLine("sıfıra bölüm hatası: "+ f2.Message);

    //}
    //catch(OverflowException f3)
    //{
    //    Console.WriteLine("beklenenden büyük sayı . "+ f3.Message);
    //}
    //catch (Exception f4)  //en genel hata, hiçbir hata yakalayamazsak buraya düşsün.
    //{
    //    Console.WriteLine("beklenmeyen hata. "+f4.Message);
    //}
    //finally
    //{
    //    Console.WriteLine("finally çalıştı");
    //}




    //int[] x = { 1, 2, 3, 4 };
    //try
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        x[i] = 5;
    //    }

    //}
    //catch (IndexOutOfRangeException)
    //{

    //    throw;
    //}

    //---------------------------------------------------------------------------------------------
    //örnekler

    //Console.WriteLine(" yaşınızı girin");
    //int yas =Int32.Parse(Console.ReadLine());

    //if (yas < 18)
    //{
    //    throw new ArgumentException("Bu uygulamada yaş sınırı bulunmaktadır");

    //}

    //string sicilNo=Console.ReadLine(); //6 haneli
    //if (sicilNo.Length > 6)
    //{
    //    throw new OverflowException("Sicil No 6 hanelidir.");
    //}

    //try
    //{


    //    char c = 'C';
    //    DateTime dt = new DateTime(c);

    //}
    //catch(InvalidCastException i1) 
    //{
    //    Console.WriteLine("hatalı dönüşüm işlemi ."+i1.Message );
    //}
    //try
    //{
    //    int i = 1;
    //    bool result = Convert.ToBoolean(i);
    //}
    //catch(InvalidCastException i1)
    //{
    //    Console.WriteLine("hatalı dönüşüm işlemi. "+ i1.Message);
    //

    //try
    //{
    //    object[] dizi = new string[3];
    //    dizi[0] = "merhaba";
    //    dizi[1] = 42;
    //}
    //catch (ArrayTypeMismatchException e)
    //{
    //    Console.WriteLine(e.Message);
    //}

    //---------------------------------------------------------------------------------------------------
    //15 MAYIS

    //public class Programm
    //{





    //    static void Main(string[] args)
    //    {
    //        Student student1 = new Student();  //çalışınca studente ait yapılandırıcı metot olsa hem humanın hem studentin yaoılandırıcısı var ikisi de
    //                                           //aynı şeyi diyor,hangisi önce çalışır?=
    //        student1.Name = "Ali";
    //        student1.Surname = "Deneme";
    //        student1.StudentID = 12345;
    //        student1.BirthDate= DateTime.Now;
    //        student1.University = "Kıırkkale uni";
    //        student1.Department = "Bilgisayar Müh";
    //        student1.Email = "ali@kku.edu.tr";
    //        Console.WriteLine("Human INFO");
    //        student1.HumanInfo();
    //        Console.WriteLine("STUDENT INFO");
    //        Console.WriteLine(student1.StudentInfo());





    //    }


    //}
    //public  class Human
    //{

    //    public Human() 
    //    {
    //        Console.WriteLine("Merhaba ben Human");
    //    }

    //    public string HumanInfo()
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append(Name);

    //    }

    //}

    //public class Student:Human
    //{
    //    public int StudentID { get; set; }
    //    public string University { get; set; }
    //    public string Faculty { get; set; }
    //    public string Department { get; set; }
    //    public DateTime RecordDate { get; set; }

    //    public Student()
    //    {
    //        Console.WriteLine("Merhaba ben Student");  //yapılandırıcı   ! önce hujman çalışır ,kalıtım aldıgımız sınıf base class!
    //    }
    //    public string StudentInfo()
    //    {
    //        StringBuilder sb=new StringBuilder();
    //        sb.Append(StudentID);
    //        sb.Append(Environment.NewLine);// yeni bir satır ekler.
    //        sb.Append(Name);
    //        sb.Append(Surname);
    //        sb.Append(TcNo.ToString());
    //        sb.Append(Environment.NewLine);
    //        sb.Append(University);
    //        sb.Append(Faculty);
    //        sb.Append(Department);  
    //        return sb.ToString(); 
    //    }
    //    public class AcademicStaff : Human
    //    {
    //        public string PersonelID { get; set; }  
    //        public string Title { get; set; }
    //        public string AcademicStaffInfo()
    //        {
    //            StringBuilder sb = new StringBuilder();
    //            sb.Append(PersonelID.ToString());
    //            sb.Append(Name);
    //            sb.Append(Surname);

    //            sb.AppendLine(Title); //bir yan satıra ekleme.
    //            return sb.ToString();
    //        }
    //    }

    //}


    //-------------------------------------------------------------------------------------------
    //public class VehicleManager
    //{

    //    //private:sadece kendi sınıfı
    //    //public :her yerden
    //    //protected: private+kalıtım
    //    protected bool carKey;
    //    protected int vehicleStart(bool carKey)
    //    {
    //        if (carKey)
    //        {
    //            return 1;

    //        }
    //        else
    //        {
    //            return 0;
    //        }
    //    }
    //    public virtual void VehicleMove()// base classtaki bir sınıf ust classta da kulllanacaksan iki sınıftaki metotlar farklı gorev yapacaksa override etmek gerekir ama virtual olmalı

    //    {
    //        Console.WriteLine("taşıt hareket halinde");
    //    }

    //}
    //public class CarManager : VehicleManager
    //{
    //    protected bool gas, gasPedal, derPedal;
    //    protected string MoveCheck(bool key, bool gas, bool gasPedal, bool derPedal)
    //    {
    //        int keyValue = vehicleStart(key);
    //        if (keyValue.Equals(1))
    //        {
    //            if (gas)
    //            {
    //                if (derPedal && gasPedal != true)
    //                {
    //                    return "Hareket için gaza basın";
    //                }
    //                else if (derPedal != true && gasPedal == false)
    //                {
    //                    return "hareket için debriyaja basın";
    //                }
    //                else if (derPedal == false && gasPedal == false)
    //                {
    //                    return "hareket için gaz ve debriyaja basın";
    //                }
    //                else
    //                {
    //                    return "araba harekete geçti";
    //                }
    //            }
    //            else
    //            {
    //                return "hareket için benzin bulunmamaktadır";
    //            }
    //        }
    //        else
    //        {
    //            return "çalıştırmak için anahtar gerekli";
    //        }
    //    }


    //    public override void VehicleMove()
    //    {
    //        string mark = new string('-', 6);
    //        Console.WriteLine("{0}\n<Araba Çalıştırma"+ "Gereksinimker>\n {1}" , mark , mark);
    //        Console.WriteLine("\n-> Aracın anahtarı var mı?");
    //       carKey= Convert.ToBoolean(Console.ReadLine());
    //        Console.WriteLine("\n->Aracın yakıtı yeterli mi?");
    //        gas=Convert.ToBoolean(Console.ReadLine());
    //        Console.WriteLine("\n->Gaz pedalı basıyor musun?");
    //        gasPedal=Convert.ToBoolean(Console.ReadLine());
    //        Console.WriteLine("\n->DEBRİYAJ pedalı basıyor musun?");
    //        derPedal=Convert.ToBoolean(Console.ReadLine());
    //        Console.WriteLine("{0}\n<Araba Hareketi için"+
    //        "Kontroller saglanıyor> \n{1},",mark ,mark);
    //        Console.WriteLine(MoveCheck(carKey,gasPedal,gas,derPedal));
    //    }

    //}
    //public class  BicycleMnager:VehicleManager
    //{
    //    private string mark = new string('-', 6);
    //    public override void VehicleMove()
    //    {
    //        int keyCheck;
    //        bool key, pedalCheck;
    //        Console.WriteLine("{0}\nzBisiklet çalıştırma"+"Gereksinimler >\n{1}",mark,mark);
    //        Console.WriteLine("Bisiklet anahtarı üzerinde mi");
    //        key= Convert.ToBoolean(Console.ReadLine());
    //        keyCheck = vehicleStart(key);

    //        Console.WriteLine( "->Pedal çeviriyor musunuz");
    //        pedalCheck = Convert.ToBoolean(Console.ReadLine());
    //        if (pedalCheck && keyCheck == 1)
    //        {
    //            Console.WriteLine("Bisiklet harekete geçti");
    //        }
    //        else if(pedalCheck ==false&&keyCheck == 1) 
    //        {
    //            Console.WriteLine("Hareket için pedal çevir");
    //        }
    //        else if(pedalCheck==true&&keyCheck == 0) 
    //        {
    //            Console.WriteLine("Hareket için anahtarı takın");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Hareket için anahtar ve pedalları çevirmek gerek");
    //        }
    //    }
    //}

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //22 MAYIS
    //internal class Program2
    //{
       //
//        // static void Main(string[] args)
//        {
//            string yapısal = "Yapısaldan kesin geçtim.";
//            string ptg = "Ben PTG den geçeçceğim.";

//            //string result=ptg.Remove(12);
//            //Console.WriteLine(result);

//            result = yapısal.Replace("kaldım", "geçtim");
//            Console.WriteLine(result);

//            //string result = ptg.PadLeft(10, '0');
//            //result = ptg.PadRight(10, '0');

//            int result = yapısal.IndexOf("kesin");
//            Console.WriteLine(result);

//            string[] x = { "C#", "OOP", "bir", "dildir" };
//            string temp = String.Join(" ", x);
//            Console.WriteLine(temp);




//        }

//        static void Main(string[] args)
//        {
//            string yapısal = "yapısaldan kesin geçtim";
//            string ptg = "ben ptg den geçeceğim";

//           //1) int result = yapısal.IndexOf("kesin");
//            Console.WriteLine(result);

//            //o değeri eşitse
//            //1 değeri birinci string sonra geliyorsa 

//            //-1 değeri birinci string önce geliyorsa

//           //2) if (yapısal.Equals(ptg))
//            {

//            }


//           //3) bool result = yapısal.Contains("kaldım");
////
////
////          4)if (result)
//            {
//                Console.WriteLine("SENEYE GÖRÜŞÜRÜZ");
//            }

//           //5) bool result = yapısal.EndsWith("kaldım");


//        }
//    }
//}




            
            
        //}
    //}

    public  class A
    {
        public  virtual void Ornekmetot()
        {
            Console.WriteLine("bu örnek");
        }
            
    }
    public class B : A
    {
        public override void Ornekmetot()
        {
            Console.WriteLine("bu ezilmiş");
        }
    }
    public class p
    {
        static void Main(string[] args)
        {
            A b = new B();   //B b =new B();
            b.Ornekmetot();
            Console.ReadLine();
        }
    }
    
}


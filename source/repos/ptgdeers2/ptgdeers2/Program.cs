using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ptgdeers2
{
    internal class Program
    {




        //static void Main(string[] args)


        //{





        //string abc = "Programlama Tekniklerine Giriş";
        //string[]test =abc.Split(',');

        //for(int i=0; i > abc.Length; i++)
        //{
        //    Console.WriteLine(abc[i]);
        //}

        //-not---içerisine bir string ve bir karakter alan karaktere göre stringi parçalayıp parçalanmış stringe geri dönen------> bir dizi


        //substring metodu

        //string abc = "Programlama Tekniklerine Giriş";
        //Console.WriteLine(abc.Substring(4)); //-----------------> ilk 4 karakteri atıp hepsini yazar.

        //string abc = "Programlama Tekniklerine Giriş";
        //Console.WriteLine(abc.Substring(4,4)); //---------------> ilk 4 karakteri atıp sonraki 4 ü yazar.

        //kullanıcıdan bir string bir int alan 0dan başlayıp girilen int kadar karakteri  çöpe atan kalan karakteri geri dönen:

        //equals metodu

        //string x = "abc";
        //string y = "abc";

        //if (x.Equals(y))
        //{
        //    Console.WriteLine("aynı string");

        //}
        //else
        //{
        //    Console.WriteLine("farklı string");
        // }





        // }

        //static string MySubString(int sayı,string kelime)
        //{
        //    string temp = "";
        //    for(int i=sayı;i<kelime.Length;i++)

        //        temp += kelime[i];
        //    return temp;

        //}
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("kaç harf");
        //    int sayı = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine("kelimeyi giriniz");
        //    string kelime=Convert.ToString(Console.ReadLine());
        //    for(int i = sayı; i < kelime.Length; i++)
        //    {
        //        Console.Write(kelime[i]);
        //    }
        //}



        //static string MySubStr2(string isim,int sayı1,int sayı2)
        //{
        //    string result = String.Empty;
        //    for(int i = sayı1; i < sayı2 + sayı1; i++)
        //    {
        //        result += isim[i];

        //    }
        //    return result;
        //}
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("bir sayı giriniz");
        //    int sayı1=Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine("bir sayı daha giriniz");
        //    int sayı2=Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine(  "bölmek istediğiniz yazıyı giriniz");
        //    string isim = Console.ReadLine();
        //    for (int i = sayı1; i < sayı2 + sayı1; i++)
        //    {
        //        Console.WriteLine(isim[i]);
        //    } 


        //}

        //equals metodu kodlanması:
        //static bool Esitlik (string x ,string y)
        //{
        //    bool flag = false;
        //    if (x.Length != y.Length)
        //        return false;
        //    else
        //    {
        //        for(int i=0;i<=x.Length;i++)
        //        {
        //            if (x[i] == y[[i])
        //                    return false;

        //        }
        //    }
        //    if (flag == true)
        //        return false;
        //    else
        //        return true;
        static void Main(string[] args)
        {

            //int [] tipinde ters çevir fonksiyona gelen diziye dokunma 
            //a) temp değişken kullan
            //b)kullanma 



            //int[] array = { 1, 2, 3, 4, 5 };
            //int[] secondArray = TersCevir(array);
            //for(int i = 0; i < secondArray.Length; i++)
            //{
            //    Console.WriteLine(secondArray[i]);
            //}



            //public static int[] TersCevir(int[] dizi)
            //{
            //    int[] dizi2 = new int[dizi.Length];
            //    for(int i=0;i<dizi.Length; i++)
            //    {
            //        dizi2[i] = dizi[dizi.Length - 1 - i];             //----------- EKSİK
            //    }

            //}






            //a=5,b=3 temp değişkeni olmadan yerini değiştirin.

            //Console.WriteLine("birinci değeri giriniz");
            //int a=Int32.Parse(Console.ReadLine());
            //Console.WriteLine("ikinci değeri giriniz");
            //int b=Int32.Parse(Console.ReadLine());

            //Console.WriteLine("a =" + a + "--b =" + b);

            //a = a + b;
            //    b = a - b;
            //    a = a - b;
            //Console.WriteLine( "a =" +a+ "--b =" + b );













            /*S1-int türünde verilen diziyi ters çevir.
             * eski diziye dokunma ,geri döndürülen dizi tersine çevrilmiş hali olacak
             * 
             * S2-Yine eski diziye dokunmadan;
             * temp değişken kullanmadan yer değiştir çevir
             * 
             * S3-Verilen dizi içinde (pozitif tam sayılar içeren)
             * tekrar eden ilk elemanı bul ve adetini bul
             * eğer yoksa -1 döndür
             * örn: input[]=1,2,3,4 output =-1
             * input [] =5,3,1,7,8,9,3,7,5,9 output = 3
             * 
             * S4= Saniye cinsinden verilen int sayının kaç saat kaç dakika kaç saniye olduğunu ekrana yazdıran metodu yazınız.
             * 
             * 
             * S5= Saat kolları arası açıyı hesapla
             * (saat ve dakika kolları-akrep yelkovan)
             */



            //saatler arası açı
            //Insertion sort
            //zeytinyağı sorusu






            //a=5,b=3 temp değişkeni olmadan yerini değiştirin.

            //Console.WriteLine("birinci değeri giriniz");
            //int a=Int32.Parse(Console.ReadLine());
            //Console.WriteLine("ikinci değeri giriniz");
            //int b=Int32.Parse(Console.ReadLine());

            //Console.WriteLine("a =" + a + "--b =" + b);

            //a = a + b;
            //    b = a - b;
            //    a = a - b;
            //Console.WriteLine( "a =" +a+ "--b =" + b );



            //a=5,b=3
            //Console.WriteLine("birinci değeri giriniz");
            //int a=Int32.Parse(Console.ReadLine());
            //Console.WriteLine("ikinci değeri giriniz");
            //int b = Int32.Parse(Console.ReadLine());

            //Console.WriteLine("a=" + a + "-----b=" + b);
            //a = a + b;
            //b = a - b;
            //a = a - b;
            //Console.WriteLine("a=" + a + "----b=" + b );




            //Console.ReadLine();




           

            Console.ReadLine();

            



        }



    }



}

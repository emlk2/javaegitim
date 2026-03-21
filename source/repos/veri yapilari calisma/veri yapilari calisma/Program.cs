using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veri_yapilari_calisma
{
    //internal class Program
    //{
    //static void Print(int x)
    //{
    //    //if (x == 0)
    //{
    //    return;  //x ,0 olunca metot sonlanır.
    //}

    //Console.WriteLine("sayac: "+ x);
    //x--;
    //Print(x);

    //}
    //--------------------------------------
    //static int Sum(int n)
    //{
    //    Console.WriteLine(n);
    //    if (n == 1)
    //    {
    //        return 1;
    //    }

    //    int result = Sum(n - 1) + n;
    //    Console.WriteLine(result) ;
    //    Console.WriteLine("-----");
    //    return result;
    //}


    //static void Main(string[] args)
    //{
    //    //Recursive metotlar( özyinelemeli metotlar)
    //metot içinde aynı metodu cagırmak.
    //herhangi bir recursive metot varsa bunu bir sekilde bitirmeliyiz, kısır döngüye giriyor.
    //Bir bitiş kuralı olmak zorunda.


    /*metot(1)=1;
     * metot(2)=1+2;           =>metot(2)=metot(1)+2;
     * metot(3)=1+2+3;
     * metot(4)=1+2+3+4;      =>metot(4)=metot(3)+4;
     * metot(5)=1+2+3+4+5;   =>metot(5)=metot(4)+5;

     * **/


    //Print(10);


    // Sum(4);
    //Console.ReadLine();

    //      not:FOR DÖNGÜSÜ
    //for(başlangıç;koşul;artış/azalış)
    // {
    //işlemler


    // }
    //for,başı ve sonunu bildiğimiz kaç kere döneceğini bildiğimiz döngüde kullanılır.
    //while,başlangıcı ve sonu belli olmayan döngülerde kullanılır.

    //for (int i = 1; i <= 10; i++)
    //{
    //    Console.WriteLine(i);
    //}
    //10 kere dönecek.1,2,3,4,5,6,7,8,9,10
    //---------------------------------------------------------------------


    //COK BOYUTLU DİZİLER:

    //int[] dizi = { 12, 13, 14 };
    //int[] dizi2 = new int[3];
    //int[,] dizi3 = { { 2, 3 }, { 4, 6 }, { 7, 8, } };
    //Console.WriteLine(dizi3[1,1]); //6

    //int[,,]dizi4=new int[2, 3, 4];

    //3 boyutlu dizinin en büyük elemanını bulma:

    //        int[,,] dizi = new int[3, 3, 3];
    //        Console.WriteLine("dizi elemanlarını girin:");
    //        for(int i = 0; i < 3; i++)
    //        {
    //            for(int j = 0; j < 3; j++)
    //            {
    //                for(int k = 0; k < 3; k++)
    //                {
    //                    dizi[i,j,k]=int.Parse(Console.ReadLine()); //konsoldan bir satır okur,stringi inte cevirir, diziye atar.
    //                }
    //            }
    //        }
    //        int max = enbüyükbul(dizi);
    //        Console.WriteLine("en büyük değer: "+max);
    //        Console.ReadLine();
    //    }
    //    static int enbüyükbul(int[,,] dizi)
    //    {
    //        int max = dizi[0, 0, 0]; //max değişkenini dizinin ilk elemanıyla başlatıyor.
    //        for(int i = 0; i < 3; i++)
    //        {
    //            for(int j = 0; j < 3; j++)
    //            {
    //                for(int k =0; k < 3; k++)
    //                {
    //                    if (dizi[i, j, k] > max)
    //                    {
    //                        max = dizi[i, j, k];
    //                    }
    //                }
    //            }
    //        }
    //        return max;
    //    }
    //}
    //-----------------------------------------------------------------------------------

    //DERS NOTLARI


    //internal class pp
    //{



    //    static void ders1(int a)
    //    {
    //        Console.WriteLine(a);
    //        ders1(a * 2);
    //    }
    //    static void Main(string[] args)
    //    {
    //        ders1(5);
    //        Console.ReadKey();

    //    }
    //}
    //------------------------------------------
    //internal class p
    //{
    //    static void ders1(int a) //kodun amacı tek tek 2 artırarak 100e ve üstüne ulaşana kadar çağrı yapmak,
    //                             //sonra çağrılar geri çözüldükçe a değerlerini ekrana yazdırmak.
    //    {
    //        if (a >= 100)
    //        {
    //            return; //a 100den büyükse metodu bitirmek için.
    //        }
    //        ders1(a + 2); 
    //        Console.WriteLine(a);
    //    }
    //    static void Main(string[] args)
    //    {
    //        ders1(5);
    //    }
    //}
    //-------------------------------------------------

    //class ppp
    //{
    //    static void ders1(int a, int b)
    //    {
    //        if (a >= 100)
    //        {
    //            return;
    //        }
    //        ders1(a + 2, b * (-1));
    //        Console.WriteLine(a*b);
    //    }
    //    static void Main(string[] args)
    //    {
    //        ders1(1, 1);
    //        Console.ReadKey();

    //    }
    //}
    //--------------------------------------------------------------------

    //***Recursive metotları yazmamızın sebebi=> karmaşık bir işi aynı işlemin daha küçük bir versiyonunu çözerek yapmak.Böylece problem
    //basit alt problemlere bölünür.

    //-----------------------------------------------------------------

    //class e
    //{
    //    static int emel(int a)
    //    {
    //        if (a >= 100) return 0;
    //        return a + emel(a + 1);
    //    }
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine(emel(1));
    //        Console.ReadKey();
    //    }
    //    //çıktısı= 1+2+3+...+n=[n(n+1)]/2 = 4950
    //}

    //----------------------------------------------------------------

    //15 satır, her satırda 2 veri var, fakültenin bölümlerinin nö ve iö sınıfları var.
    //0 bilgisayar 1 elektronik 3 makine 

    //    class a 
    //    { 
    //        static void Main(string[] args)
    //        {
    //            int[,] x = new int[15, 2]; // 15 satır 2 sütun.
    //            string[] str = new string[15]; //ikinci dizi.
    //            str[0] = "bilgisayar";
    //            str[1] = "elektronik";
    //            str[2] = "makine";
    //            int t = x[0, 1]; //başlangıç değeri.
    //            int b = 0;
    //            for(int i = 0; i < 15; i++) //bu döngü 0dan 14e kadar tüm satırları tarar, her satırın 1.sütunundaki değere bakar

    //            {
    //                if (x[i, 1] > t)
    //                {
    //                    t = x[i, 1]; //tye o yeni değeri atar.
    //                    b = i;      // bye o satırın numarasını kaydeder.
    //                }
    //                //x dizisinin 2. sütunundaki en büyük sayıyı bulur, ve
    //// o değerin kaçıncı satırda olduğunu b ile kaydeder.


    //            }
    //            Console.WriteLine(t); t: 
    //            Console.WriteLine(str[b]);
    //            Console.ReadLine();

    //        }
    //    }
    //--------------------------------------------------------------------

    //15 satır, her satırda 2 veri var, fakültenin bölümlerinin nö ve iö sınıfları var.

    //class a2
    //{
    //    static void Main(string[] args)
    //    {
    //        int[,] x=new int[15, 2];
    //        string[] ikincidizi = new string[15];

    //        ikincidizi[0] = "bilg";
    //        ikincidizi[1] = "elektr";
    //        ikincidizi[2] = "makine";

    //        int t = x[0, 1];
    //        int b = 0;
    //        for (int i = 0; i < 15; i++)
    //        {
    //            if (x[i, 1] > t)
    //            {
    //                t = x[i, 1];
    //                b = i;
    //            }

    //        }
    //            Console.WriteLine(t);
    //            Console.WriteLine(ikincidizi[b]);


    //            int[,] y = new int[25, 2];
    //            string []z = new string[15];
    //            z[0] = "bilg";
    //            z[1] = "elktr";
    //            z[2] = "makine";
    //            x[0, 0] = 550;
    //            x[0, 1] = 150;
    //            int temp = x[0, 1];
    //            int bolum = 0;
    //        for(int i = 0; i < 15; i++)
    //        {
    //            if (x[i, 1] > temp)
    //            {
    //                temp = x[i, 1];
    //                bolum = 1;

    //            }
    //        }


    //    }
    //}
    //-----------------------------------------------------------------------

    //class ee
    //{
    //    static int metot(int[,]array,int index)
    //    {
    //        int i1 = index / 100;
    //        int i2 = index % 100;
    //        return array[i1, i2] + metot(array, index + 1);

    //    }
    //    //amaç: indexten başlayıp dizi sonuna kadar tüm elemanların toplamını almak.
    //}

    //-------------------------------------------------------------------------

    class örn
    {
        //hangi harf daha cok?
        //static void Main(string[] args)
        //{
        //    string metin = "abbcdacbbbbbbabbbbbbbbbb";
        //    int sayi = 0;
        //    char ch = '';
        //    for(int i = 0; i < metin.Length; i++)
        //    {
        //        int adet = 0;
        //        for(int j = 0; j < metin.Length; j++)
        //        {
        //            if (metin[i] == metin[j])
        //            { adet++; }

        //        }
        //        if (adet>sayi)
        //        {
        //            sayi = adet;
        //            ch = metin[i];
        //        }
        //    }

        //------------------------------------------------------------------------------
        //class örn2
        //{
        //    static void Main(string[] args)
        //    {
        //        int[,] x = new int[25, 2];
        //        string[] st = new string[15];
        //        st[0]= "bilg";
        //        st[1]= "elktr";
        //        st[2]= "makine";
        //        x[0, 0] = 550;
        //        x[0, 1] = 150;
        //        int temp = x[0, 1];
        //        int bolum = 0;
        //        for (int i = 0; i < 15; i++)
        //        {
        //            if (x[i, 1] + x[i, 0] > temp)
        //            {
        //                temp = x[i, 1] + x[i, 0];
        //                bolum = 1;
        //            }
        //        }
        //    }


        //}
        //----------------------------------------------------------

        class örn3
        {
            static int ders2(int[,] dizi, int index1,, int index2)
            {
                if (index2 >= 100)
                {
                    index1++;
                    index2 = 0;
                }
                if (index1 >= 100)
                {
                    return 0;
                    return dizi[index1, index2] + ders2(dizi, index1, index2 + 1);

                }
            }
                static int ders3(int[,]array,int index)
                {

                }

            
        }





        }
    }
    





}
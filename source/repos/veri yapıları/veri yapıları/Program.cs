using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace veri_yapıları
{
//    internal class Program
//    {
        //    //RECURSİON 
        //RECURSİVE CODİNG  , bir metodun kendini çagırması,
        //static void ders1(int a,int f)
        //{
        //    //not:
        //    //stack olusur, 10 mb default değer    

        //    short s = 1;
        //    byte b = 2;  //stacktaki degiskenlerin hepsi aynı boyut kaplar 4 byte ve 4 ün katları (long 8)
        //    int c = 2;
        //    //-----------------------------------------------------------------------



        //    if (a >= 100) return;   //Recursive yapılarda şarttır.
        //    Console.WriteLine(a);
        //    ders1(a + 1,f*-1);
        //}

        //recursive yapılar:
        //forla gerçekleşen her sey recursive olur, 
        //static void Main()
        //{
        //    ders1(1,1);




        //}
        //static void Main(string[] args) 
        //{



        //-------------------DİZİLER-------------------

        //1den 100 e kadar olan sayıları toplayalım:

        //int t = 0;

        //for (int i = 1; i < 100; i++)
        //{
        //    t = t + i;

        //}
        //Console.WriteLine(t);

        ////1+3+5+7+9+11..
        //int b = 0;
        //for (int i = 1; i < 100; i = i + 2)
        //{
        //    b = b + i;

        //}
        //Console.WriteLine(b);

        //1-2+3-4+5-6....100
        //int t = 0;
        //for(int i = 1; i < 100; i++)
        //{
        //    if (i % 2 == 1)
        //    {
        //        t = t + 1;

        //    }
        //    else
        //    {
        //        t = t - i;
        //    }
        //}

        //int f = 1;
        //t = 0;
        //for (int i = 1; i < 100; i++)
        //{
        //    t = t + f * i;
        //    f = f * -1;
        //}

        //1+2+3-4-5+6+7-8-9...100
        //int f = 1;
        //int t = 1;
        //int a = 0;
        //for(int i = 2; i < 100; i++)
        //{
        //    t = t + f * i;
        //    a++;
        //    if (a == 2)
        //    {
        //        f = f * -1;
        //        a = 0;
        //    }

        //}

        //1+2-3-4+5+6+7-8-9-10-11..100
        //int f = 1;
        //int t = 1;
        //int a = 0;
        //int b = 1;
        //for(int i = 2; i <= 100; i++)
        //{
        //    t = t + f * i;
        //    a++;
        //    if (a == b)
        //    {
        //        f = f * -1;
        //        b++;
        //        a = 0;
        //    }
        //}


        //1+;2-;3+4
        //------------------------------------------------------------------------------
        //Diziler yeni değişken tanımlamayı kolaylaştırır, çoklu değişkendir.
        //kaç eleman olacagı bellidir, tipi bellidir, 
        //x[0]ın hafıza adresi 1250  ise x[1] in 1254 -> dörder byte gider.
        //x[0] ve x[3] arasında 12 byte fark  olur.
        //int[] x = new int[10];
        //int[,] y = new int[2, 4];  //bilgisayarda veriler tek boyutlu olarak saklanır.

        //int[,] z = new int[5000, 5000];
        //Stopwatch sw =new Stopwatch();

        //int[,] x = new int[15, 2];
        //string[] str = new string[15];
        //    str[0] = "Bilgisayar";
        //    str[1] = "Elektronik";
        //    str[2] = "Makine";
        ////15v satır her satırda 2 veri var
        ////fakültenin bölğmlerinin nö ve iö öğrenci sayıları
        ////0 bilgisayar, 1 elektronik, 3 makıne
        //x[0, 0]=550 ;   
        //x[0, 1] =350;

        //  int t = x[0, 1];
        //    int b = 0;
        //    for (int i = 0; i < 15; i++)
        //    {
        //        //for (int j=0;j<2;j++)
        //        {
        //            if (x[i, 1] > t)
        //            {
        //                t = x[i, 1];
        //                b = i;
        //            }
        //        }
        //    }
        //    Console.WriteLine(t);
        //    Console.WriteLine(str[b]);
        //----------------------------------------------------------------

        //6 EKİM
        //iki boyutlu dizinin recursive toplanması.
    //    static int ders1(int[] x, int indis)
    //        {
    //            if (indis <= x.Length) return 0;
    //            return x[indis] + ders1(x, indis + 1);
    //        }


    //    static int ders2(int[] x, int indis1,int indis2)    
    //    {
            
    //        if (indis2 >= 100)
    //        {
    //            indis1++;
    //            indis2 = 0;
    //        }
    //        if (indis1 >= x.Length) return 0;
    //        return x[indis1,indis2] + ders1(x, indis1 + 1,indis2+1);
    //    }

    //    static int ders3(int[,] x, int indis)  //*****
    //    {
    //        if (indis >= 100) return 0;
    //        int i1 = indis / 100;
    //        int i2 = indis % 100;

    //        return x[i1, i2] + ders3(x, indis + 1);



    //    }


    //    static void Main()
    //    {

    //        int[,] x = new int[100];//,100];
    //        Console.WriteLine(ders1(x, 0));
    //        int t = 0;
    //        for (int i = 0; i < 100; i++)
    //        {
    //            t = t + x[i];

                  
    //        }
            

    //        int[,] y = new int[100, 100];

    //        //-----------------------------------------------------------------------------------------------------------
    //        //örnek2:

    //        string st = "abcdefabbbbbccbbbbbbbbdef";    //bu dizideki en çok kullanılan harfi bulun : b

    //        int sayi = 0;
    //        char ch = ' ';

    //        for (int i = 0;i < st.Length; i++)
    //        {
    //            int adt = 0;
    //            for (int j = 0; j < st.Length; j++)
    //            {
    //                if (st[i] == st[j])
    //                {
    //                    adt++;
    //                }
    //            }
    //            if (adt > sayi)
    //            {
    //                sayi = adt;
    //                ch = st[i];
    //            }
    //        }

    //        //daha hızlı çözmek için:
    //        //abcdefabbbbbccbbbbbbbbdef
    //        //iki döngü var ama yan yana iç içe değil. 
    //        //*********önemli******
    //        sayi = 0;
    //        char chh = ' ';
    //        int[] z = new int[250];
    //        for (int i = 0; i < st.Length; i++)
    //        {
    //            z[(byte)st[i]]++;

    //        }
    //        for (int i = 0; i<st.Length; i++)
    //        {
    //            if (z[i] > sayi)
    //            {
    //                sayi = z[i];
    //                chh = (char)i;
    //            }
    //        }


    //        //4 ve 5 boyutlu diziler
    //         int[,,,,] uni = new int[15, 10, 2, 4, 2]; // fakuülte, bölüm, nö-iö,sınıflar,cinsiyet

    //        //ödev1 :birinci diziyi recursive olarak  topla*****
    //        //bu dizideki kadın ya da erkeklerden  sayısı fazla olanı alalım

    //        int[,,,,,,,,,,,,,,,,,,] xx = new int[8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8]; //19 tane for yazamayız
    //        //ödev2:bunu tek döngü ile çöz.
            

    //        for (int i = 0; i < 1; i++)
    //        {
    //            for (int j = 0; j < 10; j++)
    //            {
    //                for (int k = 0; k < 2; k++)
    //                {
    //                    for (int m=0; m < 4; m++)
    //                    {
    //                        for (int q=0 ; q < 2; q++)
    //                        {

    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        //uni[0, 0, 0, 0, 0] = 400;

    //        //int[,,,,,] yk = new int[300, 15, 10,2, 4, 2];

    //        //int[,,,,] iller = new int[81,20,50,20,200]; //il, ilçe,mahalle,sokak,hane


    //        //**DİZİLERDE PROBLEMLER**
    //        //problem1:
    //        int[] abc = new int[100];
    //        abc[0]=12;
    //        abc [1] = 15;
    //        abc[2] = 22;
    //        //belirlediğimiz indisindeki değeri silmek için  birbirlerinin üstüne yazacagız ama yine de son elemanı silemeyiz
    //        //dizilerin bu yüzden ELEMAN SİLME PROBLEMİ vardır.
    //        for (int i = 2;i < 99; i++)
    //        {
    //            abc[i] = abc[i + 1];
    //        }

    //        //recursive soru kesin gelecek sınavda.
    //        //------------------------------------------------------------------

    //        //DİZİLERE ALTERNATİF
    //        //LİNKED LİST
    //        //BAGLI LİSTELER
    //        //POINTERLARI KULLANARAK YENİ BİR VERİ YAPISI GELİSTİRİLDİ
            

    //        BlockList b1 = new BlockList();
    //        b1.data = 1;



    //        }

    //    }






    //    // }
    //    //}


    //    //********
















    //***13 EKİM***
    class Block
    {
        public int data;
        public Block next;
        
    }
    class Block2
    {
        public int data;
        public Block2 next;
        public Block2 prev;
    }

    
    class program2
    {

        static void linkAl(ref Block bl, int data)
        {
            if (data == 101) return;

            if (bl == null) { bl = new Block(); bl.data = data; }

            else
            {
                bl.next = new Block();
                bl.next.data = data;

            }
            linkAl(ref bl, data+1);
        }

        static void linkYaz(Block tmp)
        {
            if (tmp == null) return;
            Console.WriteLine(tmp.data);

            linkYaz(tmp.next);
        }

        static void x (ref int a,  int b)
        {
            a = 123;
            b=
        }
        static void Main (string[] args)
        {

            int a = 0;
            int b = 1;
            x(ref w, b);
            Console.WriteLine(w);
            Console.WriteLine(b);
            return;

            //linkede list

            //Elle liste oluşturma:
            //Block bl = new Block();
            //bl.data = 4;

            //bl = new Block();

            //****100 adet block oluşturalım data değerleri 1den 10a kadar olsun:****

            //listenin ilk elemanı kesinlikle tutulmalıdır(point edilmelidir).
            


            Block head = null;

            Block last = null;
            Block tmp = null;

            linkAl(ref head,1);
            linkYaz(head);
            return;

            head = new Block();
            head.data = 1;

            last = head;

            tmp = new Block();
            tmp.data = 2;

            last.next = tmp;
            last = tmp;

            tmp = new Block();
            tmp.data = 3;
            last.next = tmp;
            last = tmp;

            tmp = new Block();
            tmp.data = 4;
            last.next = tmp;
            last = tmp;
            last.data = 99; 

            tmp = head;
            while (tmp != null)
            {
                Console.WriteLine(tmp.data);
                tmp = tmp.next;
            }

            head = null;

            last = null;
            for(int i = 22; i >=11; i++)
            {
                tmp=new Block();
                tmp.data= i;

                tmp.next = last;
                last = tmp;
            }
            head = last;
            tmp = last;
            while(tmp!= null)
            {
                Console.WriteLine(tmp.data);
                tmp=tmp.next;
            }



//            Pointer, bir değişkenin bellek adresini(yani RAM’deki konumunu) tutan değişkendir.

//Normal değişkenler değeri saklar,
//pointer’lar ise değerin bulunduğu adresi saklar.



            //Recursive algoritmayı ileri taşır, bazı problemlerde alternarif yoktur kullanmak zorundayız, 

        }
    }

    }






using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _ptgfinalcalisma
{
    internal class Program
    {




        //static void Main(string[] args)
        //{
        //switch case

        //girilen mevsimin aylarını yazdıran program:
        //string mevsim;
        //Console.WriteLine("lütfen bir mevsim adı giriniz");
        //mevsim = Console.ReadLine();
        //switch (mevsim)
        //{
        //    case "kış":
        //        Console.WriteLine("kış=aralık,ocak,şubat");
        //        break;
        //    case "ilkbahar":
        //        Console.WriteLine("ilkbahar=mart,nisan,mayıs");
        //        break;
        //    case "yaz":
        //        Console.WriteLine("yaz=haziran,temmuz,ağustos");
        //        break;
        //    case "sonbahar":
        //        Console.WriteLine("sonbahar=eylül,ekim,kasım");
        //        break;
        //    default:
        //        Console.WriteLine("hatalı mevsim girişi");
        //        break;

        //}
        //----------------------------------------------------------

        //random komutu

        //Random emel = new Random();
        //int a;
        //a=emel.Next(0,11);   //0,1,2,3,4,5,6,7,8,9,10 syılarından biri rastgele gelir.
        //Console.WriteLine(a);

        //Random rastgele = new Random();
        //int sayi;                          //65e kadar herhangi bir sayı.
        //sayi = rastgele.Next(65);
        //Console.WriteLine(sayi);

        //Random rnd=new Random();   //int tanımlı aralığından herhangi bir sayı gelir.
        //int s1;
        //s1=rnd.Next();
        //Console.WriteLine(s1);


        //Random random=new Random();
        //Console.WriteLine("****şehir atama pogramı****");
        //string[] sehir = { "a", "b", "c", "d" ,"s"};

        //int a;
        //a=random.Next(0,sehir.Length);
        //Console.WriteLine(a);
        //Console.WriteLine(sehir[a]);

        //}

        //----------------------------------------------------------------


        //int[] sayilar = new int[5];
        //    for(int i=0;i<5;i++)
        //{
        //    Console.WriteLine((i+1).ToString()+" .sayıyı giriniz: ");
        //    sayilar[i] = Convert.ToInt32(Console.ReadLine());
        //}                                                                     //buraya kadar kullanıcıya 5 tane sayı girdiriyor.

        //int enbuyuk;
        //enbuyuk = sayilar[0];
        //for(int i=1;i<5;i++)
        //{
        //    if (enbuyuk < sayilar[i])
        //    {
        //        enbuyuk = sayilar[i];
        //    }
        //}
        //Console.WriteLine(enbuyuk);     //en büyük olan sayıyı yazdırdı.



        //string[] isim = new string[3];
        //int[]s1= new int[3];
        //int[]s2= new int[3];
        //int[] ort = new int[3];
        //for(int i=0;i<3;i++)
        //{
        //    Console.Clear();

        //    Console.Write(i+1 + " Öğrencinin adı: ");
        //    isim[i] = Console.ReadLine();
        //    Console.Write(i+1 + " sınav1  notu");
        //    s1[i]=Convert.ToInt32(Console.ReadLine());

        //    Console.Write(i+1 + " sınav2  notu"  );
        //    s2[i]=Convert.ToInt32(Console.ReadLine());

        //    ort[i] = (s1[i] + s2[i]) / 2;


        //}
        //Console.Write("Öğrenci   Sınav1    Sınav2    Ortalama");
        //Console.WriteLine();

        //for(int i=0;i<3; i++)
        //{
        //    Console.WriteLine(" " + isim[i] +" "+ s1[i] + " " + s2[i]+ " " + ort[i]);
        //}

        //Foreach döngüsü  (değişkenin türü+ değişkenin adı+ in + dizinin ismi)
        //string[] sehirler = { "ankara", "artvin", "samsun", "diyarbakır", "tokat", "şırnak" };

        //foreach(string i in sehirler)    (int i in sayilar)
        //{
        //    Console.WriteLine(i);
        //}

        //dizideki sayıları forech ile toplatalım

        //int[] sayilar = { 1, 4, 5, 9, 6 };
        //int toplam = 0;
        //foreach(int i in sayilar)
        //{
        //    toplam = toplam + i;
        //}
        //Console.WriteLine("toplam:" + toplam);

        //sadece 2ye bölünenleri yazdırsın

        //int[] rakamlar = { 1, 3, 4, 6, 8, 9 };
        //foreach(int i in rakamlar)
        //{
        //    if(i % 2 == 0)
        //    {
        //        Console.WriteLine(i);
        //    }
        //}

        //6 nın faktorıyeli

        //int[] degerler = { 1, 2, 3, 4, 5, 6, };
        //int sonuc = 1;
        //foreach (int i in degerler)
        //{
        //    sonuc = sonuc * i;
        //}
        //Console.WriteLine("6 sayısının faktöriyeli: " + sonuc);


        //pozitif ve tek sayıları yazdıran

        //int[] numbers = { 4, 1, 7, -8, 23, 24, 51, 65, 76345, 11 };
        //foreach(int i in numbers)
        //{
        //    if(i>0 && i%2== 1)
        //    {
        //        Console.WriteLine(i);
        //    }
        //}

        //10-30 arasında &&çift



        //int[] emelsayiları = { 1, 2, 4,-5, 34, 45, 67, 23, 24, 22, 12 };
        //foreach(int i in emelsayiları)
        //{
        //    if(i<=30 && i>=10&& i % 2 == 0)
        //    {
        //        Console.WriteLine(i);
        //    }
        //}
        //----------------------------
        //sort komutu
        //int[] sayilarım = new int[5];
        //for(int i=0;i<5;i++)
        //{
        //    Console.WriteLine("sayı giriniz: ");
        //    sayilarım[i]=Convert.ToInt32(Console.ReadLine());
        //}
        //Array.Sort(sayilarım);

        //foreach (int i in sayilarım)
        //{
        //    Console.WriteLine(i);
        //}
        //---------------------------------------

        //Array.Sort------>küçükten büyüğe sıralar.
        //Array.Reverse--->tersten yazar.


        //int[] sayilar = new int[3];
        //for(int i=0;i<3;i++)
        //{
        //    Console.WriteLine("sayı giriniz: ");
        //    sayilar[i]=Convert.ToInt32(Console.ReadLine());
        //}
        //Array.Reverse(sayilar);
        //foreach(int i in sayilar)
        //{
        //    Console.WriteLine(i);
        //}

        //int[] sayilar = new int[5];
        //for(int i=0;i<5;i++)
        //{
        //    Console.WriteLine("sayı gir:");
        //    sayilar[i] = Convert.ToInt32(Console.ReadLine());
        //}

        //Array.Sort(sayilar);

        //Console.WriteLine("en küçük: " +sayilar[0]);   //en büyük ve en küçüğü yazdırır.
        //Console.WriteLine("en büyük: " + sayilar[4]);
        //Console.WriteLine("dizinin boyutu: "+sayilar.Length);

        //writeLine alt alta!
        //write yan yana !


        //Console.WriteLine("dizinin eleman sayısını giriniz");
        //int elemansayisi=Convert.ToInt32(Console.ReadLine());
        //int[]dizi=new int[elemansayisi];
        //for(int i=0;i<elemansayisi;i++)
        //{
        //    Console.WriteLine("sayı giriniz: ");
        //    dizi[i] = Convert.ToInt32(Console.ReadLine());
        //}
        //Array.Sort(dizi);
        //Console.WriteLine("en büyük: "+ dizi[elemansayisi-1]);
        //-----------------------------------------------------------




        //-----------------------

        // Console.WriteLine("En Büyük Sayı: " + sayilar[(sayilar.Length - 1)]);
        //-----------------------
        //-----------------------------------------------------------------------
        //char değişkeni
        //char harf;
        //harf = 'g';
        //Console.WriteLine(harf);

        //char cinsiyet;
        //cinsiyet = Convert.ToChar(Console.ReadLine());
        //if(cinsiyet=='e'|| cinsiyet == 'E')
        //{
        //    Console.WriteLine("cinsiyet erkektir");
        //}
        //else
        //{
        //    Console.WriteLine("cinsiyet kadın");//e ve E dışındaki bütün karakterlerde kadın.
        //}
        //Console.ReadLine();

        /*if(cinsiyet=='e' || cinsiyet=='E')
         * Console.WriteLine("erkek")
         * else if(cinsiyet=='k'||cinsiyet =='K')
         * Console.WriteLine("kadın")
         */

        //char ders;
        //Console.WriteLine("......YKS DERSLERİ MENÜSÜ");
        //Console.WriteLine("türkçe");
        //Console.WriteLine("matematik");
        //Console.WriteLine("fizik");
        //Console.WriteLine("kimya");
        //Console.WriteLine("biyoloji");
        //Console.WriteLine("hangi dersin bilgilerini görmek istersiniz? ");

        //ders =Convert.ToChar(Console.ReadLine());
        //if(ders == 't' || ders == 'T')
        //{
        //    Console.WriteLine("türkçe");
        //}
        //else if (ders == 'm' || ders == 'M')
        //{
        //    Console.WriteLine("matematik");
        //}
        //else if (ders == 'f' || ders == 'F')
        //{
        //    Console.WriteLine("fizik");
        //}
        //else if (ders == 'k' || ders == 'K')
        //{
        //    Console.WriteLine("kimya");
        //}
        //else if (ders == 'b' || ders == 'B')
        //{
        //    Console.WriteLine("biyoloji");
        //}
        //else
        //{
        //    Console.WriteLine("hatalı terim girdiniz,sonuç yok.");
        //}

        //aynısını switch case i,le de yapabiliriz:

        //char ders;
        //ders=Convert.ToChar(Console.ReadLine());
        //Console.WriteLine("hangi dersi görmek istiyorsunuz?");
        //switch (ders)
        //{
        //    case 'b':
        //        Console.WriteLine("biyoloji");
        //        break;
        //    case 't':
        //        Console.WriteLine("türkçe");
        //        break;
        //    default:
        //        Console.WriteLine("hatalı terim");
        //        break;

        //}


        //float: 32 bitlik kayan noktalı değerleri saklayan değişken. (double ,float decimal: kayan noktalı değişkenler)
        //(7 basamak)

        //float sayi;
        //sayi = 12334;
        //Console.WriteLine("sayı: "+sayi);

        //float s1, s2;
        //s1 = 23;
        //s2 = 34;
        //float toplam;
        //toplam = s1 + s2;
        //Console.WriteLine("toplam: "+toplam);

        //int x = 3;
        //float y = 2.5f;
        //int z = 9;
        //float toplam;
        //toplam = x + y + z;
        //Console.WriteLine("toplam: "+toplam);


        //Decimal (128 bit=16 byte, 29 basamak)

        //satış fiyatları decimal,satış adeti int ;gün sonunda kasadakai parayı bul.

        //decimal urun1fiyat, urun2fiyat, toplam;
        //int s1, s2;
        //urun1fiyat=14.54M;
        //urun2fiyat = 43.678m;
        //Console.WriteLine("birinci ürünün satış adeti: ");
        //s1 = Convert.ToInt32(Console.ReadLine());
        //Console.WriteLine("2.ürünün satış adeti: ");
        //s2= Convert.ToInt32(Console.ReadLine());

        //toplam = (s1 * urun1fiyat) + (s2 * urun2fiyat);
        //Console.WriteLine("toplam: "+toplam+"tl");

        //bool

        //int sayi;
        //Console.WriteLine("sayıyı girin:");
        //sayi =Convert.ToInt32(Console.ReadLine());
        //bool durum1= sayi > 0;
        //bool durum2 = sayi > 10;
        //Console.WriteLine("sayı pozitif mi? " +durum1);
        //Console.WriteLine("sayı 10'dan büyük mü? " + durum2);

        //Console.WriteLine("şifreyi girin");
        //int sifre;
        //sifre=Convert.ToInt32(Console.ReadLine());
        //bool durum1 = sifre == 1234;
        //Console.WriteLine("sifre doğru mu ? " + durum1);





        // çok boyutlu dizi
        //2x2

        //int[,] dizi = new int[2, 2];
        //dizi[0, 0] = 25;
        //dizi[0, 1] = 35;
        //dizi[1, 0] = 17;
        //dizi[1, 1] = 16;

        //for (int i = 0; i < 2; i++)
        //{
        //    for (int j = 0; j < 2; j++)
        //    {
        //        Console.Write(dizi [i , j]);
        //    }
        //    Console.WriteLine();
        //}


        //2x3

        //int[,] dizi = new int[2, 3];
        //dizi[0, 0] = 10;
        //dizi[0, 1] = 20;
        //dizi[0, 2] = 30;
        //dizi[1, 0] = 40;
        //dizi[1, 1] = 50;
        //dizi[1, 2] = 60;
        //for(int i = 0; i < 2; i++)
        //{
        //    for(int j = 0; j < 3; j++)
        //    {
        //        Console.Write("  "+ dizi[i,j]);
        //    }
        //    Console.WriteLine();
        //}


        //4x4 iki tane matrisi toplama

        //int[,] matris1 = { { 40, 30, 20, 10 }, { 20, 14, 16, 13 }, { 25, 14, 41, 21 }, { 32, 36, 41, 57 } };
        //int[,] matris2 = { { 40, 30, 20, 10 }, { 7, 9, 8, 6 }, { 21, 22, 23, 24 }, { 34, 35, 36, 37 } };
        //int[,] toplam = new int[4, 4];
        //for(int i = 0; i < 4; i++)
        //{
        //    for(int j = 0; j < 4; j++)
        //    {
        //        toplam[i, j] = matris1[i, j] + matris2[i, j];
        //    }
        //}

        //for(int k = 0; k < 4; k++)
        //{
        //    for(int m = 0; m < 4; m++)
        //    {
        //        Console.Write(" "+toplam[k, m]);
        //    }
        //    Console.WriteLine();

        //}


        //kullanıcının satır ,sütun değerlerini girdiği matrisin transpozunu al.
        //int satir, sutun;
        //Console.WriteLine("satır sayısını gir: ");
        //satir=Convert.ToInt32(Console.ReadLine());
        //Console.WriteLine("sütun sayısını gir: ");
        //sutun = Convert.ToInt32(Console.ReadLine());
        //int[,]matris=new int[satir,sutun];
        //for(int i = 0; i < satir; i++)
        //{
        //    for(int j = 0; j < sutun; j++)
        //    {
        //        Console.Write("matrisin 0x1 değeri"+i+1,j+1);
        //        matris[i,j]= Convert.ToInt32(Console.ReadLine());
        //    }
        //    Console.WriteLine();
        //}
        //for(int k = 0; k < satir; k++)
        //{
        //    for(int m = 0; m < sutun; m++)
        //    {
        //        Console.Write(matris[k,m]);
        //    }
        //    Console.WriteLine();
        // }  
        //buraya kadar kullancının girdiği değerlere göre matrisi yazdırdık.
        //şimdi transpozunu alacağız. (transpoz=satırlar sütun sütunlar satır olmalı 
        //2 5 6            2 4
        //       --------> 5 7
        //4 7 9            6 9

        //for(int x = 0; x < satir; x++)
        //{
        //    for(int y =0;y < sutun; y++)
        //    {
        //        Console.Write(matris[y,x]+" ");
        //    }
        //    Console.WriteLine();
        //}

        //yıldızlarla şekil

        //for(int i=0; i<=10;i++)
        //{
        //    Console.WriteLine("*"); //ekrana 10 tane alt alta yıldız
        //}

        /*
         **
         *** 
         ****
         *****
         ******
         *******
         ********
         *********
         **********
         * 
         */

        //for (int i = 1; i <= 10; i++)
        //{
        //    for (int j = 0; j < i; j++)
        //    {
        //        Console.Write("*");
        //    }
        //    Console.WriteLine();
        //}


        //for (int i = 1; i <= 10; i++)
        //{
        //    for (int j = 0; j < i; j++)
        //    {
        //        Console.Write("*");
        //    }
        //    Console.WriteLine();
        //}

        //for(int i=0;i<=10;i++)
        //{
        //    for(int j = 10; j > i; j--)
        //    {
        //        Console.Write("*");
        //    }
        //    Console.WriteLine();
        //}



        //}

        //metotlar

        //    private static void emel(string bilgi)
        //    {
        //        for(int i = 0; i <= 10; i++)
        //        {
        //            Console.WriteLine(bilgi);
        //        }
        //    }
        //    static void Main(string[] args)
        //    {
        //    Console.WriteLine("metni girin: ");
        //    string blg = Console.ReadLine();
        //    emel(blg);
        //    Console.ReadLine();

        //}

        //private static int topla(int s1,int s2)
        //{
        //    int toplam = s1 + s2;
        //    return toplam;
        //}

        //static void Main(string[] args)
        //{
        //    Console.WriteLine("toplamm= " + topla(5, 8));
        //    Console.ReadLine();
        //}




        //------------------------------------------------------------------------------------
        //NOT

        //        int i, j = 1, k;
        //for (i = 0; i< 5; i++)
        //{
        //    k = j++ + ++j;
        //    Console.Write(k + " ");
        //}


        //        i = 0 için:
        //Başlangıçta j = 1.
        //k = j++ + ++j ifadesi:
        //j++ → j = 1 (yani önce 1 alınır, sonra j 2'ye çıkar).
        //++j → j = 3 (önce j 2'den 3'e çıkar, sonra 3 alınır).
        //k = 1 + 3 = 4.
        //k değeri ekrana yazdırılır: 4.
        //2. i = 1 için:
        //j = 3 (bir önceki döngüde j 3 olmuştu).
        //k = j++ + ++j ifadesi:
        //j++ → j = 3 (yani önce 3 alınır, sonra j 4'e çıkar).
        //++j → j = 5 (önce j 4'ten 5'e çıkar, sonra 5 alınır).
        //k = 3 + 5 = 8.
        //k değeri ekrana yazdırılır: 8.
        //3. i = 2 için:
        //j = 5 (önceki döngüde j 5 olmuştu).
        //k = j++ + ++j ifadesi:
        //j++ → j = 5 (yani önce 5 alınır, sonra j 6'ya çıkar).
        //++j → j = 7 (önce j 6'dan 7'ye çıkar, sonra 7 alınır).
        //k = 5 + 7 = 12.
        //k değeri ekrana yazdırılır: 12.
        //-------------------------------------------------------------------------------

        //birim matris
        //static void Main(string[] args)
        //{
        //    int[,] matris2d = new int[3, 3];
        //    for (int i = 0; i < matris2d.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < matris2d.GetLength(1); j++)
        //        {
        //            if (i == j)
        //            {
        //                matris2d[i, j] = 1;
        //            }
        //            else
        //                matris2d[i, j] = 0;
        //        }

        //    }
        //    for (int i = 0; i < matris2d.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < matris2d.GetLength(1); j++)
        //        {
        //            Console.Write(matris2d[i, j] + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //        Console.ReadLine();

   // }


//        public static string[] SplitSorusu(string metin,char ayrac)
//        {
//            int elemansayisi = 1;
//            for(int i=0;i<metin.Length;i++)
//            {
//               if( metin[i] == ayrac)
//                {
//                    elemansayisi++;
                        
//                }

                
//            }
//            string[] gecicidizi = new string[metin.Length];
//                string kelime = "";
//            int index = 0;
//            for(int i = 0; i < metin.Length; i++)
//            {
//                if (metin[i] == ayrac)
//                {
//                    gecicidizi[index] = kelime;
//                    index++;
//                    kelime = "";
//                }
//                else
//                {
//                    kelime += metin[i];
//                }
//            }

//            if (kelime.Length > 0)
//            {
//                gecicidizi[index] = kelime;
//                index++;
//            }
//            return gecicidizi;
//        }
}

}

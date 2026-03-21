using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace örnekler
{
    internal class Program
    {
        //static void Main(string[] args)
        //{

            //int sayi;
            //Console.Write("sayı giriniz:");
            //sayi=Convert.ToInt32(Console.ReadLine());
            //if (sayi >= 0)
            //{
            //    Console.WriteLine("SAYI POZİTİF");
            //}
            //else
            //{
            //    Console.WriteLine("SAYI NEGATİF");
            //}
            //Console.ReadKey();


            //string ad, sifre;
            //Console.Write("Kullanıcı adı giriniz:");
            //ad=Console.ReadLine();
            //Console.WriteLine("şifre gir");
            //sifre=Console.ReadLine();
            //if (ad == "admin" && sifre == "12345") 
            //{
            //    Console.WriteLine("giriş başarılı");

            // }
            //else
            //{
            //    Console.WriteLine("kullanıcı adı ve şifreyi kontrol et");
            //}
            //Console.ReadKey();

            //dikdörtgenin alanını ve çevreini hesaplayan örnek

            //int kisa, uzun, alan, cevre;
            //Console.Write("KISA KENAR:");
            //kisa=Convert.ToInt32(Console.ReadLine());
            //Console.Write("UZUN KENAR:");
            //uzun =Convert.ToInt32(Console.ReadLine());
            //alan = kisa * uzun;
            //cevre = 2 * (kisa + uzun);
            //Console.WriteLine("Alan:" + alan);
            //Console.WriteLine("Çevre:" + cevre);
            //Console.ReadKey();

            //  veriTipi[] değişkenismi= new veriTipi[elemansayısı]

            // veritipi[] değişkenismi= { 1,2,3,4,5,6,}

            //  int[] dizi1= new int[10];
            //   int[] dizi2= {1,2,3,4,5,6,7};

            //dizi1 [0] = 10;
            // int değer=dizi2[4];

            //  for(int i = 0; i < dizi1.Length; i++)
            //  {
            //      dizi1[i] = i + 2;

            //  }
            //  for (int i = 0; i < dizi2.Length; i++)
            //  {
            //      Console.WriteLine(dizi2[i]);
            //  }

            //Console.WriteLine("kaç elemanlı bir dizi oluşturmak istersiniz?");
            //int[] dizi = new int[Int32.Parse(Console.ReadLine())];

            //Random rnd = new Random();
            //for(int i=0; i < dizi.Length; i++)
            //{
            //    dizi[i] = rnd.Next(0, 100);
            //}
            //int toplam = 0;
            //for(int i = 0; i < dizi.Length; i++)
            //{
            //    toplam += dizi[i];
            //    Console.WriteLine("ortalama:" +toplam/dizi.Length);
            //}

            //_-------------------------------------------------------------------------------------------------------------------------
            //cok boyutlu diziler  murat yücedağ
            //2 boyutlu


            //int[,] dizi=new int[2,2];//2 satır ve 2 sütundan oluşuyor.
            //dizi[0, 0] = 25;
            //dizi[0, 1] = 35;//0.satır 1.sütun
            //dizi[1, 0] = 17;
            //dizi[1, 1] = 16;

            //for (int i = 0; i<2;i++)  // sütunları okuması için döngü ,0dan 2 ye kadar bir bir artacak.

            //{
            //    for (int j = 0; j < 2; j++)//sütunlar için

            //        Console.Write(" {0} ", dizi[i, j]);
            //        Console.WriteLine();

            //


            //int[,] dizi = new int[2, 3];
            //dizi[0, 0] = 10;
            //dizi[0, 1] = 20;
            //dizi[0,2] = 30;
            //dizi[1,0] = 40;
            //dizi[1, 1] = 50;
            //dizi[1, 2] = 60;

            //for(int i = 0; i < 2; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //        Console.Write(" {0} ", dizi[i, j]);
            //    Console.WriteLine();
            //}


            //cok boyutlu dizilerde toplama

            //Console.WriteLine("matrislerde Toplama");
            //int[,] matris1 = { { 10, 12, 14, 18 }, { 20, 14, 16, 13 }, { 25, 14, 41, 21 }, { 32, 36, 41, 57 } };
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
            //    Console.WriteLine();
            //    for(int m = 0; m < 4; m++)
            //    {
            //        Console.Write(toplam[k, m] + " ");
            //    }
            //}
            //-----------------------------------------------------------------
            //kullanıcı tarafından satır ve sütun sayıları girilen matris

            //int satir, sutun;
            //Console.Write("Satır sayısını giriniz:");
            //satir = Convert.ToInt32(Console.ReadLine());
            //Console.Write("Sütun sayıısını giriniz");
            //sutun = Convert.ToInt32(Console.ReadLine());
            //int[,] matris = new int[satir, sutun];

            //for (int i = 0; i < satir; i++)
            //{
            //    for (int j = 0; j < sutun; j++)
            //    {
            //        Console.WriteLine("Matrisin {0} x {1} Değeri: ", i + 1, j + 1);
            //        matris[i, j] = Convert.ToInt32(Console.ReadLine());

            //    }
            //}

            //for (int k = 0; k < satir; k++)
            //{
            //    for (int n = 0; n < sutun; n++)
            //    {
            //        Console.Write(matris[k, n] + " ");
            //    }
            //    Console.WriteLine();
            //}

            //Console.WriteLine();
            ////katsayı ile çarpımı

            //for(int s=0; s < satir; s++)
            //{
            //    for(int p = 0; p < sutun; p++)
            //    {
            //        Console.WriteLine(matris[s,p] * 5+ " ");
            //    }
            //}

            ////klavyeden hangi sayı ile çarpılması gerektiğini giren?
            //-------------------------------------------------------------------------------------------------------

            //METOTLAR

            //metotlar kod fazlalıgından kurtarır. (sürekli tekrar eden kodlar)
            
            //erişim belirleyici+static+türü+metot adı
            //(public=dışardan erişimi var, private=dışardan erişimi yok
            
         // her sayfanın sonuda müdür müdür yardımcısı vs. adı olduğu 10 sayfalık bir dokuman


        private static  void veriler()   //veriler metodun adı(biz verdik)
        {
            Console.WriteLine("müdür: baran yücedağ ");
            Console.WriteLine("öğretmen: eylül güneş ");
            Console.WriteLine("okul: atatürk ortaokulu");
            Console.WriteLine("eğitim kolu: kütüphanecilik ");
            Console.WriteLine("sehir: samsun");
            Console.WriteLine(DateTime.Now.ToLongDateString());
            //veriler metodunun içindeki komutları yazdık


        }
        //static void Main(string[] args)
        //{
//            veriler();  //kaç kere yazarsak (veriler();)   yukarıdakileri o kadar kez yazdırır 
//            veriler();
//            Console.Read();  
//;
            



       // }


            

            




       // }



        //private static void yazdir(string bilgi)
        //{
        //    for(int i = 0; i < 10; i++)
        //    {
        //        Console.WriteLine(bilgi);
        //    }
        //}
        //static void Main(string[] args)
        //{
        //    Console.Write("metni girin");
        //    string blg = Console.ReadLine();
        //    yazdir(blg);
        //    Console.Read();
            
             /////////(girdiğimiz metni 10 kez yazdırıyor)
            //////////10 kez yazmaktansa metotla kıslatıyoruz.
      //----------------------------------------------------------------------------

        //toplama işlemini bir metotla yapalım, sadece 2 tane değer girince onları toplasın(değişken tanımlamadan.)


        ////private static  int Topla(int s1,int s2)
        ////{
        ////    int t = s1 + s2;
        ////    return t;
        ////}
        ////static void Main(string[] args)
        ////{

        ////    Console.WriteLine("toplam:  " + Topla(5, 8));
        ////    Console.Read();

        ////}
      
        //klavyeden girilen sayının küpünü aldıran programı metotla yazdırın

        private static int kupu(int sayi)
        {
            int küpdeger = sayi * sayi * sayi;
            return küpdeger;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("sayıyı girin");
            int s=Convert.ToInt32(Console.ReadLine());
            Console.Write("sonuç: " +kupu(s));


        }






        //}
    }
}

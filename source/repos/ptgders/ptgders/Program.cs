using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ptgders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //veriTipi[] değişkenİsmi=
            //        new veriTipi[ElemanSayısı];



            //veriTipi[] değişkenİsmi=
            //           {1,2,3,34,4};


            //int[] myInts1 = new int[10];
            //int[] myInts2 = { 1, 2, 3, 4,
            //
            //5, 6 };

            //myInts2[0] = 10;
            //int değer = myInts2[4];

            //for (int i = 0; i < myInts1.Length; i++)
            //{
            //    myInts1[i] = i + 2;
            //}

            //for (int i = 0; i < myInts2.Length; i++)
            //{
            //    Console.WriteLine(myInts2[i]);
            //}

            //kullanıcıya kaç elemanlı bir sayı listesi oluşturmak istediğini soran,sonrasında 0 ile 100 arasında bu
            //elemanları rastgele değerlerle dolduran en sonunda da dizinin ortalamasını
            //hesaplayan kod bloğunu yazınız.

            //Console.WriteLine("Kaç elemanlı bir liste oluşturmak istersiniz?");
            //int[] myInts = new int[Int32.Parse(Console.ReadLine())];

            //Random rnd = new Random();
            //for (int i = 0; i < myInts.Length; i++)
            //{
            //    myInts[i] = rnd.Next(0, 100);

            //}
            //int toplam = 0;
            //for (int i = 0; i < myInts.Length; i++)
            //{
            //    toplam += myInts[i];
            //    Console.WriteLine("ortalama:" + toplam / myInts.Length);
            //}

            //11 ile 100 arasında vize, 0  ile 85 arası final notları ortalamaya ait                    //EKSİK
            //puan 50 den büyükse true küçükse false


            //int[] vize = new int[250];
            //int[] final = new int[250];
            //int[] ortalama = new int[250];
            //int kalan = 0;
            //int gecen = 0;
            //Random random = new Random();
            //for (int i = 0; i < vize.Length; i++)
            //{
            //    vize[i] = random.Next(11, 100);
            //    final[i] = random.Next(0, 85);
            //}
            //for (int i = 0; i < ortalama.Length; i++)
            //{
            //    


            //}


            //rastgele değerlerden oluşan 1000 elemanlı dizi tanımlayın ,kullanıcıdan sürekli değer alınacak ve her defasında kullanıcıdan bir sayı istenecek,
            //kullanıcının girdiği sayı sayı içinde var mı varsa kaçbtane var? girilen sayı 1453 ise break

            //int[] list = new int[1000];
            //int sayac = 0;
            //Random random = new Random();

            //for (int i = 0; i < list.Length; i++)
            //{
            //    list[i] = random.Next(0, 1000);
            //    Console.WriteLine("sayı giriniz");
            //    int sayı = Int32.Parse(Console.ReadLine());
            //    if (sayı == 1453)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        if (list[i] == sayı)
            //        {
            //            sayac++;
            //        }
            //    }
            //    Console.WriteLine(sayac + "adet sayı liste ile aynı ");


            //}

            //0 ile 100 arasında değerler, eleman sayısı 100, atanan her bir değer benzersiz olacak. dizi içindeki en büyük, ikinci en büyük ve üçüncü en büyük?

            //int[] vize = new int[250];
            //int[] final = new int[250];
            //int[] ortalama = new int[250];
            //int kalan = 0;
            //int gecen = 0;
            //Random rnd = new Random();
            //for (int i = 0; i < vize.Length; i++)
            //{
            //    vize[i] = rnd.Next(11, 100);
            //    final[i] = rnd.Next(0, 85);
            //}
            //for (int i = 0; i < ortalama.Length; i++)
            //{
            //    ortalama[i] = (vize[i] * 4) / 10 + (final[i] * 6) / 10;

            //    if (ortalama[i] >= 50)
            //        gecen++;
            //    else kalan++;

            //}
            //Console.WriteLine(gecen);
            //Console.WriteLine(kalan);
            //Console.ReadLine();

            //string[] isimler = new string[5];
            //for(int i=0; i<isimler.Length; i++)
            //{
            //    Console.WriteLine(i + 1 + ". ismi giriniz: ");
            //    isimler[i] =Console.ReadLine();
            //}
            //Console.WriteLine("diziniz=");
            //for(int i = 0; i < isimler.Length; i++)
            //{
            //    Console.WriteLine(isimler[i]);
            //}
            //Console.ReadLine();


            //rastgele değerlerden oluşan 1000 elemanlı dizi tanımlayın ,kullanıcıdan sürekli değer alınacak ve her defasında kullanıcıdan bir sayı istenecek,
            //kullanıcının girdiği sayı sayı içinde var mı varsa kaçbtane var? girilen sayı 1453 ise break
            //int[] list = new int[1000];
            //int sayac = 0;
            //Random rastgelesayı = new Random();
            //for (int i = 0; i < list.Length; i++)
            //{
            //    list[i]=rastgelesayı.Next(0,1000);
            //}
            //Console.WriteLine("Yeni sayıyı girin");
            //int sayı=int.Parse(Console.ReadLine());
            //while(sayı!=1453)
            //{
            //    for (int i = 0;i < list.Length;i++)
            //    {
            //        if (list[i] == sayı)
            //        {
            //            sayac++;
            //        }
            //    }
            //    Console.WriteLine(sayı + " Sayısı bu listenin içinde " + sayac + " kadar var.");
            //    Console.WriteLine("Yeni sayıyı girin");
            //    sayı = int.Parse(Console.ReadLine());
            //}
            //Console.ReadLine();



            //2. ders notlar


            //birbirinden farklı random 100 elemanlı dizi 0-1000 arası dizideki en büyük 3 ü bul

            //int[] sayilar = new int[100];
            //int index = 0;
            //Random random = new Random();
            //while (index < 100)
            //{
            //    int sayi = random.Next(0, 1001);
            //    bool varMi = false;

            //    for (int i = 0; i < index; i++)
            //    {
            //        if (sayilar[i] == sayi)
            //        {
            //            varMi = true;
            //            break;
            //        }
            //    }
            //    if (!varMi)
            //    {
            //        sayilar[index] = sayi;
            //        index++;
            //    }
            //}
            //for (int i = 0; i < sayilar.Length; i++)
            //{
            //    Console.WriteLine(sayilar[i]);
            //}

            //-------------------------------------------------------------------
            //for (int i = 0; i < sayilar.Length - 1; i++)
            //{
            //    for (int j = 0; j < sayilar.Length - 1; j++)
            //    {
            //        if (sayilar[j] > sayilar[j + 1])
            //        {
            //            int temp = sayilar[j];
            //            sayilar[j] = sayilar[j + 1];
            //            sayilar[j + 1] = temp;
            //        }
            //    }
            //}
            //for (int i = 0; i < sayilar.Length; i++)
            //{
            //    Console.WriteLine(sayilar[i] + ",");
            //}


            //bubble short ile kodlama:
            //2 boyutlu dizi

            //string[] a = new string[5];
            //a[0] = "programlama";
            //a[2] = "tekniklerine";
            //a[3] = "giriş";
            //bool[] b = new bool[5];
            //b[0] = true;
            //int[,] myInt = new bool[5];
            //myInt[0, 1] = 5;
            //myInt[1, 2] = 6;
            //myInt[2, 3] = 7;

           //3 boyutlu dizi
            int[,,] myInt2 = new int[3, 4, 4];
            //5x5 boolen olmak üzere 
            bool[,] matrix = new bool[5, 5];
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = true;
                    }
                }
            }

            //4 katlı ve her katında 4 odası var otel her odanıjn ayrı bir fiyatı  var
            //ama bazıları hayaletli, bu odslsr ve bu odaların altındaki odalar tutulmuyo
            //kalan odaların fiyatları toplamı


            //2025 yılının 1 ile 12 ay  arasında akademik  takvimi yılın şu ayı ayın şu günü günün şu saati
            //en son for döngüsü ile ayın hangi günleri neler var yazdır
            //yıl gün ay ve gün diye 3 boyutlu yapabilirsin.
            //int[,] oda = new int[4, 4];
            //oda[0, 0] = 1; oda[0, 1] = 2; oda[0, 2] = 2;
            //oda[1, 0] = 5; oda[1, 1] = 0;
            //oda[2, 0] = 0; oda[2, 1] = 5;
            //oda[3, 0] = 1; oda[3, 1] = 2;

            //int toplam = 0;
            //for(int i = 0; i < 4; i++)
            //{
            //    for(int j= 0; j < 4; j++)
            //    {
            //        if (oda[j, i] == 0)
            //        {
            //            break;
            //        }
            //        toplam += oda[j, i];
            //    }
            //}
            //Console.WriteLine(toplam);
            
          










        }
    }
}

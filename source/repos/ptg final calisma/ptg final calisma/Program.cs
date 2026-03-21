using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ptg_final_calisma
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ////kullanıcıdan alınan bir sayının basamaklar toplamı:
            //Console.WriteLine("bir sayı gir: ");
            //int sayi=Convert.ToInt32(Console.ReadLine());
            //int toplam = 0;
            //while (sayi > 0)
            //{
            //    int birler = sayi % 10;
            //    toplam += birler;
            //    sayi = sayi % 10;

            //}
            //Console.WriteLine("basamaklar toplamı: "+ toplam);

            //girilen sayının tersini alan 356-->653
            //Console.WriteLine("sayı gir: ");
            //int sayi=Convert.ToInt32(Console.ReadLine());
            //int ters = 0;
            //while (sayi > 0) 
            //{
            //    int birler = sayi % 10;
            //    ters = ters * 10 + birler;
            //    sayi = sayi % 10;
            //}

            //kullanıcıdan sürekli sayı girişi al , 0 girilene kadar o sayıları topla
            //Console.WriteLine("sayı gir , bitirmek için 0 gir: ");
            //int sayi=Convert.ToInt32(Console.ReadLine());
            //int toplam = 0;
            //while (sayi != 0)
            //{
            //    toplam += sayi;
            //    Console.WriteLine("yeni sayı gir , bitmek için 0");
            //    int sayi2=Convert.ToInt32(Console.ReadLine());
            //}
            //Console.WriteLine(toplam);

            //1 ile 100 arası rastgele sayı tahmini do while
            //Random rnd = new Random();
            //int hedef = rnd.Next(1, 100);

            //int tahmin;
            //do
            //{
            //    Console.WriteLine("sayı tahmin et: ");
            //    tahmin = Convert.ToInt32(Console.ReadLine());
            //    if (hedef > tahmin)
            //    {
            //        Console.WriteLine("daha büyük gir");
            //    }
            //    else
            //    {
            //        Console.WriteLine("daha küçük");
            //    }
            //}
            //while (tahmin != hedef);

            //bir şifre belirle , kullanıcıdan sürekli şifre iste , dogruysa başarılı değilse yanlış de, do while
            //string sifre =Console.ReadLine();

            //string tahminsifre;
            //do
            //{
            //    Console.WriteLine("tahmin: ");
            //    tahminsifre = Console.ReadLine();
            //    if (tahminsifre!=sifre)
            //    {
            //        Console.WriteLine("yanlış tekrar");
            //    }
            //}
            //while (tahminsifre != sifre);



            //çarpım tablosu:

            //for(int i = 0; i <= 10; i++)
            //{
            //    for(int j = 0; j <= 10; j++)
            //    {
            //        Console.WriteLine(i + "x"+j+"="+  i*j);
            //    }
            //}

            //for(int i = 0; i <= 10; i++)
            //{
            //    if (i == 5)
            //    {
            //        continue; //Atla ama devam et
            //    }
            //    if (i == 8)
            //    {
            //        break;// 8i de yazdırma ve dur
            //    }
            //    Console.WriteLine(i);
            //}

            //üs alma:
            //Console.WriteLine("taban: ");
            //int taban=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("üs: ");
            //int üs=Convert.ToInt32(Console.ReadLine());
            //int sonuc = 1;
            //for (int i = 1; i <=üs; i++)
            //{
            //    sonuc *= taban;
            //}
            //Console.WriteLine(sonuc);


            //Switch case

            //Console.WriteLine("gün girin: ");
            //int gun=Convert.ToInt32(Console.ReadLine());
            //switch (gun)
            //{
            //    case 1:
            //        Console.WriteLine("pazartesi");
            //        break;
            //        case 2:
            //        Console.WriteLine("salı");
            //        break;
            //    default:
            //        Console.WriteLine("geçersiz");
            //        break;

            // }

            ////hesap makinesi: 
            //Console.WriteLine("1.sayı: ");
            //int sayi1=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("sayı2 : ");
            //int sayi2=Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("işlwm seç: x,-,+,/");
            //char islem=Char.Parse(Console.ReadLine());
            //switch (islem)
            //{
            //    case '+':
            //        Console.WriteLine(sayi1+sayi2);
            //        break;
            //    case '-':
            //        Console.WriteLine(sayi1-sayi2);
            //        break;
            //    default:
            //        Console.WriteLine("geçersiz");
            //        break;


            //}

            //KAPREKAR SAYISI: 
            //for(int i = 1; i < 100; i++)
            // {
            //     int kare = i * i;
            //     string karestr=kare.ToString();
            //     int uzunluk = karestr.Length;
            //     bool kaprekarmi = false;
            //     for(int j=0;j<uzunluk; j++)
            //     {
            //         int sol = 0;
            //         int sag = 0;
            //         //sol kısım hesabı
            //         for( int k=0; k<j; k++)
            //         {
            //             sol = sol * 10 + (karestr[k] - '0'); //Sol tarafı basamak basamak sayıya çevir
            //         }
            //         //Sag kısım hesabı
            //         for(int k=0; k < uzunluk; k++)
            //         {
            //             sag = sag * 10 + (karestr[k] - '0');
            //         }
            //         if(sol +sag==i && sag !=)
            //         {
            //             kaprekarmi = true;
            //             break;
            //         }
            //         //Tek basamaklılar için
            //         if (kare == i)
            //         {
            //             kaprekarmi = true;
            //         }

            //     }
            // }

            //fibonacci( il n e kadar açmak, x.terimi bulmak)

            //Console.WriteLine("kaçıncı terimi bulmak istiyorsunuz?");
            //int n=Convert.ToInt32(Console.ReadLine());
            //int bir = 0;
            //int iki = 1;
            //int sonuc = 0;
            //if (n <= 0)
            //{
            //    Console.WriteLine("daha büyük gir ");
            //}
            //else if (n == 1)
            //{
            //    sonuc = bir;
            //}
            //else if (n == 2)
            //{
            //    sonuc = iki;
            //}
            //else
            //{
            //    for(int i = 3; i <= n; i++)
            //    {
            //        sonuc = bir + iki;
            //        bir = iki;
            //        iki = sonuc;
            //    }
            //}

            static void Main(string[] args)
            {
                /* S1)
                //2 sayının ebobunu bulan metod

                //Kendi:
                //Console.WriteLine("1. sayıyı giriniz");
                //int sayı1 = Int32.Parse(Console.ReadLine());
                //Console.WriteLine("2. sayıyı giriniz");
                //int sayı2 = Int32.Parse(Console.ReadLine());
                //int cikti = Ebob(sayı1, sayı2);
                //Console.WriteLine("Sayıların ebob'u: "+cikti);

                int min = Ebob(12, 4);
                Console.WriteLine(min);
                */

                /* S2) 
                //Bir kelimenin, bir metinde kaç kere geçtiğini bulan metot
                // metin= "kalem kağıt silgi kağıt silgi defter kalem silgi
                // aranan silgi

                string metin = "kalem kağıt silgi kağıt silgi defter kalem silgi";
                string aranan = "silgi";
                int sayac = KacKere(metin, aranan);
                Console.WriteLine(sayac);
                */

                /* S3)
                // Diziyi n adım sağa kaydıran
                int[] dizi = { 2, 3, 4, 5, 6 }; //45623
                NSagaKaydir(dizi, 3);
                for (int i = 0; i < dizi.Length; i++)
                {
                    Console.Write(dizi[i]+", ");
                }
                */

                /* S4)
                // Kullanıcıdan 2 string al, anagram mı kontrol et
                // Anagram: Aynı harfler ama yerleri farklı

                string kelime1 = "anne";
                string kelime2 = "nane";
                bool sonuc = AnagramMı(kelime1, kelime2);
                if(sonuc)
                {
                    Console.WriteLine("Evet, anagram");
                }
                else
                {
                    Console.WriteLine("Hayır, anagram değil");
                }
                */
            }
            /* S1)
            public static int Ebob(int a, int b)
            {
                int min = a <= b ? a : b; //ternary 
                for (int i = min; i > 0; i--)
                {
                    if (a % i == 0 && b % 1 == 0)
                    {
                        min= i;
                        break;
                    }
                }
                return min;
            }
            */
            /* S2) 
            public static int KacKere(string metin, string aranan)
            {
                int sayac = 0;
                for (int i = 0; i < metin.Length - aranan.Length; i++)
                {
                    bool bayrak = true;
                    if (metin[i] == aranan[0])
                    {
                        for (int j = 0; j < aranan.Length; j++)
                        {
                            if (aranan[j] != metin[i + j])
                            {
                                bayrak = false;
                                break;
                            }
                        }
                        if (bayrak)
                        {
                            sayac++;
                            i = i + aranan.Length - 1;
                        }
                    }
                }
                return sayac;
            }
            */
            /* Ek:
            public static void BirSagaKaydir(int[] dizi)
            {
                //2,3,4,5,6
                //6,2,3,4,5

                int sonEleman = dizi.Length - 1;
                for (int i = dizi.Length - 1; i >= 1; i--)
                {
                    dizi[i] = dizi[i - 1];
                    //2,2,3,4,5
                }
                dizi[0] = sonEleman;
                //6,2,3,4,5
            }
            */
            /* S3)
            public static void NSagaKaydir(int[] dizi, int n)
            {
                //eleman sayısı=5
                //n=5
                // İkisi aynı olmamalı

                if (dizi.Length <= 1)
                {
                    return;
                }
                n = n % dizi.Length;
                for (int i = 0; i < n; i++)
                {
                    int sonEleman = dizi[dizi.Length - 1];
                    for (int j = dizi.Length - 1; j >= 1; j--)
                    {
                        dizi[j] = dizi[j - 1];
                        //2,2,3,4,5
                    }
                    dizi[0] = sonEleman;
                }

            }
            */
            /* S4)
            public static bool AnagramMı(string k1, string k2)
            {
                bool bayrak = true;
                if (k1.Length != k2.Length)
                {
                    bayrak = false;
                }
                //abcçdefgğhıijklm...
                int[] karakterDizisi = new int[256];

                for (int i = 0; i < k1.Length; i++)
                {
                    karakterDizisi[k1[i]]++;
                }
                //karakterlerDizisi=[0-----256]
                //a-->asci=33
                //karakterler[33]++;

                for(int i = 0;i < k2.Length; i++)
                {
                    karakterDizisi[k2[i]]--;
                }
                for(int i = 0; i<karakterDizisi.Length; i++)
                {
                    if (karakterDizisi[i]!=0)
                    {
                        bayrak=false;
                        break;
                    }
                }
                return bayrak;
            }
            */
        }
    }


    Console.ReadLine();
        }
    }
}

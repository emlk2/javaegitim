using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ptg_vize2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //klavyeden 10dan küçük bir sayı alan ve faktoriyel hesaplayan kod

            //int faktoriyel = 1;
            //Console.WriteLine("10 dan küçük bir sayı giriniz");
            //int sayı=Int32.Parse(Console.ReadLine());
            //if (sayı < 10)
            //{
            //    for (int i = sayı; i >= 2; i--)
            //        faktoriyel *= i;
            //    Console.WriteLine(faktoriyel);
            //}
            //else
            //    Console.WriteLine("başka sayı giriniz");


            //1 den 9 a kadar çarpım tablosu yazınız.

            //int carp = 1;
            //for (int i = 1; i <= 9; i = i+1)
            //{
            //    for (int j = 1; j <= 9; j++)
            //        carp = i * j;
            //    Console.WriteLine(carp);
            //}




            //Console.WriteLine("bir sayı giriniz");
            //int sayi=Convert.ToInt32(Console.ReadLine());
            //bool asalMi = true;
            //if (sayi<=1)
            //{
            //    asalMi = false;
            //}
            //else if(sayi==2)
            //    asalMi=true;
            //else
            //{
            //    for(int i=2;i<=sayi/2;i++)
            //    {
            //        if (sayi % i == 0)
            //        {
            //            asalMi = false;
            //            break;
            //        }
            //    }
            //}
            //if (asalMi) 
            //{
            //    Console.WriteLine("bu sayı asal");
            //}
            //else
            //    Console.WriteLine("asal değil");

            //fibonacci

            //int a = 1;
            //Console.WriteLine(a);
            //int b = 1;
            //Console.WriteLine(b);
            //int c = 0;
            //int sayac = 0;
            //for (int i = 0; i < 20; i++)
            //{
            //    c = a + b;
            //    Console.WriteLine(c);
            //    a = b;
            //    b = c;
            //}
            //while (sayac<98)
            //{
            //    c = a + b;
            //    Console.WriteLine(c);
            //    a = b;
            //    b = c;
            //    sayac++;
            //}
            bool asalMi = false;
            int sayac = 1;
            while (sayac <=10)
            {
                asalMi = false;
                sayac++;
                if (sayac == 2)
                {
                    asalMi = true;

                }
                else
                {

                    for (int i = 2; i < sayac / 2; i++)
                    {
                        if (sayac % i == 0)
                        {
                            asalMi = false;
                        }
                        else
                        {
                            asalMi = true;
                        }
                    }
                }
              
            }
            if (asalMi == true)
            {
                Console.WriteLine(sayac);
            }
         
            

            //arkadaş sayı(iki sayının kendisi hariç pozitif bölenleri toplamı birbirine eşit ise bunlar arkadaş sayı.)

            //kullanıcıdan alt sınır ve üst sınır alan ve bu aradaki asalları bulan
            //Console.WriteLine("alt sınırı giriniz");
            //int alt=Int32.Parse(Console.ReadLine());
            //Console.WriteLine("üst sınırı giriniz");
            //int üst=Int32.Parse(Console.ReadLine());
            //if (alt > üst)
            //{
            //    Console.WriteLine("program hatalı");
            //}
            //for(int i = alt; i <= üst; i++)
            //{

            //}
                









            Console.ReadLine();





        }
    }
}

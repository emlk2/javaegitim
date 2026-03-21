using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class SayiBulucu
    {
        public int BuyukOlaniBul(int sayi1, int sayi2)
        {
            int sonuc;
            if (sayi1 < sayi2)
                sonuc = sayi2;

            else
                sonuc = sayi1;
            return sonuc;

                
                

        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SayiBulucu sb= new SayiBulucu();
            int a = 100;
            int b = 25;
            int sonuc = sb.BuyukOlaniBul(a, b);
            Console.WriteLine("Büyük olan sayı={0}",sonuc);
            
        }
    }
}

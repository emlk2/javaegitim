using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ptgvize2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //klavyeden 10dan küçük bir sayı alan ve faktöriyel hesaplayan kod

            Console.WriteLine("sayıyı griiniz");
            double sayi = Convert.ToInt32(Console.ReadLine());
            double faktoriyel = 1;
            for(int i = 1; i <= sayi; i++)
            {
                faktoriyel = faktoriyel * i;
            }
            Console.WriteLine("{o} sayısının faktoriyeli={1}",sayi,faktoriyel);
            Console.ReadKey();4




        }
    }
}

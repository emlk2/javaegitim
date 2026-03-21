using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Dikdörtgen
    {
        private int a, b;
        public Dikdörtgen (int a, int b)
        {
            this.a = a; 
            this.b = b;
        }
        public int AlanHesapla()
        {
            return a * b;
        }
        public int CevreHesapla()
        {
            return 2 * (b + a);
        }

    }








    internal class Program
    {
        static void Main(string[] args)
        {

            Dikdörtgen d = new Dikdörtgen(3, 4);
            Console.WriteLine("dikdörtgeninalanı={0}", d.AlanHesapla());
            Console.WriteLine("dikdörtgenincevresi={0}",d.CevreHesapla());
        }
    }
}

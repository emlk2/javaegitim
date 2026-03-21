using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yapısal_çalışma
{
    internal class Program
    {
        static void Main(string[] args)
        {
            araba arabasınıf = new araba();
            arabasınıf.renk = "siyah";
            arabasınıf.fiyat = 150000;//(int oldyğu için tırnaksız)
            arabasınıf.model = "2016";
            arabasınıf.vites = "Otomatik";
            arabasınıf.plaka = "55 EA 2025";
                

            Console.WriteLine("Araba Rengi: " + arabasınıf.renk);
            Console.WriteLine("Araba Fiyatı : "+ arabasınıf.fiyat);
            Console.WriteLine("Araba Model Yılı: "+ arabasınıf.model);
            Console.WriteLine("Vites türü: "+arabasınıf.vites);
            Console.WriteLine("Plaka: "+ arabasınıf.plaka);

            Console.ReadLine();


        }
    }
}

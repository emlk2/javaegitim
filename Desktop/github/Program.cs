using System;

namespace BitwiseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sayıları tanımlayalım (İkilik karşılıklarını yanına yazdım)
            int a = 12; // 1100
            int b = 10; // 1010

            Console.WriteLine($"Sayı A: {a} (Binary: {Convert.ToString(a, 2)})");
            Console.WriteLine($"Sayı B: {b} (Binary: {Convert.ToString(b, 2)})");
            Console.WriteLine("------------------------------------------");

            // 1. AND (&) İşlemi: Her iki bit 1 ise 1 döner
            int andSonuc = a & b; 
            Console.WriteLine($"AND (&) İşlemi: {andSonuc} (Binary: {Convert.ToString(andSonuc, 2).PadLeft(4, '0')})");

            // 2. OR (|) İşlemi: Bitlerden biri 1 ise 1 döner
            int orSonuc = a | b;
            Console.WriteLine($"OR (|) İşlemi:  {orSonuc} (Binary: {Convert.ToString(orSonuc, 2)})");

            // 3. XOR (^) İşlemi: Bitler farklı ise 1 döner
            int xorSonuc = a ^ b;
            Console.WriteLine($"XOR (^) İşlemi: {xorSonuc} (Binary: {Convert.ToString(xorSonuc, 2).PadLeft(4, '0')})");

            // 4. Sola Kaydırma (<<): Sayıyı 2 katına çıkarır
            int solaKaydir = a << 1;
            Console.WriteLine($"Sola Kaydırma (1 bit): {solaKaydir}");

            // Pratik İpucu: Tek/Çift kontrolü
            Console.WriteLine("------------------------------------------");
            int sayi = 15;
            string sonuc = (sayi & 1) == 0 ? "Çift" : "Tek";
            Console.WriteLine($"{sayi} sayısı bitwise ile kontrol edildi: {sonuc}");
        }
    }
}
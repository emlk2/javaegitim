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

using System;

class Program
{
    static void Main()
    {
        // Başlangıç değerimiz 8 olsun (Binary: 1000)
        int sayi = 8; 
        Console.WriteLine($"Baslangic Degeri: {sayi} (Binary: {Convert.ToString(sayi, 2).PadLeft(4, '0')})");

        // 1. SET BIT (Bir biti 1 yapma)
        // 0. biti (en sağdaki) 1 yapalım. 8 (1000) -> 9 (1001) olur.
        int setSonuc = sayi | (1 << 0);
        Console.WriteLine($"SET (0. bit 1 yapildi): {setSonuc} (Binary: {Convert.ToString(setSonuc, 2).PadLeft(4, '0')})");

        // 2. CLEAR BIT (Bir biti 0 yapma)
        // 3. biti (en soldaki 1'i) 0 yapalım. 8 (1000) -> 0 (0000) olur.
        int clearSonuc = sayi & ~(1 << 3);
        Console.WriteLine($"CLEAR (3. bit 0 yapildi): {clearSonuc} (Binary: {Convert.ToString(clearSonuc, 2).PadLeft(4, '0')})");

        // 3. TOGGLE BIT (Bir biti tersine çevirme)
        // 1. biti tersine çevirelim. 8 (1000) -> 10 (1010) olur.
        int toggleSonuc = sayi ^ (1 << 1);
        Console.WriteLine($"TOGGLE (1. bit degistirildi): {toggleSonuc} (Binary: {Convert.ToString(toggleSonuc, 2).PadLeft(4, '0')})");

        // 4. CHECK BIT (Bir bitin durumunu kontrol etme)
        bool isThirdBitSet = (sayi & (1 << 3)) != 0;
        Console.WriteLine($"Soru: 3. bit 1 mi? Cevap: {isThirdBitSet}");
    }
}
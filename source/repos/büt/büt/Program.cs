using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace büt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("satır sayısını giriniz:"); 
            int satir = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("sütun sayısını giriniz:"); 
            int sutun = Convert.ToInt32(Console.ReadLine());
            int[,] matris = new int[satir, sutun]; 
            for (int i = 0; i < satir; i++) 
            { for (int j = 0; j < sutun; j++)
                { Console.Write("matrisin 0x1 değeri: " + i + 1+ j + 1); matris[i, j] = Int32.Parse(Console.ReadLine()); } }
            for (int k = 0; k < satir; k++) 
            { for (int m = 0; m < sutun; m++)
                { Console.Write(matris[k, m]); } Console.WriteLine(); }
            for (int x = 0; x < satir; x++) 
            { for (int y = 0; y < sutun; y++) 
                { Console.Write(matris[y, x]); 
                }
                Console.WriteLine(); }
            Console.ReadLine();
            Console.ReadLine();

        }
    }
}

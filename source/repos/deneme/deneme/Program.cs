using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace deneme
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] kodlar = {
            "// Booting up the system...",
            "int power = 9000;",
            "Console.WriteLine(\"It's over 9000!\");",
            "for (int i = 0; i < 10; i++) Console.WriteLine(i);",
            "// Running diagnostics...",
            "if (true) Console.WriteLine(\"Code never lies.\");",
            "// Loading core modules...",
            "Console.WriteLine(\"Hello, World!\");",
            "Console.WriteLine(\"Stay curious, keep coding.\");",
            "Console.WriteLine(\"while(true) { learn++; }\");",
            "// Compiling dreams into reality...",
            "Console.WriteLine(\"Debugging life one line at a time.\");",
            "Console.WriteLine(\"Trust me, I'm a developer.\");",
            "Console.WriteLine(\"Coffee detected. Productivity +100%\");",
            "Console.WriteLine(\"// End of line? Never.\");"
        };

            int i = 0;
            while (true)
            {
                Console.WriteLine(kodlar[i]);
                i++;
                if (i == kodlar.Length)
                    i = 0; // başa dön
                Thread.Sleep(300); // yavaş akış efekti
            }


        }


    }
}





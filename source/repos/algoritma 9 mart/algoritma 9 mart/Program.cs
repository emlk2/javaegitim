using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algoritma_9_mart
{
    internal class Program
    {
        //static int test(int[] x,int indis)
        //{
        //    if (indis >= x.Length - 1) return 0;
        //    if (x[indis] > x[indis + 1]) return 1;
        //    else return test(x, indis + 1);

        //} 
        static void Main(string[] args)
        {
            //bitwise işlemler
            //32 bitlik bir sayıda sign bit 31 bit 
            //1 111 = -7 değil , (değilini alıp 1 ile  toplayacagız) 
            // (değili ) 0000 + 1= -1 dir. 

            //int[] x = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //// soru1)  x dizisinin sıralı  olup olmadıgını test edelim
            ////baştan sona kadar baktık x[i] , x[i+1] den küçük eşit mi?
            //int test = 0;
            //for (int i = 0; i < x.Length-1; i++) 
            //{
            //    if (x[i] > x[i + 1]) { test = 1; break; }
            //}
            //Console.WriteLine(test);

            //bunun recursive?


            ////soru 2)  
            //int[] x = {1,2,3,0,4,6,7,-1,9,10,1,12,2,14,10};
            //int test = 0;
            //int adet = 0;
            //for(int i = 0;i < x.Length-1;i++)
            //{
            //    int a;
            //    int indis = i;
            //    while (x[indis + 1 >= x[indis]])
            //    {

            //        a++;
            //        indis++;
            //        if (indis == x.Length - 1) break;
            //    }
            //    if (a > adet) adet = a;



            //}
            //Console.WriteLine(test);

            ////döngü değişkenine müdahale etmeyi düşünğyorsan foru bırak while a geç
            //while (int < x.Length - 1)
            //{
            //    int indis = int;
            //    int a = 0;
            //    while (x[indis + 1] >= x[indis])
            //    {
            //        a++;
            //        indis++;
            //        if (indis == x.Length - 1) break;
            //    }
            //}
            //-----------------------------------------------------------

            ////bitwise
            //uint x = 0xfbabfff07;   //x sayısının bitleri içinde  kaç tane 1 var?

            ////maske kullanacagız
            ////x and 1=x
            ////x and 0=0
            ////x or 1=1;
            ////x or 0=x;
            ////xxxxxxxxxxxxxxxxxxxxxxxxx
            ////0000000000000000000000001
            ////000000000000000000000000x
            //int a = 0;

            //int b = 1; 
            //for (int i = 0;i < 32 - 1; i++)
            //{
            //    if (x & b == 1) a++;  //2 ihtimak var , maskeyi bir bit sola , sayıyı bir bit saga kaydırabiliriz.

            //    x = x >> 1;

            //}
            //Console.WriteLine(a);


            ////-------------------------------------------------------------------------------
            //uint x;
            //string s = '';
            //x=0xf0f0f0f0 ; //bu sayının 2lik sayı sistemindeki karşılığı nedir?
            //// x hafızada ve 32 bit

            //for (int i = 0; i < 32; i++)
            //{
            //    if ((x & 1) == 1)
            //    {
            //        s = s + "1";
                   
            //    }
            //    else
            //    {
            //        s = "0" + s;
            //        x = x >> 1;
            //    }
            //}
            //Console.WriteLine(s);


            //else s="0"+s
            //b=b<<1;

            //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
            //0000000000000000000000000000000010
            //0000000000000000000000000000000010
            //-----------------------------------------


            ////bunun recursive çözümü:
            ///
            //static string hextoBin(uint i,int indis)
            //{
            //    if ((i & 1) == 1)
            //    {
            //        return "0" + hextoBin(i >> 1, indis + 1);
            //    }
            //    else
            //    {
            //        return "1" + hextoBin(i >> 1, indis + 1);
            //    }
            //    string b = '';
            //    for (int i = 0; i < 32; i++)
            //    {
            //        if ((x & 1) == 1)
            //        {
            //            s = s + "1";

            //        }
            //        else
            //        {
            //            s = "0" + s;
            //            b = b << 1;
            //        }
            //    }
            //}
            //static void Main(string[] args)
            //{ }
            //soru 4:
           
          
            //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx x x x
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy y y y
            //x = 0xfbfbfbfb;
            //uint y = 0xfbfbfbfbfb;
            ////x ve y aynı sayılar mıdır?

            //int flag = 0;
            //for (int i = 0; i < 32;i++)
            //{
            //    if ((x & 1) == (y & 1))
            //    {
            //        flag = 1; break;
            //        x >>= 1;
            //        y = y >> 1;
            //    }
            //}
            //if(flag==0) Console.WriteLine("eşit");
            //else Console.WriteLine("eşit değil");

            //recursive hali?
            //static string esitmi(uint i, uint b,int indis)
            //{
            //    if (indis >= 32) return 0;
            //    if ((i & 1) == (b & 1))
            //        return hextoBin(i >> 1, indis + 1);
            //    else
            //        return 1;
                        
            //}
            //static void Main(string[] args)
            //{

            //}




            }
    }
}

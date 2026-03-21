using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veri_final_ders_not
{
    internal class Program
    {
        static int  hashfunction(string st)
        {
            //ali --> 65+75+85=225 , dizinin modunu al .
            int t = 0;
            for (int i = 0; i < st.Length; i++)
            {
                t = t + (byte)st[ i];
                //2) t = t + (i + 1) * (byte)st[i];   //buradaki donk. bize kalmış
            }
            t = t % 100;
            return t;
        }
        static void hashyaz(string[] hash,string st)
        {
            int index =hashfunction(st);
            hash[index] = st;
        }
        static int hashsearch(string[]hash, string st)
        {
            int index = hashfunction(st);
            if (hash[index] == st)
                return 1;
            else return 0;
        }
        static void Main(string[] args)
        {
            //15 aralık

            //int[] hash = new int[100];
            //string[] st = new string[100];

            //for(int i=0;i<st.Length; i++)
            //{
            //    if (st[i]=="ALİ" ) Console.WriteLine("bulundu");
            //}

            ////ali , ahmet, ayşe( dizide sakla)
            ////ali=3, ahmet=10 , ayşe =20

            ///////HASH FUNCTİON   : basit olmalı , mod olmalı
            ////string--->int gitmek gerekiyor (ali ahmet ayse indisleri bulabilmek için.)
            ////aliyi sakla
            //st[3] = "ali"; //sakladım
            ////ali varr mı ? ara:
            //if (st[3]=="ali") Console.WriteLine("ali var");
            //----------------------------------------------------

            //string[] st = new string[100];
            //st[hashfunction("ali")] = "ali";
            //if (st[hashfunction("ali")] != "ali") Console.WriteLine("yoktur");
            //if (st[hashfunction("ali")]=="ali") Console.WriteLine("vardır");

            //Console.WriteLine((hashsearch(st,"ali"));
            //hashyaz(st, "ali");
            //Console.WriteLine(hashsearch(st,"iman");
            //hashyaz(st, "iman");

            //hashyaz(st, "ila");
            //Console.WriteLine(hashsearch(st,"ali"));


             //!çakışmayı en aza indiren matematiksel fonksiyon
            
            



        }
    }
}

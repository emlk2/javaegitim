using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace veri_vize_devam
{
    public class Block
    {
        public int data;
        public Block next;
        public Block prev;
    }

    internal class Program
    {
        static void st1()
        {
            Console.WriteLine("1");
            st2();
            //adres2
        }
        static void st2()
        {
            st3();
            //adres3
            Console.WriteLine("2");
        }
        static void st3()
        {
            Console.WriteLine("3");
            st4();
            //adres4
        }
        static void st4()
        {
            Console.WriteLine("ben 4.yüm");
        }
        //st1  st2'yi çağırır ve bekler.
        //st2  st3'ü çağırır ve bekler.
        //st3  st4'ü çağırır ve bekler.
        //st4  İşini yapar (en alta ulaşır), biter ve geri döner.
        //st3  Beklemesi biter, işine devam eder ve geri döner.
        //st2 Beklemesi biter, işine devam eder ("2" yazar) ve geri döner.
        //st1  Beklemesi biter, işine devam eder ve geri döner.

        //1)baglı listenin tüm elemanlarını ekrana yazdırmak.
        static void Yaz(Block bl)
        {
            //bir while döngüsü kullanmak yerine recursive ile listeyi geziyoruz.
            //amaç ekrana verileri yazdırmak.
            if (bl == null) return;
            Console.WriteLine(bl.data);
            Yaz(bl.next);
        }
        //') eleman sayısını bulmak:
        static int ders2(Block bl)
        {
            if (bl == null) return 0;
            return 1 + ders2(bl.next);   //Fonksiyon, kendisinin bir sonraki düğüm (bl.next) için olan sonucunu hesaplamasını ister. Bu, "benden sonraki elemanların sayısını bul" demektir.

        }
        //search algoritması
        //bir bağlı listede (bl) belirli bir data değerinin kaç kez tekrarlandığını (kaç adet olduğunu) sayar.
        static int ders3(Block bl, int data)
        {
            if (bl == null) return 0;
            if (data == bl.data) return 1 + ders3(bl.next, data);
            else return ders3(bl.next, data); //aranan data o anki düğümün değeriyle eşleşmiyorsa burası çalışır

        }
        //ortada olan bir bloktan eleman bulma
        //ya da da şey diyebiliriz bl listenin herhangi bir elemanına bakmaktadır. data değeri 7 olan
        //kaç tane eleman vardır recursive çözünüz
        static int ders4(Block bl, int yon)
        {
            if (yon == 0)
            {
                if (bl.prev == null) yon = 1;
                else bl = bl.prev;
            }
            else
            {
                bl = bl.next;
                if (bl == null) return 0;
                return yon + ders4(bl, yon);
            }
        }
       



        // Sizin kodunuz (Sınıfın içine taşındı)
        static int[] stack = new int[100];
        static int sp = -1; // sp = Stack Pointer

        static void push(int data)
        {
            // Güvenlik Kontrolü (Overflow)
            if (sp >= 99)
            {
                Console.WriteLine("Hata: Yığın Dolu (Stack Overflow)");
                return;
            }
            sp++;
            stack[sp] = data;
        }

        static int pop()
        {
            // Güvenlik Kontrolü (Underflow)
            if (sp < 0)
            {
                Console.WriteLine("Hata: Yığın Boş (Stack Underflow)");
                return -1; // Hata durumunda -1 döndür (veya hata fırlat)
            }

            // return stack[sp--]; // Kısa yol

            int data = stack[sp];
            sp--;
            return data;
            //return stack [sp--]; böyle de yapabiliriz.

            // bunun recursivesiz halini yap:
             static void recursivecozum(string path)
             {
                Console.WriteLine(path);
                string[] dirs=Directory.GetDirectories(path);
                foreach(String item in dirs)
                {
                    recursivecozum(item);
                }
             }
            //ALGORİTMA SORUSU
            //açılan parantez kapatılmalı ve aynı türden olması lazım
            // bir string verilsin string içerisindeki parantezlerin dogru olmadıgını karar versin.

            static void Main(string[] args)
            {
                string st = "ggg(hh(jjj)kkdfkl)flgrfg";
                string prntz = "({[)}]";
                string left = ")}]";
                //indexof değerleri 0,1,2,3,4,5
                for (int i = 0; i < st.Length; i++)
                {
                    int index = prntz.IndexOf(st[i]);
                    if (index == -1)
                    {
                        continue;
                    }
                    if (index <= 2)
                    {
                        //stacka push yapıcaz
                        //push(byte) st[i] ;sol parantezin ascıı değerini gönderiyoruz
                        push(left[index]);
                        continue;

                    }
                    push(left[index]);
                    continue;
                }
                char ch = (char)pop();
                int ind = prntz.IndexOf(ch);
                if (index - 3 != ind)
                {
                    Console.WriteLine("hata var");
                    break;


                }
                if (Stackcount() > 0)
                {

                }


                }
            }

        }

        


    }
        
    }
}

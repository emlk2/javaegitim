using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace veri_final_video_notları
{
    //internal class Program
    //{

    // static void Main(string[] args)
    //{

    ///4 ARALIK VİDEO
    //Stack < char > st= new Stack<char>();
    //st.Push('$');
    //char chr = ' ';

    //string infix = "a+b*(a-b*c+d)-f/g"; //abc*fd
    //Console.WriteLine("ababc*-d+*+fg");
    //string postfix = "";
    //string ch = "$(+-*/)";
    //string pr = "001122";
    //for(int i = 0; i < infix.Length; i++)
    //{
    //    int index = ch.IndexOf(infix[i]);
    //    if (index == -1) { postfix = postfix + infix[i];continue; }
    //    if (index == 1) { st.Push('(');continue; }
    //    {
    //        if (index == 6)
    //        {
    //            while (st.Peek() != '(') postfix = postfix + st.Pop();
    //            st.Pop();
    //            continue;
    //        }
    //        while (pr[ch.IndexOf(st.Peek())] >= pr[index])
    //        {
    //            postfix = postfix + st.Pop();
    //        }
    //        st.Push(infix[i]);
    //    }
    //stakta linked list kullanacağız.

    //    while (st.Peek() != '$')
    //        postfix = postfix + st.Pop();
    //    Console.WriteLine(postfix);

    //    //--------------------------------------------
    //    string postfix = "abcd/-*e+";
    //    //a,b,c,d,e
    //    //d,h,j,n,v
    //    string operands = "abcde";
    //    string[] vars = { "en", "boy", "yükseklik" };
    //    int[] values = { 1, 2, 3, 3, 4, };
    //    Stack<int> st = new Stack<int>();

    //    string var1 = "";
    //    for (int i = 0; i < postfix.Length; i++)
    //{ 
    //    int index = operands.IndexOf(postfix[i]);
    //        if (index != -1)
    //        {
    //            var1 = var1 + st.Pop();
    //            continue;
    //        }
    //        int v2 = st.Pop();
    //        int v1=st.Pop   ();

    //        int sonuc = 0;
    //        if (postfix[i] == '+') sonuc = topla1(v1, v2);
    //        if (postfix[i]=='-') sonuc=fark1(v1, v2);
    //        if (postfix[i] == '*') sonuc = carp1(v1, v2);
    //        if (postfix[i] == '/') sonuc = divide1(v1, v2);
    //        st.Push(sonuc);

    //    }
    //Console.WriteLine(st.Pop());
    //------------------------------------
    //STAKLARDA LİNKED LİST








    //}

    //class Block
    //{

    //}
    //static Block SP = null;
    //static void PUSH(int data)
    //{
    //    Block bl = new Block();
    //    bl.data = data;
    //    bl.next = SP;
    //    //bl.prev=null;

    //    if (SP != null)
    //    {
    //        SP.prev = bl;
    //        SP = bl;
    //    }


    //}
    //static int POP()
    //{
    //    int data = SP.data;
    //    SP = SP.next;
    //    return data;
    //}

    //**KUYRUKLAR**
    //QUEUE
    //hizmet almak iççin beklemek=kuyruk
    //2 işlem var kuyruğa ekleme ve kuyruktan çıkarma
    //2 şekilde diziler ve linked listlerle , push ve pop gibi kuyruğa eleman ekleyip çıkartacağız.

    //static int front = 0; //hixmet alınan nokta
    //static rear=-1;   //kuyruğa dahil olmaya gelenler
    //static int queue = new int[100];
    //    // rear+front-1

    //   for(int i = front;i<=rear;i++ ){
    //        queue[int - front]=queue[i]

    //        }
    //rear=rear-front
    //front=0;   //bu işlemler veri transferini sağladı
    //***bu algoritma kullanılıyor ama daha iyisini yazacağız.
    //sürekli veri hareketi yapmak zorunda oldugu için verimli değil.
    //rear frontun önünde ve eşit olduğu sürece dizide eleman var.

    //******** mod aldık
    //static void enqueue(int data)
    //{
    //    //rear++;
    //    queue[rear % 100] = data;
    //}
    //static int dequeue()
    //{
    //    int data = queue[front % 100];
    //    front++;
    //    return data;
    //}

    //!!!soru: bunu  çiftli linked list ile nasıl çözeriz? dq ve enq yu nasıl programlarız?
    //rearla listenin sonuna eleman ekleyeceğiz. frontla listeden eleman alacağız.

    //class Block
    //{

    //}
    //static front=0;
    //static int rear = -1;
    //static int[] queue = new int[100];
    //static Block front1 = null;
    //static Block rear1 = null;

    //static void enqueueBlock(int data)
    //{
    //    Block bl=new Block();
    //    bl.data = data;
    //    bl.prev = rear1;
    //    if (rear1 != null) rear1.next = bl;
    //    if (front1 = null) front1 = bl;
    //        rear1 = bl;
    //    // not: front1=frontbl
    //}


    //6 ARALIK İKİNCİ VİDEO

    //class Block
    //{
    //    public int data;
    //    public Block next;
    //    public Block tmp;
    //}
    //class Program
    //{
    //    // Kuyruk için gerekli değişkenler
    //    static int[] queue = new int[10]; // 10 elemanlık bir dizi
    //    static int front = 0;             // Kuyruğun başı
    //    static int rear = -1;            // Kuyruğun sonu

    //    // Eleman ekleme metodu
    //    static void ENQUEUEBlock(int data)
    //    {
    //        if (rear == queue.Length - 1)
    //        {
    //            Console.WriteLine("Hata: Kuyruk dolu!");
    //            return;
    //        }
    //        rear++;
    //        queue[rear] = data;
    //    }

    //    // Eleman çıkarma metodu
    //    static int DEQUEUEBlock()
    //    {
    //        if (front > rear)
    //        {
    //            Console.WriteLine("Hata: Kuyruk boş!");
    //            return -1;
    //        }
    //        int data = queue[front];
    //        front++;
    //        return data;
    //    }
    //    static int QueueCount()
    //    {
    //        return rear - front + 1;
    //    }
    //    static int QueueCountBlock()
    //    {
    //        Block bl=new Block();
    //        Block frontbl=new Block();

    //        Block tmp = frontbl;
    //        int adt = 0;
    //        while (tmp != null)
    //        {
    //            tmp = tmp.next;adt++;

    //        }
    //        return adt;

    //    }
    //    static int QueueBlockHesapla(Block front)
    //    {
    //        if (front == null) return 0;
    //        return 1 + QueueBlockHesapla(front.next);
    //    }
    //    public static void Main(string[] args)
    //    {
    //        ENQUEUEBlock(100);
    //        ENQUEUEBlock(200);
    //        ENQUEUEBlock(300);

    //        Console.WriteLine(DEQUEUEBlock());
    //        Console.WriteLine(DEQUEUEBlock());
    //        Console.WriteLine(DEQUEUEBlock());
    // }

    //------------------------------------------------------------------------------
    //bunu recursive nasıl çözersin?
    //reardan itibaren eleman sayısını nasıl buluruz?
    //rear 0 olsaydı?

    //Recursive cıkacak tekrar bak.

    //HASHİNG   (ARAMA /SEARCHİNG)

    //1) Sequential search =sıralı arama , en maliyetli arama , sort edilmemiş verilerde olur.

    //sequential search:
    //internal class pp
    //{
    //    static void Main(string[] args)
    //    {
    //        string[] st = new string[100];
    //        for (int i = 0; i < 100; i++)
    //        {
    //            if (st[i] == "ilke") Console.WriteLine("bulundu");
    //        }




    //        //(maliyet=0(n))
    //        //2) Binary search =  0(log(n)) ->buradaki log 10 tabanlı değil 2 tabanında.

    //        //3) B+tree  treenin en üst yapısı. çok hızlı, bir kişiye ulaşmanın maliyeti 1,
    //        //bunlar hakkında bilgi edin testlerde sorar.
    //        //0(1) 
    //        //hashing searchi hızlandıran  bir mekanizmadır , cok hızlı yapacagız.

    //        int[] hash = new int[100];
    //        hash[7] = 123;
    //        hash[99] = 567;
    //-----------------------------------------------------------------------------

    //11 ARALIK (defterde slayt)
    //TREELER

    //19 ARALIK
    class Block
    {
        public int data;
        public string hashdata;
        public Block next;
        public Block prev;
    }
    internal class ppp
    {
        //static void Main(string[] args)
        //{
        //    int[] hash = new int[100];
        //    string[] st = new string[100]; //hash ve st pointer , 4 byte

        //    //ali . ahmet , ayse bunları dizide sakla ve agırlıgını bul

        //    //forlu olan sequential aramadır.
        //    //for (int i = 0; i < st.Length; i++)
        //    //{
        //    //    if (st[i]=="ALİ") Console.WriteLine("bulundu");
        //    //}

        //    //ali=3, ahmet=10, ayse=20 olsun
        //    //önce aliyi sakla=

        //    st[3] = "ali";
        //    // ali var mı?
        //    if (st[3] == "ali")
        //    {
        //        Console.WriteLine("ali var");
        //    }
        //    else
        //    {
        //        Console.WriteLine("ali yok");
        //    }

        //    // aynısı ahmet için
        //    st[10] = "ahmet";
        //    if (st[10] == "ahmet")
        //    {
        //        Console.WriteLine("ahmet var");
        //    }
        //    else
        //    {
        //        Console.WriteLine("ahmet yok");
        //    }
        //}

              //*** HASH FUNCTİON***
            //1) BASİT OLMALI
            //2)MOD OLMALI
            //3)STRİNG---->İNT gitmem gerekiyor
        
       // static int hashfunction (string st)
       // {
       //     //ali
       //     //65+ 75+85  (a , l ve i nin değerleri) =225  mod alınacak , dizinin eleman sayısı kadar

       //     int t = 0;
       //     for (int i = 0; i < st.Length; i++)
       //     {
       //         t = t + (byte)st[i];
       //     }
       //     t = t % 100;
       //     return t;
       // }
       // static void hashyaz( string []  hash, string st)
       // {
       //     int index=hashfunction (st);

       //     if (hash[index]=="") { hash[index] = st; return; }
            
       //     for(int i=index+1; i<st.Length; i++)
       //     {
       //         if (hash[(index +1)%100] == "") { hash[(index+1)%100] = st;break ; }
       //     }
       //     hash[index] = st;



       //     for (int i = index + 1; i < st.Length; i++)
       //     {
       //         if (hash[(index + 1) % 100] == "") { hash[(index + 1) % 100] = st; break; }
       //     }
       // }

       // static void hashyazBlock(Block[] hash, string st)
       // {
       //     int index = hashfunction (st);
       //     Block tmp = new Block();
       //     tmp.hashdata = st;

       //     if (hash[index] == null)
       //     {
       //         hash[index] = tmp;
       //         return;
       //     }
       // }

       // static int hashSearch(string[] hash, string st)
       // {
       //     int index=hashfunction (st);
       //     if (hash[index] == st) return 1;
       //     int c = 0;
           
       //     for (int i = index + 1; i < st.Length; i++)
       //     {
       //         if (hash[(index + 1) % 100] == st) { c = 1; break; }
       //     }
       //     return c;
       // }

       //public static void Main(string[] args)
       //{
       //     string[] st = new string[100];
       //     st[hashfunction("ali")] = "ali";

       //     if (st[hashfunction("ali")] != "ali")
       //     {
       //         Console.WriteLine("var");
       //     }

       //     if (st[hashfunction("ali")] == "ali")
       //     {
       //         Console.WriteLine("var");
       //     }
       //     Console.WriteLine(hashSearch(st,"ali"));
       //     hashyaz(st,"ali");
       //     Console.WriteLine(hashSearch(st, "ali"));
       //     hashyaz(st,"emel");

       //     hashyaz(st, "ali");
       //     hashyaz(st, "ila");
       //     hashyaz(st, "ial");
       //     Console.WriteLine(hashSearch (st,"ila"));  //çakışma kontrolü!!!!!
       //     Console.WriteLine(hashSearch(st, "ali"));
       //     Console.WriteLine(hashSearch(st, "ial"));

       //     //**linked list kullanılırsa ne olur???!!:

       //     Block[] hashblock = new Block[100]; //100 tane linked list var
       //-------------------------------------------

        //btree
        //internal class pppp
        //{
        //    static int btders2(int[] bt, int indis)     // dizi 1 milyon itemli olsaydı bu metot kendini ne kadar çağıracaktı, btders3
        //                                                // ne kadar çağıracaktı?
        //                                                //= btders2 =1 milyon , btders3 =logn(dengeli ise logn)
        //                                                //bu sebeple binary treeleri searchte kullanmamız dogru değil.
        //    {
        //        if (indis >= bt.Length) return 0;

        //        return 1+ btders2(bt, indis * 2 + 1) + btders2(bt, indis * 2 + 2);
        //    }
        //    //bu konudan soru var.
        //    //indis değeri 5 olanın sol çocugu kaçtır? indis değeri 7 olanın parenti nedir?, indisi verilen sayının parentlerini yaza yaza git.

        //    static int btders3(int[] bt, int indis)
        //    {
        //        if(indis >= bt.Length) return 0;

        //        if (bt[indis] == search) return 1;
        //        if (bt[indis] > search) 
        //            return btders3(bt, 2 * indis + 1, search);
        //        else return btders3(bt, 2 * indis + 2, search);
                

        //    }
        //    static int btders4(int[] bt, int indis)
        //    {
        //        if (indis >= bt.Length) return 0;

        //        if (bt[indis] == search) return 1;

        //        return btders4(bt, 2 * indis + 1, search) + btders4(bt, 2 * indis + 2, search);

        //    }

        //        static void btders1(int[] bt, int indis)
        //    {

        //        //!! kendisini 2 defa çağıran recursive yapı**
        //        if(indis>=bt.Length) return;

        //        if (bt[indis] != 0)  
        //        Console.WriteLine(bt[indis]);

        //        btders1(bt, indis * 2 + 1);
        //        btders1(bt, indis * 2 + 2);


        //        //!  1)  böyle oldugunda sonuc değişir mi , değişirse nasıl değişir?

                //btders1(bt, indis * 2 + 1);
                //   if (bt[indis] != 0)      Console.WriteLine(bt[indis]);
                //btders1(bt, indis * 2 + 2);


              //  2) böyle olursa ne olur?
                //btders1(bt, indis * 2 + 1);
                //btders1(bt, indis * 2 + 2);

                //if (bt[indis] != 0)
        //        //    Console.WriteLine(bt[indis]);

        //    }
        //    public static void Main(string[] args)
        //    {
        //        int[] btree = new int[100];
        //        btree[0] = 11;
        //        btree[1] = 7;
        //        btree[2] = 15;

        //        //                              11
        //        //                 7                        15
        //        //           5         9                13       20
        //        //                                                     17
        //        //        77                                                  45
        //        //    88                                                           71


        //        // !binary treede sıralı olmak zorunda değil ağaç , binary search tree derse öyle olacak.
        //        btree[3] = 5;
        //        btree[4] = 9;

        //        btree[7] = 77;
        //        btree[15] = 88;

        //        btree[5] = 13;
        //        btree[6] = 20;

        //        btree[14] = 17;
        //        btree[30] = 45;
        //        btree[62] = 71;

        //        int[] bt = new int[100];  ///önemli , sınav sorusu olabilir
        //                                  //binary treede dolu ve boş hücreleri nasıl belirlersin?
        //        int[] btba=new int[100];

        //        bt[0] = 25;   btba[0] = 1;
        //        bt[1] = 55;   btba[1] = 1;
        //    }
        //}




                //postfix hesaplama
                class p
        {
            static void Main(String[] args)
            {
                Stack<int> st = new Stack<int>();
                string operands = "abcde";
                int[] values = { 10, 20, 30, 40, 50 };
                string postfix = "abcde/-*+";

                for (int i = 0; i <= postfix.Length; i++) 
                {
                    int index=operands.IndexOf(postfix[i]);

                    if (index == -1)
                    {
                        st.Push(values[index]);
                        continue;

                    }
                    int sonuc;
                    int v2 = (int)st.Pop();
                    int v1 = (int)st.Pop();
                    if (postfix[i] == '+') { sonuc = v1 + v2; }
                    else if (postfix[i] == '-') { sonuc = v1 - v2; }
                    else if(postfix[i] == '*') { sonuc = v1 * v2; }
                    else if (postfix[i] == '/') { sonuc = v1 / v2; }
                    st.Push(sonuc);


                }
                Console.WriteLine(st.Pop());

            }
        }

        



        
    }





























    //    }

    //}














    
}
    

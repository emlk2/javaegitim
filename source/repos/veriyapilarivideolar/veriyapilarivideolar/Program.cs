using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace veriyapilarivideolar
{
    internal class Program
    {
        //6EKİM
        //static int ders1(int[]x,int indis)
        //{
        //    if (indis >= x.Length)
        //    { return 0; }
        //    return x[indis] + ders1(x, indis + 1);
        //}
        //static int ders2(int[,] x ,int indis1,int indis2)   //Fonksiyonun temel amacı, x[indis1, indis2] elemanından başlayarak,
        //                                                    //x[i, i] şeklindeki ana köşegen elemanlarının toplamını hesaplamaktır (Başlangıç değerleri (0, 0) ise).
        //{
        //    if (indis2 >= 100)
        //    {
        //        indis1++;
        //        indis2 = 0;
        //    }
        //    if (indis1 >= 100) { return 0; }

        //    return x[indis1,indis2] + ders2(x, indis1 + 1,indis2+1);

        //}

        //static int ders3(int[,]x,int indis)  //****bu yazım stili daha önemli***
        //{
        //    if (indis >= 100) return 0;
        //    int i1 = indis / 100;
        //    int i2 = indis % 100;
        //    return x[i1, i2] + ders3(x, indis + 1);
        //}

        //static void Main(string[] args)
        //{
        //    int[] x = new int[100];
        //    Console.WriteLine(ders1(x,0));
        //    int t = 0;
        //    for (int i = 0; i < 100; i++)
        //    {
        //        t = t + x[i];
        //    }
        //    int[,] y = new int[100, 100];

        //    //Soru: bu string içerisinde en çok kullanılan harf?
        //    //string st = "abcdefabbbbbccbbefbbbbbb";

        //    //int sayi = 0;
        //    //char ch = ' ';

        //    //for (int i = 0;i <st.Length; i++)
        //    //{
        //    //    int adt = 0;
        //    //    for (int j = 0; j < st.Length,; j++)
        //    //    {
        //    //        if (st[1] == st[j])
        //    //        {
        //    //            adt++;
        //    //        }
        //    //        if (adt > sayi)  sayi = adt;ch = st[i];
        //    //    }
        //    //}
        //    //2 ya da daha fazla forla çözünce daha hızlı nasıl oolabilir diye düşün!
        //    //daha hızlı çözüm:
        //    string st = "aabbbbbccbbbbbbbb";
        //    int sayi = 0;
        //    char ch = ' ';
        //    int[] z = new int[250];

        //    for (int i = 0; i <st. Length; i++)
        //    {
        //        z[(byte)st[i]]++;
        //        for (int i = 0; i < z.Length; i++)
        //        {
        //            if (z[i] > sayi)
        //            {
        //                sayi = z[i];
        //            }
        //        }

        //    }

        //**2.DERS**

        //çok boyutlu diziler
        // new int[b1,b2,b1,b4]
        //b1 adet satır var , her satırda b2*b3*b4 adet int eleman var.

        //x[0,1,2,3]=1500;
        //x[i1,i2,i3,i4]

        //ba+i1*b2*b3*b4*b5*4 + i2*b3*b4*b5*4 + i3*b4*b5*4 + i4*b5*4 + i5*4 


        //int[,,,,] dizi = new int[15, 10, 2, 4, 2]; //15 fakülte ,10 bölüm,nö iö, sınıf, cinsiyet

        //bu diziyi recursive kodla:
        //bu dizide en çok hangi grup var?

        //*****forlu kodlanışı:*****
        //int toplam = 0;
        //for (int i = 0; i < 15; i++)
        //{
        //    for (int j = 0; j < 10; j++)
        //    {
        //        for (int k = 0; k < 2; k++)
        //        {
        //            for (int l = 0; l < 4; l++)
        //            {
        //                for (int z = 0; z < 2; z++)
        //                {
        //                    toplam = dizi[i, j, k, l, z];
        //                }
        //            }
        //        }
        //    }


        //dizi[0, 0, 0, 0, 0] = 400;
        //int[,,,,,] dizi2 = new int[100, 15, 10, 2, 4, 2];
        //int[,,,,] iller = new int[81, 20, 50, 20, 200];


        //
        //***recursive hali:***

        //static int BesBoyutluDiziToplaNoConst(int[,,,,] x, int indis)
        //{
        //    // Dizinin toplam eleman sayısı: 15 * 10 * 2 * 4 * 2 = 2400

        //    // 1. Temel Durum (Base Case): 2400 değeri doğrudan kullanıldı.
        //    if (indis >= 2400)
        //    {
        //        return 0; // Toplamaya etki etmeden geri dön
        //    }

        //    // 2. Tek İndisi 5 Boyutlu İndislere Çevirme
        //    // İndisler: [i, j, k, l, z] -> [Fakülte, Bölüm, NÖ/İÖ, Sınıf, Cinsiyet]

        //    // z (Cinsiyet: 2 eleman)
        //    int z = indis % 2;
        //    int kalan1 = indis / 2;

        //    // l (Sınıf: 4 eleman)
        //    int l = kalan1 % 4;
        //    int kalan2 = kalan1 / 4;

        //    // k (NÖ/İÖ: 2 eleman)
        //    int k = kalan2 % 2;
        //    int kalan3 = kalan2 / 2;

        //    // j (Bölüm: 10 eleman)
        //    int j = kalan3 % 10;
        //    int kalan4 = kalan3 / 10;

        //    // i (Fakülte: 15 eleman)
        //    int i = kalan4 % 15;

        //    // 3. Özyinelemeli Çağrı
        //    return x[i, j, k, l, z] + BesBoyutluDiziToplaNoConst(x, indis + 1);

        //    //!!!indis + 1 dememizin sebebi, özyinelemenin (recursive) yapısı gereği, dizinin bir sonraki elemanına geçmek ve tüm elemanları sırayla gezmek istememizdir.
        //    //Bu, iç içe for döngülerinin i++, j++, k++, vb.demesiyle aynı amaca hizmet eder.
        //}

        //**ÜÇÜNCÜ DERS**
        //LİNKED LİT(BAĞLI LİSTE)    
        //class Blocklist //linked listteki her bir elemanı(düğümü) temsil eden sınıf.
        //{
        //    public int data;     //bu düğümün taşıdığı değeri saklar.
        //    public Blocklist next; // kendi türünden (blocklist) başka bir nesneyi işaret eden bir referanstır. Bu sıradaki düğümün nerede olduğunu gösterir.
        //                           //bu pointerdır.
        //}
        //main metodunda düğüm oluşturma ve atama
        //static void Main(string[] args)
        //{
        //    Blocklist b1 = new Blocklist();   // Bellekte yeni bir Blocklist nesnesi(düğümü) oluşturulur ve b1 değişkeni bu ilk düğümü işaret eder.Bu, listenin başı olacaktır.
        //                                      // b1.data = 1;: İlk düğümün taşıdığı veri değeri 1 olarak ayarlanır.
        //    b1.data = 1;
        //    Blocklist b2 = new Blocklist();
        //    b2.data = 2;

        //    b1.next = b2;//bagladım

        //    b2.next = null;  //b2den sonra başka bir düğüm yok!
        //    Console.WriteLine(b1.data);


        //}

        ////***2.ÖRNEK***
        //class Blocklist
        //{
        //    public int data;
        //    public Blocklist next;

        //}


        //static void ders4(Blocklist b1)
        //{
        //    if (b1 == null)
        //    {
        //        return;
        //    }
        //    Console.WriteLine(b1.data);         
        //    ders4(b1.next);
        //    Console.WriteLine(b1.data);
        //}
        //static void Main(string[] args)

        //{
        //    Blocklist b1 = new Blocklist();
        //    b1.data = 1;
        //    Blocklist b2 = new Blocklist();
        //    b2.data = 2;
        //    b1.next = b2;
        //    b2.next = null;

        //    b2.next = new Blocklist();
        //    b2.next.data = 3;
        //    Console.WriteLine(b1.data); //1 yazar
        //    Console.WriteLine(b1.next.data); //2 yazar
        //    b2.next.data = 3;
        //    Console.WriteLine(b1.next.next.data); //3 yazar

        //    b1.next.data = 5;
        //    //Console.WriteLine(b2.data); //5 yazar

        //    //b1.next.next.next = new Blocklist();
        //    //b2.next.next.data = 6;

        //    // **Linked listin sonuna eleman eklemek istersek:
        //    //Blocklist temp = b1;
        //    //while (temp != null)  //temp(temporary pointer) ==> GEÇİÇİ İŞARETÇİ,GEZGİN
        //    //{
        //    //    temp = temp.next;  //linked listte geziniyor.
        //    //}
        //    //!!!Bu döngüden çıkınca, temp elimizdeki son düğümü işaret ettiği için ekleme işlemini güvenle yaparız=
        //    //temp.next = b3; //gibi.
        //    ders4(b1);  //!!!TERSTEN YAZABİLSİN DİYE CAGIRDIK.
        //}

        //       ders4(b1) çağrılır.
        //	1. Satır: Console.WriteLine(1) çalışır. 1
        //ders4(b2) çağrılır. (1, yığına atılır.)

        // Console.WriteLine(2) çalışır. 1 \newline 2
        //ders4(null) çağrılır. (2, yığına atılır.)
        // if (b1 == null) return; çalışır ve geri döner.
        //. Console.WriteLine(2) çalışır. (2'nin çağrısı yığından çıkar.)	1 \newline 2 \newline 2
        // Console.WriteLine(1) çalışır. (1'in çağrısı yığından çıkar.)


        //  ÖRNEK

        //static void Main(string[] args)
        //{
        //    int isaret = 1;
        //    int sayac = 0;
        //    int periyot = 2;
        //    for(int i = 1; i <= 20; i++)
        //    {
        //        Console.WriteLine((isaret*i)+"");
        //        sayac++;
        //        if (sayac == periyot)
        //        {
        //            isaret *= -1;
        //            sayac = 0;
        //        }
        //    }
        //}
        //çıktısı= 1 2 -3 -4 5 6 -7 -8 9 10 -11 -12 13 14 -15 -16 17 18 -19 -20
        //---------------------------------------------------------------------------------------------------------------------

        //--25 EKİM  VİDEOLARI--
        //class Block
        //{
        //    public int data;
        //    public Block head;
        //    public Block next;
        //    public Block prev;

        //}
        //class Blocklist               //1.adım düğüm(node) sınıfı , trendeki tek bir vagon

        //{
        //    public int data;
        //    public Blocklist head;
        //    public Blocklist next;
        //    public Blocklist prev;
        //}
        //class yazlist
        //{

        //}

        //static void Main(string[] args)
        //{


        // Block head=new Block();
        //Block tmp=new Block();
        // tmp.data = 10;
        // tmp.next = head;
        // head = tmp;

        // //**listenin ilk elemanını delete ediniz.
        // head = head.next;


        //tmp = head; satırı, bağlı listelerde çok kritik bir görev üstlenir: Listeyi gezmek için bir başlangıç noktası oluşturur, ancak listenin asıl başlangıç noktasını (yani head'i) değiştirmez.
        //tmp=temporary , geçici
        //   head (Baş) Nedir?
        //head, tüm bağlı listeye erişimi sağlayan ve listenin ilk düğümünün adresini tutan ana referanstır(bir nevi listenin tapusudur).
        //head'i değiştirirseniz, listenin başlangıcını kaybeder, dolayısıyla tüm listeye
        //erişiminizi kaybedersiniz.

        //!!!! tmp değişkeni listenin sonuna(null) ulaşana kadar
        //değer değiştirir ve ilerler.

        //**listenin son elemanına data değeri -1 olan bir block ekle.
        //tmp = head;
        //Block bl=new Block();
        //bl.data = -1;
        // bl.next=//son eleman ;

        //while (tmp != null)
        //{
        //    if (tmp.next == null)
        //    {
        //        tmp.next = bl;  //tmpnin next, null ise yeni bir
        //                        //eleman ekledik=bl(-1)
        //        break;
        //    }
        //    tmp = tmp.next;

        //}

        //while (tmp.next != null)
        //{
        //    tmp = tmp.next;
        //    tmp.next = bl;
        //}
        //listenin son elemanını delete edelim

        //tmp = head;

        //while (tmp.next.next != null)
        //{
        //    tmp = tmp.next;

        //}
        //tmp.next = null;

        //ödev: baştan itibaren 4.bloktan sonra data değeri 12 olan block ekleyiniz
        //sondan 4.bloğu delete ediniz.
        //bu ödevleri recursive düşününüz

        //----------------------------------------------------
        //tmp = head;
        //int adt = 0;
        //while (adt<3)
        //{
        //    tmp = tmp.next;

        //}
        //Block bl=new Block();
        //bl.data = 12;
        //bl.next=tmp.next;
        //tmp.next = bl;

        //data değeri 7den önce olursa
        //data değeri 7 olan bloğu delete ediniz

        //Blocklist tmp= new Blocklist();
        //Blocklist head= new Blocklist();
        //Blocklist bl=new Blocklist();
        //Blocklist b1= new Blocklist();  //düğümlerin(vagonların) oluşturulması            Blocklist b2= new Blocklist();
        //Blocklist b3= new Blocklist();  //b1, b2,b3 adında 3 tane vagon(düğüm) oluşturduk ve içlerine 1,2,3, yüklerini(veri) koyduk.Şu anda vagonlar birbirinden bağımsız.

        //bl.prev = null;                

        //bl.next = new Blocklist();
        //bl.next.data = 2;
        //bl.data = 1;

        //bl.next.prev = bl;
        //bl.next.next = null;

        //b1.data = 1;
        //b2.data = 2;
        //b3.data = 3;

        //b1.next = b2;           //vagonları birbirine bağlıyoruz. (çengellerle bağlamak gibi)
        //b2.next = b3;

        //b3.prev = b2;          //çift yönlü liste olduğu için vagonların geriye doğru olan bağlantılarını da kuruyoruz.
        //b2.prev = b1;

        //Blocklist tmp = b3;   //gezgin listenin son  elemanından başlıyor.

        //while (tmp != null)    //  Gezgin (tmp) boşluğa (null) düşene kadar (yani vagonlar bitene kadar) devam et" diyen bir döngü başlatıyoruz.
        //                       //buradaki tmp asıl işi yapan=GEZGİN,, LİSTEYİ SONDAN BAŞA DOĞRU GEZİYOR.
        //{

        //    Console.WriteLine(tmp.data); //Gezginin o an üzerinde durduğu vagonun (tmp) içindeki yükü (data) ekrana yazdırıyoruz.
        //    tmp = tmp.prev;              //Gezgine diyoruz ki: "İşin bittiyse, prev (önceki) çengelini takip ederek bir önceki vagona zıpla."
        //}
        //-----------------------------------------------------------------------------
        //-----25 ekim videoları boyunca yapılanlar:
        //ana amaç= linked list  mantıgını anlamak, 
        //tekli bağlı liste = listnein  başına eleman ekleme,sonuna eleman ekleme, listeden elaman silme
        //çift b.l = düğümlerin next ve prev ile çift yönlü bağlanması
        //listede gezinme= listeyi baştan sona gezme(while(tmp.next!=null), sondan başa gezme=(while(tmp.prev!=null)



        //tmp = head;
        //while (tmp != null)
        //{
        //    if (tmp.next.data == 7)
        //    {
        // Blocklist bl = new Blocklist();
        //bl.data = 22;
        //bl.next = tmp.next;
        //tmp.next = bl; 

        //        tmp.next = tmp.next.next;

        //    }
        //    tmp = tmp.next;
        //}

        //Tekli bağlı liste, düğümlerin tek bir yönde ilerlemesini sağlayan en basit bağlı liste türüdür.
        //çiftli bağlı liste, düğümlerin hem ileri hem de geri yönde bağlantı kurmasını sağlayan, daha esnek bir yapıdır.
        //Dairesel Bağlı Liste (Circular Linked List), tekli veya çiftli bağlı listenin özel bir varyasyonudur.
        //Bu yapının en ayırt edici özelliği, zincirin hiçbir zaman sona ermemesidir.
        //Listenin son düğümü, next referansıyla listenin İLK düğümünü (head) işaret eder.



        //****circular linked list (dairesel bağlı liste)***

        //head tekli circular  listenin herhangi bir elemanına b almaktadır.
        //a- listenin eleman sayısını bulunuz-recursive-normal-
        //bu listenin içerisindeki 7 olanları delte ediniz,


        //Blocklist bl = null;
        //Blocklist last = null;
        //for (int i = 0; i < 10; i++)
        //{
        //    tmp =new Blocklist();
        //    tmp.data = i;

        //    if (last == null)
        //    {
        //        last = tmp; bl = tmp;
        //        continue;

        //    }
        //    last.next = tmp;
        //    tmp.prev = last;
        //    last = tmp;
        //}


        //}

        //****3 KASIM***
        //STACKLER

        class Block
        {
            public int first;
            public int data;
            public Block tmp;
            public Block next;
            public Block prev;
        }
        //static void yaz(Block bl)
        //{
        //    if (bl == null) return;
        //    Console.WriteLine(bl.data);
        //    yaz(bl.next);

        //}
        //public static void Main()
        //{
        //    Block first = null;
        //    Block tmp = null;
        //    for(int i = 0; i < 10; i++)
        //    {
        //        Block bl=new Block();
        //        bl.data = i;
        //        if (i == 0)
        //        {
        //            first = bl;
        //            tmp = bl;
        //        }
        //        else 
        //        {
        //            bl.prev = tmp;
        //            tmp.next = bl;
        //            tmp = bl;
        //        }
        //    }
        //     yaz(first);  //eleman sayısını isteseydi ne olacaktı?
        //}
        //----------------------------------------------------

        //datası 7 olan  var mı?
        //static int ders2(Block bl)
        //{
        //    if (bl == null) return 0;
        //    return 1 + ders2(bl.next);

        //}
        //static int ders3(Block bl, int data)
        //{
        //    if (bl == null) return 0;
        //    if (data == bl.data)
        //    {
        //        return 1 + ders3(bl.next.next.data);
        //    }
        //    else return ders3(bl.next,data);
        //}
        //-------------------------------------------------------------------------------------------------
    //    static int ders4(Block bl, int yon)
    //    {
    //        if (yon == 0)
    //        {
    //            if (bl.prev == null) yon = 1; else bl = bl.prev;
    //        }
    //        else
    //        {
    //            bl = bl.next;

    //        }
    //        if (bl == null) return 0;
    //        return yon + ders4(bl, yon);
    //    }
    //    //stack örneği:
    //    public class mystack
    //    { 
    //    public Stack<char> items = new Stack<char>() 
        
    //    static void push(char item)
    //    {
    //        items.Push(item);
               
    //    }
    //    public char  pop()
    //    {
    //            if (items.Count > 0)
    //            {
    //                return items.Pop();
    //            }
    //            return ' ';
    //    }
    //    public int stackcount()
    //    {
    //            return items.Count;

    //    }
    //}
    //    public static void Main()
    //    {
            
    //        //  () {} [] ksdjfhdsjaklsdkfjdkslaskdfj
    //        //{[()]} 

    //        string st = "{([])}";
    //        string prntz = "{([])}";
    //        mystack yigin = new mystack();
            

    //        for(int i = 0; i < st.Length; i++)
    //        {
    //            int index = prntz.IndexOf(st[i]);
    //            if (index == -1) continue;
    //            if (prntz.IndexOf(st[i]) == -1) continue;
    //            if (index <= 2) { yigin.push(st[i]); continue; }
    //        }
        
            

    //        if (yigin.stackcount() > 0)
    //        {
    //            Console.WriteLine("hata var,kapanmayan parantez kaldı");
    //        }
    //        else if (yigin.stackcount() == 0)
    //        {
    //            Console.WriteLine("hatasız");
    //        }
              

    //    }
        //Temel amacınız, st = "{([])}" dizesindeki açılış parantezlerinin ( {, (, [ ) karşılık gelen kapanış parantezleriyle ( }, ), ] ) doğru sırada ve sayıda eşleşip eşleşmediğini kontrol etmektir
        //Yığına Atma (Push): Açılış parantezlerini bir yığına atmayı (Stack) amaçladınız.
        //Yığından Çıkarma(Pop) ve Kontrol: Kapanış parantezi geldiğinde yığından bir eleman çıkarıp, çıkan elemanın eşleşen açılış parantezi olup olmadığını kontrol etmeyi amaçladınız.
       // Son Kontrol: İşlem bittiğinde yığın boşsa, eşleşmenin doğru olduğunu bildirmeyi amaçladını


// Sizin sınıf tanımınız
public class mystack
    {
        // C#'ın hazır Stack'ini kullanıyoruz
        public Stack<char> items = new Stack<char>();

        // Artık 'yigin' nesnesi üzerinden çağrılacağı için STATIC değil.
        public void push(char item)
        {
            items.Push(item); // C#'ın kendi Push metodunu kullanır.
        }

        // Yığının en üstündeki elemanı çıkarır ve döndürür.
        public char pop()
        {
            if (items.Count > 0)
            {
                return items.Pop();
            }
            // Hata durumunda boş karakter döndür (veya bir istisna fırlat)
            return ' ';
        }

        // Yığındaki eleman sayısını döndürür.
        public int stackcount()
        {
            return items.Count; // Stack'in gerçek eleman sayısını döndürmeli.
        }
    }
    // -------------------------------------------------------------------
    // NOT: Bu metotların Program sınıfı içinde olmasına gerek kalmadı, kaldırılabilirler.
    /* static void push() {}
    static void pop() {}
    static int stackcount() {}
    */

    public class Programgemini
    {
        public static void Main()
        {
            
            
                // ...
                string st = "{([])}";
                string prntz = "{([])}";
                mystack yigin = new mystack(); // Yığın nesnesi oluşturuldu.

                // ----------------------------------------------------
                // DÜZELTİLMİŞ FOR DÖNGÜSÜ
                // ----------------------------------------------------
                for (int i = 0; i < st.Length; i++)
                {
                    int index = prntz.IndexOf(st[i]);

                    // st[i] karakteri, prntz dizesinde yoksa (genellikle olmayacak bir kontrol)
                    if (index == -1) continue;

                    // Eğer açılış parantezi ise (index 0, 2, 4 -> '}', '(', ']' olmalıydı,
                    // ama sizin prntz diziniz hatalı olduğu için {([])} üzerinden yola çıkıyoruz)
                    // Eğer index <= 2 ise, yani ilk üç karakterden biri ise...
                    if (index <= 2)
                    {
                        // 1. Hata Düzeltildi: Push metodu yigin nesnesi üzerinden char ile çağrıldı.
                        yigin.push(st[i]);
                        continue;
                    }

                    // Kapanış parantezi geldiğinde (index > 2 ise)
                    // Yığının boş olup olmadığını kontrol et
                    if (yigin.stackcount() == 0)
                    {
                        Console.WriteLine("Hata: Kapanış Parantezi Fazlası!");
                        // Programı durdurmak için break; veya return kullanılmalıdır.
                    }

                    // 2. Pop işlemi ve Kontrol
                    // ----------------------------------------------------
                    // Bu kısım döngü içinde olmalıydı!

                    // int ch = (char)yigin.pop(); // Yığından karakteri çıkar
                    // int cikanIndex = prntz.IndexOf(ch);

                    // if (index - 3 != cikanIndex) // Sizin hatalı eşleşme mantığınız
                    // {
                    //     Console.WriteLine("Hata var");
                    //     // break; // Döngü içindeysek durmalıyız.
                    // }
                }
                // ----------------------------------------------------
                // DÖNGÜ SONRASI HATA KONTROLLERİ
                // ----------------------------------------------------

                // int ind; // Tanımlı değil/kullanılmıyor
                // int index; // Tanımlı değil/kullanılmıyor

                // int ch = (char)pop(); // Hata: pop() statik değil, yigin.pop() olmalı

                // Yığın boş olmalıydı.
                if (yigin.stackcount() > 0)
                {
                    Console.WriteLine("Hata var: Kapanmayan parantez kaldı.");
                }
                else if (yigin.stackcount() == 0)
                {
                    Console.WriteLine("Hatasız");
                }
                
                //RECURSİVE YAPILARDA BİR BİTİŞ KURALI OLMAK ZORUNDA.
            
        }
    }











}
}

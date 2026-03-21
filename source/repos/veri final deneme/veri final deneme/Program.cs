using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace veri_final_deneme
{
    //   


    /// ikili arama için verimsiz arama kodu:

    //class Dugum
    //        {
    //            public int Deger;
    //            public Dugum Sol, Sag;
    //            public Dugum(int veri) { Deger = veri; }
    //        }

    //        class AgacIslemleri
    //        {
    //            // btders4: Körlemesine eleman arama fonksiyonu
    //            public static bool ElemanVarMi(Dugum kok, int aranan)
    //            {
    //                // 1. Durum: Ağaç boşsa veya sona geldiysek bulamadık
    //                if (kok == null) return false;

    //                // 2. Durum: Aranan sayı bu düğümde mi?
    //                if (kok.Deger == aranan) return true;

    //                // 3. Durum: BST kuralını kullanmıyoruz! 
    //                // Hem sol tarafa bak, hem sağ tarafa bak (Körlemesine arama)
    //                bool soldaBulundu = ElemanVarMi(kok.Sol, aranan);
    //                bool sagdaBulundu = ElemanVarMi(kok.Sag, aranan);

    //                // İki taraftan birinde bile bulsa yeter
    //                return soldaBulundu || sagdaBulundu;
    //            }

    //            // btders2: Tüm düğümleri sayma fonksiyonu
    //            public static int DugumSayisi(Dugum kok)
    //            {
    //                if (kok == null) return 0;

    //                // Kendim(1) + solumdakiler + sağımdakiler
    //                return 1 + DugumSayisi(kok.Sol) + DugumSayisi(kok.Sag);
    //            }
    //        }

    ///-----------------------------------------------------------------------------------------------------
    ///ikili arama için verimli arama kodu :
    ///using System;

    //class Dugum
    //{
    //    public int Deger;
    //    public Dugum Sol, Sag;
    //    public Dugum(int veri) { Deger = veri; }
    //}

    //class AgacIslemleri
    //{
    //    // btders3: Akıllı (BST) Arama Fonksiyonu
    //    public static bool VerimliAra(Dugum kok, int aranan)
    //    {
    //        // 1. Durum: Sona geldik ve bulamadık
    //        if (kok == null) return false;

    //        // 2. Durum: Tam üstüne bastık!
    //        if (kok.Deger == aranan) return true;

    //        // 3. ADIM: Karar Verme (Arama uzayını yarıya indirme)
    //        if (aranan < kok.Deger)
    //        {
    //            // Aranan küçükse, sağ tarafı tamamen çöpe at, sadece SOLA bak
    //            return VerimliAra(kok.Sol, aranan);
    //        }
    //        else
    //        {
    //            // Aranan büyükse, sol tarafı tamamen çöpe at, sadece SAĞA bak
    //            return VerimliAra(kok.Sag, aranan);
    //        }
    //    }
    //}
    //-----------------------------------------------------------------------------------------------------------------------------------------


    //BİNARY TREE 

    //post-order sol -sag -kok
    //static void Btreegez(int[]agacdizisi,int indis)
    //{
    //    if (indis >= agacdizisi.Length) return;   //kod sonsuza kadar olmayan düğümleri aramasın , dursun
    //    Btreegez(agacdizisi, indis * 2 + 1); //sol alt agac
    //    Btreegez(agacdizisi, indis * 2 + 2);// sag alt agac
    //    Console.WriteLine(agacdizisi[indis]);// mevcut düğüm
    //}
    //----------------------------
    //AĞAÇTAKİ AKTİF DÜĞÜM SAYISI

    //static int aktifdügüm(int[] dizi, int indis)
    //{
    //    if (indis >= dizi.Length) return 0;
    //    if (dizi[indis] == 0)
    //        return 0 + aktifdügüm(dizi, indis * 2 + 1) + aktifdügüm(dizi, indis * 2 + 2);
    //--->  kutudaki sayı 0 ise geçersiz kabul ediyor ama belki cocukları doludur diye bakmaya devam.

    //    else
    //        return 1 + aktifdügüm(dizi, indis * 2 + 1) + aktifdügüm(dizi, indis * 2 + 2);

    // -->aktif düğüm , sonuca bir ekle ve cocuklarına da bak.

    //}
    //= boş olmayan(aktif) düğümlerin sayısını bulmaya yarar,

    //----------------------------------------------------------

    //BELİRLİ BİR DEĞERİN AĞACIN İÇİNDE OLUP OLMADIGINI ARAMA

    //static internal VarMi(int[] dizi, int indis, int deger)
    //{
    //    if (indis >= dizi.Length) return 0;
    //    if (dizi[indis] == deger) return 1;

    //    int sol = VarMi(dizi, indis * 2 + 1);
    //    int sag = VarMi(dizi, indis * 2 + 2);
    //    return sol + sag;

    //-->kaç tane oldugunu saymıyor agacta olup olmadıgına bakıyor.
    //}

    ///-----------------------------
    ///HER SEVİYEDE MAKSİMUM DEĞERİ BULMA

    //static void Maxdegerbulma(int[] dizi, int indis, int[] svy, int seviye)
    //{
    //    if (indis >= dizi.Length) return; // dizi sınırını aştıysa dur
    //    if (dizi[indis] == 0) return;   // boş düğümse dur



    //    // eğer mevcut düğümün değeri , suana kadar bulunan maxtan büyükse güncelle:

    //    if (svy[seviye] < dizi[indis])
    //    {
    //        svy[seviye] = dizi[indis];
    //    }

    //    //Sol ve sag agacları kontrol et ve aynı işlemi uygula

    //    Maxdegerbulma(dizi, indis * 2 + 1, svy, seviye + 1);
    //    Maxdegerbulma(dizi, indis * 2 + 1, svy, seviye + 2);
    // }

    //    dizi:  Sayıların olduğu ana dizi(ağaç).
    //indis: Şu an baktığımız kutunun numarası.
    //svy(Seviye): Her katın en büyük değerini sakladığımız ayrı bir dizi. (Örneğin svy[0] 0. katın rekorunu, svy[1] 1. katın rekorunu tutar).
    //seviye: Şu an ağacın kaçıncı katında(derinliğinde) olduğumuzu belirten sayı.

    //--------------------------------------------------

    //AĞAÇTAKİ YAPRAK DÜĞÜM SAYISINI BULMA:
    //static int yaprakdugum(int[] bt, int indis)
    //{
    //    if (indis >= bt.Length) return 0;
    //    if (bt[indis] == 0) return 0;

    //    //Eğer bir düğümün sol ve sag  cocukları dizide yoksa= yaprak
    //    if (indis * 2 + 1 >= bt.Length && indis * 2 + 2 >= bt.Length) return 1;

    //    int sol = yaprakdugum(bt, indis * 2 + 1);// sol agacın yaprak düğm sayısını bul
    //      int  sag = yaprakdugum(bt, indis * 2 + 2);//sag bul
    //    return sol + sag;

    //}
    //AGAÇTAKİ TOPLAM DUGUM DEĞERLERİNİN TOPLAMI:
    //static internal dugumtoplam(int[] bt, int sira)
    //{
    //    if (sira >= bt.Length) return 0;
    //    if (bt[sira] == 0) return 0;

    //    int sol = dugumtoplam(bt, sira * 2 + 1);
    //    int sag = dugumtoplam(bt, sira * 2 + 2);
    //    return bt[sira] + sol + sag;

    //Yol bittiğinde (if koşulları), fonksiyonlar değer döndürmeye başlar. Buna "recursive geri dönüş" diyoruz:En dipteki çocuk (Yaprak), "Benden sonra kimse yok (0), benim değerim 5, o halde toplam 5'tir" diyerek bir üstteki babasına 5 sonucunu fırlatır.
    //Baba düğüm iki elini açar:Sol eline sol daldan gelen sayı gelir (Örn: 5).Sağ eline sağ daldan gelen sayı gelir (Örn: 20).Baba düğüm son işlemi yapar:
    //Kendi Değerim + Sol + Sağ ($10 + 5 + 20$) ve bulduğu 35 sonucunu kendi babasına fırlatır.
    //}

    //BELİRLİ BİR DEĞERİN AGACIN KACINCI SEVİYESİNDE OLDUGUNU BULMA
    //static int seviyebul(int[] bt, int indis, int deger, int seviye)
    //{
    //    if (indis >= bt.Length) return -1;
    //    if (bt[indis] == 0) return -1;
    //    if (bt[indis] == deger) return seviye;
    //    int sol = seviyebul(bt, indis * 2 + 1, deger, seviye + 1);
    //    int sag = seviyebul(bt, indis * 2 + 2, deger, seviye + 1);
    //    return sol!=-1 ? sol:
    //    
    //-------------------------
    //    if (sol != -1) {
    // Sol taraftaki dallardan birinde sayı bulundu (örneğin 2. kat dendi)
    ///  return sol; }
    //else{
    //    // Sol tarafta bulunamadı (-1 geldi), o zaman sağ tarafın sonucunu gönder
    //    // (Sağ taraf bulduysa kat numarasını, bulamadıysa o da -1 gönderecek)
    //return sag;
    //}-------------------------------------------------------------
    //}

    //1. Ne Yapmaya Çalışıyoruz? (Toplama mı? Kat Numarası mı?)Eğer yaptığın işlem bir miktar veya toplam belirtiyorsa (Sayma, Toplama, Yaprak Sayısı vb.), "yokluk" anlamına gelen 0 döndürülür.
    //Çünkü matematikte $5 + 0 = 5$ eder, sıfır sonucu etkilemez.Ancak bu son kodda bir "Yer/İndis/Kat" arıyoruz.Ağaçta 0. seviye (kat) zaten vardır (Ana kök).Eğer sayı bulunamadığında 0 döndürürsen, bilgisayar sayıyı 0. katta (en tepede) bulduğunu sanır.
    //-------------------
    //İKİ DÜĞÜM ARASINDAKİ MESAFEYİ BULMA
    //static int mesafe(int[] bt, int indis, int deger1, int deger2,)
    //{
    //    if (indis >= bt.Length) return -1;
    //    if (bt[indis]==deger1 ||  bt[indis]==deger2) return 1;

    //    int sol = mesafe(bt, indis * 2 + 1, deger1, deger2);
    //    int sag = mesafe(bt, indis * 2 + 2, deger1, deger2);
    //    if (sol > 0 && sag > 0) { return sol + sag - 1; } //hem sag hem sol
    //    if (sol > 0) //sol alt agacta mesafe bulundu üzerine 1 ekle yukarı bildir
    //    {
    //        return sol + 1;
    //    }
    //    else if (sag > 0)// solda yoksa sagda varsa
    //    {
    //        return sag + 1;

    //    }
    //    else // ne sagda ne solda yok
    //    {
    //        return 0;
    //    }
    //    //return sol>0 ?sol+1: sag>0= sag +1:0


    //}

    //AGACIN AYNA GÖRÜNÜMÜNÜ CIKARMA:
    //static void Ayna(int[] bt, int indis)
    //{
    //    if (indis >= bt.Length) return;
    //    if (bt[indis] == 0) return;

    //    int sol = indis * 2 + 1;
    //    int sag = indis * 2 + 2;
    //    int temp = 0;

    //    if (sol< bt.Length)
    //    {
    //        temp = bt[sol];

    //    }
    //    if(sol<bt.Length)
    //    {
    //        if (sag < bt.Length)
    //        {
    //            bt[sol] = bt[sag];
    //        }
    //        else
    //        {
    //            bt[sol] = 0;
    //        }
    //    }

    //    if (sag < bt.Length)
    //    {
    //        bt[sag] = temp;
    //    }
    //    Ayna(bt, sol);
    //    Ayna(bt, sag);
    //}

    //AGACTAKİ EN KÜÇÜK DEĞERİ BULMA:
    //static int Mindeger(int[] bt, int indis)
    //{
    //    if (indis >= bt.Length) return 0;
    //    if (bt[indis] == 0) return 0;

    //    int sol = Mindeger(bt, indis * 2 + 1);
    //    int sag = Mindeger(bt, indis * 2 + 2);
    //    int min = bt[indis];
    //    if (sol < min) min = sol;
    //    if (sag < min) min = sag;
    //    return min;
    //}
    //static int Maxdeger(int[] bt, int indis)
    //{

    //    if (indis >= bt.Length) return 0;
    //    if (bt[indis] == 0) return 0;
    //    int sol = Maxdeger(bt, indis * 2 + 1);
    //    int sag = Maxdeger(bt, indis * 2 + 2);
    //    int max = bt[indis];
    //    if (sol > max) max = sol;
    //    if (sag > max) max = sag;
    //    return max;

    //}
    //AGACTAKİ DERİNLİGİ BULMA

    //static int derinlikbul(int[] bt, int indis, int hedef)
    //{
    //    if (indis >= bt.Length || bt[indis] == 0) return -1;
    //    if (indis == hedef) return 0;

    //    int solderinlik = derinlikbul(bt, indis * 2 + 1, hedef);
    //    int sagderinlik = derinlikbul(bt, indis * 2 + 2 hedef);

    //    if (solderinlik != -1) //-1 cıkmaz sokak
    //    {
    //        return solderinlik + 1; //Sol alt ağaç daha derinse
    //    }
    //    if(sagderinlik != -1)
    //    {
    //        return sagderinlik;
    //    }
    //}

    //AGACTAKİ YÜKSEKLİGİ BULMA:
    //static int yükseklikbul(int[] bt, int indis)
    //{ 
    // if (indis >= bt.Length || bt[indis] == 0) return 0;
    // int solyükseklik = yükseklikbul(bt, indis * 2 + 1);
    // int sagyükseklik = yükseklikbul(bt, indis * 2 + 2);
    //    if (solyükseklik < sagyükseklik)
    //    {
    //        return solyükseklik + 1;
    //    }
    //    else
    //    {
    //        return sagyükseklik + 1;
    //    }


    //}
    //AGACTAKİ YAPRAK DÜĞÜMLERİ BULMA: ***
    //static int yaprakdugum(int[] bt, int indis)
    //{
    //    if (indis >= bt.Length || bt[indis] == 0) return 0;
    //    if ((indis * 2 + 1 > bt.Length || bt[indis] == 0) && (indis * 2 + 2 > bt.Length || bt[indis] == 0)) return 1;
    //    int sol = yaprakdugum(bt, indis * 2 + 1);
    //    int sag = yaprakdugum(bt, indis * 2 + 2);
    //    return sol + sag;
    //}

    //AGACTAKİ İÇ DÜĞÜMLERİ BULMA
    //static int icdugum(int[] bt, int indis)
    //{
    //    if (indis >= bt.Length || bt[indis] == 0) return 0;

    ////bool solDurum = (indis * 2 + 1 >= bt.Length && bt[indis] == 0);
    //bool sagDurum = (indis * 2 + 2 >= bt.Length && bt[indis] == 0);
    //if (solDurum || sagDurum)
    //{
    //    // Eğer bu düğümün değeri 0 ise ve çocukları artık dizi sınırının dışındaysa
    //    // Bu düğüm ne bir yapraktır ne de aktif bir düğümdür.
    //    return 0;
    //}

    // int sol = icDugum(bt, indis * 2 + 1);
    //    int sag = icDugum(bt, indis * 2 + 2);

    // }

    //AGACTAKİ DÜĞÜMLERİ BULMA
    //static int dugumler(int[] bt, int indis)
    //{
    //    if (indis >= bt.Length || bt[indis] == 0) return 0;
    //    int sol = dugumler(bt, indis * 2 + 1);
    //    int sag = dugumler(bt, indis * 2 + 2);
    //    return sol + sag;

    //}

    //AGACTAKİ DÜĞÜMLERİ YAZDIRMA(PREORDER , INORDER, POSTORDER)

    //static void preorder(int[]bt, int indis)
    //{
    //    if (indis >= bt.Length || bt[indis] == 0) return ;
    //    Console.WriteLine(bt[indis]); //kök
    //    preorder(bt, indis * 2 + 1);
    //    preorder(bt, indis * 2 + 2);


    //}
    //sttaic void ınorder(int[]bt, int indis)
    //{
    //    if (indis >= bt.Length || bt[indis] == 0) return;
    //    ınorder(bt, indis * 2 + 1);
    //    Console.WriteLine(bt[indis]);
    //    ınorder(bt, indis * 2 + 2);



    //}

    //static void postorder(int[]bt, int indis)
    //{
    //    if (indis >= bt.Length || bt[indis] == 0) return;

    //    postorder(bt, indis * 2 + 1);
    //    postorder(bt, indis * 2 + 2);
    //    Console.WriteLine(bt[indis]);
    //}



    //İşin sonunda elinde bir sayı kalsın istiyorsan: int kullan.
    // Sadece bir işlem(ekrana yazdırma, diziyi aynalama, sıralama vb.) yapmak istiyorsan: void kullan

    //AGACTAKİ DÜĞÜMLERİ SİLME/TEMİZLEME
    //static void silme(int[]bt, int indis)
    //{
    //    bt[indis] = 0;
    //    silme(bt, indis * 2 + 1);
    //    silme(bt, indis * 2 + 2);
    //}

    //AGACTAKİ DÜĞÜMLERİ KOPYALAMA
    //static int kopyalama(int[] bt1, int indis1, int[]bt2, int indis2)
    //{
    //    if (indis1 >= bt1.Length || bt1[indis1] == 0 || indis2 >= bt2.Length)
    //    {
    //        return 0;
    //    }
    //    //kaynaktaki düğüm hedefe kopyalanır
    //    bt2[indis2] = bt1[indis1];

    //    Kopyalama(bt1, indis1 * 2 + 1, bt2, indis2 * 2 + 1);

    //    //    // Sağ alt ağaç kopyalanır
    //    //    Kopyalama(bt1, indis1 * 2 + 2, bt2, indis2 * 2 + 2);
    //    //}

    //}

    //AGACTAKİ DÜĞÜM DEĞERLERİNİ DEĞİŞTİREREK KOPYALAMA:
    //static void Kopyalama1(int[] bt1, int indis1, int[] bt2, int indis2)
    //{
    //    // Eğer kaynak veya hedef sınırları geçilmişse, işlem durdurulur
    //    if (indis1 >= bt1.Length || bt1[indis1] == 0 || indis2 >= bt2.Length)
    //        return;

    //  bt2[indis2]=bt1[indis1];
    // Sol alt ağaç kopyalanır
    //    Kopyalama1(bt1, indis1 * 2 + 1, bt2, indis2 * 2 + 1);

    //    // Sağ alt ağaç kopyalanır
    //    Kopyalama1(bt1, indis1 * 2 + 2, bt2, indis2 * 2 + 2);
    //}
    //}

    ///Ağaçtaki Maksimum Değeri Bulma
    //static int MaxDeger2(int[] bt, int indis)
    //{
    //    if (indis >= bt.Length || bt[indis] == 0) return 0;

    //    int sol = MaxDeger2(bt, indis * 2 + 1);
    //    int sag = MaxDeger2(bt, indis * 2 + 2);

    //    int max = bt[indis];
    //    if (sol > max) max = sol;
    //    if (sag > max) max = sag;

    //    return max;
    //}


    // Ağaçtaki Minimum Değeri Bulma
    //static int MinDeger2(int[] bt, int indis)
    //{
    //    if (indis >= bt.Length || bt[indis] == 0) return 0;

    //    int sol = MinDeger2(bt, indis * 2 + 1);
    //    int sag = MinDeger2(bt, indis * 2 + 2);

    //    int min = bt[indis];
    //    if (sol < min) min = sol;
    //    if (sag < min) min = sag;

    //    return min;
    //}


    //AGACTAKİ BELİRLİ BİR DEĞERİ ARAMA:
    //static bool arama(int[] bt, int indis, int deger)
    //{
    //    if (indis >= bt.Length || bt[indis] == 0) return false;
    //    if (bt[indis] == deger) return true;

    //    bool sol = arama(bt, indis * 2 + 1);
    //    bool sag = arama(bt, indis * 2 + 2);
    //    return sol + sag;
    //}

    //AGACTAKİ BELİRLİ BİR DEĞERİN SEVİYESİNİ BULMA:
    //    static int degerseviye(int[] bt, int indis, int deger, int seviye)
    //    {

    //        if (indis >= bt.Length || bt[indis] == 0) return -1;
    //        if (bt[indis] == deger) return seviye;

    //        //bt: Sayıların tutulduğu ağaç dizisi.
    //        //indis: Şu an hangi kutucuğa baktığımız.
    //        //deger: "Nerede?" diye aradığımız sayı.
    //        //seviye: O anki derinliğimiz(Kökten ne kadar uzaktayız).
    //(
    //        int sol=degerseviye)(bt, indis * 2 + 1, deger, seviye + 1);
    //        if (soll != -1) return SocketOptionLevel;
    //        int sag = degerseviye(bt, indis * 2 + 2, deger, seviye);
    //        return sag;
    // }
    //---------------
    //static int SeviyeDedektifi(int[] agac, int sira, int aranan, int kat)
    //{
    //    // Yol bittiyse veya kutu boşsa -1 dön
    //    if (sira >= agac.Length || agac[sira] == 0)
    //    {
    //        return -1;
    //    }

    //    // Aradığımız sayıyı bulduysak kat numarasını ver
    //    if (agac[sira] == aranan)
    //    {
    //        return kat;
    //    }
    //    // Önce sol tarafı tara
    //    int solSonuc = SeviyeDedektifi(agac, sira * 2 + 1, aranan, kat + 1);
    //    // Sol tarafta bulduysak sağ tarafa hiç bakma, sonucu gönder
    //    if (solSonuc != -1)
    //    {
    //        return solSonuc;
    //    }
    //    // Sol taraf boş çıktıysa sağ tarafa bak ve sonucu döndür
    //    return SeviyeDedektifi(agac, sira * 2 + 2, aranan, kat + 1);
    //}

    //AGAC DENGELİ Mİ:
    //static bool dengelimi(int[] bt, int indis,ref yükseklik )
    //{
    //    if (indis >= bt.Length || bt[indis] == 0)
    //    {
    //        yükseklik = 0;
    //        return true; //olmayan agac dengelidir
    //    }

    //    int solyukseklik = 0;
    //    int sahyukseklik = 0;

    //    bool sag = dengelimi(bt, indis * 2 + 1, ref solyukseklik);
    //    bool sag = dengelimi(bt, indis * 2 + 2, refsagyukseklik);
    //    int fark;
    //    if (solYukselik > sagYukseklik)
    //    {
    //        // Sol taraf daha uzunsa soldan sağı çıkar
    //        fark = solYukselik - sagYukseklik;
    //    }
    //    else
    //    {
    //        // Sağ taraf daha uzunsa (veya eşitse) sağdan solu çıkar
    //        fark = sagYukseklik - solYukselik;
    //    }
    //    // if (!sol || !sag || fark > 1) return false;

    //    if (sol == false)
    //    {
    //        // 1. Durum: Sol alt ağaç zaten kendi içinde dengesizmiş
    //        return false;
    //    }
    //    else if (sag == false)
    //    {
    //        // 2. Durum: Sağ alt ağaç zaten kendi içinde dengesizmiş
    //        return false;
    //    }
    //    else if (fark > 1)
    //    {
    //        // 3. Durum: Her iki taraf da kendi içinde dengeli ama 
    //        // aralarındaki boy farkı çok fazla (1'den büyük)
    //        return false;
    //    }
    //    else
    //    {
    //        // Hiçbir sorun yoksa dengelidir
    //        return true;
    //    }
    //    return true;

    //}

    //AGAC İKİZ Mİ:
    //static bool ikiz(int[] bt1, int indis1, int[] bt2, int indis2)
    //{
    //    if ((indis1 > bt1.Length || bt1[indis1] == 0) && (indis2 > bt2.Length || bt2[indis2] == 0))
    //        //    {
    //        //        return true;
    //        //    }

    //   if ((indis1 >= bt1.Length || bt1[indis1] == 0) || (indis2 >= bt2.Length || bt2[indis2] == 0))
    //            //    {
    //            //        return false;
    //            //    }

    //   if (bt1[indis1] != bt2[indis2]) return false;
    //    return İkiz(bt1, indis1 * 2 + 1, bt2, indis2 * 2 + 1) && İkiz(bt2, indis1 * 2 + 2, bt2, indis2 * 2 + 2);

    //}
    
    //İKİ DÜĞÜM ARASI MESAFE

    //static int mesafeler(int[] bt, int indis, int deger, int deger1)
    //{
    //    // 1. DURDURMA KOŞULU
    //    if (indis >= bt.Length || bt[indis] == 0)
    //    {
    //        return 0;
    //    }

    //    // 2. HEDEFLERDEN BİRİNİ BULDUK MU?
    //    if (bt[indis] == deger || bt[indis] == deger1)
    //    {
    //        return 1;
    //    }

    //    // 3. KEŞİF: Sol ve sağ tarafa sor
    //    int sol = mesafeler(bt, indis * 2 + 1, deger, deger1);
    //    int sag = mesafeler(bt, indis * 2 + 2, deger, deger1);

        // 4. KARAR MERKEZİ (Senin istediğin if-else dönüşümü)

        // EĞER SAYILARDAN BİRİ SOLDA, DİĞERİ SAĞDA BULUNDUYSA:
        //if (sol > 0 && sag > 0)
        //{
        //    // Bu düğüm "Ortak Ata"dır. İki koldan gelen mesafeyi birleştir.
        //    // (Kodda sol + sag - 1 denmiş, bu birleştirme mantığıdır)
        //    return sol + sag;
        //}

        //// EĞER SADECE BİR TARAFTAN SONUÇ GELDİYSE (VEYA HİÇ GELMEDİYSE):
        //if (sol > 0)
        //{
        //    // Sadece sol tarafta bir şeyler bulundu, mesafeyi 1 artırıp yukarı ilet
        //    return sol + 1;
        //}
        //else if (sag > 0)
        //{
        //    // Sadece sağ tarafta bir şeyler bulundu, mesafeyi 1 artırıp yukarı ilet
        //    return sag + 1;
        //}
        //else
        //{
        //    // İki tarafta da hiçbir şey bulunamadı
        //    return 0;
        //}
    //}



}

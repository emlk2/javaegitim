/*
BINARY TREE
Binary Tree (ikili ağaç), her bir düğümün en fazla iki alt düğüme sahip olduğu bir ağaç veri yapısıdır. 
Bu yapı, verilerin hiyerarşik olarak organize edilmesine ve hızlı bir şekilde erişilmesine olanak sağlar.

Özellikler:
1. Her düğüm üç bileşenden oluşur:
   - Değer (key): Düğümde saklanan veri.
   - Sol çocuk (left child): Düğümün sol alt düğümü.
   - Sağ çocuk (right child): Düğümün sağ alt düğümü.
2. Her düğümün en fazla iki çocuğu olabilir.
3. Her düğümün en fazla bir ebeveyni olabilir.
4. Kök (root), ağacın en üstteki düğümüdür.
5. Yaprak (leaf), alt düğümü olmayan düğümlerdir.
6. Yükseklik (height), kökten en derin yaprağa kadar olan yolun uzunluğudur.
7. Derinlik (depth), kökten belirli bir düğüme kadar olan yolun uzunluğudur.
*/

#region Örnek Ağaçlar
// 1: Basit Bir Binary Tree
//        1
//       / \
//      2   3
//     / \
//    4   5
// Açıklama:
// - Kök (Root): 1
// - Yapraklar: 4, 5, 3
// - Ağacın Yüksekliği: 2

// 2: Eğik Binary Tree (Skewed Binary Tree)
//       10
//         \
//          20
//            \
//             30
//               \
//                40
// Açıklama:
// - Kök: 10
// - Yükseklik: 3
// - Derinlik (40 için): 3

// 3: Dengeli Binary Tree (Balanced Binary Tree)
//          15
//        /    \
//       10     20
//      /  \   /  \
//     8   12 16  25
// Açıklama:
// - Yapraklar: 8, 12, 16, 25
// - Yükseklik: 2
// - Dengeli: Evet (her iki alt ağaç arasında yükseklik farkı en fazla 1)
#endregion

#region Ağacı Gez ve Yazdır (Sağ, Sol, Kök)
using System;

using System.Collections.Generic;

/// <summary>
/// Binary Tree'yi sağ, sol ve kök sırasına göre gezer ve düğüm değerlerini yazdırır.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
//static void BtreeYaz(int[] bt, int indis)
//{
//    if (indis >= bt.Length) return;

//    BtreeYaz(bt, indis * 2 + 1); // Sol alt ağaç
//    BtreeYaz(bt, indis * 2 + 2); // Sağ alt ağaç
//    Console.WriteLine(bt[indis]); // Mevcut düğüm
//}
//#endregion

#region Ağaçtaki Aktif Düğüm Sayısı
/// <summary>
/// Binary Tree'deki aktif düğüm sayısını döndürür.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <returns>Aktif düğüm sayısı</returns>
//static int AktifDugum(int[] bt, int indis)
//{
//    if (indis >= bt.Length) return 0;
//    if (bt[indis] == 0)
//        return 0 + AktifDugum(bt, indis * 2 + 1) + AktifDugum(bt, indis * 2 + 2);
//    else
//        return 1 + AktifDugum(bt, indis * 2 + 1) + AktifDugum(bt, indis * 2 + 2);
//}
#endregion

#region Belirli Bir Değerin Ağacın İçinde Olup Olmadığını Arama
/// <summary>
/// Belirli bir değerin Binary Tree içerisinde olup olmadığını kontrol eder.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <param name="deger">Aranacak değer</param>
/// <returns>Değer bulunduysa 0, aksi halde 1</returns>
//static int Varmı(int[] bt, int indis, int deger)
//{
//    if (indis >= bt.Length) return 0;
//    if (bt[indis] == deger) return 1;

//    int sol = Varmı(bt, indis * 2 + 1, deger);
//    int sag = Varmı(bt, indis * 2 + 2, deger);

//    return sol + sag;
//}
//#endregion

#region Her Seviyede Maksimum Değeri Bulma
/// <summary>
/// Her seviyedeki maksimum düğüm değerini bulur.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <param name="svy">Seviyelere göre maksimum değerleri saklayan dizi</param>
/// <param name="seviye">Başlangıç seviyesi</param>
//static void MaxDeger(int[] bt, int indis, int[] svy, int seviye)
//{
//    if (indis >= bt.Length) return; // Dizi sınırını aştıysa dur
//    if (bt[indis] == 0) return; // Boş düğümse dur

//    // Eğer mevcut düğümün değeri, şu ana kadar bulunan maksimum değerden büyükse, 
//    // bu seviyedeki maksimum değeri güncelle.
//    if (svy[seviye] < bt[indis])
//    {
//        svy[seviye] = bt[indis];
//    }

//    // Sol ve sağ alt ağaçları kontrol et ve aynı işlemi uygula
//    MaxDeger(bt, indis * 2 + 1, svy, seviye + 1); // Sol alt ağaç için devam et
//    MaxDeger(bt, indis * 2 + 2, svy, seviye + 1); // Sağ alt ağaç için devam et
//}
//#endregion

#region Ağaçtaki Yaprak Düğüm Sayısını Bulma
/// <summary>
/// Binary Tree'deki yaprak düğüm sayısını bulur.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <returns>Yaprak düğüm sayısı</returns>
//static int YaprakDugum(int[] bt, int indis)
//{
//    if (indis >= bt.Length) return 0; // Dizi sınırını aştıysa dur
//    if (bt[indis] == 0) return 0; // Düğüm boşsa dur

//    // Eğer bir düğümün sol ve sağ çocukları dizide yoksa, bu düğüm bir yapraktır.
//    // Çünkü bir yaprak düğümün alt çocukları yoktur. (Sol ve sağ alt ağaçlara işaret eden indisler,
//    // dizinin sınırını aşmıştır veya bu indislerde herhangi bir düğüm değeri bulunmamaktadır.)
//    if (indis * 2 + 1 >= bt.Length && indis * 2 + 2 >= bt.Length) return 1;

//    int sol = YaprakDugum(bt, indis * 2 + 1); // Sol alt ağacın yaprak düğüm sayısını bul
//    int sag = YaprakDugum(bt, indis * 2 + 2); // Sağ alt ağacın yaprak düğüm sayısını bul

//    return sol + sag; // Toplam yaprak düğüm sayısını döndür
//}




//#region Ağaçtaki Toplam Düğüm Değerlerinin Toplamı
///// <summary>
///// Binary Tree'deki tüm düğüm değerlerinin toplamını hesaplar.
///// </summary>
///// <param name="bt">Binary Tree dizisi</param>
///// <param name="indis">Başlangıç indisi</param>
///// <returns>Düğüm değerlerinin toplamı</returns>
//static int DugumToplam(int[] bt, int indis)
//{
//    if (indis >= bt.Length) return 0;
//    if (bt[indis] == 0) return 0;

//    int sol = DugumToplam(bt, indis * 2 + 1);
//    int sag = DugumToplam(bt, indis * 2 + 2);

//    return bt[indis] + sol + sag;
//}
//#endregion

//#region Belirli Bir Değerin Ağacın Kaçıncı Seviyesinde Olduğunu Bulma
/// <summary>
/// Belirli bir değerin Binary Tree içerisindeki seviyesini bulur.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <param name="deger">Aranacak değer</param>
/// <param name="seviye">Başlangıç seviyesi</param>
/// <returns>Değerin bulunduğu seviye</returns>
//static int SeviyeBul(int[] bt, int indis, int deger, int seviye)
//{
//    if (indis >= bt.Length) return -1;
//    if (bt[indis] == 0) return -1;
//    if (bt[indis] == deger) return seviye;

//    int sol = SeviyeBul(bt, indis * 2 + 1, deger, seviye + 1);
//    int sag = SeviyeBul(bt, indis * 2 + 2, deger, seviye + 1);

//    return sol != -1 ? sol : sag;

//1. Ne Yapmaya Çalışıyoruz? (Toplama mı? Kat Numarası mı?)Eğer yaptığın işlem bir miktar veya toplam belirtiyorsa (Sayma, Toplama, Yaprak Sayısı vb.), "yokluk" anlamına gelen 0 döndürülür. Çünkü matematikte $5 + 0 = 5$ eder, sıfır sonucu etkilemez.Ancak bu son kodda bir "Yer/İndis/Kat" arıyoruz.Ağaçta 0. seviye (kat) zaten vardır (Ana kök).Eğer sayı bulunamadığında 0 döndürürsen, bilgisayar sayıyı 0. katta (en tepede) bulduğunu sanır.
//}
//#endregion

#region İki Düğüm Arasındaki Mesafeyi Bulma
/// <summary>
/// Binary Tree içerisindeki iki düğüm arasındaki mesafeyi bulur.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <param name="deger1">Birinci düğüm değeri</param>
/// <param name="deger2">İkinci düğüm değeri</param>
/// <returns>Düğümler arasındaki mesafe</returns>
//static int Mesafe(int[] bt, int indis, int deger1, int deger2)
//{
//    // Eğer indis, dizi uzunluğunu aşarsa -1 döner (geçersiz düğüm).
//    if (indis >= bt.Length) return -1;

//    // Eğer düğüm değeri 0 ise bu düğüm geçersizdir, mesafe 0 döner.
//    if (bt[indis] == 0) return 0;

//    // Eğer mevcut düğüm değerlerinden biri (deger1 veya deger2) ile eşleşiyorsa, mesafe 1 döner.
//    if (bt[indis] == deger1 || bt[indis] == deger2) return 1;

//    // Sol alt ağacın mesafesini hesapla.
//    int sol = Mesafe(bt, indis * 2 + 1, deger1, deger2);

//    // Sağ alt ağacın mesafesini hesapla.
//    int sag = Mesafe(bt, indis * 2 + 2, deger1, deger2);

//    // Eğer hem sol hem de sağ alt ağaçta düğümler bulunduysa, toplam mesafeyi hesapla.
//    if (sol > 0 && sag > 0) return sol + sag - 1;

//    // Sadece sol alt ağaçta düğüm bulunduysa, mesafeyi artırarak döner.
//    return sol > 0 ? sol + 1 : sag > 0 ? sag + 1 : 0;
//}
//#endregion

#region Ağacın Ayna Görünümünü Çıkarma
/// <summary>
/// Binary Tree'nin ayna görüntüsünü çıkarır.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
//static void Ayna(int[] bt, int indis)
//{
//    if (indis >= bt.Length) return;
//    if (bt[indis] == 0) return;

//    int temp = indis * 2 + 1 < bt.Length ? bt[indis * 2 + 1] : 0;
//    if (indis * 2 + 1 < bt.Length) bt[indis * 2 + 1] = indis * 2 + 2 < bt.Length ? bt[indis * 2 + 2] : 0;
//    if (indis * 2 + 2 < bt.Length) bt[indis * 2 + 2] = temp;

//    Ayna(bt, indis * 2 + 1);
//    Ayna(bt, indis * 2 + 2);
//}
//#endregion

//// Ağaçtaki En Küçük Değeri Bulma
//static int MinDeger(int[] bt, int indis)
//{
//    if (indis >= bt.Length) return 0;
//    if (bt[indis] == 0) return 0;

//    int sol = MinDeger(bt, indis * 2 + 1);
//    int sag = MinDeger(bt, indis * 2 + 2);

//    int min = bt[indis];

//    if (sol < min) min = sol;
//    if (sag < min) min = sag;

//    return min;
//}

// Ağaçtaki En Büyük Değeri Bulma
//static int MaxDeger1(int[] bt, int indis)
//{
//    if (indis >= bt.Length) return 0;
//    if (bt[indis] == 0) return 0;

//    int sol = MinDeger(bt, indis * 2 + 1);
//    int sag = MinDeger(bt, indis * 2 + 2);

//    int max = bt[indis];

//    if (sol > max) max = sol;
//    if (sag > max) max = sag;

//    return max;
//}

#region Ağaçtaki Derinliği Bulma
/// <summary>
/// Binary Tree'nin derinliğini bulur (en uzun yolun kenar sayısı).
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <returns>Ağacın derinliği</returns>
//static int DerinlikBul(int[] bt, int indis, int hedef)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return -1; // Dizinin sınırını aştıysa veya düğüm boşsa derinlik 0
//    if (indis == hedef) return 0;
//    // Sol ve sağ alt ağaçların derinliğini bul
//    int solDerinlik = DerinlikBul(bt, indis * 2 + 1, hedef);
//    int sagDerinlik = DerinlikBul(bt, indis * 2 + 2, hedef);

//    // Daha derin olan alt ağacı seç ve kenar sayısını döndür
//    if (solDerinlik != -1)
//        return solDerinlik + 1; // Sol alt ağaç daha derinse
//    if (sagDerinlik != -1)
//        return sagDerinlik + 1; // Sağ alt ağaç daha derinse
//    return -1;
//}
#endregion

//// Ağaçtaki Yüksekliği Bulma
//static int Yuksek(int[] bt, int indis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return 0; // Dizinin sınırını aştıysa veya düğüm boşsa derinlik 0

//    // Sol ve sağ alt ağaçların derinliğini bul
//    int solDerinlik = Yuksek(bt, indis * 2 + 1);
//    int sagDerinlik = Yuksek(bt, indis * 2 + 2);

//    // Daha derin olan alt ağacı seç ve kenar sayısını döndür
//    if (solDerinlik > sagDerinlik)
//        return solDerinlik + 1; // Sol alt ağaç daha derinse
//    else
//        return sagDerinlik + 1; // Sağ alt ağaç daha derinse
//}

// Ağaçtaki Yaprak Düğümleri Bulma
//static int YaprakDugum1(int[] bt, int indis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return 0;

//    if ((indis * 2 + 1 > bt.Length || bt[indis] == 0) && (indis * 2 + 2 > bt.Length || bt[indis] == 0)) return 1;

//    int sol = YaprakDugum1(bt, indis * 2 + 1);
//    int sag = YaprakDugum1(bt, indis * 2 + 2);

//    return sol + sag;
//}

//// Ağaçtaki İç Düğümleri Bulma
//static int icDugum(int[] bt, int indis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return 0;

//    if ((indis * 2 + 1 > bt.Length && bt[indis] == 0) || (indis * 2 + 2 > bt.Length && bt[indis] == 0)) return 0;

//    int sol = icDugum(bt, indis * 2 + 1);
//    int sag = icDugum(bt, indis * 2 + 2);

//    return sol + sag;
//}

/// Ağaçtaki Düğümleri Bulma
//static int Dugumler(int[] bt, int indis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return 0;

//    int sol = Dugumler(bt, indis * 2 + 1);
//    int sag = Dugumler(bt, indis * 2 + 2);
//    return sol + sag + 1;
//}

// Ağaçtaki Düğümleri Yazdırma (Preorder, Inorder, Postorder)
//static void PreorderYazdir(int[] bt, int indis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return;

//    Console.WriteLine(bt[indis]); // Kök
//    PreorderYazdir(bt, indis * 2 + 1); // Sol
//    PreorderYazdir(bt, indis * 2 + 2); // Sağ
//}

//static void InorderYazdir(int[] bt, int indis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return;

//    InorderYazdir(bt, indis * 2 + 1); // Sol
//    Console.WriteLine(bt[indis]); // Kök
//    InorderYazdir(bt, indis * 2 + 2); // Sağ
//}

//static void PostorderYazdir(int[] bt, int indis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return;

//    PostorderYazdir(bt, indis * 2 + 1); // Sol
//    PostorderYazdir(bt, indis * 2 + 2); // Sağ
//    Console.WriteLine(bt[indis]); // Kök
//}

////Ağaçtaki Düğümleri Silme / Temizleme
//static void silme(int[] bt, int indis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return;

//    bt[indis] = 0;

//    silme(bt, indis * 2 + 1);
//    silme(bt, indis * 2 + 2);
//}

// Ağaçtaki Düğümleri Kopyalama
//static void Kopyalama(int[] bt1, int indis1, int[] bt2, int indis2)
//{
//    // Eğer kaynak veya hedef sınırları geçilmişse, işlem durdurulur
//    if (indis1 >= bt1.Length || bt1[indis1] == 0 || indis2 >= bt2.Length)
//        return;

//    // Kaynaktaki düğüm hedefe kopyalanır
//    bt2[indis2] = bt1[indis1];

//    // Sol alt ağaç kopyalanır
//    Kopyalama(bt1, indis1 * 2 + 1, bt2, indis2 * 2 + 1);

//    // Sağ alt ağaç kopyalanır
//    Kopyalama(bt1, indis1 * 2 + 2, bt2, indis2 * 2 + 2);
//}

//Ağaçtaki Düğümleri Kopyalama (Düğüm Değerlerini Değiştirerek)
//static void Kopyalama1(int[] bt1, int indis1, int[] bt2, int indis2)
//{
//    // Eğer kaynak veya hedef sınırları geçilmişse, işlem durdurulur
//    if (indis1 >= bt1.Length || bt1[indis1] == 0 || indis2 >= bt2.Length)
//        return;

//    // Kaynaktaki düğüm hedefe kopyalanır
//    bt2[indis2] = bt1[indis1] * 2;

//    // Sol alt ağaç kopyalanır
//    Kopyalama1(bt1, indis1 * 2 + 1, bt2, indis2 * 2 + 1);

//    // Sağ alt ağaç kopyalanır
//    Kopyalama1(bt1, indis1 * 2 + 2, bt2, indis2 * 2 + 2);
//}

////Ağaçtaki Maksimum Değeri Bulma
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

// Ağaçtaki Belirli Bir Değeri Arama
//static bool arama(int[] bt, int indis, int deger)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return false;
//    if (bt[indis] == deger) return true;

//    bool sol = arama(bt, indis * 2 + 1, deger);
//    bool sag = arama(bt, indis * 2 + 2, deger);

//    return sol || sag;
//}

//// Ağaçtaki Belirli Bir Değerin Seviyesini Bulma
//static int DegerSeviye1(int[] bt, int indis, int deger, int seviye)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return -1;
//    if (bt[indis] == deger) return seviye;

//    int sol = DegerSeviye1(bt, indis * 2 + 1, deger, seviye + 1);
//    if (sol != -1) return sol;

//    int sag = DegerSeviye1(bt, indis * 2 + 2, deger, seviye + 1);
//    return sag;
//}

//// Ağaç Dengeli mi? (Balanced Tree Kontrolü) (yükseklik kontrol edilmeli dengelilik durumu için)
////static bool DengeliMi(int[] bt, int indis, ref int yukseklik)
////{
////    if (indis >= bt.Length || bt[indis] == 0)
////    {
////        yukseklik = 0;
////        return true;
////    }

////    int solYukselik = 0;
////    int sagYukseklik = 0;

////    bool sol = DengeliMi(bt, indis * 2 + 1, ref solYukselik);
////    bool sag = DengeliMi(bt, indis * 2 + 2, ref sagYukseklik);

////    int fark = (solYukselik > sagYukseklik) ? solYukselik - sagYukseklik : sagYukseklik - solYukselik;

////    yukseklik = (solYukselik > sagYukseklik) ? (solYukselik + 1) : (sagYukseklik + 1);

////    if (!sol || !sag || fark > 1) return false;

////    return true;
////}

// Ağaç Mükemmel mi? (Perfect Tree Kontrolü) 
//static bool MukemmelMi(int[] bt, int indis, int derinlik, int seviyer)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return true;

//    if ((indis * 2 + 1 >= bt.Length || bt[indis * 2 + 1] == 0)
//        && (indis * 2 + 2 >= bt.Length || bt[indis * 2 + 2] == 0))
//    {
//        return seviyer == derinlik;
//    }

//    if ((indis * 2 + 1 >= bt.Length || bt[indis * 2 + 1] == 0)
//        || (indis * 2 + 2 >= bt.Length || bt[indis * 2 + 2] == 0))
//    {
//        return false;
//    }

//    return MukemmelMi(bt, indis * 2 + 1, derinlik, seviyer + 1) && MukemmelMi(bt, indis * 2 + 2, derinlik, seviyer + 1);
//}

// Ağaç İkiz mi? (Mirror Image Kontrolü)
//static bool İkiz(int[] bt1, int indis1, int[] bt2, int indis2)
//{
//    if ((indis1 > bt1.Length || bt1[indis1] == 0) && (indis2 > bt2.Length || bt2[indis2] == 0))
//    {
//        return true;
//    }

//    if ((indis1 >= bt1.Length || bt1[indis1] == 0) || (indis2 >= bt2.Length || bt2[indis2] == 0))
//    {
//        return false;
//    }
//    if (bt1[indis1] != bt2[indis2]) return false;

//    return İkiz(bt1, indis1 * 2 + 1, bt2, indis2 * 2 + 1) && İkiz(bt2, indis1 * 2 + 2, bt2, indis2 * 2 + 2);
//}

//// Ağaçta İki Düğüm Arasındaki Mesafeyi Bulma
//static int mesafeler(int[] bt, int indis, int deger, int deger1)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return 0;

//    if (bt[indis] == deger || bt[indis] == deger1) return 1;

//    int sol = mesafeler(bt, indis * 2 + 1, deger, deger1);
//    int sag = mesafeler(bt, indis * 2 + 2, deger, deger1);

//    if (sol > 0 && sag > 0) return sol + sag - 1;
//    return (sol > 0) ? sol + 1 : (sag > 0) ? sag + 1 : 0;
//}

// Ağaçtaki En Büyük Alt Ağacı Bulma
//static int EnbuyukAltAgac(int[] bt, int indis, ref int maxDugum, ref int maxKokIndıs)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return 0;

//    int sol = EnbuyukAltAgac(bt, indis * 2 + 1, ref maxDugum, ref maxKokIndıs);
//    int sag = EnbuyukAltAgac(bt, indis * 2 + 2, ref maxDugum, ref maxKokIndıs);


//    int toplam = sol + sag + 1;

//    if (toplam > maxDugum)
//    {
//        maxDugum = toplam;
//        maxKokIndıs = indis;
//    }

//    return toplam;
//}

// Ağaçtaki Belirli Bir Değere Giden Yolu Bulma
//static bool DegereGidenYol(int[] bt, int indis, int deger, List<int> yol)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return false;

//    yol.Add(bt[indis]);

//    if (bt[indis] == deger) return true;

//    if (DegereGidenYol(bt, indis * 2 + 1, deger, yol) || DegereGidenYol(bt, indis * 2 + 2, deger, yol))
//    {
//        return true;
//    }

//    yol.RemoveAt(yol.Count - 1);
//    return false;
//}

// Ağaçtaki En Uzun Yolu Bulma
//static List<int> EnUzunYol(int[] bt, int indis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return new List<int>();

//    List<int> sol = EnUzunYol(bt, indis * 2 + 1);
//    List<int> sag = EnUzunYol(bt, indis * 2 + 2);

//    if (sol.Count > sag.Count)
//    {
//        sol.Add(bt[indis]);
//        return sol;
//    }
//    else
//    {
//        sag.Add(bt[indis]);
//        return sag;
//    }
//}


// Ağaçtaki Tüm Seviyelerin Yüksekliğini Bulma
//static void SeviyeYukselik(int[] bt, int indis, int seviye, List<int> seviyeler)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return;

//    if (seviyeler.Count <= seviye)
//    {
//        seviyeler.Add(1);
//    }
//    else
//    {
//        seviyeler[seviye]++;
//    }

//    SeviyeYukselik(bt, indis * 2 + 1, seviye + 1, seviyeler);
//    SeviyeYukselik(bt, indis * 2 + 2, seviye + 1, seviyeler);

//}

// Ağaçtaki Belirli Bir Seviyedeki Düğümleri Yazdırma
//static void DugumYazdır(int[] bt, int indis, int seviye, int mevcutSeviye)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return;

//    if (seviye == mevcutSeviye)
//    {
//        Console.WriteLine(bt[indis]);
//        return;
//    }

//    DugumYazdır(bt, indis * 2 + 1, seviye, mevcutSeviye + 1);
//    DugumYazdır(bt, indis * 2 + 2, seviye, mevcutSeviye + 1);
}

// Ağaçta Yaprak Düğümleri Değerine Göre Sıralama
//static int dugumSirala(int[] bt, int indis, int[] sirali, int siraliindis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return siraliindis;

//    if ((indis * 2 + 1 >= bt.Length || bt[indis * 2 + 1] == 0) &&
//        (indis * 2 + 2 >= bt.Length || bt[indis * 2 + 2] == 0))
//    {
//        sirali[siraliindis] = bt[indis]; // Yaprak düğümü ekle
//        return siraliindis + 1;          // İndisi artır
//    }
//    dugumSirala(bt, indis * 2 + 1, sirali, siraliindis);
//    dugumSirala(bt, indis * 2 + 2, sirali, siraliindis);

//    return siraliindis;
//}

// Ağaçta İç Düğümleri Değerine Göre Sıralama
//static int icDugumSirala(int[] bt, int indis, int[] sirali, int siraliindis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return siraliindis;

//    if ((indis * 2 + 1 >= bt.Length || bt[indis * 2 + 1] == 0) ||
//        (indis * 2 + 2 >= bt.Length || bt[indis * 2 + 2] == 0))
//    {
//        sirali[siraliindis] = bt[indis]; // İç düğümü ekle
//        return siraliindis + 1;          // İndisi artır
//    }
//    icDugumSirala(bt, indis * 2 + 1, sirali, siraliindis);
//    icDugumSirala(bt, indis * 2 + 2, sirali, siraliindis);

//    return siraliindis;
//}

//// Ağaçtaki En Derin Düğümü Bulma
//static (int enDerinDugum, int maxSeviye) EnDerinDugumuBul(int[] bt, int indis, int seviye)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return (-1, -1); // Boş düğüm için dur

//    // Eğer yaprak düğümse, mevcut düğüm ve seviyesi döndürülür
//    if ((indis * 2 + 1 >= bt.Length || bt[indis * 2 + 1] == 0) &&
//        (indis * 2 + 2 >= bt.Length || bt[indis * 2 + 2] == 0))
//    {
//        return (bt[indis], seviye);
//    }

//    // Sol ve sağ alt ağaçları işle
//    var sol = EnDerinDugumuBul(bt, indis * 2 + 1, seviye + 1);
//    var sag = EnDerinDugumuBul(bt, indis * 2 + 2, seviye + 1);

//    // Daha derin olanı seç
//    if (sol.maxSeviye > sag.maxSeviye)
//    {
//        return sol;
//    }
//    else
//    {
//        return sag;
//    }
//}

// Ağaçtaki Belirli Bir Alt Ağacı Kopyalama
static void kopyalama(int[] bt, int indis, int[] kopya, int kopyaindis)
{
    if (indis >= bt.Length || bt[indis] == 0) return;

    kopya[kopyaindis] = bt[indis];

    kopyalama(bt, indis * 2 + 1, kopya, kopyaindis * 2 + 1);
    kopyalama(bt, indis * 2 + 2, kopya, kopyaindis * 2 + 2);
}

// Ağaçta Belirli Bir Değeri Kaç Kere Geçtiğini Bulma
static void tekrarlanma(int[] bt, int indis, int deger, ref int sayac)
{
    if (indis >= bt.Length || bt[indis] == 0) return;

    if (bt[indis] == deger) sayac++;

    tekrarlanma(bt, indis * 2 + 1, deger, ref sayac);
    tekrarlanma(bt, indis * 2 + 2, deger, ref sayac);
}
// Ağaçta Belirli Bir Değeri Kaç Kere Geçtiğini Bulma
static int tekrarlanama1(int[] bt, int indis, int deger)
{
    if (indis >= bt.Length || bt[indis] == 0) return 0;


    int sol = tekrarlanama1(bt, indis * 2 + 1, deger);
    int sag = tekrarlanama1(bt, indis * 2 + 2, deger);

    return bt[indis] == deger ? 1 + sol + sag : sol + sag;
}

// Ağaçtaki En Çok Tekrarlanan Değeri Bulma

#region Ağaçtaki Belirli Bir Değerin Hangi Düğümde Olduğunu Bulma
/// <summary>
/// Binary Tree'deki belirli bir değerin hangi düğümde olduğunu bulur.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <param name="deger">Aranacak değer</param>
/// <returns>Değerin bulunduğu düğüm indisi</returns>
static int DegerDugum(int[] bt, int indis, int deger)
{
    if (indis >= bt.Length || bt[indis] == 0) return -1;

    // Değer bulunduysa mevcut indisi dön
    if (bt[indis] == deger) return indis;

    // Sol alt ağaçta ara
    int sol = DegerDugum(bt, indis * 2 + 1, deger);
    if (sol != -1) return sol;

    // Sağ alt ağaçta ara
    return DegerDugum(bt, indis * 2 + 2, deger);
}
#endregion

// Ağaçtaki En Derin Yaprak Düğümü Bulma

#region Ağaç Yapısını Diziye Çevirme
/// <summary>
/// Binary Tree yapısını diziye dönüştürür.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <param name="dizi">Sonuç dizi</param>
/// <param name="diziindis">Dizinin mevcut elemanını takip eden sayaç</param>
//static void DiziyeCevir(int[] bt, int indis, int[] dizi, ref int diziindis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return;

//    // Binary Tree'den diziye değer aktarımı yap
//    dizi[diziindis] = bt[indis];
//    diziindis++;

//    // Sol ve sağ alt ağaçları kontrol et
//    DiziyeCevir(bt, indis * 2 + 1, dizi, ref diziindis);
//    DiziyeCevir(bt, indis * 2 + 2, dizi, ref diziindis);
//}
#endregion

#region Diziden Ağaç Oluşturma
/// <summary>
/// Bir dizi kullanarak Binary Tree yapısı oluşturur.
/// </summary>
/// <param name="bt">Oluşturulacak Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <param name="dizi">Kaynak dizi</param>
/// <param name="dizindis">Dizinin mevcut elemanını takip eden sayaç</param>
//static void AgacOlustur(int[] bt, int indis, int[] dizi, ref int dizindis)
//{
//    if (dizindis >= dizi.Length) return;

//    // Diziden Binary Tree'ye değer aktarımı yap
//    bt[indis] = dizi[dizindis];
//    dizindis++;

//    // Sol ve sağ alt ağaçları oluştur
//    AgacOlustur(bt, indis * 2 + 1, dizi, ref dizindis);
//    AgacOlustur(bt, indis * 2 + 2, dizi, ref dizindis);
//}
//#endregion

#region Ağacın Düğümlerini Seviyelere Göre Yazdırma
/// <summary>
/// Binary Tree'deki düğümleri belirli bir seviyeye göre yazdırır.
/// </summary>
/// <param name="bt">Binary Tree dizisi</param>
/// <param name="indis">Başlangıç indisi</param>
/// <param name="mevcutseviye">Mevcut seviyeyi takip eden sayaç</param>
/// <param name="seviye">Yazdırılacak hedef seviye</param>
static void SeviyeYazdirma(int[] bt, int indis, int mevcutseviye, int seviye)
{
    if (indis >= bt.Length || bt[indis] == 0) return;

    // Hedef seviyedeki düğümü yazdır
    if (mevcutseviye == seviye)
    {
        Console.WriteLine(bt[indis]);
        return;
    }

    // Alt seviyeleri kontrol et
    SeviyeYazdirma(bt, indis * 2 + 1, mevcutseviye + 1, seviye);
    SeviyeYazdirma(bt, indis * 2 + 2, mevcutseviye + 1, seviye);
}
#endregion

//// Ağaç Yapısındaki Çift Sayı Değerli Düğümleri Bulma
//static void ciftDugum(int[] bt, int indis)
//{
//    if (indis >= bt.Length || bt[indis] == 0) return;
//    if (bt[indis] % 2 == 0) Console.WriteLine(bt[indis]);

//    ciftDugum(bt, indis * 2 + 1);
//    ciftDugum(bt, indis * 2 + 2);
//}






//#region Btree Sınıfı
///// <summary>
///// Binary Tree düğüm yapısını temsil eden sınıf.
///// </summary>
//class Btree
//{
//	public int data; // Düğümdeki veri
//	public Btree left; // Sol alt düğüm
//	public Btree right; // Sağ alt düğüm
//}
//#endregion



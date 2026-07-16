// JavaScript = Programlama dili
// Node.js    = JavaScript’i bilgisayarda ve sunucuda çalıştıran ortam
// npm        = Hazır JavaScript paketlerini kuran araç
// Express    = Node.js ile backend yazmayı kolaylaştıran paket
console.log(10 + 5);
console.log("10" + "5");
let isim = "Veli";
// let     → değişken oluşturur
// isim    → değişkenin adı
// "Veli"  → değişkenin değeri
const isim = "Veli";
let yas = 22;
const ogrenciMi = true;

console.log("İsim:", isim);
console.log("Yaş:", yas);
console.log("Öğrenci mi:", ogrenciMi);

yas = yas + 1;

console.log("Bir yıl sonraki yaş:", yas);

let not = 75;

if (not >= 85) {
    console.log("Pekiyi");
} else if (not >= 70) {
    console.log("İyi");
} else if (not >= 50) {
    console.log("Geçti");
} else {
    console.log("Kaldı");
}
//--------------------------------------
function topla(sayi1, sayi2) {
    return sayi1 + sayi2;
}

function bilgileriYazdir(isim, yas) {
    console.log(`İsim: ${isim}`);
    console.log(`Yaş: ${yas}`);
}

bilgileriYazdir("Veli", 22);

//hesap makinesi örneği:
function hesapla(sayi1, sayi2, islem) {
    if (islem === "topla") {
        return sayi1 + sayi2;
    }

    if (islem === "cikar") {
        return sayi1 - sayi2;
    }

    if (islem === "carp") {
        return sayi1 * sayi2;
    }

    if (islem === "bol") {
        return sayi1 / sayi2;
    }

    return "Geçersiz işlem";
}

console.log(hesapla(10, 5, "topla"));
console.log(hesapla(10, 5, "carp"));
console.log(hesapla(10, 5, "cikar"));
console.log(hesapla(10, 5, "bol"));

//push : diizye elmaan ekler
//unshift : dizinin başına eleman ekler
//pop : dizinin sonundaki elemanı siler
//shift : dizinin başındaki elemanı siler

const kullanici = {
    isim: "Veli",
    yas: 22,
    sehir: "İstanbul"
};

console.log(kullanici.isim);
console.log(kullanici.yas);
console.log(kullanici.sehir);

//JSON, verilerin internet üzerinden taşınmasında kullanılan bir formattır
// Temel fark, JSON içinde özellik isimlerinin çift tırnakla yazılmasıdır.
// Backend, frontend’e çoğunlukla JSON gönderir.

// Node.js sayesinde JavaScript ile:

// Dosya oluşturabiliriz.
// Dosya okuyabiliriz.
// Dosyaya yazı ekleyebiliriz.
// Dosya silebiliriz.
// Kodumuzu farklı dosyalara bölebiliriz. 
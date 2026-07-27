"use strict";

/* =====================================================
   UYGULAMA DURUMU
===================================================== */

const durum = {
    token: localStorage.getItem("edupanel_token") || "",
    kullanici: kayitliKullaniciyiOku(),

    ogrenciler: [],
    ogretmenler: [],
    dersler: [],

    ogretmenDersleri: [],
    ogrenciDersleri: [],

    ogretmenSinavlari: [],
    ogrenciSinavlari: [],

    ogretmenSinavOgrencileri: [],
    seciliNotSinavi: null,
    ogrenciNotlari: [],

ogretmenDevamsizlikOgrencileri: [],
seciliDevamsizlikDersi: null,
seciliDevamsizlikTarihi: "",

ogrenciDevamsizliklari: [],
ogrenciDevamsizlikOzeti: {
    toplam: 0,
    geldi: 0,
    gelmedi: 0,
    gec_kaldi: 0,
    izinli: 0
}
};


/* =====================================================
   YARDIMCI FONKSİYONLAR
===================================================== */

function element(id) {
    return document.getElementById(id);
}

function kayitliKullaniciyiOku() {
    try {
        const kayit =
            localStorage.getItem("edupanel_kullanici");

        return kayit
            ? JSON.parse(kayit)
            : null;
    } catch {
        return null;
    }
}

function htmlGuvenli(deger) {
    return String(deger ?? "")
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#039;");
}

function tarihYaz(tarih) {
    if (!tarih) {
        return "—";
    }

    const tarihNesnesi = new Date(tarih);

    if (Number.isNaN(tarihNesnesi.getTime())) {
        return "—";
    }

    return tarihNesnesi.toLocaleString("tr-TR");
}

function rolAdi(rol) {
    const roller = {
        admin: "Yönetici",
        ogretmen: "Öğretmen",
        ogrenci: "Öğrenci"
    };

    return roller[rol] || rol || "Kullanıcı";
}

function jwtOku(token) {
    try {
        const parcalar = token.split(".");

        if (parcalar.length !== 3) {
            return {};
        }

        let base64 = parcalar[1]
            .replaceAll("-", "+")
            .replaceAll("_", "/");

        while (base64.length % 4 !== 0) {
            base64 += "=";
        }

        const ikiliMetin = atob(base64);

        const jsonMetni = decodeURIComponent(
            Array.from(ikiliMetin)
                .map((karakter) => {
                    const kod = karakter
                        .charCodeAt(0)
                        .toString(16)
                        .padStart(2, "0");

                    return `%${kod}`;
                })
                .join("")
        );

        return JSON.parse(jsonMetni);
    } catch {
        return {};
    }
}

function listeyiAl(veri, alanAdi) {
    if (Array.isArray(veri)) {
        return veri;
    }

    if (
        veri &&
        alanAdi &&
        Array.isArray(veri[alanAdi])
    ) {
        return veri[alanAdi];
    }

    return [];
}


/* =====================================================
   API İSTEK FONKSİYONU
===================================================== */

async function apiIstek(
    adres,
    secenekler = {},
    tokenGerekli = true
) {
    const basliklar = {
        ...(secenekler.headers || {})
    };

    if (
        secenekler.body &&
        !(secenekler.body instanceof FormData)
    ) {
        basliklar["Content-Type"] =
            basliklar["Content-Type"] ||
            "application/json";
    }

    if (tokenGerekli && durum.token) {
        basliklar.Authorization =
            `Bearer ${durum.token}`;
    }

    const cevap = await fetch(adres, {
        ...secenekler,
        headers: basliklar
    });

    const hamCevap = await cevap.text();

    let veri = null;

    if (hamCevap) {
        try {
            veri = JSON.parse(hamCevap);
        } catch {
            veri = null;
        }
    }

    if (!cevap.ok) {
        const mesaj =
            veri?.mesaj ||
            veri?.hata ||
            (
                hamCevap &&
                !hamCevap.includes("<!DOCTYPE")
                    ? hamCevap
                    : null
            ) ||
            `İstek başarısız oldu (${cevap.status})`;

        if (
            cevap.status === 401 &&
            tokenGerekli &&
            durum.token
        ) {
            oturumuKapat(false);

            bildirimGoster(
                "Oturum süreniz doldu. Tekrar giriş yapın.",
                "uyari"
            );
        }

        const hata = new Error(mesaj);

        hata.status = cevap.status;
        hata.veri = veri;

        throw hata;
    }

    return veri;
}


/* =====================================================
   BİLDİRİM
===================================================== */

let bildirimZamanlayici;

function bildirimGoster(
    mesaj,
    tur = "basarili"
) {
    const bildirim = element("bildirim");
    const metin = element("bildirim-metni");

    if (!bildirim || !metin) {
        return;
    }

    clearTimeout(bildirimZamanlayici);

    bildirim.classList.remove(
        "basarili",
        "hata",
        "uyari"
    );

    bildirim.classList.add(tur);

    metin.textContent = mesaj;

    bildirim.classList.add("goster");

    bildirimZamanlayici = setTimeout(() => {
        bildirim.classList.remove("goster");
    }, 3500);
}


/* =====================================================
   SİSTEM DURUMU
===================================================== */

async function sistemDurumunuKontrolEt() {
    const durumMetni =
        element("sistem-durum-metni");

    const durumNoktasi =
        element("sistem-durum-noktasi");

    const apiDurumu =
        element("istatistik-api");

    try {
        await apiIstek(
            "/db-test",
            {},
            false
        );

        if (durumMetni) {
            durumMetni.textContent =
                "Çalışıyor";
        }

        if (durumNoktasi) {
            durumNoktasi.classList.add(
                "aktif"
            );
        }

        if (apiDurumu) {
            apiDurumu.textContent =
                "Çalışıyor";
        }
    } catch {
        if (durumMetni) {
            durumMetni.textContent =
                "Bağlantı yok";
        }

        if (durumNoktasi) {
            durumNoktasi.classList.remove(
                "aktif"
            );
        }

        if (apiDurumu) {
            apiDurumu.textContent =
                "Bağlantı yok";
        }
    }
}


/* =====================================================
   GİRİŞ VE ÇIKIŞ
===================================================== */

async function girisYap(event) {
    event.preventDefault();

    const kullaniciAdi =
        element("giris-kullanici-adi")
            .value
            .trim();

    const sifre =
        element("giris-sifre").value;

    const mesajAlani =
        element("giris-mesaji");

    const buton =
        element("giris-butonu");

    mesajAlani.textContent = "";

    buton.disabled = true;
    buton.textContent =
        "Giriş yapılıyor...";

    try {
        const cevap = await apiIstek(
            "/api/auth/giris",
            {
                method: "POST",

                body: JSON.stringify({
                    kullanici_adi: kullaniciAdi,
                    sifre
                })
            },
            false
        );

        if (!cevap?.token) {
            throw new Error(
                "Sunucudan token alınamadı"
            );
        }

        const tokenVerisi =
            jwtOku(cevap.token);

        const kullanici = {
            id:
                cevap.kullanici?.id ??
                tokenVerisi.id ??
                tokenVerisi.kullanici_id,

            kullanici_adi:
                cevap.kullanici
                    ?.kullanici_adi ??
                tokenVerisi.kullanici_adi ??
                kullaniciAdi,

            rol: String(
                cevap.kullanici?.rol ??
                tokenVerisi.rol ??
                ""
            ).toLowerCase()
        };

        if (
            ![
                "admin",
                "ogretmen",
                "ogrenci"
            ].includes(kullanici.rol)
        ) {
            throw new Error(
                "Kullanıcı rolü belirlenemedi"
            );
        }

        durum.token = cevap.token;
        durum.kullanici = kullanici;

        localStorage.setItem(
            "edupanel_token",
            durum.token
        );

        localStorage.setItem(
            "edupanel_kullanici",
            JSON.stringify(kullanici)
        );

        await oturumuAc();

        bildirimGoster(
            "Giriş başarılı",
            "basarili"
        );
    } catch (hata) {
        mesajAlani.textContent =
            hata.message;
    } finally {
        buton.disabled = false;
        buton.textContent = "Giriş Yap";
    }
}

async function oturumuAc() {
    element("giris-ekrani")
        .classList
        .add("gizli");

    element("uygulama")
        .classList
        .remove("gizli");

    kullaniciBilgileriniYaz();
    roleGoreMenuyuAyarla();
    hosgeldinAlaniniAyarla();

    gorunumGoster("gorunum-ozet");

    await sistemDurumunuKontrolEt();

    if (
        durum.kullanici.rol === "admin"
    ) {
        await adminVerileriniYukle();
    }

    if (
        durum.kullanici.rol === "ogretmen"
    ) {
        await Promise.all([
    ogrenciDersleriniYukle(),
    ogrenciSinavlariniYukle(),
    ogrenciNotlariniYukle(),
    ogrenciDevamsizliklariniYukle()
]);
    }

    if (
        durum.kullanici.rol === "ogrenci"
    ) {
        await Promise.all([
            ogrenciDersleriniYukle(),
            ogrenciSinavlariniYukle(),
            ogrenciNotlariniYukle()
        ]);
    }
}

function oturumuKapat(
    mesajGoster = true
) {
    durum.token = "";
    durum.kullanici = null;

    durum.ogrenciler = [];
    durum.ogretmenler = [];
    durum.dersler = [];

    durum.ogretmenDersleri = [];
    durum.ogrenciDersleri = [];

    durum.ogretmenSinavlari = [];
    durum.ogrenciSinavlari = [];

    durum.ogretmenSinavOgrencileri = [];
    durum.seciliNotSinavi = null;
    durum.ogrenciNotlari = [];
    durum.ogretmenDevamsizlikOgrencileri = [];
durum.seciliDevamsizlikDersi = null;
durum.seciliDevamsizlikTarihi = "";

durum.ogrenciDevamsizliklari = [];

durum.ogrenciDevamsizlikOzeti = {
    toplam: 0,
    geldi: 0,
    gelmedi: 0,
    gec_kaldi: 0,
    izinli: 0
};

    localStorage.removeItem(
        "edupanel_token"
    );

    localStorage.removeItem(
        "edupanel_kullanici"
    );

    element("uygulama")
        ?.classList
        .add("gizli");

    element("giris-ekrani")
        ?.classList
        .remove("gizli");

    element("giris-formu")
        ?.reset();

    element("not-giris-alani")
        ?.classList
        .add("gizli");

    element("devamsizlik-giris-alani")
    ?.classList
    .add("gizli");    


    if (mesajGoster) {
        bildirimGoster(
            "Çıkış yapıldı",
            "basarili"
        );
    }
}

function kullaniciBilgileriniYaz() {
    const kullaniciAdi =
        durum.kullanici
            ?.kullanici_adi ||
        "Kullanıcı";

    const rol =
        durum.kullanici?.rol;

    element(
        "aktif-kullanici-adi"
    ).textContent = kullaniciAdi;

    element(
        "aktif-kullanici-rolu"
    ).textContent = rolAdi(rol);

    element(
        "kullanici-avatar"
    ).textContent =
        kullaniciAdi
            .charAt(0)
            .toUpperCase();
}

function hosgeldinAlaniniAyarla() {
    const rol =
        durum.kullanici?.rol;

    const baslik =
        element("hosgeldin-basligi");

    const metin =
        element("hosgeldin-metni");

    if (rol === "admin") {
        baslik.textContent =
            "Yönetim paneline hoş geldiniz";

        metin.textContent =
            "Öğrenci, öğretmen, ders ve atama işlemlerini bu panelden yönetebilirsiniz.";
    }

    if (rol === "ogretmen") {
        baslik.textContent =
            "Öğretmen paneline hoş geldiniz";

        metin.textContent =
            "Size atanmış dersleri görüntüleyebilir, sınav oluşturabilir ve öğrenci notlarını yönetebilirsiniz.";
    }

    if (rol === "ogrenci") {
        baslik.textContent =
            "Öğrenci paneline hoş geldiniz";

        metin.textContent =
            "Kayıtlı olduğunuz dersleri, sınavları ve açıklanan notlarınızı görüntüleyebilirsiniz.";
    }
}


/* =====================================================
   MENÜ VE GÖRÜNÜMLER
===================================================== */

function roleGoreMenuyuAyarla() {
    const rol =
        durum.kullanici?.rol;

    document
        .querySelectorAll(".menu-link")
        .forEach((buton) => {
            const roller =
                buton.dataset.roller
                    ?.split(",")
                    .map((deger) =>
                        deger.trim()
                    );

            const izinli =
                roller?.includes(rol);

            buton.classList.toggle(
                "gizli",
                !izinli
            );
        });
}

function gorunumGoster(gorunumId) {
    document
        .querySelectorAll(".gorunum")
        .forEach((gorunum) => {
            gorunum.classList.add(
                "gizli"
            );
        });

    element(gorunumId)
        ?.classList
        .remove("gizli");

    document
        .querySelectorAll(".menu-link")
        .forEach((buton) => {
            buton.classList.toggle(
                "aktif",
                buton.dataset.gorunum ===
                    gorunumId
            );
        });

    const menuButonu =
        document.querySelector(
            `.menu-link[data-gorunum="${gorunumId}"]`
        );

    if (menuButonu) {
        element(
            "sayfa-basligi"
        ).textContent =
            menuButonu.textContent.trim();
    }
}

async function menuTiklandi(event) {
    const buton =
        event.target.closest(
            ".menu-link"
        );

    if (!buton) {
        return;
    }

    const gorunum =
        buton.dataset.gorunum;

    gorunumGoster(gorunum);

    if (
        gorunum ===
            "gorunum-ogrenciler" &&
        durum.kullanici.rol === "admin"
    ) {
        await ogrencileriYukle();
    }

    if (
        gorunum ===
            "gorunum-ogretmenler" &&
        durum.kullanici.rol === "admin"
    ) {
        await ogretmenleriYukle();
    }

    if (
        gorunum ===
            "gorunum-dersler" &&
        durum.kullanici.rol === "admin"
    ) {
        await dersleriYukle();
    }

    if (
        gorunum ===
            "gorunum-atamalar" &&
        durum.kullanici.rol === "admin"
    ) {
        await adminVerileriniYukle();
    }

    if (
        gorunum ===
            "gorunum-ogretmen-paneli" &&
        durum.kullanici.rol ===
            "ogretmen"
    ) {
        await Promise.all([
            ogretmenDersleriniYukle(),
            ogretmenSinavlariniYukle()
        ]);
    }

    if (
        gorunum ===
            "gorunum-ogrenci-paneli" &&
        durum.kullanici.rol ===
            "ogrenci"
    ) {
        await Promise.all([
            ogrenciDersleriniYukle(),
            ogrenciSinavlariniYukle(),
            ogrenciNotlariniYukle()
        ]);
    }
}


/* =====================================================
   ADMIN VERİLERİ
===================================================== */

async function adminVerileriniYukle() {
    try {
        await Promise.all([
            ogrencileriYukle(),
            ogretmenleriYukle(),
            dersleriYukle()
        ]);

        atamaSecenekleriniDoldur();

        element(
            "istatistik-ogrenci"
        ).textContent =
            durum.ogrenciler.length;

        element(
            "istatistik-ogretmen"
        ).textContent =
            durum.ogretmenler.length;

        element(
            "istatistik-ders"
        ).textContent =
            durum.dersler.length;
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}


/* =====================================================
   ÖĞRENCİLER
===================================================== */

async function ogrencileriYukle() {
    const veri = await apiIstek(
        "/api/ogrenciler"
    );

    durum.ogrenciler =
        listeyiAl(
            veri,
            "ogrenciler"
        );

    ogrencileriYaz();
}

function ogrencileriYaz() {
    const tablo =
        element("ogrenci-tablosu");

    if (!tablo) {
        return;
    }

    if (
        durum.ogrenciler.length === 0
    ) {
        tablo.innerHTML = `
            <tr>
                <td
                    colspan="4"
                    class="bos-liste"
                >
                    Aktif öğrenci bulunmuyor.
                </td>
            </tr>
        `;

        return;
    }

    tablo.innerHTML =
        durum.ogrenciler
            .map((ogrenci) => `
                <tr>
                    <td>
                        ${ogrenci.id}
                    </td>

                    <td>
                        ${htmlGuvenli(
                            ogrenci.ad
                        )}
                        ${htmlGuvenli(
                            ogrenci.soyad
                        )}
                    </td>

                    <td>
                        ${htmlGuvenli(
                            ogrenci.ogrenci_no
                        )}
                    </td>

                    <td>
                        <div class="tablo-islemleri">

                            <button
                                class="tablo-buton duzenle"
                                type="button"
                                data-islem="duzenle"
                                data-id="${ogrenci.id}"
                            >
                                Düzenle
                            </button>

                            <button
                                class="tablo-buton sil"
                                type="button"
                                data-islem="sil"
                                data-id="${ogrenci.id}"
                            >
                                Pasife Al
                            </button>

                        </div>
                    </td>
                </tr>
            `)
            .join("");
}

async function ogrenciOlustur(event) {
    event.preventDefault();

    const veri = {
        ad:
            element("ogrenci-ad")
                .value
                .trim(),

        soyad:
            element("ogrenci-soyad")
                .value
                .trim(),

        ogrenci_no:
            element("ogrenci-no")
                .value
                .trim(),

        kullanici_adi:
            element(
                "ogrenci-kullanici-adi"
            )
                .value
                .trim(),

        email:
            element("ogrenci-email")
                .value
                .trim(),

        sifre:
            element("ogrenci-sifre")
                .value
    };

    try {
        const cevap = await apiIstek(
            "/api/ogrenciler",
            {
                method: "POST",
                body: JSON.stringify(veri)
            }
        );

        event.target.reset();

        bildirimGoster(
            cevap.mesaj ||
                "Öğrenci oluşturuldu",
            "basarili"
        );

        await ogrencileriYukle();

        atamaSecenekleriniDoldur();

        element(
            "istatistik-ogrenci"
        ).textContent =
            durum.ogrenciler.length;
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

function ogrenciTablosuTiklandi(event) {
    const buton =
        event.target.closest(
            "button[data-islem]"
        );

    if (!buton) {
        return;
    }

    const ogrenciId =
        Number(buton.dataset.id);

    const islem =
        buton.dataset.islem;

    if (islem === "duzenle") {
        ogrenciModaliniAc(
            ogrenciId
        );
    }

    if (islem === "sil") {
        ogrenciyiPasifeAl(
            ogrenciId
        );
    }
}

function ogrenciModaliniAc(
    ogrenciId
) {
    const ogrenci =
        durum.ogrenciler.find(
            (kayit) =>
                kayit.id === ogrenciId
        );

    if (!ogrenci) {
        bildirimGoster(
            "Öğrenci bulunamadı",
            "hata"
        );

        return;
    }

    element(
        "guncelleme-ogrenci-id"
    ).value = ogrenci.id;

    element(
        "guncelleme-ogrenci-ad"
    ).value = ogrenci.ad;

    element(
        "guncelleme-ogrenci-soyad"
    ).value = ogrenci.soyad;

    element(
        "guncelleme-ogrenci-no"
    ).value = ogrenci.ogrenci_no;

    element("ogrenci-modal")
        .classList
        .remove("gizli");
}

function ogrenciModaliniKapat() {
    element("ogrenci-modal")
        ?.classList
        .add("gizli");
}

async function ogrenciGuncelle(event) {
    event.preventDefault();

    const ogrenciId =
        Number(
            element(
                "guncelleme-ogrenci-id"
            ).value
        );

    const veri = {
        ad:
            element(
                "guncelleme-ogrenci-ad"
            )
                .value
                .trim(),

        soyad:
            element(
                "guncelleme-ogrenci-soyad"
            )
                .value
                .trim(),

        ogrenci_no:
            element(
                "guncelleme-ogrenci-no"
            )
                .value
                .trim()
    };

    try {
        const cevap = await apiIstek(
            `/api/ogrenciler/${ogrenciId}`,
            {
                method: "PUT",
                body: JSON.stringify(veri)
            }
        );

        ogrenciModaliniKapat();

        bildirimGoster(
            cevap.mesaj ||
                "Öğrenci güncellendi",
            "basarili"
        );

        await ogrencileriYukle();

        atamaSecenekleriniDoldur();
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

async function ogrenciyiPasifeAl(
    ogrenciId
) {
    const onay = window.confirm(
        "Bu öğrenci pasif duruma getirilecek. Devam edilsin mi?"
    );

    if (!onay) {
        return;
    }

    try {
        const cevap = await apiIstek(
            `/api/ogrenciler/${ogrenciId}`,
            {
                method: "DELETE"
            }
        );

        bildirimGoster(
            cevap.mesaj ||
                "Öğrenci pasif duruma getirildi",
            "basarili"
        );

        await ogrencileriYukle();

        atamaSecenekleriniDoldur();

        element(
            "istatistik-ogrenci"
        ).textContent =
            durum.ogrenciler.length;
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}


/* =====================================================
   ÖĞRETMENLER
===================================================== */

async function ogretmenleriYukle() {
    const veri = await apiIstek(
        "/api/ogretmenler"
    );

    durum.ogretmenler =
        listeyiAl(
            veri,
            "ogretmenler"
        );

    ogretmenleriYaz();
}

function ogretmenleriYaz() {
    const tablo =
        element("ogretmen-tablosu");

    if (!tablo) {
        return;
    }

    if (
        durum.ogretmenler.length === 0
    ) {
        tablo.innerHTML = `
            <tr>
                <td
                    colspan="4"
                    class="bos-liste"
                >
                    Aktif öğretmen bulunmuyor.
                </td>
            </tr>
        `;

        return;
    }

    tablo.innerHTML =
        durum.ogretmenler
            .map((ogretmen) => `
                <tr>
                    <td>
                        ${ogretmen.id}
                    </td>

                    <td>
                        ${htmlGuvenli(
                            ogretmen.ad
                        )}
                        ${htmlGuvenli(
                            ogretmen.soyad
                        )}
                    </td>

                    <td>
                        ${htmlGuvenli(
                            ogretmen.sicil_no
                        )}
                    </td>

                    <td>
                        ${htmlGuvenli(
                            ogretmen.brans
                        )}
                    </td>
                </tr>
            `)
            .join("");
}

async function ogretmenOlustur(event) {
    event.preventDefault();

    const veri = {
        ad:
            element("ogretmen-ad")
                .value
                .trim(),

        soyad:
            element("ogretmen-soyad")
                .value
                .trim(),

        sicil_no:
            element(
                "ogretmen-sicil-no"
            )
                .value
                .trim(),

        brans:
            element("ogretmen-brans")
                .value
                .trim(),

        kullanici_adi:
            element(
                "ogretmen-kullanici-adi"
            )
                .value
                .trim(),

        email:
            element("ogretmen-email")
                .value
                .trim(),

        sifre:
            element("ogretmen-sifre")
                .value
    };

    try {
        const cevap = await apiIstek(
            "/api/ogretmenler",
            {
                method: "POST",
                body: JSON.stringify(veri)
            }
        );

        event.target.reset();

        bildirimGoster(
            cevap.mesaj ||
                "Öğretmen oluşturuldu",
            "basarili"
        );

        await ogretmenleriYukle();

        atamaSecenekleriniDoldur();

        element(
            "istatistik-ogretmen"
        ).textContent =
            durum.ogretmenler.length;
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}


/* =====================================================
   DERSLER
===================================================== */

async function dersleriYukle() {
    const veri = await apiIstek(
        "/api/dersler"
    );

    durum.dersler =
        listeyiAl(
            veri,
            "dersler"
        );

    dersleriYaz();
}

function dersleriYaz() {
    const tablo =
        element("ders-tablosu");

    if (!tablo) {
        return;
    }

    if (
        durum.dersler.length === 0
    ) {
        tablo.innerHTML = `
            <tr>
                <td
                    colspan="4"
                    class="bos-liste"
                >
                    Aktif ders bulunmuyor.
                </td>
            </tr>
        `;

        return;
    }

    tablo.innerHTML =
        durum.dersler
            .map((ders) => `
                <tr>
                    <td>
                        ${ders.id}
                    </td>

                    <td>
                        ${htmlGuvenli(
                            ders.ders_kodu
                        )}
                    </td>

                    <td>
                        ${htmlGuvenli(
                            ders.ders_adi
                        )}
                    </td>

                    <td>
                        ${htmlGuvenli(
                            ders.aciklama ||
                            "—"
                        )}
                    </td>
                </tr>
            `)
            .join("");
}

async function dersOlustur(event) {
    event.preventDefault();

    const veri = {
        ders_kodu:
            element("ders-kodu")
                .value
                .trim(),

        ders_adi:
            element("ders-adi")
                .value
                .trim(),

        aciklama:
            element("ders-aciklama")
                .value
                .trim()
    };

    try {
        const cevap = await apiIstek(
            "/api/dersler",
            {
                method: "POST",
                body: JSON.stringify(veri)
            }
        );

        event.target.reset();

        bildirimGoster(
            cevap.mesaj ||
                "Ders oluşturuldu",
            "basarili"
        );

        await dersleriYukle();

        atamaSecenekleriniDoldur();

        element(
            "istatistik-ders"
        ).textContent =
            durum.dersler.length;
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}


/* =====================================================
   ADMIN ATAMA İŞLEMLERİ
===================================================== */

function selectDoldur(
    select,
    liste,
    etiketOlustur
) {
    if (!select) {
        return;
    }

    if (liste.length === 0) {
        select.innerHTML = `
            <option value="">
                Kayıt bulunmuyor
            </option>
        `;

        select.disabled = true;

        return;
    }

    select.disabled = false;

    select.innerHTML = `
        <option value="">
            Seçiniz
        </option>

        ${liste
            .map((kayit) => `
                <option value="${kayit.id}">
                    ${htmlGuvenli(
                        etiketOlustur(kayit)
                    )}
                </option>
            `)
            .join("")}
    `;
}

function atamaSecenekleriniDoldur() {
    selectDoldur(
        element("atama-ogretmen"),
        durum.ogretmenler,
        (ogretmen) =>
            `${ogretmen.ad} ${ogretmen.soyad} - ${ogretmen.brans}`
    );

    selectDoldur(
        element("atama-ders"),
        durum.dersler,
        (ders) =>
            `${ders.ders_kodu} - ${ders.ders_adi}`
    );

    selectDoldur(
        element("kayit-ogrenci"),
        durum.ogrenciler,
        (ogrenci) =>
            `${ogrenci.ad} ${ogrenci.soyad} - ${ogrenci.ogrenci_no}`
    );

    selectDoldur(
        element("kayit-ders"),
        durum.dersler,
        (ders) =>
            `${ders.ders_kodu} - ${ders.ders_adi}`
    );
}

async function ogretmeneDersAta(event) {
    event.preventDefault();

    const veri = {
        ogretmen_id:
            Number(
                element(
                    "atama-ogretmen"
                ).value
            ),

        ders_id:
            Number(
                element(
                    "atama-ders"
                ).value
            )
    };

    try {
        const cevap = await apiIstek(
            "/api/ogretmen-ders-atamalari",
            {
                method: "POST",
                body: JSON.stringify(veri)
            }
        );

        bildirimGoster(
            cevap.mesaj ||
                "Ders öğretmene atandı",
            "basarili"
        );

        event.target.reset();
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

async function ogrenciyiDerseKaydet(
    event
) {
    event.preventDefault();

    const veri = {
        ogrenci_id:
            Number(
                element(
                    "kayit-ogrenci"
                ).value
            ),

        ders_id:
            Number(
                element(
                    "kayit-ders"
                ).value
            )
    };

    try {
        const cevap = await apiIstek(
            "/api/ogrenci-ders-kayitlari",
            {
                method: "POST",
                body: JSON.stringify(veri)
            }
        );

        bildirimGoster(
            cevap.mesaj ||
                "Öğrenci derse kaydedildi",
            "basarili"
        );

        event.target.reset();
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}


/* =====================================================
   ÖĞRETMEN PANELİ
===================================================== */

async function ogretmenDersleriniYukle() {
    try {
        const veri = await apiIstek(
            "/api/ogretmen/derslerim"
        );

        durum.ogretmenDersleri =
            veri?.dersler || [];

    ogretmenDersleriniYaz();
    sinavDersleriniDoldur();
    devamsizlikDersleriniDoldur();
    devamsizlikVarsayilanTarihiniAyarla();
        element(
            "istatistik-ogrenci"
        ).textContent = "—";

        element(
            "istatistik-ogretmen"
        ).textContent = "1";

        element(
            "istatistik-ders"
        ).textContent =
            durum.ogretmenDersleri.length;
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

function ogretmenDersleriniYaz() {
    const alan =
        element(
            "ogretmen-ders-listesi"
        );

    if (!alan) {
        return;
    }

    if (
        durum.ogretmenDersleri
            .length === 0
    ) {
        alan.innerHTML = `
            <div class="bos-durum">
                Size atanmış aktif bir ders bulunmuyor.
            </div>
        `;

        return;
    }

    alan.innerHTML =
        durum.ogretmenDersleri
            .map((ders) => `
                <article class="ders-karti">

                    <div class="ders-karti-sol">

                        <span class="ders-kodu">
                            ${htmlGuvenli(
                                ders.ders_kodu
                            )}
                        </span>

                        <div>
                            <h4>
                                ${htmlGuvenli(
                                    ders.ders_adi
                                )}
                            </h4>

                            <p>
                                ${htmlGuvenli(
                                    ders.aciklama ||
                                    "Açıklama bulunmuyor"
                                )}
                            </p>
                        </div>

                    </div>

                    <span class="ders-karti-tarih">
                        ${tarihYaz(
                            ders.atama_tarihi
                        )}
                    </span>

                </article>
            `)
            .join("");
}

function sinavDersleriniDoldur() {
    selectDoldur(
        element("sinav-ders"),
        durum.ogretmenDersleri,
        (ders) =>
            `${ders.ders_kodu} - ${ders.ders_adi}`
    );
}

async function ogretmenSinavlariniYukle() {
    try {
        const veri = await apiIstek(
            "/api/ogretmen/sinavlarim"
        );

        durum.ogretmenSinavlari =
            veri?.sinavlar || [];

        ogretmenSinavlariniYaz();
        notSinavSecenekleriniDoldur();
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

function ogretmenSinavlariniYaz() {
    const alan =
        element(
            "ogretmen-sinav-listesi"
        );

    if (!alan) {
        return;
    }

    if (
        durum.ogretmenSinavlari
            .length === 0
    ) {
        alan.innerHTML = `
            <div class="bos-durum">
                Henüz oluşturduğunuz bir sınav bulunmuyor.
            </div>
        `;

        return;
    }

    alan.innerHTML =
        durum.ogretmenSinavlari
            .map((sinav) => `
                <article class="sinav-karti">

                    <div class="sinav-karti-sol">

                        <div class="sinav-ikon">
                            📝
                        </div>

                        <div>
                            <h4>
                                ${htmlGuvenli(
                                    sinav.sinav_adi
                                )}
                            </h4>

                            <p>
                                ${htmlGuvenli(
                                    sinav.ders
                                        ?.ders_kodu ||
                                    ""
                                )}
                                ·
                                ${htmlGuvenli(
                                    sinav.ders
                                        ?.ders_adi ||
                                    ""
                                )}
                            </p>

                            <p>
                                ${htmlGuvenli(
                                    sinav.aciklama ||
                                    "Açıklama bulunmuyor"
                                )}
                            </p>
                        </div>

                    </div>

                    <div class="sinav-karti-sag">

                        <strong>
                            ${sinav.maksimum_puan}
                            Puan
                        </strong>

                        <span>
                            ${tarihYaz(
                                sinav.sinav_tarihi
                            )}
                        </span>

                    </div>

                </article>
            `)
            .join("");
}

function notSinavSecenekleriniDoldur() {
    const select =
        element("not-sinav-secimi");

    if (!select) {
        return;
    }

    const seciliDeger =
        select.value;

    selectDoldur(
        select,
        durum.ogretmenSinavlari,
        (sinav) => {
            const dersKodu =
                sinav.ders?.ders_kodu ||
                "";

            const dersAdi =
                sinav.ders?.ders_adi ||
                "";

            return (
                `${dersKodu} - ${dersAdi}` +
                ` / ${sinav.sinav_adi}`
            );
        }
    );

    if (
        seciliDeger &&
        durum.ogretmenSinavlari.some(
            (sinav) =>
                String(sinav.id) ===
                seciliDeger
        )
    ) {
        select.value =
            seciliDeger;
    }
}

async function sinavOlustur(event) {
    event.preventDefault();

    const tarihDegeri =
        element("sinav-tarihi").value;

    const tarih =
        tarihDegeri
            ? new Date(tarihDegeri)
            : null;

    const veri = {
        ders_id:
            Number(
                element("sinav-ders")
                    .value
            ),

        sinav_adi:
            element("sinav-adi")
                .value
                .trim(),

        sinav_tarihi:
            tarih
                ? tarih.toISOString()
                : "",

        maksimum_puan:
            Number(
                element("sinav-puan")
                    .value
            ),

        aciklama:
            element("sinav-aciklama")
                .value
                .trim()
    };

    try {
        const cevap = await apiIstek(
            "/api/ogretmen/sinavlar",
            {
                method: "POST",
                body: JSON.stringify(veri)
            }
        );

        const sinav =
            cevap.sinav;

        const sonucAlani =
            element(
                "son-olusturulan-sinav"
            );

        sonucAlani.innerHTML = `
            <strong>
                ${htmlGuvenli(
                    cevap.mesaj ||
                    "Sınav oluşturuldu"
                )}
            </strong>

            <span>
                ${htmlGuvenli(
                    sinav?.sinav_adi ||
                    ""
                )}
                ·
                ${tarihYaz(
                    sinav?.sinav_tarihi
                )}
            </span>
        `;

        sonucAlani.classList.remove(
            "gizli"
        );

        event.target.reset();

        element("sinav-puan").value =
            100;

        sinavDersleriniDoldur();

        await ogretmenSinavlariniYukle();

        bildirimGoster(
            cevap.mesaj ||
                "Sınav oluşturuldu",
            "basarili"
        );
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}


/* =====================================================
   SINAV ÖĞRENCİLERİ VE NOT GİRİŞİ
===================================================== */

async function sinavOgrencileriniYukle() {
    const sinavId =
        Number(
            element(
                "not-sinav-secimi"
            ).value
        );

    if (
        !Number.isInteger(sinavId) ||
        sinavId <= 0
    ) {
        bildirimGoster(
            "Önce bir sınav seçin",
            "uyari"
        );

        return;
    }

    try {
        const veri = await apiIstek(
            `/api/ogretmen/sinavlar/${sinavId}/ogrenciler`
        );

        durum.seciliNotSinavi =
            veri.sinav;

        durum
            .ogretmenSinavOgrencileri =
            veri.ogrenciler || [];

        sinavOgrencileriniYaz();

        element("not-giris-alani")
            .classList
            .remove("gizli");
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

function sinavOgrencileriniYaz() {
    const tablo =
        element(
            "not-ogrenci-tablosu"
        );

    const sinav =
        durum.seciliNotSinavi;

    if (!tablo || !sinav) {
        return;
    }

    element(
        "not-sinav-basligi"
    ).textContent =
        `${sinav.ders.ders_kodu} - ` +
        `${sinav.sinav_adi}`;

    element(
        "not-sinav-puani"
    ).textContent =
        `Maksimum ${sinav.maksimum_puan} puan`;

    if (
        durum
            .ogretmenSinavOgrencileri
            .length === 0
    ) {
        tablo.innerHTML = `
            <tr>
                <td
                    colspan="5"
                    class="bos-liste"
                >
                    Bu ders için kayıtlı aktif öğrenci bulunmuyor.
                </td>
            </tr>
        `;

        return;
    }

    tablo.innerHTML =
        durum
            .ogretmenSinavOgrencileri
            .map((ogrenci) => {
                const mevcutPuan =
                    ogrenci.not?.puan ??
                    "";

                const mevcutAciklama =
                    ogrenci.not?.aciklama ??
                    "";

                return `
                    <tr
                        data-ogrenci-id="${ogrenci.id}"
                    >

                        <td>
                            ${htmlGuvenli(
                                ogrenci.ad
                            )}
                            ${htmlGuvenli(
                                ogrenci.soyad
                            )}
                        </td>

                        <td>
                            ${htmlGuvenli(
                                ogrenci.ogrenci_no
                            )}
                        </td>

                        <td>
                            <input
                                class="not-puan-input"
                                data-not-alani="puan"
                                type="number"
                                min="0"
                                max="${sinav.maksimum_puan}"
                                step="0.01"
                                value="${mevcutPuan}"
                                placeholder="Puan"
                            >
                        </td>

                        <td>
                            <input
                                class="not-aciklama-input"
                                data-not-alani="aciklama"
                                type="text"
                                maxlength="500"
                                value="${htmlGuvenli(
                                    mevcutAciklama
                                )}"
                                placeholder="Açıklama"
                            >
                        </td>

                        <td>
                            <button
                                class="tablo-buton duzenle"
                                type="button"
                                data-not-kaydet
                            >
                                ${
                                    ogrenci.not
                                        ? "Güncelle"
                                        : "Kaydet"
                                }
                            </button>
                        </td>

                    </tr>
                `;
            })
            .join("");
}

async function notTablosuTiklandi(
    event
) {
    const buton =
        event.target.closest(
            "button[data-not-kaydet]"
        );

    if (!buton) {
        return;
    }

    const satir =
        buton.closest(
            "tr[data-ogrenci-id]"
        );

    if (!satir) {
        return;
    }

    const ogrenciId =
        Number(
            satir.dataset.ogrenciId
        );

    const puanInput =
        satir.querySelector(
            '[data-not-alani="puan"]'
        );

    const aciklamaInput =
        satir.querySelector(
            '[data-not-alani="aciklama"]'
        );

    const puan =
        Number(puanInput.value);

    const maksimumPuan =
        Number(
            durum
                .seciliNotSinavi
                ?.maksimum_puan
        );

    if (
        puanInput.value.trim() === "" ||
        !Number.isFinite(puan)
    ) {
        bildirimGoster(
            "Geçerli bir puan girin",
            "uyari"
        );

        return;
    }

    if (
        puan < 0 ||
        puan > maksimumPuan
    ) {
        bildirimGoster(
            `Puan 0 ile ${maksimumPuan} arasında olmalıdır`,
            "uyari"
        );

        return;
    }

    buton.disabled = true;
    buton.textContent =
        "Kaydediliyor...";

    try {
        const cevap = await apiIstek(
            `/api/ogretmen/sinavlar/${durum.seciliNotSinavi.id}/notlar`,
            {
                method: "POST",

                body: JSON.stringify({
                    ogrenci_id:
                        ogrenciId,

                    puan,

                    aciklama:
                        aciklamaInput
                            .value
                            .trim()
                })
            }
        );

        bildirimGoster(
            cevap.mesaj ||
                "Not kaydedildi",
            "basarili"
        );

        await sinavOgrencileriniYukle();
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );

        buton.disabled = false;
        buton.textContent = "Kaydet";
    }
}


/* =====================================================
   ÖĞRENCİ PANELİ
===================================================== */

async function ogrenciDersleriniYukle() {
    try {
        const veri = await apiIstek(
            "/api/ogrenci/derslerim"
        );

        durum.ogrenciDersleri =
            veri?.dersler || [];

        ogrenciDersleriniYaz();

        element(
            "istatistik-ogrenci"
        ).textContent = "1";

        element(
            "istatistik-ogretmen"
        ).textContent = "—";

        element(
            "istatistik-ders"
        ).textContent =
            durum.ogrenciDersleri.length;
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

function ogrenciDersleriniYaz() {
    const alan =
        element(
            "ogrenci-ders-listesi"
        );

    if (!alan) {
        return;
    }

    if (
        durum.ogrenciDersleri
            .length === 0
    ) {
        alan.innerHTML = `
            <div class="bos-durum">
                Kayıtlı olduğunuz aktif bir ders bulunmuyor.
            </div>
        `;

        return;
    }

    alan.innerHTML =
        durum.ogrenciDersleri
            .map((ders) => `
                <article class="ders-karti">

                    <div class="ders-karti-sol">

                        <span class="ders-kodu">
                            ${htmlGuvenli(
                                ders.ders_kodu
                            )}
                        </span>

                        <div>
                            <h4>
                                ${htmlGuvenli(
                                    ders.ders_adi
                                )}
                            </h4>

                            <p>
                                ${htmlGuvenli(
                                    ders.aciklama ||
                                    "Açıklama bulunmuyor"
                                )}
                            </p>
                        </div>

                    </div>

                    <span class="ders-karti-tarih">
                        ${tarihYaz(
                            ders.kayit_tarihi
                        )}
                    </span>

                </article>
            `)
            .join("");
}

async function ogrenciSinavlariniYukle() {
    try {
        const veri = await apiIstek(
            "/api/ogrenci/sinavlarim"
        );

        durum.ogrenciSinavlari =
            veri?.sinavlar || [];

        ogrenciSinavlariniYaz();
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

function ogrenciSinavlariniYaz() {
    const alan =
        element(
            "ogrenci-sinav-listesi"
        );

    if (!alan) {
        return;
    }

    if (
        durum.ogrenciSinavlari
            .length === 0
    ) {
        alan.innerHTML = `
            <div class="bos-durum">
                Görüntülenecek aktif sınav bulunmuyor.
            </div>
        `;

        return;
    }

    alan.innerHTML =
        durum.ogrenciSinavlari
            .map((sinav) => `
                <article class="sinav-karti">

                    <div class="sinav-karti-sol">

                        <div class="sinav-ikon">
                            📅
                        </div>

                        <div>
                            <h4>
                                ${htmlGuvenli(
                                    sinav.sinav_adi
                                )}
                            </h4>

                            <p>
                                ${htmlGuvenli(
                                    sinav.ders
                                        ?.ders_kodu ||
                                    ""
                                )}
                                ·
                                ${htmlGuvenli(
                                    sinav.ders
                                        ?.ders_adi ||
                                    ""
                                )}
                            </p>

                            <p>
                                ${htmlGuvenli(
                                    sinav.aciklama ||
                                    "Açıklama bulunmuyor"
                                )}
                            </p>
                        </div>

                    </div>

                    <div class="sinav-karti-sag">

                        <strong>
                            ${sinav.maksimum_puan}
                            Puan
                        </strong>

                        <span>
                            ${tarihYaz(
                                sinav.sinav_tarihi
                            )}
                        </span>

                    </div>

                </article>
            `)
            .join("");
}

async function ogrenciNotlariniYukle() {
    try {
        const veri = await apiIstek(
            "/api/ogrenci/notlarim"
        );

        durum.ogrenciNotlari =
            veri?.notlar || [];

        ogrenciNotlariniYaz();
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

function ogrenciNotlariniYaz() {
    const alan =
        element(
            "ogrenci-not-listesi"
        );

    if (!alan) {
        return;
    }

    if (
        durum.ogrenciNotlari
            .length === 0
    ) {
        alan.innerHTML = `
            <div class="bos-durum">
                Henüz açıklanmış bir notunuz bulunmuyor.
            </div>
        `;

        return;
    }

    alan.innerHTML =
        durum.ogrenciNotlari
            .map((notKaydi) => {
                const maksimumPuan =
                    Number(
                        notKaydi
                            .sinav
                            .maksimum_puan
                    );

                const yuzde =
                    maksimumPuan > 0
                        ? (
                            Number(
                                notKaydi.puan
                            ) /
                            maksimumPuan *
                            100
                        ).toFixed(1)
                        : "0.0";

                return `
                    <article class="not-karti">

                        <div class="not-karti-sol">

                            <div class="not-karti-ikon">
                                📊
                            </div>

                            <div>
                                <h4>
                                    ${htmlGuvenli(
                                        notKaydi
                                            .sinav
                                            .sinav_adi
                                    )}
                                </h4>

                                <p>
                                    ${htmlGuvenli(
                                        notKaydi
                                            .ders
                                            .ders_kodu
                                    )}
                                    ·
                                    ${htmlGuvenli(
                                        notKaydi
                                            .ders
                                            .ders_adi
                                    )}
                                </p>

                                <p>
                                    Öğretmen:
                                    ${htmlGuvenli(
                                        notKaydi
                                            .ogretmen
                                            .ad
                                    )}
                                    ${htmlGuvenli(
                                        notKaydi
                                            .ogretmen
                                            .soyad
                                    )}
                                </p>

                                <p>
                                    ${htmlGuvenli(
                                        notKaydi
                                            .aciklama ||
                                        "Açıklama bulunmuyor"
                                    )}
                                </p>
                            </div>

                        </div>

                        <div class="not-karti-sag">

                            <span class="not-puan">
                                ${notKaydi.puan}
                                /
                                ${notKaydi
                                    .sinav
                                    .maksimum_puan}
                            </span>

                            <span class="not-yuzde">
                                Başarı oranı:
                                %${yuzde}
                            </span>

                        </div>

                    </article>
                `;
            })
            .join("");
}

/* =====================================================
   DEVAMSIZLIK YARDIMCI FONKSİYONLARI
===================================================== */

function bugununYerelTarihi() {
    const simdi = new Date();

    const yil =
        simdi.getFullYear();

    const ay =
        String(
            simdi.getMonth() + 1
        ).padStart(2, "0");

    const gun =
        String(
            simdi.getDate()
        ).padStart(2, "0");

    return `${yil}-${ay}-${gun}`;
}

function sadeceTarihYaz(tarih) {
    if (!tarih) {
        return "—";
    }

    const tarihNesnesi =
        new Date(tarih);

    if (
        Number.isNaN(
            tarihNesnesi.getTime()
        )
    ) {
        return "—";
    }

    return tarihNesnesi.toLocaleDateString(
        "tr-TR"
    );
}

function devamsizlikDurumAdi(durumDegeri) {
    const durumlar = {
        geldi: "Geldi",
        gelmedi: "Gelmedi",
        gec_kaldi: "Geç Kaldı",
        izinli: "İzinli"
    };

    return (
        durumlar[durumDegeri] ||
        durumDegeri ||
        "Bilinmiyor"
    );
}

function devamsizlikDurumSinifi(
    durumDegeri
) {
    if (
        durumDegeri === "gec_kaldi"
    ) {
        return "gec-kaldi";
    }

    return durumDegeri;
}

function devamsizlikDurumIkonu(
    durumDegeri
) {
    const ikonlar = {
        geldi: "✅",
        gelmedi: "❌",
        gec_kaldi: "⏰",
        izinli: "📄"
    };

    return (
        ikonlar[durumDegeri] ||
        "📋"
    );
}


/* =====================================================
   ÖĞRETMEN DEVAMSIZLIK İŞLEMLERİ
===================================================== */

function devamsizlikDersleriniDoldur() {
    selectDoldur(
        element(
            "devamsizlik-ders-secimi"
        ),
        durum.ogretmenDersleri,
        (ders) =>
            `${ders.ders_kodu} - ${ders.ders_adi}`
    );
}

function devamsizlikVarsayilanTarihiniAyarla() {
    const tarihInput =
        element("devamsizlik-tarihi");

    if (
        tarihInput &&
        !tarihInput.value
    ) {
        tarihInput.value =
            bugununYerelTarihi();
    }
}

function devamsizlikSecimiDegisti() {
    durum
        .ogretmenDevamsizlikOgrencileri = [];

    durum.seciliDevamsizlikDersi = null;
    durum.seciliDevamsizlikTarihi = "";

    element("devamsizlik-giris-alani")
        ?.classList
        .add("gizli");
}

async function devamsizlikOgrencileriniYukle() {
    const dersId =
        Number(
            element(
                "devamsizlik-ders-secimi"
            ).value
        );

    const tarih =
        element(
            "devamsizlik-tarihi"
        ).value;

    if (
        !Number.isInteger(dersId) ||
        dersId <= 0
    ) {
        bildirimGoster(
            "Önce bir ders seçin",
            "uyari"
        );

        return;
    }

    if (!tarih) {
        bildirimGoster(
            "Yoklama tarihini seçin",
            "uyari"
        );

        return;
    }

    const buton =
        element(
            "devamsizlik-ogrencilerini-getir"
        );

    buton.disabled = true;
    buton.textContent =
        "Öğrenciler getiriliyor...";

    try {
        const veri = await apiIstek(
            `/api/ogretmen/dersler/${dersId}/devamsizliklar?tarih=${encodeURIComponent(tarih)}`
        );

        durum.seciliDevamsizlikDersi =
            veri.ders;

        durum.seciliDevamsizlikTarihi =
            veri.devamsizlik_tarihi;

        durum
            .ogretmenDevamsizlikOgrencileri =
            veri.ogrenciler || [];

        devamsizlikOgrencileriniYaz();

        element(
            "devamsizlik-giris-alani"
        )
            .classList
            .remove("gizli");
    } catch (hata) {
        element(
            "devamsizlik-giris-alani"
        )
            ?.classList
            .add("gizli");

        bildirimGoster(
            hata.message,
            "hata"
        );
    } finally {
        buton.disabled = false;
        buton.textContent =
            "Öğrencileri Getir";
    }
}

function devamsizlikOgrencileriniYaz() {
    const tablo =
        element(
            "devamsizlik-ogrenci-tablosu"
        );

    const ders =
        durum.seciliDevamsizlikDersi;

    if (!tablo || !ders) {
        return;
    }

    element(
        "devamsizlik-ders-basligi"
    ).textContent =
        `${ders.ders_kodu} - ${ders.ders_adi}`;

    element(
        "devamsizlik-secili-tarih"
    ).textContent =
        sadeceTarihYaz(
            durum.seciliDevamsizlikTarihi
        );

    if (
        durum
            .ogretmenDevamsizlikOgrencileri
            .length === 0
    ) {
        tablo.innerHTML = `
            <tr>
                <td
                    colspan="4"
                    class="bos-liste"
                >
                    Bu derse kayıtlı aktif öğrenci bulunmuyor.
                </td>
            </tr>
        `;

        return;
    }

    tablo.innerHTML =
        durum
            .ogretmenDevamsizlikOgrencileri
            .map((ogrenci) => {
                const mevcutDurum =
                    ogrenci.devamsizlik
                        ?.durum ||
                    "geldi";

                const mevcutAciklama =
                    ogrenci.devamsizlik
                        ?.aciklama ||
                    "";

                return `
                    <tr
                        data-devamsizlik-ogrenci-id="${ogrenci.id}"
                    >
                        <td>
                            ${htmlGuvenli(
                                ogrenci.ad
                            )}
                            ${htmlGuvenli(
                                ogrenci.soyad
                            )}
                        </td>

                        <td>
                            ${htmlGuvenli(
                                ogrenci.ogrenci_no
                            )}
                        </td>

                        <td>
                            <select
                                class="devamsizlik-durum-secimi"
                                data-devamsizlik-alani="durum"
                            >
                                <option
                                    value="geldi"
                                    ${
                                        mevcutDurum ===
                                        "geldi"
                                            ? "selected"
                                            : ""
                                    }
                                >
                                    Geldi
                                </option>

                                <option
                                    value="gelmedi"
                                    ${
                                        mevcutDurum ===
                                        "gelmedi"
                                            ? "selected"
                                            : ""
                                    }
                                >
                                    Gelmedi
                                </option>

                                <option
                                    value="gec_kaldi"
                                    ${
                                        mevcutDurum ===
                                        "gec_kaldi"
                                            ? "selected"
                                            : ""
                                    }
                                >
                                    Geç Kaldı
                                </option>

                                <option
                                    value="izinli"
                                    ${
                                        mevcutDurum ===
                                        "izinli"
                                            ? "selected"
                                            : ""
                                    }
                                >
                                    İzinli
                                </option>
                            </select>
                        </td>

                        <td>
                            <input
                                class="devamsizlik-aciklama-input"
                                data-devamsizlik-alani="aciklama"
                                type="text"
                                maxlength="500"
                                value="${htmlGuvenli(
                                    mevcutAciklama
                                )}"
                                placeholder="Açıklama"
                            >
                        </td>
                    </tr>
                `;
            })
            .join("");
}

async function devamsizliklariKaydet() {
    const dersId =
        Number(
            durum
                .seciliDevamsizlikDersi
                ?.id
        );

    const tarih =
        durum.seciliDevamsizlikTarihi;

    const satirlar =
        document.querySelectorAll(
            "#devamsizlik-ogrenci-tablosu tr[data-devamsizlik-ogrenci-id]"
        );

    if (
        !Number.isInteger(dersId) ||
        dersId <= 0 ||
        !tarih
    ) {
        bildirimGoster(
            "Önce ders ve tarih seçerek öğrencileri getirin",
            "uyari"
        );

        return;
    }

    if (satirlar.length === 0) {
        bildirimGoster(
            "Kaydedilecek öğrenci bulunmuyor",
            "uyari"
        );

        return;
    }

    const kayitlar =
        Array.from(satirlar)
            .map((satir) => {
                const durumSecimi =
                    satir.querySelector(
                        '[data-devamsizlik-alani="durum"]'
                    );

                const aciklamaInput =
                    satir.querySelector(
                        '[data-devamsizlik-alani="aciklama"]'
                    );

                return {
                    ogrenci_id:
                        Number(
                            satir.dataset
                                .devamsizlikOgrenciId
                        ),

                    durum:
                        durumSecimi.value,

                    aciklama:
                        aciklamaInput
                            .value
                            .trim()
                };
            });

    const buton =
        element(
            "devamsizliklari-kaydet"
        );

    buton.disabled = true;
    buton.textContent =
        "Kaydediliyor...";

    try {
        const cevap = await apiIstek(
            `/api/ogretmen/dersler/${dersId}/devamsizliklar`,
            {
                method: "POST",

                body: JSON.stringify({
                    devamsizlik_tarihi:
                        tarih,

                    kayitlar
                })
            }
        );

        bildirimGoster(
            cevap.mesaj ||
            "Devamsızlıklar kaydedildi",
            "basarili"
        );

        await devamsizlikOgrencileriniYukle();
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    } finally {
        buton.disabled = false;
        buton.textContent =
            "Devamsızlıkları Kaydet";
    }
}


/* =====================================================
   ÖĞRENCİ DEVAMSIZLIK EKRANI
===================================================== */

async function ogrenciDevamsizliklariniYukle() {
    try {
        const veri = await apiIstek(
            "/api/ogrenci/devamsizliklarim"
        );

        durum.ogrenciDevamsizliklari =
            veri?.devamsizliklar ||
            [];

        durum.ogrenciDevamsizlikOzeti =
            veri?.ozet || {
                toplam: 0,
                geldi: 0,
                gelmedi: 0,
                gec_kaldi: 0,
                izinli: 0
            };

        ogrenciDevamsizliklariniYaz();
    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

function ogrenciDevamsizliklariniYaz() {
    const alan =
        element(
            "ogrenci-devamsizlik-listesi"
        );

    const ozet =
        durum
            .ogrenciDevamsizlikOzeti;

    element(
        "devamsizlik-toplam"
    ).textContent =
        ozet.toplam ?? 0;

    element(
        "devamsizlik-geldi"
    ).textContent =
        ozet.geldi ?? 0;

    element(
        "devamsizlik-gelmedi"
    ).textContent =
        ozet.gelmedi ?? 0;

    element(
        "devamsizlik-gec-kaldi"
    ).textContent =
        ozet.gec_kaldi ?? 0;

    element(
        "devamsizlik-izinli"
    ).textContent =
        ozet.izinli ?? 0;

    if (!alan) {
        return;
    }

    if (
        durum
            .ogrenciDevamsizliklari
            .length === 0
    ) {
        alan.innerHTML = `
            <div class="bos-durum">
                Henüz devamsızlık kaydınız bulunmuyor.
            </div>
        `;

        return;
    }

    alan.innerHTML =
        durum
            .ogrenciDevamsizliklari
            .map((kayit) => {
                const durumSinifi =
                    devamsizlikDurumSinifi(
                        kayit.durum
                    );

                return `
                    <article class="devamsizlik-karti">

                        <div class="devamsizlik-karti-sol">

                            <div class="devamsizlik-karti-ikon">
                                ${devamsizlikDurumIkonu(
                                    kayit.durum
                                )}
                            </div>

                            <div>
                                <h4>
                                    ${htmlGuvenli(
                                        kayit.ders
                                            ?.ders_kodu ||
                                        ""
                                    )}
                                    ·
                                    ${htmlGuvenli(
                                        kayit.ders
                                            ?.ders_adi ||
                                        ""
                                    )}
                                </h4>

                                <p>
                                    Öğretmen:
                                    ${htmlGuvenli(
                                        kayit.ogretmen
                                            ?.ad ||
                                        ""
                                    )}
                                    ${htmlGuvenli(
                                        kayit.ogretmen
                                            ?.soyad ||
                                        ""
                                    )}
                                </p>

                                <p>
                                    ${htmlGuvenli(
                                        kayit.aciklama ||
                                        "Açıklama bulunmuyor"
                                    )}
                                </p>
                            </div>

                        </div>

                        <div class="devamsizlik-karti-sag">

                            <span
                                class="devamsizlik-durum-etiketi ${durumSinifi}"
                            >
                                ${htmlGuvenli(
                                    devamsizlikDurumAdi(
                                        kayit.durum
                                    )
                                )}
                            </span>

                            <span class="devamsizlik-tarih">
                                ${sadeceTarihYaz(
                                    kayit.devamsizlik_tarihi
                                )}
                            </span>

                        </div>

                    </article>
                `;
            })
            .join("");
}

/* =====================================================
   OLAY DİNLEYİCİLERİ
===================================================== */

function olaylariBagla() {
    element("giris-formu")
        ?.addEventListener(
            "submit",
            girisYap
        );

    element("cikis-butonu")
        ?.addEventListener(
            "click",
            () => oturumuKapat(true)
        );

    element("menu")
        ?.addEventListener(
            "click",
            menuTiklandi
        );

    element("ogrenci-formu")
        ?.addEventListener(
            "submit",
            ogrenciOlustur
        );

    element("ogrencileri-yenile")
        ?.addEventListener(
            "click",
            ogrencileriYukle
        );

    element("ogrenci-tablosu")
        ?.addEventListener(
            "click",
            ogrenciTablosuTiklandi
        );

    element(
        "ogrenci-guncelleme-formu"
    )
        ?.addEventListener(
            "submit",
            ogrenciGuncelle
        );

    document
        .querySelectorAll(
            "[data-modal-kapat]"
        )
        .forEach((buton) => {
            buton.addEventListener(
                "click",
                ogrenciModaliniKapat
            );
        });

    element("ogretmen-formu")
        ?.addEventListener(
            "submit",
            ogretmenOlustur
        );

    element("ders-formu")
        ?.addEventListener(
            "submit",
            dersOlustur
        );

    element("ogretmen-ders-formu")
        ?.addEventListener(
            "submit",
            ogretmeneDersAta
        );

    element("ogrenci-ders-formu")
        ?.addEventListener(
            "submit",
            ogrenciyiDerseKaydet
        );

    element("sinav-formu")
        ?.addEventListener(
            "submit",
            sinavOlustur
        );

    element(
        "ogretmen-sinavlari-yenile"
    )
        ?.addEventListener(
            "click",
            ogretmenSinavlariniYukle
        );

    element(
        "ogrenci-sinavlari-yenile"
    )
        ?.addEventListener(
            "click",
            ogrenciSinavlariniYukle
        );

    element(
        "sinav-ogrencilerini-getir"
    )
        ?.addEventListener(
            "click",
            sinavOgrencileriniYukle
        );

    element("not-ogrenci-tablosu")
        ?.addEventListener(
            "click",
            notTablosuTiklandi
        );

    element(
        "ogrenci-notlari-yenile"
    )
        ?.addEventListener(
            "click",
            ogrenciNotlariniYukle
        );
        element(
    "devamsizlik-ogrencilerini-getir"
)
    ?.addEventListener(
        "click",
        devamsizlikOgrencileriniYukle
    );

element(
    "devamsizliklari-kaydet"
)
    ?.addEventListener(
        "click",
        devamsizliklariKaydet
    );

element(
    "ogrenci-devamsizliklari-yenile"
)
    ?.addEventListener(
        "click",
        ogrenciDevamsizliklariniYukle
    );

element(
    "devamsizlik-ders-secimi"
)
    ?.addEventListener(
        "change",
        devamsizlikSecimiDegisti
    );

element(
    "devamsizlik-tarihi"
)
    ?.addEventListener(
        "change",
        devamsizlikSecimiDegisti
    );
}


/* =====================================================
   BAŞLANGIÇ
===================================================== */

async function uygulamayiBaslat() {
    olaylariBagla();

    await sistemDurumunuKontrolEt();

    if (
        durum.token &&
        durum.kullanici
    ) {
        try {
            await oturumuAc();
        } catch {
            oturumuKapat(false);
        }
    }
}

document.addEventListener(
    "DOMContentLoaded",
    uygulamayiBaslat
);
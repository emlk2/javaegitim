const API_ADRESI = "/api/ogrenciler";

const ogrenciTablosu =
    document.getElementById("ogrenci-tablosu");

const ogrenciFormu =
    document.getElementById("ogrenci-formu");

const adInput =
    document.getElementById("ad");

const soyadInput =
    document.getElementById("soyad");

const numaraInput =
    document.getElementById("numara");

const mesaj =
    document.getElementById("mesaj");

const ekleButonu =
    document.getElementById("ekle-butonu");

const aramaInput =
    document.getElementById("arama-input");

const yenileButonu =
    document.getElementById("yenile-butonu");

const bosDurum =
    document.getElementById("bos-durum");

const toplamOgrenci =
    document.getElementById("toplam-ogrenci");

const gosterilenOgrenci =
    document.getElementById("gosterilen-ogrenci");

const sonOgrenciNo =
    document.getElementById("son-ogrenci-no");

const apiDurumu =
    document.getElementById("api-durumu");

const yanMenuDurumNoktasi =
    document.getElementById("yan-menu-durum-noktasi");

const yanMenuDurumMetni =
    document.getElementById("yan-menu-durum-metni");

const modal =
    document.getElementById("modal");

const guncellemeFormu =
    document.getElementById("guncelleme-formu");

const guncellemeId =
    document.getElementById("guncelleme-id");

const guncellemeAd =
    document.getElementById("guncelleme-ad");

const guncellemeSoyad =
    document.getElementById("guncelleme-soyad");

const guncellemeNumara =
    document.getElementById("guncelleme-numara");

const guncellemeButonu =
    document.getElementById("guncelleme-butonu");

const bildirim =
    document.getElementById("bildirim");

const bildirimIkon =
    document.getElementById("bildirim-ikon");

const bildirimMetni =
    document.getElementById("bildirim-metni");

let ogrenciler = [];
let bildirimZamanlayici;

/*
    API isteği gönderen ortak fonksiyon.
    Hata mesajlarını tek bir yerden yönetiyoruz.
*/
async function apiIstegi(adres, ayarlar = {}) {
    const cevap = await fetch(adres, ayarlar);

    let sonuc = null;

    try {
        sonuc = await cevap.json();
    } catch {
        sonuc = null;
    }

    if (!cevap.ok) {
        throw new Error(
            sonuc?.mesaj ||
            sonuc?.hata ||
            "İşlem sırasında bir hata oluştu"
        );
    }

    return sonuc;
}

/*
    PostgreSQL'deki aktif öğrencileri getirir.
*/
async function ogrencileriGetir() {
    yukleniyorGoster();

    try {
        const sonuc = await apiIstegi(API_ADRESI);

        ogrenciler = Array.isArray(sonuc)
            ? sonuc
            : [];

        sistemDurumunuGuncelle(true);
        listeyiFiltrele();

    } catch (hata) {
        ogrenciler = [];

        sistemDurumunuGuncelle(false);

        ogrenciTablosu.innerHTML = `
            <tr>
                <td colspan="6">
                    <div class="yukleniyor">
                        ${htmlTemizle(hata.message)}
                    </div>
                </td>
            </tr>
        `;

        istatistikleriGuncelle([]);

        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

/*
    Arama alanına göre listeyi filtreler.
*/
function listeyiFiltrele() {
    const arama =
        aramaInput.value
            .trim()
            .toLocaleLowerCase("tr-TR");

    const filtrelenmisOgrenciler =
        ogrenciler.filter((ogrenci) => {
            const metin = `
                ${ogrenci.ad || ""}
                ${ogrenci.soyad || ""}
                ${ogrenci.ogrenci_no || ""}
                ${ogrenci.id || ""}
            `.toLocaleLowerCase("tr-TR");

            return metin.includes(arama);
        });

    ogrencileriGoster(filtrelenmisOgrenciler);
    istatistikleriGuncelle(filtrelenmisOgrenciler);
}

/*
    Öğrencileri HTML tablosunda gösterir.
*/
function ogrencileriGoster(gosterilecekOgrenciler) {
    ogrenciTablosu.innerHTML = "";

    if (gosterilecekOgrenciler.length === 0) {
        bosDurum.classList.remove("gizli");
        return;
    }

    bosDurum.classList.add("gizli");

    gosterilecekOgrenciler.forEach((ogrenci) => {
        const satir = document.createElement("tr");

        const basHarfler = basHarfleriGetir(
            ogrenci.ad,
            ogrenci.soyad
        );

        const kayitTarihi =
            tarihBicimlendir(ogrenci.kayit_tarihi);

        satir.innerHTML = `
            <td>
                <strong>#${htmlTemizle(ogrenci.id)}</strong>
            </td>

            <td>
                <div class="ogrenci-bilgisi">
                    <div class="ogrenci-avatar">
                        ${htmlTemizle(basHarfler)}
                    </div>

                    <div>
                        <strong>
                            ${htmlTemizle(ogrenci.ad)}
                            ${htmlTemizle(ogrenci.soyad)}
                        </strong>

                        <span>
                            Öğrenci kaydı
                        </span>
                    </div>
                </div>
            </td>

            <td>
                <strong>
                    ${htmlTemizle(ogrenci.ogrenci_no)}
                </strong>
            </td>

            <td>
                <span class="durum-etiketi">
                    Aktif
                </span>
            </td>

            <td>
                ${htmlTemizle(kayitTarihi)}
            </td>

            <td>
                <div class="islem-butonlari">

                    <button
                        type="button"
                        class="islem-butonu duzenle-butonu"
                        data-islem="duzenle"
                        data-id="${ogrenci.id}"
                    >
                        Düzenle
                    </button>

                    <button
                        type="button"
                        class="islem-butonu sil-butonu"
                        data-islem="sil"
                        data-id="${ogrenci.id}"
                    >
                        Pasif Yap
                    </button>

                </div>
            </td>
        `;

        ogrenciTablosu.appendChild(satir);
    });
}

/*
    İstatistik kartlarını günceller.
*/
function istatistikleriGuncelle(filtrelenmisOgrenciler) {
    toplamOgrenci.textContent =
        ogrenciler.length;

    gosterilenOgrenci.textContent =
        filtrelenmisOgrenciler.length;

    if (ogrenciler.length === 0) {
        sonOgrenciNo.textContent = "—";
        return;
    }

    const sonOgrenci =
        [...ogrenciler].sort(
            (a, b) => Number(b.id) - Number(a.id)
        )[0];

    sonOgrenciNo.textContent =
        sonOgrenci.ogrenci_no || "—";
}

/*
    Formdan yeni öğrenci ekler.
*/
ogrenciFormu.addEventListener(
    "submit",
    async (event) => {
        event.preventDefault();

        const yeniOgrenci = {
            ad: adInput.value.trim(),
            soyad: soyadInput.value.trim(),
            ogrenci_no: numaraInput.value.trim()
        };

        if (
            yeniOgrenci.ad.length < 2 ||
            yeniOgrenci.soyad.length < 2 ||
            yeniOgrenci.ogrenci_no.length < 3
        ) {
            formMesajiGoster(
                "Lütfen alanları geçerli şekilde doldurun.",
                "hata"
            );

            return;
        }

        butonYukleniyor(
            ekleButonu,
            true,
            "Kaydediliyor..."
        );

        try {
            await apiIstegi(API_ADRESI, {
                method: "POST",

                headers: {
                    "Content-Type": "application/json"
                },

                body: JSON.stringify(yeniOgrenci)
            });

            ogrenciFormu.reset();

            formMesajiGoster(
                "Öğrenci başarıyla kaydedildi.",
                "basarili"
            );

            bildirimGoster(
                "Yeni öğrenci PostgreSQL veritabanına kaydedildi.",
                "basarili"
            );

            await ogrencileriGetir();

        } catch (hata) {
            formMesajiGoster(
                hata.message,
                "hata"
            );

            bildirimGoster(
                hata.message,
                "hata"
            );

        } finally {
            butonYukleniyor(
                ekleButonu,
                false,
                "Öğrenciyi Kaydet"
            );
        }
    }
);

/*
    Tablodaki düzenle ve pasif yap
    butonlarını yakalar.
*/
ogrenciTablosu.addEventListener(
    "click",
    (event) => {
        const buton =
            event.target.closest("[data-islem]");

        if (!buton) {
            return;
        }

        const id =
            Number(buton.dataset.id);

        const islem =
            buton.dataset.islem;

        if (islem === "duzenle") {
            guncellemePenceresiniAc(id);
        }

        if (islem === "sil") {
            ogrenciyiPasifYap(id);
        }
    }
);

/*
    Güncelleme penceresini açar.
*/
function guncellemePenceresiniAc(id) {
    const ogrenci =
        ogrenciler.find(
            (kayit) => Number(kayit.id) === Number(id)
        );

    if (!ogrenci) {
        bildirimGoster(
            "Öğrenci kaydı bulunamadı.",
            "hata"
        );

        return;
    }

    guncellemeId.value =
        ogrenci.id;

    guncellemeAd.value =
        ogrenci.ad;

    guncellemeSoyad.value =
        ogrenci.soyad;

    guncellemeNumara.value =
        ogrenci.ogrenci_no;

    modal.classList.remove("gizli");

    document.body.style.overflow = "hidden";

    setTimeout(() => {
        guncellemeAd.focus();
    }, 50);
}

/*
    Güncelleme formunu PostgreSQL'e gönderir.
*/
guncellemeFormu.addEventListener(
    "submit",
    async (event) => {
        event.preventDefault();

        const id =
            Number(guncellemeId.value);

        const guncelOgrenci = {
            ad: guncellemeAd.value.trim(),
            soyad: guncellemeSoyad.value.trim(),
            ogrenci_no: guncellemeNumara.value.trim()
        };

        butonYukleniyor(
            guncellemeButonu,
            true,
            "Güncelleniyor..."
        );

        try {
            await apiIstegi(
                `${API_ADRESI}/${id}`,
                {
                    method: "PUT",

                    headers: {
                        "Content-Type": "application/json"
                    },

                    body: JSON.stringify(guncelOgrenci)
                }
            );

            modalKapat();

            bildirimGoster(
                "Öğrenci bilgileri başarıyla güncellendi.",
                "basarili"
            );

            await ogrencileriGetir();

        } catch (hata) {
            bildirimGoster(
                hata.message,
                "hata"
            );

        } finally {
            butonYukleniyor(
                guncellemeButonu,
                false,
                "Değişiklikleri Kaydet"
            );
        }
    }
);

/*
    DELETE endpointini çağırır.

    Backend tarafındaki DELETE endpointi öğrenciyi
    gerçekten silmemeli; aktif = FALSE yapmalıdır.
*/
async function ogrenciyiPasifYap(id) {
    const ogrenci =
        ogrenciler.find(
            (kayit) => Number(kayit.id) === Number(id)
        );

    if (!ogrenci) {
        return;
    }

    const onay = confirm(
        `${ogrenci.ad} ${ogrenci.soyad} adlı öğrenci ` +
        `pasif duruma getirilecek.\n\n` +
        `Kayıt veritabanından tamamen silinmeyecek. ` +
        `Devam etmek istiyor musun?`
    );

    if (!onay) {
        return;
    }

    try {
        const sonuc = await apiIstegi(
            `${API_ADRESI}/${id}`,
            {
                method: "DELETE"
            }
        );

        bildirimGoster(
            sonuc?.mesaj ||
            "Öğrenci pasif duruma getirildi.",
            "basarili"
        );

        await ogrencileriGetir();

    } catch (hata) {
        bildirimGoster(
            hata.message,
            "hata"
        );
    }
}

/*
    Arama kutusu değiştikçe filtre uygular.
*/
aramaInput.addEventListener(
    "input",
    listeyiFiltrele
);

/*
    Verileri yeniden yükler.
*/
yenileButonu.addEventListener(
    "click",
    async () => {
        yenileButonu.disabled = true;
        yenileButonu.textContent = "Yükleniyor...";

        await ogrencileriGetir();

        yenileButonu.disabled = false;
        yenileButonu.textContent = "↻ Verileri Yenile";
    }
);

/*
    Modal kapatma işlemleri.
*/
document
    .querySelectorAll("[data-modal-kapat]")
    .forEach((eleman) => {
        eleman.addEventListener(
            "click",
            modalKapat
        );
    });

document.addEventListener(
    "keydown",
    (event) => {
        if (event.key === "Escape") {
            modalKapat();
        }
    }
);

function modalKapat() {
    modal.classList.add("gizli");
    document.body.style.overflow = "";
    guncellemeFormu.reset();
}

/*
    Yardımcı fonksiyonlar
*/

function yukleniyorGoster() {
    bosDurum.classList.add("gizli");

    ogrenciTablosu.innerHTML = `
        <tr>
            <td colspan="6">
                <div class="yukleniyor">
                    <span class="spinner"></span>
                    Öğrenciler yükleniyor...
                </div>
            </td>
        </tr>
    `;
}

function sistemDurumunuGuncelle(cevrimici) {
    yanMenuDurumNoktasi.classList.remove(
        "cevrimici",
        "cevrimdisi"
    );

    if (cevrimici) {
        apiDurumu.textContent = "Çevrimiçi";
        yanMenuDurumMetni.textContent =
            "API bağlantısı aktif";

        yanMenuDurumNoktasi.classList.add(
            "cevrimici"
        );

        return;
    }

    apiDurumu.textContent = "Çevrimdışı";
    yanMenuDurumMetni.textContent =
        "API bağlantısı kurulamadı";

    yanMenuDurumNoktasi.classList.add(
        "cevrimdisi"
    );
}

function basHarfleriGetir(ad = "", soyad = "") {
    const adHarfi =
        ad.trim().charAt(0);

    const soyadHarfi =
        soyad.trim().charAt(0);

    return `${adHarfi}${soyadHarfi}`
        .toLocaleUpperCase("tr-TR");
}

function tarihBicimlendir(tarih) {
    if (!tarih) {
        return "Belirtilmedi";
    }

    const tarihNesnesi =
        new Date(tarih);

    if (Number.isNaN(tarihNesnesi.getTime())) {
        return "Belirtilmedi";
    }

    return tarihNesnesi.toLocaleDateString(
        "tr-TR",
        {
            day: "2-digit",
            month: "short",
            year: "numeric"
        }
    );
}

function formMesajiGoster(metin, tur) {
    mesaj.textContent = metin;
    mesaj.className = `form-mesaji ${tur}`;

    setTimeout(() => {
        mesaj.textContent = "";
        mesaj.className = "form-mesaji";
    }, 5000);
}

function bildirimGoster(metin, tur = "basarili") {
    clearTimeout(bildirimZamanlayici);

    bildirim.classList.remove(
        "basarili",
        "hata",
        "goster"
    );

    bildirimMetni.textContent = metin;

    bildirimIkon.textContent =
        tur === "hata"
            ? "!"
            : "✓";

    bildirim.classList.add(tur);

    requestAnimationFrame(() => {
        bildirim.classList.add("goster");
    });

    bildirimZamanlayici =
        setTimeout(() => {
            bildirim.classList.remove("goster");
        }, 4000);
}

function butonYukleniyor(
    buton,
    yukleniyor,
    metin
) {
    buton.disabled = yukleniyor;
    buton.textContent = metin;
}

/*
    API'den gelen metni doğrudan HTML olarak
    çalıştırmamak için özel karakterleri dönüştürür.
*/
function htmlTemizle(deger) {
    return String(deger ?? "")
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#039;");
}

/*
    Sayfa açıldığında öğrencileri getir.
*/
ogrencileriGetir();
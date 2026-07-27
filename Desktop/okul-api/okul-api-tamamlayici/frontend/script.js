"use strict";

const TOKEN_ANAHTARI = "okul_api_token";
const KULLANICI_ANAHTARI = "okul_api_kullanici";

const durum = {
    token: localStorage.getItem(TOKEN_ANAHTARI) || "",
    kullanici: null,
    ogrenciler: []
};

const eleman = (id) => document.getElementById(id);

function guvenliMetin(deger) {
    return String(deger ?? "")
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#039;");
}

function tarihYaz(tarih) {
    if (!tarih) return "—";
    const deger = new Date(tarih);
    return Number.isNaN(deger.getTime())
        ? "—"
        : new Intl.DateTimeFormat("tr-TR", { dateStyle: "medium" }).format(deger);
}

async function apiIstek(url, secenekler = {}) {
    const headers = new Headers(secenekler.headers || {});

    if (secenekler.body && !headers.has("Content-Type")) {
        headers.set("Content-Type", "application/json");
    }

    if (durum.token) {
        headers.set("Authorization", `Bearer ${durum.token}`);
    }

    const cevap = await fetch(url, { ...secenekler, headers });
    const veri = await cevap.json().catch(() => ({}));

    if (!cevap.ok) {
        if (cevap.status === 401 && !url.includes("/auth/giris")) {
            oturumuKapat(false);
        }
        throw new Error(veri.mesaj || "İşlem gerçekleştirilemedi");
    }

    return veri;
}

function bildirimGoster(metin, tip = "basarili") {
    const kutu = eleman("bildirim");
    const metinAlani = eleman("bildirim-metni");
    const ikon = eleman("bildirim-ikon");

    if (!kutu || !metinAlani || !ikon) return;

    metinAlani.textContent = metin;
    ikon.textContent = tip === "hata" ? "!" : "✓";
    kutu.className = `bildirim ${tip} goster`;

    clearTimeout(bildirimGoster.zamanlayici);
    bildirimGoster.zamanlayici = setTimeout(() => {
        kutu.classList.remove("goster");
    }, 3200);
}

function girisEkraniOlustur() {
    if (eleman("giris-ekrani")) return;

    const stil = document.createElement("style");
    stil.textContent = `
        .giris-ekrani{position:fixed;inset:0;z-index:1000;display:grid;place-items:center;padding:22px;background:linear-gradient(135deg,#111633,#312e81 55%,#6d28d9)}
        .giris-karti{width:min(430px,100%);padding:34px;border-radius:24px;background:#fff;box-shadow:0 35px 100px rgba(0,0,0,.3)}
        .giris-karti h2{margin:8px 0 8px;font-size:28px}.giris-karti p{margin:0 0 24px;color:#64748b;line-height:1.6}
        .giris-karti .form-grubu{margin-top:15px}.giris-karti .ana-buton{width:100%;margin-top:22px}
        .giris-hata{min-height:22px;margin:14px 0 0;color:#b91c1c;font-size:13px;font-weight:700}
        .cikis-butonu{color:#b91c1c!important;background:#fee2e2!important;border-color:#fecaca!important}
        body.oturum-kapali .uygulama{visibility:hidden}
    `;
    document.head.appendChild(stil);

    const ekran = document.createElement("div");
    ekran.id = "giris-ekrani";
    ekran.className = "giris-ekrani gizli";
    ekran.innerHTML = `
        <form id="giris-formu" class="giris-karti">
            <span class="bolum-etiketi">EduPanel</span>
            <h2>Yönetici Girişi</h2>
            <p>Öğrenci yönetim panelini kullanmak için admin hesabınızla giriş yapın.</p>
            <div class="form-grubu">
                <label for="giris-kullanici-adi">Kullanıcı adı</label>
                <input id="giris-kullanici-adi" autocomplete="username" required>
            </div>
            <div class="form-grubu">
                <label for="giris-sifre">Şifre</label>
                <input id="giris-sifre" type="password" autocomplete="current-password" required>
            </div>
            <p id="giris-hata" class="giris-hata"></p>
            <button id="giris-butonu" class="ana-buton" type="submit">Giriş Yap</button>
        </form>`;
    document.body.appendChild(ekran);

    eleman("giris-formu").addEventListener("submit", girisYap);

    const ustSag = document.querySelector(".ust-bar-sag");
    if (ustSag && !eleman("cikis-butonu")) {
        const cikis = document.createElement("button");
        cikis.id = "cikis-butonu";
        cikis.type = "button";
        cikis.className = "ikincil-buton cikis-butonu";
        cikis.textContent = "Çıkış Yap";
        cikis.addEventListener("click", () => oturumuKapat(true));
        ustSag.prepend(cikis);
    }
}

function girisEkraniGoster() {
    document.body.classList.add("oturum-kapali");
    eleman("giris-ekrani")?.classList.remove("gizli");
}

function girisEkraniGizle() {
    document.body.classList.remove("oturum-kapali");
    eleman("giris-ekrani")?.classList.add("gizli");
}

async function girisYap(event) {
    event.preventDefault();

    const buton = eleman("giris-butonu");
    const hataAlani = eleman("giris-hata");
    buton.disabled = true;
    buton.textContent = "Giriş yapılıyor...";
    hataAlani.textContent = "";

    try {
        const veri = await apiIstek("/api/auth/giris", {
            method: "POST",
            body: JSON.stringify({
                kullanici_adi: eleman("giris-kullanici-adi").value.trim(),
                sifre: eleman("giris-sifre").value
            })
        });

        if (String(veri.kullanici.rol).toUpperCase() !== "ADMIN") {
            throw new Error("Bu ekran yalnızca admin kullanıcıları içindir");
        }

        durum.token = veri.token;
        durum.kullanici = veri.kullanici;
        localStorage.setItem(TOKEN_ANAHTARI, veri.token);
        localStorage.setItem(KULLANICI_ANAHTARI, JSON.stringify(veri.kullanici));
        kullaniciBilgisiniGoster();
        girisEkraniGizle();
        await Promise.all([sistemDurumunuKontrolEt(), ogrencileriGetir()]);
        bildirimGoster("Giriş başarılı");
    } catch (hata) {
        durum.token = "";
        hataAlani.textContent = hata.message;
    } finally {
        buton.disabled = false;
        buton.textContent = "Giriş Yap";
    }
}

function oturumuKapat(bildirim = true) {
    durum.token = "";
    durum.kullanici = null;
    durum.ogrenciler = [];
    localStorage.removeItem(TOKEN_ANAHTARI);
    localStorage.removeItem(KULLANICI_ANAHTARI);
    girisEkraniGoster();
    if (bildirim) bildirimGoster("Oturum kapatıldı");
}

function kullaniciBilgisiniGoster() {
    const kart = document.querySelector(".kullanici-karti");
    if (!kart || !durum.kullanici) return;

    const ad = durum.kullanici.kullanici_adi || "Yönetici";
    kart.querySelector(".kullanici-avatar").textContent = ad.charAt(0).toUpperCase();
    kart.querySelector("strong").textContent = ad;
    kart.querySelector("span").textContent = `${durum.kullanici.rol} Paneli`;
}

async function oturumuDogrula() {
    if (!durum.token) {
        girisEkraniGoster();
        return false;
    }

    try {
        const veri = await apiIstek("/api/auth/ben");
        if (String(veri.kullanici.rol).toUpperCase() !== "ADMIN") {
            throw new Error("Admin yetkisi gerekli");
        }
        durum.kullanici = veri.kullanici;
        kullaniciBilgisiniGoster();
        girisEkraniGizle();
        return true;
    } catch {
        oturumuKapat(false);
        return false;
    }
}

async function sistemDurumunuKontrolEt() {
    const nokta = eleman("yan-menu-durum-noktasi");
    const metin = eleman("yan-menu-durum-metni");
    const api = eleman("api-durumu");

    try {
        await apiIstek("/api/sistem/durum");
        nokta.className = "durum-noktasi cevrimici";
        metin.textContent = "Çevrim içi";
        api.textContent = "Bağlı";
    } catch {
        nokta.className = "durum-noktasi cevrimdisi";
        metin.textContent = "Bağlantı yok";
        api.textContent = "Hata";
    }
}

async function ogrencileriGetir() {
    tabloYukleniyorGoster();

    try {
        durum.ogrenciler = await apiIstek("/api/ogrenciler");
        ogrencileriCiz(durum.ogrenciler);
        istatistikleriGuncelle(durum.ogrenciler);
    } catch (hata) {
        tabloHatasiGoster(hata.message);
        bildirimGoster(hata.message, "hata");
    }
}

function tabloYukleniyorGoster() {
    eleman("bos-durum").classList.add("gizli");
    eleman("ogrenci-tablosu").innerHTML = `
        <tr><td colspan="6"><div class="yukleniyor"><span class="spinner"></span>Öğrenciler yükleniyor...</div></td></tr>`;
}

function tabloHatasiGoster(mesaj) {
    eleman("ogrenci-tablosu").innerHTML = `
        <tr><td colspan="6"><div class="bos-durum"><div class="bos-durum-ikon">⚠</div><h4>Veriler alınamadı</h4><p>${guvenliMetin(mesaj)}</p></div></td></tr>`;
}

function ogrencileriCiz(liste) {
    const govde = eleman("ogrenci-tablosu");
    const bos = eleman("bos-durum");

    if (!liste.length) {
        govde.innerHTML = "";
        bos.classList.remove("gizli");
        return;
    }

    bos.classList.add("gizli");
    govde.innerHTML = liste.map((ogrenci) => `
        <tr>
            <td>${ogrenci.id}</td>
            <td>
                <div class="ogrenci-bilgisi">
                    <div class="ogrenci-avatar">${guvenliMetin(ogrenci.ad.charAt(0))}${guvenliMetin(ogrenci.soyad.charAt(0))}</div>
                    <div><strong>${guvenliMetin(ogrenci.ad)} ${guvenliMetin(ogrenci.soyad)}</strong><span>${ogrenci.kullanicilar ? "Giriş hesabı var" : "Giriş hesabı yok"}</span></div>
                </div>
            </td>
            <td>${guvenliMetin(ogrenci.ogrenci_no)}</td>
            <td><span class="durum-etiketi">Aktif</span></td>
            <td>${tarihYaz(ogrenci.kayit_tarihi)}</td>
            <td>
                <div class="islem-butonlari">
                    <button class="islem-butonu duzenle-butonu" data-islem="duzenle" data-id="${ogrenci.id}">Düzenle</button>
                    <button class="islem-butonu sil-butonu" data-islem="sil" data-id="${ogrenci.id}">Pasif Yap</button>
                </div>
            </td>
        </tr>`).join("");

    eleman("gosterilen-ogrenci").textContent = liste.length;
}

function istatistikleriGuncelle(liste) {
    eleman("toplam-ogrenci").textContent = durum.ogrenciler.length;
    eleman("gosterilen-ogrenci").textContent = liste.length;
    eleman("son-ogrenci-no").textContent = durum.ogrenciler.at(-1)?.ogrenci_no || "—";
}

async function ogrenciEkle(event) {
    event.preventDefault();
    const buton = eleman("ekle-butonu");
    const mesaj = eleman("mesaj");
    buton.disabled = true;
    mesaj.className = "form-mesaji";
    mesaj.textContent = "Kaydediliyor...";

    try {
        const veri = await apiIstek("/api/ogrenciler", {
            method: "POST",
            body: JSON.stringify({
                ad: eleman("ad").value.trim(),
                soyad: eleman("soyad").value.trim(),
                ogrenci_no: eleman("numara").value.trim()
            })
        });

        eleman("ogrenci-formu").reset();
        mesaj.className = "form-mesaji basarili";
        mesaj.textContent = veri.mesaj;
        await ogrencileriGetir();
        bildirimGoster(veri.mesaj);
    } catch (hata) {
        mesaj.className = "form-mesaji hata";
        mesaj.textContent = hata.message;
        bildirimGoster(hata.message, "hata");
    } finally {
        buton.disabled = false;
    }
}

function modalAc(ogrenci) {
    eleman("guncelleme-id").value = ogrenci.id;
    eleman("guncelleme-ad").value = ogrenci.ad;
    eleman("guncelleme-soyad").value = ogrenci.soyad;
    eleman("guncelleme-numara").value = ogrenci.ogrenci_no;
    eleman("modal").classList.remove("gizli");
}

function modalKapat() {
    eleman("modal").classList.add("gizli");
}

async function ogrenciGuncelle(event) {
    event.preventDefault();
    const id = Number(eleman("guncelleme-id").value);
    const buton = eleman("guncelleme-butonu");
    buton.disabled = true;

    try {
        const veri = await apiIstek(`/api/ogrenciler/${id}`, {
            method: "PUT",
            body: JSON.stringify({
                ad: eleman("guncelleme-ad").value.trim(),
                soyad: eleman("guncelleme-soyad").value.trim(),
                ogrenci_no: eleman("guncelleme-numara").value.trim()
            })
        });

        modalKapat();
        await ogrencileriGetir();
        bildirimGoster(veri.mesaj);
    } catch (hata) {
        bildirimGoster(hata.message, "hata");
    } finally {
        buton.disabled = false;
    }
}

async function ogrenciPasifYap(id) {
    const ogrenci = durum.ogrenciler.find((kayit) => kayit.id === id);
    if (!ogrenci) return;

    const onay = confirm(`${ogrenci.ad} ${ogrenci.soyad} pasif duruma getirilsin mi?`);
    if (!onay) return;

    try {
        const veri = await apiIstek(`/api/ogrenciler/${id}`, { method: "DELETE" });
        await ogrencileriGetir();
        bildirimGoster(veri.mesaj);
    } catch (hata) {
        bildirimGoster(hata.message, "hata");
    }
}

function aramaYap() {
    const arama = eleman("arama-input").value.trim().toLocaleLowerCase("tr-TR");
    const filtreli = durum.ogrenciler.filter((ogrenci) =>
        `${ogrenci.ad} ${ogrenci.soyad} ${ogrenci.ogrenci_no}`
            .toLocaleLowerCase("tr-TR")
            .includes(arama)
    );
    ogrencileriCiz(filtreli);
    istatistikleriGuncelle(filtreli);
}

function olaylariBagla() {
    eleman("ogrenci-formu")?.addEventListener("submit", ogrenciEkle);
    eleman("guncelleme-formu")?.addEventListener("submit", ogrenciGuncelle);
    eleman("arama-input")?.addEventListener("input", aramaYap);
    eleman("yenile-butonu")?.addEventListener("click", async () => {
        await Promise.all([sistemDurumunuKontrolEt(), ogrencileriGetir()]);
        bildirimGoster("Veriler yenilendi");
    });

    eleman("ogrenci-tablosu")?.addEventListener("click", (event) => {
        const buton = event.target.closest("button[data-islem]");
        if (!buton) return;
        const id = Number(buton.dataset.id);
        if (buton.dataset.islem === "duzenle") {
            const ogrenci = durum.ogrenciler.find((kayit) => kayit.id === id);
            if (ogrenci) modalAc(ogrenci);
        } else if (buton.dataset.islem === "sil") {
            ogrenciPasifYap(id);
        }
    });

    document.querySelectorAll("[data-modal-kapat]").forEach((buton) => {
        buton.addEventListener("click", modalKapat);
    });

    document.addEventListener("keydown", (event) => {
        if (event.key === "Escape") modalKapat();
    });
}

async function baslat() {
    girisEkraniOlustur();
    olaylariBagla();

    const oturumVar = await oturumuDogrula();
    if (oturumVar) {
        await Promise.all([sistemDurumunuKontrolEt(), ogrencileriGetir()]);
    }
}

document.addEventListener("DOMContentLoaded", baslat);

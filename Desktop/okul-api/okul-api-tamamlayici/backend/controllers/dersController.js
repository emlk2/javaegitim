const prisma = require("../config/prisma");
const { pozitifTamSayi, temizMetin } = require("../utils/yardimcilar");

async function ogretmenProfilId(kullaniciId) {
    const profil = await prisma.ogretmenler.findFirst({
        where: { kullanici_id: kullaniciId, aktif: true },
        select: { id: true }
    });
    return profil?.id || null;
}

async function ogrenciProfilId(kullaniciId) {
    const profil = await prisma.ogrenciler.findFirst({
        where: { kullanici_id: kullaniciId, aktif: true },
        select: { id: true }
    });
    return profil?.id || null;
}

async function listele(req, res) {
    const rol = req.kullanici.rol;
    let where = { aktif: true };

    if (rol === "OGRETMEN") {
        const id = await ogretmenProfilId(req.kullanici.id);
        where = { ...where, ogretmen_id: id || -1 };
    }

    if (rol === "OGRENCI") {
        const id = await ogrenciProfilId(req.kullanici.id);
        where = {
            ...where,
            ders_kayitlari: {
                some: { ogrenci_id: id || -1, aktif: true }
            }
        };
    }

    const dersler = await prisma.dersler.findMany({
        where,
        orderBy: { ders_adi: "asc" },
        include: {
            ogretmenler: {
                select: { id: true, ad: true, soyad: true, sicil_no: true }
            },
            _count: {
                select: { ders_kayitlari: true, sinavlar: true }
            }
        }
    });

    return res.json(dersler);
}

async function ekle(req, res) {
    const dersKodu = temizMetin(req.body.ders_kodu).toUpperCase();
    const dersAdi = temizMetin(req.body.ders_adi);
    const aciklama = temizMetin(req.body.aciklama) || null;
    const ogretmenId = pozitifTamSayi(req.body.ogretmen_id);

    if (!dersKodu || !dersAdi || !ogretmenId) {
        return res.status(400).json({
            mesaj: "Ders kodu, ders adı ve öğretmen ID'si zorunludur"
        });
    }

    const ogretmen = await prisma.ogretmenler.findFirst({
        where: { id: ogretmenId, aktif: true }
    });

    if (!ogretmen) {
        return res.status(404).json({ mesaj: "Öğretmen bulunamadı" });
    }

    const ders = await prisma.dersler.create({
        data: {
            ders_kodu: dersKodu,
            ders_adi: dersAdi,
            aciklama,
            ogretmen_id: ogretmenId,
            aktif: true
        }
    });

    return res.status(201).json({ mesaj: "Ders oluşturuldu", ders });
}

async function guncelle(req, res) {
    const id = pozitifTamSayi(req.params.id);
    const dersKodu = temizMetin(req.body.ders_kodu).toUpperCase();
    const dersAdi = temizMetin(req.body.ders_adi);
    const aciklama = temizMetin(req.body.aciklama) || null;
    const ogretmenId = pozitifTamSayi(req.body.ogretmen_id);

    if (!id || !dersKodu || !dersAdi || !ogretmenId) {
        return res.status(400).json({ mesaj: "Geçerli bilgiler zorunludur" });
    }

    const ders = await prisma.dersler.update({
        where: { id },
        data: {
            ders_kodu: dersKodu,
            ders_adi: dersAdi,
            aciklama,
            ogretmen_id: ogretmenId
        }
    });

    return res.json({ mesaj: "Ders güncellendi", ders });
}

async function pasifYap(req, res) {
    const id = pozitifTamSayi(req.params.id);

    if (!id) {
        return res.status(400).json({ mesaj: "Geçerli ders ID'si giriniz" });
    }

    const ders = await prisma.dersler.update({
        where: { id },
        data: { aktif: false, silinme_tarihi: new Date() }
    });

    return res.json({ mesaj: "Ders pasif duruma getirildi", ders });
}

async function ogrenciAta(req, res) {
    const dersId = pozitifTamSayi(req.params.id);
    const ogrenciId = pozitifTamSayi(req.body.ogrenci_id);

    if (!dersId || !ogrenciId) {
        return res.status(400).json({ mesaj: "Ders ve öğrenci ID'si zorunludur" });
    }

    const ders = await prisma.dersler.findFirst({ where: { id: dersId, aktif: true } });

    if (!ders) {
        return res.status(404).json({ mesaj: "Ders bulunamadı" });
    }

    if (req.kullanici.rol === "OGRETMEN") {
        const profilId = await ogretmenProfilId(req.kullanici.id);
        if (ders.ogretmen_id !== profilId) {
            return res.status(403).json({ mesaj: "Yalnızca kendi dersinize öğrenci ekleyebilirsiniz" });
        }
    }

    const ogrenci = await prisma.ogrenciler.findFirst({ where: { id: ogrenciId, aktif: true } });

    if (!ogrenci) {
        return res.status(404).json({ mesaj: "Öğrenci bulunamadı" });
    }

    const kayit = await prisma.ders_kayitlari.upsert({
        where: {
            ders_id_ogrenci_id: {
                ders_id: dersId,
                ogrenci_id: ogrenciId
            }
        },
        update: { aktif: true },
        create: {
            ders_id: dersId,
            ogrenci_id: ogrenciId,
            aktif: true
        }
    });

    return res.status(201).json({ mesaj: "Öğrenci derse kaydedildi", kayit });
}

async function ogrenciCikar(req, res) {
    const dersId = pozitifTamSayi(req.params.id);
    const ogrenciId = pozitifTamSayi(req.params.ogrenciId);

    if (!dersId || !ogrenciId) {
        return res.status(400).json({ mesaj: "Geçerli ders ve öğrenci ID'si giriniz" });
    }

    const ders = await prisma.dersler.findFirst({ where: { id: dersId, aktif: true } });

    if (!ders) {
        return res.status(404).json({ mesaj: "Ders bulunamadı" });
    }

    if (req.kullanici.rol === "OGRETMEN") {
        const profilId = await ogretmenProfilId(req.kullanici.id);
        if (ders.ogretmen_id !== profilId) {
            return res.status(403).json({ mesaj: "Yalnızca kendi dersinizden öğrenci çıkarabilirsiniz" });
        }
    }

    const kayit = await prisma.ders_kayitlari.update({
        where: {
            ders_id_ogrenci_id: {
                ders_id: dersId,
                ogrenci_id: ogrenciId
            }
        },
        data: { aktif: false }
    });

    return res.json({ mesaj: "Öğrenci dersten çıkarıldı", kayit });
}

module.exports = {
    listele,
    ekle,
    guncelle,
    pasifYap,
    ogrenciAta,
    ogrenciCikar,
    ogretmenProfilId,
    ogrenciProfilId
};

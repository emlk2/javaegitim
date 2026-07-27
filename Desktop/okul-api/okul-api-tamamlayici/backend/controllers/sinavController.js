const prisma = require("../config/prisma");
const { pozitifTamSayi, temizMetin } = require("../utils/yardimcilar");
const { ogretmenProfilId, ogrenciProfilId } = require("./dersController");

async function listele(req, res) {
    const rol = req.kullanici.rol;
    let where = { aktif: true };

    if (rol === "OGRETMEN") {
        const id = await ogretmenProfilId(req.kullanici.id);
        where = { ...where, dersler: { ogretmen_id: id || -1 } };
    }

    if (rol === "OGRENCI") {
        const id = await ogrenciProfilId(req.kullanici.id);
        where = {
            ...where,
            dersler: {
                ders_kayitlari: {
                    some: { ogrenci_id: id || -1, aktif: true }
                }
            }
        };
    }

    const sinavlar = await prisma.sinavlar.findMany({
        where,
        orderBy: { tarih: "desc" },
        include: {
            dersler: {
                select: { id: true, ders_kodu: true, ders_adi: true, ogretmen_id: true }
            },
            _count: { select: { notlar: true } }
        }
    });

    return res.json(sinavlar);
}

async function dersYetkisi(req, dersId) {
    if (req.kullanici.rol === "ADMIN") return true;
    const ogretmenId = await ogretmenProfilId(req.kullanici.id);
    const ders = await prisma.dersler.findFirst({ where: { id: dersId, aktif: true } });
    return Boolean(ders && ders.ogretmen_id === ogretmenId);
}

async function ekle(req, res) {
    const dersId = pozitifTamSayi(req.body.ders_id);
    const ad = temizMetin(req.body.ad);
    const tur = temizMetin(req.body.tur) || "SINAV";
    const tarih = req.body.tarih ? new Date(req.body.tarih) : null;
    const maxPuan = Number(req.body.max_puan ?? 100);

    if (!dersId || !ad || !tarih || Number.isNaN(tarih.getTime())) {
        return res.status(400).json({ mesaj: "Ders, sınav adı ve geçerli tarih zorunludur" });
    }

    if (!Number.isFinite(maxPuan) || maxPuan <= 0) {
        return res.status(400).json({ mesaj: "Maksimum puan sıfırdan büyük olmalıdır" });
    }

    if (!(await dersYetkisi(req, dersId))) {
        return res.status(403).json({ mesaj: "Bu derste sınav oluşturma yetkiniz yok" });
    }

    const sinav = await prisma.sinavlar.create({
        data: {
            ders_id: dersId,
            ad,
            tur,
            tarih,
            max_puan: maxPuan,
            aktif: true
        }
    });

    return res.status(201).json({ mesaj: "Sınav oluşturuldu", sinav });
}

async function guncelle(req, res) {
    const id = pozitifTamSayi(req.params.id);
    const mevcut = id
        ? await prisma.sinavlar.findFirst({ where: { id, aktif: true } })
        : null;

    if (!mevcut) {
        return res.status(404).json({ mesaj: "Sınav bulunamadı" });
    }

    if (!(await dersYetkisi(req, mevcut.ders_id))) {
        return res.status(403).json({ mesaj: "Bu sınavı güncelleme yetkiniz yok" });
    }

    const ad = temizMetin(req.body.ad) || mevcut.ad;
    const tur = temizMetin(req.body.tur) || mevcut.tur;
    const tarih = req.body.tarih ? new Date(req.body.tarih) : mevcut.tarih;
    const maxPuan = req.body.max_puan === undefined
        ? Number(mevcut.max_puan)
        : Number(req.body.max_puan);

    const sinav = await prisma.sinavlar.update({
        where: { id },
        data: { ad, tur, tarih, max_puan: maxPuan }
    });

    return res.json({ mesaj: "Sınav güncellendi", sinav });
}

async function pasifYap(req, res) {
    const id = pozitifTamSayi(req.params.id);
    const mevcut = id
        ? await prisma.sinavlar.findFirst({ where: { id, aktif: true } })
        : null;

    if (!mevcut) {
        return res.status(404).json({ mesaj: "Sınav bulunamadı" });
    }

    if (!(await dersYetkisi(req, mevcut.ders_id))) {
        return res.status(403).json({ mesaj: "Bu sınavı silme yetkiniz yok" });
    }

    const sinav = await prisma.sinavlar.update({
        where: { id },
        data: { aktif: false }
    });

    return res.json({ mesaj: "Sınav pasif duruma getirildi", sinav });
}

module.exports = {
    listele,
    ekle,
    guncelle,
    pasifYap,
    dersYetkisi
};

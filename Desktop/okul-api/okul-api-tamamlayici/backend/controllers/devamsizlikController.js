const prisma = require("../config/prisma");
const { pozitifTamSayi, temizMetin } = require("../utils/yardimcilar");
const { dersYetkisi } = require("./sinavController");
const { ogretmenProfilId, ogrenciProfilId } = require("./dersController");

async function listele(req, res) {
    const rol = req.kullanici.rol;
    let where = {};

    if (rol === "OGRETMEN") {
        const id = await ogretmenProfilId(req.kullanici.id);
        where = { dersler: { ogretmen_id: id || -1 } };
    }

    if (rol === "OGRENCI") {
        const id = await ogrenciProfilId(req.kullanici.id);
        where = { ogrenci_id: id || -1 };
    }

    const kayitlar = await prisma.devamsizliklar.findMany({
        where,
        orderBy: { tarih: "desc" },
        include: {
            ogrenciler: {
                select: { id: true, ad: true, soyad: true, ogrenci_no: true }
            },
            dersler: {
                select: { id: true, ders_kodu: true, ders_adi: true, ogretmen_id: true }
            }
        }
    });

    return res.json(kayitlar);
}

async function kaydet(req, res) {
    const dersId = pozitifTamSayi(req.body.ders_id);
    const ogrenciId = pozitifTamSayi(req.body.ogrenci_id);
    const tarih = req.body.tarih ? new Date(req.body.tarih) : null;
    const durum = temizMetin(req.body.durum).toUpperCase();
    const aciklama = temizMetin(req.body.aciklama) || null;
    const izinliDurumlar = ["GELMEDI", "GEC_KALDI", "IZINLI"];

    if (!dersId || !ogrenciId || !tarih || Number.isNaN(tarih.getTime())) {
        return res.status(400).json({ mesaj: "Ders, öğrenci ve geçerli tarih zorunludur" });
    }

    if (!izinliDurumlar.includes(durum)) {
        return res.status(400).json({
            mesaj: `Durum şu değerlerden biri olmalıdır: ${izinliDurumlar.join(", ")}`
        });
    }

    if (!(await dersYetkisi(req, dersId))) {
        return res.status(403).json({ mesaj: "Bu ders için devamsızlık girme yetkiniz yok" });
    }

    const kayit = await prisma.ders_kayitlari.findFirst({
        where: { ders_id: dersId, ogrenci_id: ogrenciId, aktif: true }
    });

    if (!kayit) {
        return res.status(400).json({ mesaj: "Öğrenci bu derse kayıtlı değil" });
    }

    tarih.setHours(0, 0, 0, 0);

    const devamsizlik = await prisma.devamsizliklar.upsert({
        where: {
            ders_id_ogrenci_id_tarih: {
                ders_id: dersId,
                ogrenci_id: ogrenciId,
                tarih
            }
        },
        update: { durum, aciklama },
        create: {
            ders_id: dersId,
            ogrenci_id: ogrenciId,
            tarih,
            durum,
            aciklama
        }
    });

    return res.status(201).json({ mesaj: "Devamsızlık kaydedildi", devamsizlik });
}

async function sil(req, res) {
    const id = pozitifTamSayi(req.params.id);
    const kayit = id
        ? await prisma.devamsizliklar.findUnique({ where: { id } })
        : null;

    if (!kayit) {
        return res.status(404).json({ mesaj: "Devamsızlık kaydı bulunamadı" });
    }

    if (!(await dersYetkisi(req, kayit.ders_id))) {
        return res.status(403).json({ mesaj: "Bu kaydı silme yetkiniz yok" });
    }

    await prisma.devamsizliklar.delete({ where: { id } });

    return res.json({ mesaj: "Devamsızlık kaydı silindi" });
}

module.exports = {
    listele,
    kaydet,
    sil
};

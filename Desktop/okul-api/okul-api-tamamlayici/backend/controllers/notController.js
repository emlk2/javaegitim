const prisma = require("../config/prisma");
const { pozitifTamSayi, temizMetin } = require("../utils/yardimcilar");
const { dersYetkisi } = require("./sinavController");
const { ogretmenProfilId, ogrenciProfilId } = require("./dersController");

async function listele(req, res) {
    const rol = req.kullanici.rol;
    let where = {};

    if (rol === "OGRETMEN") {
        const id = await ogretmenProfilId(req.kullanici.id);
        where = { sinavlar: { dersler: { ogretmen_id: id || -1 } } };
    }

    if (rol === "OGRENCI") {
        const id = await ogrenciProfilId(req.kullanici.id);
        where = { ogrenci_id: id || -1 };
    }

    const notlar = await prisma.notlar.findMany({
        where,
        orderBy: { guncelleme_tarihi: "desc" },
        include: {
            ogrenciler: {
                select: { id: true, ad: true, soyad: true, ogrenci_no: true }
            },
            sinavlar: {
                include: {
                    dersler: {
                        select: { id: true, ders_kodu: true, ders_adi: true, ogretmen_id: true }
                    }
                }
            }
        }
    });

    return res.json(notlar);
}

async function kaydet(req, res) {
    const sinavId = pozitifTamSayi(req.body.sinav_id);
    const ogrenciId = pozitifTamSayi(req.body.ogrenci_id);
    const puan = Number(req.body.puan);
    const aciklama = temizMetin(req.body.aciklama) || null;

    if (!sinavId || !ogrenciId || !Number.isFinite(puan)) {
        return res.status(400).json({ mesaj: "Sınav, öğrenci ve geçerli puan zorunludur" });
    }

    const sinav = await prisma.sinavlar.findFirst({ where: { id: sinavId, aktif: true } });

    if (!sinav) {
        return res.status(404).json({ mesaj: "Sınav bulunamadı" });
    }

    if (!(await dersYetkisi(req, sinav.ders_id))) {
        return res.status(403).json({ mesaj: "Bu sınava not girme yetkiniz yok" });
    }

    if (puan < 0 || puan > Number(sinav.max_puan)) {
        return res.status(400).json({
            mesaj: `Puan 0 ile ${sinav.max_puan} arasında olmalıdır`
        });
    }

    const kayit = await prisma.ders_kayitlari.findFirst({
        where: {
            ders_id: sinav.ders_id,
            ogrenci_id: ogrenciId,
            aktif: true
        }
    });

    if (!kayit) {
        return res.status(400).json({ mesaj: "Öğrenci bu derse kayıtlı değil" });
    }

    const notKaydi = await prisma.notlar.upsert({
        where: {
            sinav_id_ogrenci_id: {
                sinav_id: sinavId,
                ogrenci_id: ogrenciId
            }
        },
        update: { puan, aciklama },
        create: {
            sinav_id: sinavId,
            ogrenci_id: ogrenciId,
            puan,
            aciklama
        }
    });

    return res.status(201).json({ mesaj: "Not kaydedildi", not: notKaydi });
}

async function sil(req, res) {
    const id = pozitifTamSayi(req.params.id);
    const notKaydi = id
        ? await prisma.notlar.findUnique({
            where: { id },
            include: { sinavlar: true }
        })
        : null;

    if (!notKaydi) {
        return res.status(404).json({ mesaj: "Not kaydı bulunamadı" });
    }

    if (!(await dersYetkisi(req, notKaydi.sinavlar.ders_id))) {
        return res.status(403).json({ mesaj: "Bu notu silme yetkiniz yok" });
    }

    await prisma.notlar.delete({ where: { id } });

    return res.json({ mesaj: "Not kaydı silindi" });
}

module.exports = {
    listele,
    kaydet,
    sil
};

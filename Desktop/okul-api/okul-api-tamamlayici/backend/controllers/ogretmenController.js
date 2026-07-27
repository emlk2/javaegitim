const bcrypt = require("bcryptjs");
const prisma = require("../config/prisma");
const { pozitifTamSayi, temizMetin } = require("../utils/yardimcilar");

async function listele(req, res) {
    const ogretmenler = await prisma.ogretmenler.findMany({
        where: { aktif: true },
        orderBy: { id: "asc" },
        include: {
            kullanicilar: {
                select: {
                    id: true,
                    kullanici_adi: true,
                    email: true,
                    aktif: true
                }
            }
        }
    });

    return res.json(ogretmenler);
}

async function getir(req, res) {
    const id = pozitifTamSayi(req.params.id);

    if (!id) {
        return res.status(400).json({ mesaj: "Geçerli bir öğretmen ID'si giriniz" });
    }

    const ogretmen = await prisma.ogretmenler.findFirst({
        where: { id, aktif: true },
        include: {
            kullanicilar: {
                select: { id: true, kullanici_adi: true, email: true, aktif: true }
            },
            dersler: {
                where: { aktif: true }
            }
        }
    });

    if (!ogretmen) {
        return res.status(404).json({ mesaj: "Öğretmen bulunamadı" });
    }

    return res.json(ogretmen);
}

async function ekle(req, res) {
    const ad = temizMetin(req.body.ad);
    const soyad = temizMetin(req.body.soyad);
    const sicilNo = temizMetin(req.body.sicil_no);
    const brans = temizMetin(req.body.brans) || null;
    const kullaniciAdi = temizMetin(req.body.kullanici_adi);
    const email = temizMetin(req.body.email).toLowerCase();
    const sifre = req.body.sifre;
    const hesapBilgisiVar = Boolean(kullaniciAdi || email || sifre);

    if (!ad || !soyad || !sicilNo) {
        return res.status(400).json({ mesaj: "Ad, soyad ve sicil numarası zorunludur" });
    }

    if (hesapBilgisiVar && (!kullaniciAdi || !email || !sifre)) {
        return res.status(400).json({
            mesaj: "Hesap oluşturmak için kullanıcı adı, e-posta ve şifre birlikte girilmelidir"
        });
    }

    if (sifre && String(sifre).length < 6) {
        return res.status(400).json({ mesaj: "Şifre en az 6 karakter olmalıdır" });
    }

    const sonuc = await prisma.$transaction(async (tx) => {
        let kullanici = null;

        if (hesapBilgisiVar) {
            kullanici = await tx.kullanicilar.create({
                data: {
                    kullanici_adi: kullaniciAdi,
                    email,
                    sifre_hash: await bcrypt.hash(String(sifre), 12),
                    rol: "OGRETMEN",
                    aktif: true
                }
            });
        }

        const ogretmen = await tx.ogretmenler.create({
            data: {
                ad,
                soyad,
                sicil_no: sicilNo,
                brans,
                kullanici_id: kullanici?.id || null,
                aktif: true
            }
        });

        return { ogretmen, kullanici };
    });

    return res.status(201).json({ mesaj: "Öğretmen başarıyla eklendi", ...sonuc });
}

async function guncelle(req, res) {
    const id = pozitifTamSayi(req.params.id);
    const ad = temizMetin(req.body.ad);
    const soyad = temizMetin(req.body.soyad);
    const sicilNo = temizMetin(req.body.sicil_no);
    const brans = temizMetin(req.body.brans) || null;

    if (!id) {
        return res.status(400).json({ mesaj: "Geçerli bir öğretmen ID'si giriniz" });
    }

    if (!ad || !soyad || !sicilNo) {
        return res.status(400).json({ mesaj: "Ad, soyad ve sicil numarası zorunludur" });
    }

    const mevcut = await prisma.ogretmenler.findFirst({ where: { id, aktif: true } });

    if (!mevcut) {
        return res.status(404).json({ mesaj: "Güncellenecek öğretmen bulunamadı" });
    }

    const ogretmen = await prisma.ogretmenler.update({
        where: { id },
        data: { ad, soyad, sicil_no: sicilNo, brans }
    });

    return res.json({ mesaj: "Öğretmen başarıyla güncellendi", ogretmen });
}

async function pasifYap(req, res) {
    const id = pozitifTamSayi(req.params.id);

    if (!id) {
        return res.status(400).json({ mesaj: "Geçerli bir öğretmen ID'si giriniz" });
    }

    const mevcut = await prisma.ogretmenler.findFirst({ where: { id, aktif: true } });

    if (!mevcut) {
        return res.status(404).json({ mesaj: "Pasif yapılacak öğretmen bulunamadı" });
    }

    const ogretmen = await prisma.$transaction(async (tx) => {
        const guncellenen = await tx.ogretmenler.update({
            where: { id },
            data: { aktif: false, silinme_tarihi: new Date() }
        });

        if (mevcut.kullanici_id) {
            await tx.kullanicilar.update({
                where: { id: mevcut.kullanici_id },
                data: { aktif: false }
            });
        }

        return guncellenen;
    });

    return res.json({ mesaj: "Öğretmen pasif duruma getirildi", ogretmen });
}

async function hesapOlustur(req, res) {
    const id = pozitifTamSayi(req.params.id);
    const kullaniciAdi = temizMetin(req.body.kullanici_adi);
    const email = temizMetin(req.body.email).toLowerCase();
    const sifre = req.body.sifre;

    if (!id || !kullaniciAdi || !email || !sifre) {
        return res.status(400).json({
            mesaj: "Geçerli öğretmen ID'si, kullanıcı adı, e-posta ve şifre zorunludur"
        });
    }

    if (String(sifre).length < 6) {
        return res.status(400).json({ mesaj: "Şifre en az 6 karakter olmalıdır" });
    }

    const ogretmen = await prisma.ogretmenler.findFirst({ where: { id, aktif: true } });

    if (!ogretmen) {
        return res.status(404).json({ mesaj: "Öğretmen bulunamadı" });
    }

    if (ogretmen.kullanici_id) {
        return res.status(409).json({ mesaj: "Bu öğretmenin zaten kullanıcı hesabı var" });
    }

    const kullanici = await prisma.$transaction(async (tx) => {
        const yeni = await tx.kullanicilar.create({
            data: {
                kullanici_adi: kullaniciAdi,
                email,
                sifre_hash: await bcrypt.hash(String(sifre), 12),
                rol: "OGRETMEN",
                aktif: true
            }
        });

        await tx.ogretmenler.update({
            where: { id },
            data: { kullanici_id: yeni.id }
        });

        return yeni;
    });

    return res.status(201).json({
        mesaj: "Öğretmen kullanıcı hesabı oluşturuldu",
        kullanici: {
            id: kullanici.id,
            kullanici_adi: kullanici.kullanici_adi,
            email: kullanici.email,
            rol: kullanici.rol
        }
    });
}

async function kendiProfilim(req, res) {
    const ogretmen = await prisma.ogretmenler.findFirst({
        where: { kullanici_id: req.kullanici.id, aktif: true }
    });

    if (!ogretmen) {
        return res.status(404).json({ mesaj: "Bu kullanıcıya bağlı öğretmen profili bulunamadı" });
    }

    return res.json(ogretmen);
}

async function kendiDerslerim(req, res) {
    const ogretmen = await prisma.ogretmenler.findFirst({
        where: { kullanici_id: req.kullanici.id, aktif: true },
        select: { id: true }
    });

    if (!ogretmen) {
        return res.status(404).json({ mesaj: "Öğretmen profili bulunamadı" });
    }

    const dersler = await prisma.dersler.findMany({
        where: { ogretmen_id: ogretmen.id, aktif: true },
        orderBy: { ders_adi: "asc" },
        include: {
            _count: {
                select: { ders_kayitlari: true, sinavlar: true }
            }
        }
    });

    return res.json(dersler);
}

async function kendiOgrencilerim(req, res) {
    const ogretmen = await prisma.ogretmenler.findFirst({
        where: { kullanici_id: req.kullanici.id, aktif: true },
        select: { id: true }
    });

    if (!ogretmen) {
        return res.status(404).json({ mesaj: "Öğretmen profili bulunamadı" });
    }

    const kayitlar = await prisma.ders_kayitlari.findMany({
        where: {
            aktif: true,
            dersler: { ogretmen_id: ogretmen.id, aktif: true },
            ogrenciler: { aktif: true }
        },
        orderBy: [
            { ogrenciler: { soyad: "asc" } },
            { ogrenciler: { ad: "asc" } }
        ],
        include: {
            ogrenciler: true,
            dersler: {
                select: { id: true, ders_kodu: true, ders_adi: true }
            }
        }
    });

    return res.json(kayitlar);
}

module.exports = {
    listele,
    getir,
    ekle,
    guncelle,
    pasifYap,
    hesapOlustur,
    kendiProfilim,
    kendiDerslerim,
    kendiOgrencilerim
};

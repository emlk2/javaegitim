const bcrypt = require("bcryptjs");
const prisma = require("../config/prisma");
const { pozitifTamSayi, temizMetin } = require("../utils/yardimcilar");

function ogrenciVerisi(body) {
    return {
        ad: temizMetin(body.ad),
        soyad: temizMetin(body.soyad),
        ogrenciNo: temizMetin(body.ogrenci_no)
    };
}

async function listele(req, res) {
    const ogrenciler = await prisma.ogrenciler.findMany({
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

    return res.json(ogrenciler);
}

async function getir(req, res) {
    const id = pozitifTamSayi(req.params.id);

    if (!id) {
        return res.status(400).json({ mesaj: "Geçerli bir öğrenci ID'si giriniz" });
    }

    const ogrenci = await prisma.ogrenciler.findFirst({
        where: { id, aktif: true },
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

    if (!ogrenci) {
        return res.status(404).json({ mesaj: "Öğrenci bulunamadı" });
    }

    return res.json(ogrenci);
}

async function ekle(req, res) {
    const { ad, soyad, ogrenciNo } = ogrenciVerisi(req.body);
    const kullaniciAdi = temizMetin(req.body.kullanici_adi);
    const email = temizMetin(req.body.email).toLowerCase();
    const sifre = req.body.sifre;
    const hesapBilgisiVar = Boolean(kullaniciAdi || email || sifre);

    if (!ad || !soyad || !ogrenciNo) {
        return res.status(400).json({
            mesaj: "Ad, soyad ve öğrenci numarası zorunludur"
        });
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
                    rol: "OGRENCI",
                    aktif: true
                }
            });
        }

        const ogrenci = await tx.ogrenciler.create({
            data: {
                ad,
                soyad,
                ogrenci_no: ogrenciNo,
                aktif: true,
                kullanici_id: kullanici?.id || null
            }
        });

        return { ogrenci, kullanici };
    });

    return res.status(201).json({
        mesaj: "Öğrenci başarıyla eklendi",
        ...sonuc
    });
}

async function guncelle(req, res) {
    const id = pozitifTamSayi(req.params.id);
    const { ad, soyad, ogrenciNo } = ogrenciVerisi(req.body);

    if (!id) {
        return res.status(400).json({ mesaj: "Geçerli bir öğrenci ID'si giriniz" });
    }

    if (!ad || !soyad || !ogrenciNo) {
        return res.status(400).json({
            mesaj: "Ad, soyad ve öğrenci numarası zorunludur"
        });
    }

    const mevcut = await prisma.ogrenciler.findFirst({
        where: { id, aktif: true }
    });

    if (!mevcut) {
        return res.status(404).json({ mesaj: "Güncellenecek öğrenci bulunamadı" });
    }

    const ogrenci = await prisma.ogrenciler.update({
        where: { id },
        data: {
            ad,
            soyad,
            ogrenci_no: ogrenciNo
        }
    });

    return res.json({
        mesaj: "Öğrenci başarıyla güncellendi",
        ogrenci
    });
}

async function pasifYap(req, res) {
    const id = pozitifTamSayi(req.params.id);

    if (!id) {
        return res.status(400).json({ mesaj: "Geçerli bir öğrenci ID'si giriniz" });
    }

    const mevcut = await prisma.ogrenciler.findFirst({
        where: { id, aktif: true }
    });

    if (!mevcut) {
        return res.status(404).json({ mesaj: "Pasif yapılacak öğrenci bulunamadı" });
    }

    const ogrenci = await prisma.$transaction(async (tx) => {
        const guncellenen = await tx.ogrenciler.update({
            where: { id },
            data: {
                aktif: false,
                silinme_tarihi: new Date()
            }
        });

        if (mevcut.kullanici_id) {
            await tx.kullanicilar.update({
                where: { id: mevcut.kullanici_id },
                data: { aktif: false }
            });
        }

        return guncellenen;
    });

    return res.json({
        mesaj: "Öğrenci pasif duruma getirildi",
        ogrenci
    });
}

async function hesapOlustur(req, res) {
    const id = pozitifTamSayi(req.params.id);
    const kullaniciAdi = temizMetin(req.body.kullanici_adi);
    const email = temizMetin(req.body.email).toLowerCase();
    const sifre = req.body.sifre;

    if (!id) {
        return res.status(400).json({ mesaj: "Geçerli bir öğrenci ID'si giriniz" });
    }

    if (!kullaniciAdi || !email || !sifre) {
        return res.status(400).json({
            mesaj: "Kullanıcı adı, e-posta ve şifre zorunludur"
        });
    }

    if (String(sifre).length < 6) {
        return res.status(400).json({ mesaj: "Şifre en az 6 karakter olmalıdır" });
    }

    const ogrenci = await prisma.ogrenciler.findFirst({
        where: { id, aktif: true }
    });

    if (!ogrenci) {
        return res.status(404).json({ mesaj: "Öğrenci bulunamadı" });
    }

    if (ogrenci.kullanici_id) {
        return res.status(409).json({ mesaj: "Bu öğrencinin zaten kullanıcı hesabı var" });
    }

    const kullanici = await prisma.$transaction(async (tx) => {
        const yeniKullanici = await tx.kullanicilar.create({
            data: {
                kullanici_adi: kullaniciAdi,
                email,
                sifre_hash: await bcrypt.hash(String(sifre), 12),
                rol: "OGRENCI",
                aktif: true
            }
        });

        await tx.ogrenciler.update({
            where: { id },
            data: { kullanici_id: yeniKullanici.id }
        });

        return yeniKullanici;
    });

    return res.status(201).json({
        mesaj: "Öğrenci kullanıcı hesabı oluşturuldu",
        kullanici: {
            id: kullanici.id,
            kullanici_adi: kullanici.kullanici_adi,
            email: kullanici.email,
            rol: kullanici.rol
        }
    });
}

async function kendiProfilim(req, res) {
    const ogrenci = await prisma.ogrenciler.findFirst({
        where: {
            kullanici_id: req.kullanici.id,
            aktif: true
        }
    });

    if (!ogrenci) {
        return res.status(404).json({ mesaj: "Bu kullanıcıya bağlı öğrenci profili bulunamadı" });
    }

    return res.json(ogrenci);
}

async function kendiNotlarim(req, res) {
    const ogrenci = await prisma.ogrenciler.findFirst({
        where: { kullanici_id: req.kullanici.id, aktif: true },
        select: { id: true }
    });

    if (!ogrenci) {
        return res.status(404).json({ mesaj: "Öğrenci profili bulunamadı" });
    }

    const notlar = await prisma.notlar.findMany({
        where: { ogrenci_id: ogrenci.id },
        orderBy: { guncelleme_tarihi: "desc" },
        include: {
            sinavlar: {
                include: {
                    dersler: {
                        select: { id: true, ders_kodu: true, ders_adi: true }
                    }
                }
            }
        }
    });

    return res.json(notlar);
}

async function kendiDevamsizliklarim(req, res) {
    const ogrenci = await prisma.ogrenciler.findFirst({
        where: { kullanici_id: req.kullanici.id, aktif: true },
        select: { id: true }
    });

    if (!ogrenci) {
        return res.status(404).json({ mesaj: "Öğrenci profili bulunamadı" });
    }

    const kayitlar = await prisma.devamsizliklar.findMany({
        where: { ogrenci_id: ogrenci.id },
        orderBy: { tarih: "desc" },
        include: {
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
    kendiNotlarim,
    kendiDevamsizliklarim
};

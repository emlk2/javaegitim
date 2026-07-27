const path = require("path");

require("dotenv").config({
    path: path.join(__dirname, "../.env")
});
const express = require("express");
const bcrypt = require("bcryptjs");
const jwt = require("jsonwebtoken");
const kimlikDogrula =
    require("./middleware/kimlikDogrula");
const rolDogrula =
    require("./middleware/rolDogrula");
const { PrismaPg } = require("@prisma/adapter-pg");
const { PrismaClient } = require("../generated/prisma/client");
if (!process.env.DATABASE_URL) {
    throw new Error("DATABASE_URL .env dosyasında bulunamadı.");
}

if (!process.env.JWT_SECRET) {
    throw new Error("JWT_SECRET .env dosyasında bulunamadı.");
}

const adapter = new PrismaPg({
    connectionString: process.env.DATABASE_URL
});

const prisma = new PrismaClient({
    adapter
});
console.log(
    "Prisma modelleri:",
    Object.keys(prisma).filter(
        (anahtar) =>
            !anahtar.startsWith("$") &&
            !anahtar.startsWith("_")
    )
);

const app = express();
const PORT = 3000;

app.use(express.json());
app.use(
    express.static(
        path.join(__dirname, "../frontend")
    )
);
/* =====================================================
   GİRİŞ
===================================================== */

app.post("/api/auth/giris", async (req, res) => {
    const kullaniciAdi =
        req.body.kullanici_adi?.trim();

    const sifre =
        req.body.sifre;

    if (!kullaniciAdi || !sifre) {
        return res.status(400).json({
            mesaj: "Kullanıcı adı ve şifre zorunludur"
        });
    }

    try {
        const kullanici =
            await prisma.kullanicilar.findUnique({
                where: {
                    kullanici_adi: kullaniciAdi
                }
            });

        if (!kullanici) {
            return res.status(401).json({
                mesaj: "Kullanıcı adı veya şifre hatalı"
            });
        }

        if (!kullanici.aktif) {
            return res.status(403).json({
                mesaj: "Bu kullanıcı hesabı pasif durumdadır"
            });
        }

        const sifreDogruMu =
            await bcrypt.compare(
                sifre,
                kullanici.sifre_hash
            );

        if (!sifreDogruMu) {
            return res.status(401).json({
                mesaj: "Kullanıcı adı veya şifre hatalı"
            });
        }

        const token = jwt.sign(
            {
                kullaniciId: kullanici.id,
                rol: kullanici.rol
            },
            process.env.JWT_SECRET,
            {
                expiresIn: "2h"
            }
        );

        return res.json({
            mesaj: "Giriş başarılı",

            token,

            kullanici: {
                id: kullanici.id,
                kullanici_adi: kullanici.kullanici_adi,
                email: kullanici.email,
                rol: kullanici.rol
            }
        });

    } catch (hata) {
        console.error("Giriş hatası:", hata);

        return res.status(500).json({
            mesaj: "Giriş işlemi gerçekleştirilemedi",
            hata: hata.message
        });
    }
});
app.get(
    "/api/auth/ben",
    kimlikDogrula,
    async (req, res) => {
        try {
            const kullanici =
                await prisma.kullanicilar.findUnique({
                    where: {
                        id: req.kullanici.id
                    },
                    select: {
                        id: true,
                        kullanici_adi: true,
                        email: true,
                        rol: true,
                        aktif: true
                    }
                });

            if (!kullanici || !kullanici.aktif) {
                return res.status(401).json({
                    mesaj: "Kullanıcı bulunamadı veya pasif"
                });
            }

            return res.json({
                mesaj: "Token geçerli",
                kullanici: kullanici
            });

        } catch (hata) {
            console.error(
                "Kullanıcı bilgisi hatası:",
                hata
            );

            return res.status(500).json({
                mesaj: "Kullanıcı bilgisi alınamadı"
            });
        }
    }
);

/* =====================================================
   TÜM AKTİF ÖĞRENCİLERİ GETİR
===================================================== */

app.get("/api/ogrenciler", kimlikDogrula , rolDogrula("admin") , async (req, res) => {
    try {
        const ogrenciler =
            await prisma.ogrenciler.findMany({
                where: {
                    aktif: true
                },
                orderBy: {
                    id: "asc"
                }
            });

        return res.json(ogrenciler);

    } catch (hata) {
        console.error("Öğrencileri getirme hatası:", hata);

        return res.status(500).json({
            mesaj: "Öğrenciler getirilemedi",
            hata: hata.message
        });
    }
});

// =====================================================
  // ID'YE GÖRE TEK ÖĞRENCİ GETİR

app.get("/api/ogrenciler/:id", kimlikDogrula , rolDogrula("admin") , async (req, res) => {
    const id = Number(req.params.id);

    if (!Number.isInteger(id) || id <= 0) {
        return res.status(400).json({
            mesaj: "Geçerli bir öğrenci ID'si giriniz"
        });
    }

    try {
        const ogrenci =
            await prisma.ogrenciler.findFirst({
                where: {
                    id: id,
                    aktif: true
                }
            });

        if (!ogrenci) {
            return res.status(404).json({
                mesaj: "Öğrenci bulunamadı"
            });
        }

        return res.json(ogrenci);

    } catch (hata) {
        console.error("Öğrenci getirme hatası:", hata);

        return res.status(500).json({
            mesaj: "Öğrenci getirilemedi",
            hata: hata.message
        });
    }
});

/* =====================================================
   YENİ ÖĞRENCİ VE ÖĞRENCİ HESABI OLUŞTUR
===================================================== */

app.post(
    "/api/ogrenciler",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        const ad = req.body.ad?.trim();
        const soyad = req.body.soyad?.trim();
        const ogrenciNo = req.body.ogrenci_no?.trim();

        const kullaniciAdi =
            req.body.kullanici_adi?.trim();

        const email =
            req.body.email?.trim().toLowerCase();

        const sifre = req.body.sifre;

        if (
            !ad ||
            !soyad ||
            !ogrenciNo ||
            !kullaniciAdi ||
            !email ||
            !sifre
        ) {
            return res.status(400).json({
                mesaj:
                    "Ad, soyad, öğrenci numarası, kullanıcı adı, e-posta ve şifre zorunludur"
            });
        }

        if (sifre.length < 8) {
            return res.status(400).json({
                mesaj: "Şifre en az 8 karakter olmalıdır"
            });
        }

        try {
            const mevcutKullanici =
                await prisma.kullanicilar.findFirst({
                    where: {
                        OR: [
                            {
                                kullanici_adi: kullaniciAdi
                            },
                            {
                                email: email
                            }
                        ]
                    }
                });

            if (mevcutKullanici) {
                return res.status(409).json({
                    mesaj:
                        "Bu kullanıcı adı veya e-posta zaten kullanılıyor"
                });
            }

            const mevcutOgrenci =
                await prisma.ogrenciler.findUnique({
                    where: {
                        ogrenci_no: ogrenciNo
                    }
                });

            if (mevcutOgrenci) {
                return res.status(409).json({
                    mesaj:
                        "Bu öğrenci numarası zaten kullanılıyor"
                });
            }

            const sifreHash =
                await bcrypt.hash(sifre, 12);

            const sonuc =
                await prisma.$transaction(
                    async (transaction) => {
                        const kullanici =
                            await transaction.kullanicilar.create({
                                data: {
                                    kullanici_adi: kullaniciAdi,
                                    email: email,
                                    sifre_hash: sifreHash,
                                    rol: "ogrenci",
                                    aktif: true
                                },
                                select: {
                                    id: true,
                                    kullanici_adi: true,
                                    email: true,
                                    rol: true,
                                    aktif: true
                                }
                            });

                        const ogrenci =
                            await transaction.ogrenciler.create({
                                data: {
                                    ad: ad,
                                    soyad: soyad,
                                    ogrenci_no: ogrenciNo,
                                    aktif: true,
                                    kullanici_id: kullanici.id
                                }
                            });

                        return {
                            kullanici,
                            ogrenci
                        };
                    }
                );

            return res.status(201).json({
                mesaj:
                    "Öğrenci ve kullanıcı hesabı başarıyla oluşturuldu",
                kullanici: sonuc.kullanici,
                ogrenci: sonuc.ogrenci
            });

        } catch (hata) {
            console.error(
                "Öğrenci oluşturma hatası:",
                hata
            );

            if (hata.code === "P2002") {
                return res.status(409).json({
                    mesaj:
                        "Kullanıcı adı, e-posta veya öğrenci numarası zaten kullanılıyor"
                });
            }

            return res.status(500).json({
                mesaj: "Öğrenci oluşturulamadı",
                hata: hata.message
            });
        }
    }
);
/* =====================================================
   ÖĞRENCİ GÜNCELLE
===================================================== */

app.put("/api/ogrenciler/:id", kimlikDogrula, rolDogrula("admin") ,  async (req, res) => {
    const id = Number(req.params.id);

    const ad = req.body.ad?.trim();
    const soyad = req.body.soyad?.trim();
    const ogrenciNo = req.body.ogrenci_no?.trim();

    if (!Number.isInteger(id) || id <= 0) {
        return res.status(400).json({
            mesaj: "Geçerli bir öğrenci ID'si giriniz"
        });
    }

    if (!ad || !soyad || !ogrenciNo) {
        return res.status(400).json({
            mesaj: "Ad, soyad ve öğrenci numarası zorunludur"
        });
    }

    try {
        const mevcutOgrenci =
            await prisma.ogrenciler.findFirst({
                where: {
                    id: id,
                    aktif: true
                }
            });

        if (!mevcutOgrenci) {
            return res.status(404).json({
                mesaj: "Güncellenecek öğrenci bulunamadı"
            });
        }

        const guncellenenOgrenci =
            await prisma.ogrenciler.update({
                where: {
                    id: id
                },
                data: {
                    ad: ad,
                    soyad: soyad,
                    ogrenci_no: ogrenciNo
                }
            });

        return res.json({
            mesaj: "Öğrenci başarıyla güncellendi",
            ogrenci: guncellenenOgrenci
        });

    } catch (hata) {
        console.error("Öğrenci güncelleme hatası:", hata);

        if (hata.code === "P2002") {
            return res.status(409).json({
                mesaj: "Bu öğrenci numarası başka bir öğrenciye ait"
            });
        }

        return res.status(500).json({
            mesaj: "Öğrenci güncellenemedi",
            hata: hata.message
        });
    }
});

/* =====================================================
   ÖĞRENCİYİ PASİF YAP — SOFT DELETE
===================================================== */

app.delete("/api/ogrenciler/:id", kimlikDogrula , rolDogrula("admin"),  async (req, res) => {
    const id = Number(req.params.id);

    if (!Number.isInteger(id) || id <= 0) {
        return res.status(400).json({
            mesaj: "Geçerli bir öğrenci ID'si giriniz"
        });
    }

    try {
        const mevcutOgrenci =
            await prisma.ogrenciler.findFirst({
                where: {
                    id: id,
                    aktif: true
                }
            });

        if (!mevcutOgrenci) {
            return res.status(404).json({
                mesaj: "Pasif yapılacak öğrenci bulunamadı"
            });
        }

        const pasifOgrenci =
            await prisma.ogrenciler.update({
                where: {
                    id: id
                },
                data: {
                    aktif: false,
                    silinme_tarihi: new Date()
                }
            });

        return res.json({
            mesaj: "Öğrenci pasif duruma getirildi",
            ogrenci: pasifOgrenci
        });

    } catch (hata) {
        console.error("Öğrenciyi pasifleştirme hatası:", hata);

        return res.status(500).json({
            mesaj: "Öğrenci pasif duruma getirilemedi",
            hata: hata.message
        });
    }
});
/* =====================================================
   TÜM AKTİF ÖĞRETMENLERİ GETİR
===================================================== */

app.get(
    "/api/ogretmenler",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const ogretmenler =
                await prisma.ogretmenler.findMany({
                    where: {
                        aktif: true
                    },
                    orderBy: {
                        id: "asc"
                    },
                    select: {
                        id: true,
                        ad: true,
                        soyad: true,
                        sicil_no: true,
                        brans: true,
                        aktif: true,
                        kayit_tarihi: true,
                        guncelleme_tarihi: true,
                        kullanici_id: true
                    }
                });

            return res.json(ogretmenler);

        } catch (hata) {
            console.error(
                "Öğretmenleri getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj: "Öğretmenler getirilemedi",
                hata: hata.message
            });
        }
    }
);

/* =====================================================
   YENİ ÖĞRETMEN VE ÖĞRETMEN HESABI OLUŞTUR
===================================================== */

app.post(
    "/api/ogretmenler",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        const ad = req.body.ad?.trim();
        const soyad = req.body.soyad?.trim();
        const sicilNo = req.body.sicil_no?.trim();
        const brans = req.body.brans?.trim();

        const kullaniciAdi =
            req.body.kullanici_adi?.trim();

        const email =
            req.body.email?.trim().toLowerCase();

        const sifre = req.body.sifre;

        if (
            !ad ||
            !soyad ||
            !sicilNo ||
            !brans ||
            !kullaniciAdi ||
            !email ||
            !sifre
        ) {
            return res.status(400).json({
                mesaj:
                    "Ad, soyad, sicil numarası, branş, kullanıcı adı, e-posta ve şifre zorunludur"
            });
        }

        if (sifre.length < 8) {
            return res.status(400).json({
                mesaj: "Şifre en az 8 karakter olmalıdır"
            });
        }

        try {
            const mevcutKullanici =
                await prisma.kullanicilar.findFirst({
                    where: {
                        OR: [
                            {
                                kullanici_adi: kullaniciAdi
                            },
                            {
                                email: email
                            }
                        ]
                    }
                });

            if (mevcutKullanici) {
                return res.status(409).json({
                    mesaj:
                        "Bu kullanıcı adı veya e-posta zaten kullanılıyor"
                });
            }

            const mevcutOgretmen =
                await prisma.ogretmenler.findUnique({
                    where: {
                        sicil_no: sicilNo
                    }
                });

            if (mevcutOgretmen) {
                return res.status(409).json({
                    mesaj:
                        "Bu sicil numarası zaten kullanılıyor"
                });
            }

            const sifreHash =
                await bcrypt.hash(sifre, 12);

            const sonuc =
                await prisma.$transaction(
                    async (transaction) => {
                        const kullanici =
                            await transaction.kullanicilar.create({
                                data: {
                                    kullanici_adi: kullaniciAdi,
                                    email: email,
                                    sifre_hash: sifreHash,
                                    rol: "ogretmen",
                                    aktif: true
                                },
                                select: {
                                    id: true,
                                    kullanici_adi: true,
                                    email: true,
                                    rol: true,
                                    aktif: true
                                }
                            });

                        const ogretmen =
                            await transaction.ogretmenler.create({
                                data: {
                                    ad: ad,
                                    soyad: soyad,
                                    sicil_no: sicilNo,
                                    brans: brans,
                                    aktif: true,
                                    kullanici_id: kullanici.id
                                }
                            });

                        return {
                            kullanici,
                            ogretmen
                        };
                    }
                );

            return res.status(201).json({
                mesaj:
                    "Öğretmen ve kullanıcı hesabı başarıyla oluşturuldu",
                kullanici: sonuc.kullanici,
                ogretmen: sonuc.ogretmen
            });

        } catch (hata) {
            console.error(
                "Öğretmen oluşturma hatası:",
                hata
            );

            if (hata.code === "P2002") {
                return res.status(409).json({
                    mesaj:
                        "Kullanıcı adı, e-posta veya sicil numarası zaten kullanılıyor"
                });
            }

            return res.status(500).json({
                mesaj: "Öğretmen oluşturulamadı",
                hata: hata.message
            });
        }
    }
);

/* =====================================================
   VERİTABANI BAĞLANTI TESTİ
===================================================== */

app.get("/db-test", async (req, res) => {
    try {
        await prisma.$connect();

        const kullaniciSayisi =
            await prisma.kullanicilar.count();

        const aktifOgrenciSayisi =
            await prisma.ogrenciler.count({
                where: {
                    aktif: true
                }
            });

        return res.json({
            mesaj: "Prisma ve PostgreSQL bağlantısı başarılı",
            kullaniciSayisi: kullaniciSayisi,
            aktifOgrenciSayisi: aktifOgrenciSayisi
        });

    } catch (hata) {
        console.error("Veritabanı bağlantı hatası:", hata);

        return res.status(500).json({
            mesaj: "Veritabanı bağlantısı başarısız",
            hata: hata.message
        });
    }
});

/* =====================================================
   AKTİF DERSLERİ LİSTELE
===================================================== */

app.get(
    "/api/dersler",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const dersler = await prisma.dersler.findMany({
                where: {
                    aktif: true
                },
                orderBy: {
                    ders_adi: "asc"
                }
            });

            return res.json(dersler);
        } catch (hata) {
            console.error("Dersleri getirme hatası:", hata);

            return res.status(500).json({
                mesaj: "Dersler getirilemedi",
                hata: hata.message
            });
        }
    }
);


/* =====================================================
   YENİ DERS OLUŞTUR
===================================================== */

app.post(
    "/api/dersler",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        const dersKodu =
            req.body.ders_kodu?.trim().toUpperCase();

        const dersAdi =
            req.body.ders_adi?.trim();

        const aciklama =
            req.body.aciklama?.trim() || null;

        if (!dersKodu || !dersAdi) {
            return res.status(400).json({
                mesaj: "Ders kodu ve ders adı zorunludur"
            });
        }

        try {
            const mevcutDers =
                await prisma.dersler.findUnique({
                    where: {
                        ders_kodu: dersKodu
                    }
                });

            if (mevcutDers) {
                return res.status(409).json({
                    mesaj: "Bu ders kodu zaten kullanılıyor"
                });
            }

            const yeniDers =
                await prisma.dersler.create({
                    data: {
                        ders_kodu: dersKodu,
                        ders_adi: dersAdi,
                        aciklama: aciklama,
                        aktif: true
                    }
                });

            return res.status(201).json({
                mesaj: "Ders başarıyla oluşturuldu",
                ders: yeniDers
            });
        } catch (hata) {
            console.error("Ders oluşturma hatası:", hata);

            if (hata.code === "P2002") {
                return res.status(409).json({
                    mesaj: "Bu ders kodu zaten kullanılıyor"
                });
            }

            return res.status(500).json({
                mesaj: "Ders oluşturulamadı",
                hata: hata.message
            });
        }
    }
);
/* =====================================================
   ÖĞRETMENE DERS ATAMA
===================================================== */

app.post(
    "/api/ogretmen-ders-atamalari",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        const ogretmenId = Number(req.body.ogretmen_id);
        const dersId = Number(req.body.ders_id);

        if (
            !Number.isInteger(ogretmenId) ||
            ogretmenId <= 0 ||
            !Number.isInteger(dersId) ||
            dersId <= 0
        ) {
            return res.status(400).json({
                mesaj:
                    "Geçerli öğretmen ve ders kimliği gönderilmelidir"
            });
        }

        try {
            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        id: ogretmenId,
                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj: "Aktif öğretmen bulunamadı"
                });
            }

            const ders =
                await prisma.dersler.findFirst({
                    where: {
                        id: dersId,
                        aktif: true
                    }
                });

            if (!ders) {
                return res.status(404).json({
                    mesaj: "Aktif ders bulunamadı"
                });
            }

            const mevcutAtama =
                await prisma.ogretmen_dersleri.findUnique({
                    where: {
                        ogretmen_id_ders_id: {
                            ogretmen_id: ogretmenId,
                            ders_id: dersId
                        }
                    }
                });

            if (mevcutAtama?.aktif) {
                return res.status(409).json({
                    mesaj:
                        "Bu ders zaten bu öğretmene atanmış"
                });
            }

            let atama;

            if (mevcutAtama) {
                atama =
                    await prisma.ogretmen_dersleri.update({
                        where: {
                            id: mevcutAtama.id
                        },
                        data: {
                            aktif: true
                        },
                        include: {
                            ogretmen: true,
                            ders: true
                        }
                    });
            } else {
                atama =
                    await prisma.ogretmen_dersleri.create({
                        data: {
                            ogretmen_id: ogretmenId,
                            ders_id: dersId,
                            aktif: true
                        },
                        include: {
                            ogretmen: true,
                            ders: true
                        }
                    });
            }

            return res.status(201).json({
                mesaj: "Ders öğretmene başarıyla atandı",
                atama
            });

        } catch (hata) {
            console.error("Ders atama hatası:", hata);

            if (hata.code === "P2002") {
                return res.status(409).json({
                    mesaj:
                        "Bu ders zaten bu öğretmene atanmış"
                });
            }

            return res.status(500).json({
                mesaj: "Ders öğretmene atanamadı",
                hata: hata.message
            });
        }
    }
);

/* =====================================================
   ÖĞRETMENİN KENDİ DERSLERİNİ GETİR
===================================================== */

app.get(
    "/api/ogretmen/derslerim",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id: req.kullanici.id,
                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj: "Öğretmen profili bulunamadı"
                });
            }

            const atamalar =
                await prisma.ogretmen_dersleri.findMany({
                    where: {
                        ogretmen_id: ogretmen.id,
                        aktif: true,
                        ders: {
                            aktif: true
                        }
                    },
                    include: {
                        ders: true
                    },
                    orderBy: {
                        atama_tarihi: "desc"
                    }
                });

            const dersler = atamalar.map((atama) => ({
                atama_id: atama.id,
                atama_tarihi: atama.atama_tarihi,
                id: atama.ders.id,
                ders_kodu: atama.ders.ders_kodu,
                ders_adi: atama.ders.ders_adi,
                aciklama: atama.ders.aciklama
            }));

            return res.json({
                ogretmen: {
                    id: ogretmen.id,
                    ad: ogretmen.ad,
                    soyad: ogretmen.soyad,
                    sicil_no: ogretmen.sicil_no,
                    brans: ogretmen.brans
                },
                dersler
            });

        } catch (hata) {
            console.error(
                "Öğretmenin derslerini getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj: "Öğretmenin dersleri getirilemedi",
                hata: hata.message
            });
        }
    }
);
/* =====================================================
   ÖĞRENCİYİ DERSE KAYDET
===================================================== */

app.post(
    "/api/ogrenci-ders-kayitlari",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        const ogrenciId = Number(req.body.ogrenci_id);
        const dersId = Number(req.body.ders_id);

        if (
            !Number.isInteger(ogrenciId) ||
            ogrenciId <= 0 ||
            !Number.isInteger(dersId) ||
            dersId <= 0
        ) {
            return res.status(400).json({
                mesaj:
                    "Geçerli öğrenci ve ders kimliği gönderilmelidir"
            });
        }

        try {
            const ogrenci =
                await prisma.ogrenciler.findFirst({
                    where: {
                        id: ogrenciId,
                        aktif: true
                    }
                });

            if (!ogrenci) {
                return res.status(404).json({
                    mesaj: "Aktif öğrenci bulunamadı"
                });
            }

            const ders =
                await prisma.dersler.findFirst({
                    where: {
                        id: dersId,
                        aktif: true
                    }
                });

            if (!ders) {
                return res.status(404).json({
                    mesaj: "Aktif ders bulunamadı"
                });
            }

            const mevcutKayit =
                await prisma.ogrenci_dersleri.findUnique({
                    where: {
                        ogrenci_id_ders_id: {
                            ogrenci_id: ogrenciId,
                            ders_id: dersId
                        }
                    }
                });

            if (mevcutKayit?.aktif) {
                return res.status(409).json({
                    mesaj:
                        "Öğrenci zaten bu derse kayıtlı"
                });
            }

            let kayit;

            if (mevcutKayit) {
                kayit =
                    await prisma.ogrenci_dersleri.update({
                        where: {
                            id: mevcutKayit.id
                        },
                        data: {
                            aktif: true
                        },
                        include: {
                            ogrenci: true,
                            ders: true
                        }
                    });
            } else {
                kayit =
                    await prisma.ogrenci_dersleri.create({
                        data: {
                            ogrenci_id: ogrenciId,
                            ders_id: dersId,
                            aktif: true
                        },
                        include: {
                            ogrenci: true,
                            ders: true
                        }
                    });
            }

            return res.status(201).json({
                mesaj: "Öğrenci derse başarıyla kaydedildi",
                kayit
            });

        } catch (hata) {
            console.error(
                "Öğrenci ders kayıt hatası:",
                hata
            );

            if (hata.code === "P2002") {
                return res.status(409).json({
                    mesaj:
                        "Öğrenci zaten bu derse kayıtlı"
                });
            }

            return res.status(500).json({
                mesaj: "Öğrenci derse kaydedilemedi",
                hata: hata.message
            });
        }
    }
);

/* =====================================================
   ÖĞRENCİNİN KENDİ DERSLERİNİ GETİR
===================================================== */

app.get(
    "/api/ogrenci/derslerim",
    kimlikDogrula,
    rolDogrula("ogrenci"),
    async (req, res) => {
        try {
            const ogrenci = await prisma.ogrenciler.findFirst({
                where: {
                    kullanici_id: req.kullanici.id,
                    aktif: true
                }
            });

            if (!ogrenci) {
                return res.status(404).json({
                    mesaj: "Öğrenci profili bulunamadı"
                });
            }

            const kayitlar = await prisma.ogrenci_dersleri.findMany({
                where: {
                    ogrenci_id: ogrenci.id,
                    aktif: true,
                    ders: {
                        aktif: true
                    }
                },
                include: {
                    ders: true
                },
                orderBy: {
                    kayit_tarihi: "desc"
                }
            });

            const dersler = kayitlar.map((kayit) => ({
                kayit_id: kayit.id,
                kayit_tarihi: kayit.kayit_tarihi,
                id: kayit.ders.id,
                ders_kodu: kayit.ders.ders_kodu,
                ders_adi: kayit.ders.ders_adi,
                aciklama: kayit.ders.aciklama
            }));

            return res.json({
                ogrenci: {
                    id: ogrenci.id,
                    ad: ogrenci.ad,
                    soyad: ogrenci.soyad,
                    ogrenci_no: ogrenci.ogrenci_no
                },
                dersler
            });

        } catch (hata) {
            console.error(
                "Öğrencinin derslerini getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj: "Öğrencinin dersleri getirilemedi",
                hata: hata.message
            });
        }
    }
);
/* =====================================================
   ÖĞRETMENİN SINAV OLUŞTURMASI
===================================================== */

app.post(
    "/api/ogretmen/sinavlar",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        const dersId = Number(req.body.ders_id);
        const sinavAdi = req.body.sinav_adi?.trim();
        const sinavTarihi = new Date(req.body.sinav_tarihi);
        const maksimumPuan =
            req.body.maksimum_puan === undefined
                ? 100
                : Number(req.body.maksimum_puan);

        const aciklama =
            req.body.aciklama?.trim() || null;

        if (!Number.isInteger(dersId) || dersId <= 0) {
            return res.status(400).json({
                mesaj: "Geçerli bir ders kimliği gönderilmelidir"
            });
        }

        if (!sinavAdi) {
            return res.status(400).json({
                mesaj: "Sınav adı zorunludur"
            });
        }

        if (Number.isNaN(sinavTarihi.getTime())) {
            return res.status(400).json({
                mesaj: "Geçerli bir sınav tarihi gönderilmelidir"
            });
        }

        if (
            !Number.isFinite(maksimumPuan) ||
            maksimumPuan <= 0
        ) {
            return res.status(400).json({
                mesaj: "Maksimum puan sıfırdan büyük olmalıdır"
            });
        }

        try {
            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id: req.kullanici.id,
                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj: "Öğretmen profili bulunamadı"
                });
            }

            const dersAtamasi =
                await prisma.ogretmen_dersleri.findFirst({
                    where: {
                        ogretmen_id: ogretmen.id,
                        ders_id: dersId,
                        aktif: true,
                        ders: {
                            aktif: true
                        }
                    },
                    include: {
                        ders: true
                    }
                });

            if (!dersAtamasi) {
                return res.status(403).json({
                    mesaj:
                        "Bu ders size atanmadığı için sınav oluşturamazsınız"
                });
            }

            const yeniSinav =
                await prisma.sinavlar.create({
                    data: {
                        ders_id: dersId,
                        ogretmen_id: ogretmen.id,
                        sinav_adi: sinavAdi,
                        sinav_tarihi: sinavTarihi,
                        maksimum_puan: maksimumPuan,
                        aciklama,
                        aktif: true
                    },
                    include: {
                        ders: true,
                        ogretmen: true
                    }
                });

            return res.status(201).json({
                mesaj: "Sınav başarıyla oluşturuldu",
                sinav: yeniSinav
            });

        } catch (hata) {
            console.error("Sınav oluşturma hatası:", hata);

            return res.status(500).json({
                mesaj: "Sınav oluşturulamadı",
                hata: hata.message
            });
        }
    }
);

/* =====================================================
   ÖĞRETMENİN KENDİ SINAVLARINI GETİR
===================================================== */

app.get(
    "/api/ogretmen/sinavlarim",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id: req.kullanici.id,
                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj: "Öğretmen profili bulunamadı"
                });
            }

            const sinavlar =
                await prisma.sinavlar.findMany({
                    where: {
                        ogretmen_id: ogretmen.id,
                        aktif: true,
                        ders: {
                            aktif: true
                        }
                    },
                    include: {
                        ders: true
                    },
                    orderBy: {
                        sinav_tarihi: "desc"
                    }
                });

            return res.json({
                sinavlar
            });

        } catch (hata) {
            console.error(
                "Öğretmen sınavları getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj: "Sınavlar getirilemedi",
                hata: hata.message
            });
        }
    }
);
/* =====================================================
   ÖĞRENCİNİN KENDİ SINAVLARINI GETİR
===================================================== */

app.get(
    "/api/ogrenci/sinavlarim",
    kimlikDogrula,
    rolDogrula("ogrenci"),
    async (req, res) => {
        try {
            const ogrenci =
                await prisma.ogrenciler.findFirst({
                    where: {
                        kullanici_id: req.kullanici.id,
                        aktif: true
                    }
                });

            if (!ogrenci) {
                return res.status(404).json({
                    mesaj: "Öğrenci profili bulunamadı"
                });
            }

            const sinavlar =
                await prisma.sinavlar.findMany({
                    where: {
                        aktif: true,
                        ders: {
                            aktif: true,
                            ogrenci_kayitlari: {
                                some: {
                                    ogrenci_id: ogrenci.id,
                                    aktif: true
                                }
                            }
                        }
                    },
                    include: {
                        ders: true
                    },
                    orderBy: {
                        sinav_tarihi: "asc"
                    }
                });

            return res.json({
                sinavlar
            });

        } catch (hata) {
            console.error(
                "Öğrenci sınavları getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj: "Sınavlar getirilemedi",
                hata: hata.message
            });
        }
    }
);
/* =====================================================
   SINAVA KAYITLI ÖĞRENCİLERİ GETİR
===================================================== */

app.get(
    "/api/ogretmen/sinavlar/:sinavId/ogrenciler",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const sinavId = Number(req.params.sinavId);

            if (!Number.isInteger(sinavId) || sinavId <= 0) {
                return res.status(400).json({
                    mesaj: "Geçerli bir sınav ID değeri gönderilmelidir"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id: req.kullanici.id,
                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj: "Öğretmen profili bulunamadı"
                });
            }

            const sinav =
                await prisma.sinavlar.findFirst({
                    where: {
                        id: sinavId,
                        ogretmen_id: ogretmen.id,
                        aktif: true
                    },
                    include: {
                        ders: true
                    }
                });

            if (!sinav) {
                return res.status(404).json({
                    mesaj:
                        "Sınav bulunamadı veya bu sınavı görüntüleme yetkiniz yok"
                });
            }

            const ogrenciler =
                await prisma.ogrenciler.findMany({
                    where: {
                        aktif: true,

                        ders_kayitlari: {
                            some: {
                                ders_id: sinav.ders_id,
                                aktif: true
                            }
                        }
                    },

                    include: {
    notlars: {
        where: {
            sinav_id: sinavId,
            aktif: true
        },
        take: 1
    }
},

                    orderBy: [
                        {
                            ad: "asc"
                        },
                        {
                            soyad: "asc"
                        }
                    ]
                });

            const ogrenciListesi =
                ogrenciler.map((ogrenci) => {
                    const mevcutNot =
                        ogrenci.notlars[0] || null;

                    return {
                        id: ogrenci.id,
                        ad: ogrenci.ad,
                        soyad: ogrenci.soyad,
                        ogrenci_no: ogrenci.ogrenci_no,

                        not: mevcutNot
                            ? {
                                id: mevcutNot.id,
                                puan: mevcutNot.puan,
                                aciklama:
                                    mevcutNot.aciklama
                            }
                            : null
                    };
                });

            return res.json({
                sinav: {
                    id: sinav.id,
                    sinav_adi: sinav.sinav_adi,
                    sinav_tarihi: sinav.sinav_tarihi,
                    maksimum_puan: sinav.maksimum_puan,

                    ders: {
                        id: sinav.ders.id,
                        ders_kodu: sinav.ders.ders_kodu,
                        ders_adi: sinav.ders.ders_adi
                    }
                },

                ogrenciler: ogrenciListesi
            });

        } catch (hata) {
            console.error(
                "Sınav öğrencileri getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Sınava kayıtlı öğrenciler getirilemedi",

                hata: hata.message
            });
        }
    }
);

/* =====================================================
   SINAVA KAYITLI ÖĞRENCİLERİ GETİR
===================================================== */

app.get(
    "/api/ogretmen/sinavlar/:sinavId/ogrenciler",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const sinavId = Number(req.params.sinavId);

            if (!Number.isInteger(sinavId) || sinavId <= 0) {
                return res.status(400).json({
                    mesaj: "Geçerli bir sınav ID değeri gönderilmelidir"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id: req.kullanici.id,
                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj: "Öğretmen profili bulunamadı"
                });
            }

            const sinav =
                await prisma.sinavlar.findFirst({
                    where: {
                        id: sinavId,
                        ogretmen_id: ogretmen.id,
                        aktif: true
                    },
                    include: {
                        ders: true
                    }
                });

            if (!sinav) {
                return res.status(404).json({
                    mesaj:
                        "Sınav bulunamadı veya bu sınava erişim yetkiniz yok"
                });
            }

            const ogrenciler =
                await prisma.ogrenciler.findMany({
                    where: {
                        aktif: true,

                        ders_kayitlari: {
                            some: {
                                ders_id: sinav.ders_id,
                                aktif: true
                            }
                        }
                    },

                    include: {
                        notlar: {
                            where: {
                                sinav_id: sinav.id,
                                aktif: true
                            },
                            take: 1
                        }
                    },

                    orderBy: [
                        {
                            ad: "asc"
                        },
                        {
                            soyad: "asc"
                        }
                    ]
                });

            const ogrenciListesi =
                ogrenciler.map((ogrenci) => {
                    const mevcutNot =
                        ogrenci.notlar[0] || null;

                    return {
                        id: ogrenci.id,
                        ad: ogrenci.ad,
                        soyad: ogrenci.soyad,
                        ogrenci_no: ogrenci.ogrenci_no,

                        not: mevcutNot
                            ? {
                                id: mevcutNot.id,
                                puan: mevcutNot.puan,
                                aciklama: mevcutNot.aciklama
                            }
                            : null
                    };
                });

            return res.json({
                sinav: {
                    id: sinav.id,
                    sinav_adi: sinav.sinav_adi,
                    sinav_tarihi: sinav.sinav_tarihi,
                    maksimum_puan: sinav.maksimum_puan,

                    ders: {
                        id: sinav.ders.id,
                        ders_kodu: sinav.ders.ders_kodu,
                        ders_adi: sinav.ders.ders_adi
                    }
                },

                ogrenciler: ogrenciListesi
            });

        } catch (hata) {
            console.error(
                "Sınav öğrencileri getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj: "Sınava kayıtlı öğrenciler getirilemedi",
                hata: hata.message
            });
        }
    }
);
/* =====================================================
   ÖĞRENCİYE NOT EKLE VEYA GÜNCELLE
===================================================== */

app.post(
    "/api/ogretmen/sinavlar/:sinavId/notlar",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const sinavId = Number(req.params.sinavId);
            const ogrenciId = Number(req.body.ogrenci_id);
            const puan = Number(req.body.puan);
            const aciklama =
                String(req.body.aciklama || "").trim();

            if (!Number.isInteger(sinavId) || sinavId <= 0) {
                return res.status(400).json({
                    mesaj: "Geçerli bir sınav ID değeri gönderilmelidir"
                });
            }

            if (!Number.isInteger(ogrenciId) || ogrenciId <= 0) {
                return res.status(400).json({
                    mesaj: "Geçerli bir öğrenci ID değeri gönderilmelidir"
                });
            }

            if (!Number.isFinite(puan) || puan < 0) {
                return res.status(400).json({
                    mesaj: "Puan sıfırdan küçük olamaz"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id: req.kullanici.id,
                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj: "Öğretmen profili bulunamadı"
                });
            }

            const sinav =
                await prisma.sinavlar.findFirst({
                    where: {
                        id: sinavId,
                        ogretmen_id: ogretmen.id,
                        aktif: true
                    },
                    include: {
                        ders: true
                    }
                });

            if (!sinav) {
                return res.status(404).json({
                    mesaj:
                        "Sınav bulunamadı veya bu sınava not girme yetkiniz yok"
                });
            }

            if (puan > sinav.maksimum_puan) {
                return res.status(400).json({
                    mesaj:
                        `Puan, sınavın maksimum puanı olan ${sinav.maksimum_puan} değerinden büyük olamaz`
                });
            }

            const ogrenci =
                await prisma.ogrenciler.findFirst({
                    where: {
                        id: ogrenciId,
                        aktif: true,

                        ders_kayitlari: {
                            some: {
                                ders_id: sinav.ders_id,
                                aktif: true
                            }
                        }
                    }
                });

            if (!ogrenci) {
                return res.status(404).json({
                    mesaj:
                        "Öğrenci bulunamadı veya sınavın dersine kayıtlı değil"
                });
            }

            const notKaydi =
                await prisma.notlar.upsert({
                    where: {
                        sinav_id_ogrenci_id: {
                            sinav_id: sinav.id,
                            ogrenci_id: ogrenci.id
                        }
                    },

                    update: {
                        puan,
                        aciklama: aciklama || null,
                        aktif: true
                    },

                    create: {
                        sinav_id: sinav.id,
                        ogrenci_id: ogrenci.id,
                        puan,
                        aciklama: aciklama || null
                    },

                    include: {
                        sinav: {
                            include: {
                                ders: true
                            }
                        },
                        ogrenci: true
                    }
                });

            return res.status(200).json({
                mesaj: "Not başarıyla kaydedildi",
                not: notKaydi
            });

        } catch (hata) {
            console.error(
                "Not kaydetme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj: "Not kaydedilemedi",
                hata: hata.message
            });
        }
    }
);
/* =====================================================
   ÖĞRENCİNİN KENDİ NOTLARINI GETİR
===================================================== */

app.get(
    "/api/ogrenci/notlarim",
    kimlikDogrula,
    rolDogrula("ogrenci"),
    async (req, res) => {
        try {
            const ogrenci =
                await prisma.ogrenciler.findFirst({
                    where: {
                        kullanici_id: req.kullanici.id,
                        aktif: true
                    }
                });

            if (!ogrenci) {
                return res.status(404).json({
                    mesaj: "Öğrenci profili bulunamadı"
                });
            }

            const notlar =
                await prisma.notlar.findMany({
                    where: {
                        ogrenci_id: ogrenci.id,
                        aktif: true,

                        sinav: {
                            aktif: true,
                            ders: {
                                aktif: true
                            }
                        }
                    },

                    include: {
                        sinav: {
                            include: {
                                ders: true,
                                ogretmen: true
                            }
                        }
                    },

                    orderBy: {
                        olusturma_tarihi: "desc"
                    }
                });

            const notListesi =
                notlar.map((notKaydi) => ({
                    id: notKaydi.id,
                    puan: notKaydi.puan,
                    aciklama: notKaydi.aciklama,
                    olusturma_tarihi:
                        notKaydi.olusturma_tarihi,

                    sinav: {
                        id: notKaydi.sinav.id,
                        sinav_adi:
                            notKaydi.sinav.sinav_adi,
                        sinav_tarihi:
                            notKaydi.sinav.sinav_tarihi,
                        maksimum_puan:
                            notKaydi.sinav.maksimum_puan
                    },

                    ders: {
                        id: notKaydi.sinav.ders.id,
                        ders_kodu:
                            notKaydi.sinav.ders.ders_kodu,
                        ders_adi:
                            notKaydi.sinav.ders.ders_adi
                    },

                    ogretmen: {
                        id: notKaydi.sinav.ogretmen.id,
                        ad: notKaydi.sinav.ogretmen.ad,
                        soyad:
                            notKaydi.sinav.ogretmen.soyad
                    }
                }));

            return res.json({
                ogrenci: {
                    id: ogrenci.id,
                    ad: ogrenci.ad,
                    soyad: ogrenci.soyad,
                    ogrenci_no: ogrenci.ogrenci_no
                },

                notlar: notListesi
            });

        } catch (hata) {
            console.error(
                "Öğrenci notları getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj: "Notlar getirilemedi",
                hata: hata.message
            });
        }
    }
);
/* =====================================================
   DEVAMSIZLIK TARİHİ YARDIMCI FONKSİYONU
===================================================== */

function devamsizlikTarihiniOlustur(deger) {
    const tarihMetni = String(deger || "");

    if (
        !/^\d{4}-\d{2}-\d{2}$/.test(tarihMetni)
    ) {
        return null;
    }

    const tarih = new Date(
        `${tarihMetni}T00:00:00.000Z`
    );

    if (
        Number.isNaN(tarih.getTime()) ||
        tarih.toISOString().slice(0, 10) !== tarihMetni
    ) {
        return null;
    }

    return tarih;
}


/* =====================================================
   ÖĞRETMENİN DERS ÖĞRENCİLERİNİ VE
   DEVAMSIZLIK KAYITLARINI GETİRMESİ
===================================================== */

app.get(
    "/api/ogretmen/dersler/:dersId/devamsizliklar",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const dersId =
                Number(req.params.dersId);

            const devamsizlikTarihi =
                devamsizlikTarihiniOlustur(
                    req.query.tarih
                );

            if (
                !Number.isInteger(dersId) ||
                dersId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir ders ID değeri gönderilmelidir"
                });
            }

            if (!devamsizlikTarihi) {
                return res.status(400).json({
                    mesaj:
                        "Tarih YYYY-MM-DD biçiminde gönderilmelidir"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id:
                            req.kullanici.id,

                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen profili bulunamadı"
                });
            }

            const dersAtamasi =
                await prisma
                    .ogretmen_dersleri
                    .findFirst({
                        where: {
                            ogretmen_id:
                                ogretmen.id,

                            ders_id:
                                dersId,

                            aktif: true,

                            ders: {
                                aktif: true
                            }
                        },

                        include: {
                            ders: true
                        }
                    });

            if (!dersAtamasi) {
                return res.status(403).json({
                    mesaj:
                        "Bu ders için devamsızlık işlemi yapma yetkiniz bulunmuyor"
                });
            }

            const ogrenciDersKayitlari =
                await prisma
                    .ogrenci_dersleri
                    .findMany({
                        where: {
                            ders_id:
                                dersId,

                            aktif: true,

                            ogrenci: {
                                aktif: true
                            }
                        },

                        include: {
                            ogrenci: true
                        }
                    });

            const mevcutDevamsizliklar =
                await prisma
                    .devamsizliklar
                    .findMany({
                        where: {
                            ders_id:
                                dersId,

                            devamsizlik_tarihi:
                                devamsizlikTarihi,

                            aktif: true
                        }
                    });

            const devamsizlikHaritasi =
                new Map(
                    mevcutDevamsizliklar.map(
                        (kayit) => [
                            kayit.ogrenci_id,
                            kayit
                        ]
                    )
                );

            const ogrenciler =
                ogrenciDersKayitlari
                    .map((dersKaydi) => {
                        const ogrenci =
                            dersKaydi.ogrenci;

                        const devamsizlik =
                            devamsizlikHaritasi.get(
                                ogrenci.id
                            );

                        return {
                            id:
                                ogrenci.id,

                            ad:
                                ogrenci.ad,

                            soyad:
                                ogrenci.soyad,

                            ogrenci_no:
                                ogrenci.ogrenci_no,

                            devamsizlik:
                                devamsizlik
                                    ? {
                                        id:
                                            devamsizlik.id,

                                        durum:
                                            devamsizlik.durum,

                                        aciklama:
                                            devamsizlik.aciklama
                                    }
                                    : null
                        };
                    })
                    .sort((birinci, ikinci) => {
                        const birinciAd =
                            `${birinci.ad} ${birinci.soyad}`;

                        const ikinciAd =
                            `${ikinci.ad} ${ikinci.soyad}`;

                        return birinciAd.localeCompare(
                            ikinciAd,
                            "tr"
                        );
                    });

            return res.json({
                ders: {
                    id:
                        dersAtamasi.ders.id,

                    ders_kodu:
                        dersAtamasi
                            .ders
                            .ders_kodu,

                    ders_adi:
                        dersAtamasi
                            .ders
                            .ders_adi
                },

                devamsizlik_tarihi:
                    req.query.tarih,

                ogrenciler
            });

        } catch (hata) {
            console.error(
                "Ders devamsızlıkları getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Dersin devamsızlık bilgileri getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ÖĞRETMENİN DEVAMSIZLIK KAYDETMESİ
===================================================== */

app.post(
    "/api/ogretmen/dersler/:dersId/devamsizliklar",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const dersId =
                Number(req.params.dersId);

            const {
                devamsizlik_tarihi,
                kayitlar
            } = req.body;

            const tarih =
                devamsizlikTarihiniOlustur(
                    devamsizlik_tarihi
                );

            if (
                !Number.isInteger(dersId) ||
                dersId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir ders ID değeri gönderilmelidir"
                });
            }

            if (!tarih) {
                return res.status(400).json({
                    mesaj:
                        "Devamsızlık tarihi YYYY-MM-DD biçiminde olmalıdır"
                });
            }

            if (
                !Array.isArray(kayitlar) ||
                kayitlar.length === 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "En az bir öğrenci devamsızlık kaydı gönderilmelidir"
                });
            }

            const izinliDurumlar = [
                "geldi",
                "gelmedi",
                "gec_kaldi",
                "izinli"
            ];

            const duzenlenmisKayitlar = [];

            for (const kayit of kayitlar) {
                const ogrenciId =
                    Number(kayit.ogrenci_id);

                const durum =
                    String(
                        kayit.durum || ""
                    )
                        .trim()
                        .toLowerCase();

                const aciklama =
                    String(
                        kayit.aciklama || ""
                    )
                        .trim();

                if (
                    !Number.isInteger(
                        ogrenciId
                    ) ||
                    ogrenciId <= 0
                ) {
                    return res.status(400).json({
                        mesaj:
                            "Geçerli bir öğrenci ID değeri gönderilmelidir"
                    });
                }

                if (
                    !izinliDurumlar.includes(
                        durum
                    )
                ) {
                    return res.status(400).json({
                        mesaj:
                            "Devamsızlık durumu geldi, gelmedi, gec_kaldi veya izinli olmalıdır"
                    });
                }

                if (
                    aciklama.length > 500
                ) {
                    return res.status(400).json({
                        mesaj:
                            "Devamsızlık açıklaması en fazla 500 karakter olabilir"
                    });
                }

                duzenlenmisKayitlar.push({
                    ogrenci_id:
                        ogrenciId,

                    durum,

                    aciklama:
                        aciklama || null
                });
            }

            const ogrenciIdleri =
                duzenlenmisKayitlar.map(
                    (kayit) =>
                        kayit.ogrenci_id
                );

            const benzersizOgrenciIdleri =
                [
                    ...new Set(
                        ogrenciIdleri
                    )
                ];

            if (
                benzersizOgrenciIdleri.length !==
                ogrenciIdleri.length
            ) {
                return res.status(400).json({
                    mesaj:
                        "Aynı öğrenci için birden fazla kayıt gönderilemez"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id:
                            req.kullanici.id,

                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen profili bulunamadı"
                });
            }

            const dersAtamasi =
                await prisma
                    .ogretmen_dersleri
                    .findFirst({
                        where: {
                            ogretmen_id:
                                ogretmen.id,

                            ders_id:
                                dersId,

                            aktif: true,

                            ders: {
                                aktif: true
                            }
                        }
                    });

            if (!dersAtamasi) {
                return res.status(403).json({
                    mesaj:
                        "Bu ders için devamsızlık işlemi yapma yetkiniz bulunmuyor"
                });
            }

            const aktifDersKayitlari =
                await prisma
                    .ogrenci_dersleri
                    .findMany({
                        where: {
                            ders_id:
                                dersId,

                            ogrenci_id: {
                                in:
                                    benzersizOgrenciIdleri
                            },

                            aktif: true,

                            ogrenci: {
                                aktif: true
                            }
                        },

                        select: {
                            ogrenci_id: true
                        }
                    });

            const aktifOgrenciIdleri =
                new Set(
                    aktifDersKayitlari.map(
                        (kayit) =>
                            kayit.ogrenci_id
                    )
                );

            const kayitsizOgrenci =
                benzersizOgrenciIdleri.find(
                    (ogrenciId) =>
                        !aktifOgrenciIdleri.has(
                            ogrenciId
                        )
                );

            if (kayitsizOgrenci) {
                return res.status(400).json({
                    mesaj:
                        `${kayitsizOgrenci} ID değerine sahip öğrenci bu derse kayıtlı değildir`
                });
            }

            const islemler =
                duzenlenmisKayitlar.map(
                    (kayit) =>
                        prisma
                            .devamsizliklar
                            .upsert({
                                where: {
                                    ogrenci_id_ders_id_devamsizlik_tarihi: {
                                        ogrenci_id:
                                            kayit.ogrenci_id,

                                        ders_id:
                                            dersId,

                                        devamsizlik_tarihi:
                                            tarih
                                    }
                                },

                                update: {
                                    ogretmen_id:
                                        ogretmen.id,

                                    durum:
                                        kayit.durum,

                                    aciklama:
                                        kayit.aciklama,

                                    aktif:
                                        true
                                },

                                create: {
                                    ogrenci_id:
                                        kayit.ogrenci_id,

                                    ders_id:
                                        dersId,

                                    ogretmen_id:
                                        ogretmen.id,

                                    devamsizlik_tarihi:
                                        tarih,

                                    durum:
                                        kayit.durum,

                                    aciklama:
                                        kayit.aciklama
                                }
                            })
                );

            const kaydedilenKayitlar =
                await prisma.$transaction(
                    islemler
                );

            return res.json({
                mesaj:
                    "Devamsızlık kayıtları başarıyla kaydedildi",

                kayit_sayisi:
                    kaydedilenKayitlar.length
            });

        } catch (hata) {
            console.error(
                "Devamsızlık kaydetme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Devamsızlık kayıtları kaydedilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ÖĞRENCİNİN KENDİ DEVAMSIZLIKLARINI GÖRMESİ
===================================================== */

app.get(
    "/api/ogrenci/devamsizliklarim",
    kimlikDogrula,
    rolDogrula("ogrenci"),
    async (req, res) => {
        try {
            const ogrenci =
                await prisma.ogrenciler.findFirst({
                    where: {
                        kullanici_id:
                            req.kullanici.id,

                        aktif: true
                    }
                });

            if (!ogrenci) {
                return res.status(404).json({
                    mesaj:
                        "Öğrenci profili bulunamadı"
                });
            }

            const devamsizliklar =
                await prisma
                    .devamsizliklar
                    .findMany({
                        where: {
                            ogrenci_id:
                                ogrenci.id,

                            aktif: true,

                            ders: {
                                aktif: true
                            }
                        },

                        include: {
                            ders: true,
                            ogretmen: true
                        },

                        orderBy: {
                            devamsizlik_tarihi:
                                "desc"
                        }
                    });

            const ozet = {
                toplam:
                    devamsizliklar.length,

                geldi: 0,
                gelmedi: 0,
                gec_kaldi: 0,
                izinli: 0
            };

            const devamsizlikListesi =
                devamsizliklar.map(
                    (kayit) => {
                        if (
                            Object.hasOwn(
                                ozet,
                                kayit.durum
                            )
                        ) {
                            ozet[kayit.durum] += 1;
                        }

                        return {
                            id:
                                kayit.id,

                            devamsizlik_tarihi:
                                kayit
                                    .devamsizlik_tarihi,

                            durum:
                                kayit.durum,

                            aciklama:
                                kayit.aciklama,

                            ders: {
                                id:
                                    kayit.ders.id,

                                ders_kodu:
                                    kayit
                                        .ders
                                        .ders_kodu,

                                ders_adi:
                                    kayit
                                        .ders
                                        .ders_adi
                            },

                            ogretmen: {
                                id:
                                    kayit
                                        .ogretmen
                                        .id,

                                ad:
                                    kayit
                                        .ogretmen
                                        .ad,

                                soyad:
                                    kayit
                                        .ogretmen
                                        .soyad
                            }
                        };
                    }
                );

            return res.json({
                ogrenci: {
                    id:
                        ogrenci.id,

                    ad:
                        ogrenci.ad,

                    soyad:
                        ogrenci.soyad,

                    ogrenci_no:
                        ogrenci.ogrenci_no
                },

                ozet,
                devamsizliklar:
                    devamsizlikListesi
            });

        } catch (hata) {
            console.error(
                "Öğrenci devamsızlıkları getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Devamsızlık bilgileri getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);

/* =====================================================
   ADMIN: ÖĞRETMEN GÜNCELLEME
===================================================== */

app.put(
    "/api/ogretmenler/:id",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const ogretmenId =
                Number(req.params.id);

            const {
                ad,
                soyad,
                sicil_no,
                brans
            } = req.body;

            const duzenlenmisAd =
                String(ad || "").trim();

            const duzenlenmisSoyad =
                String(soyad || "").trim();

            const duzenlenmisSicilNo =
                String(sicil_no || "").trim();

            const duzenlenmisBrans =
                String(brans || "").trim();

            if (
                !Number.isInteger(ogretmenId) ||
                ogretmenId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir öğretmen ID değeri gönderilmelidir"
                });
            }

            if (
                !duzenlenmisAd ||
                !duzenlenmisSoyad ||
                !duzenlenmisSicilNo ||
                !duzenlenmisBrans
            ) {
                return res.status(400).json({
                    mesaj:
                        "Ad, soyad, sicil numarası ve branş zorunludur"
                });
            }

            if (
                duzenlenmisAd.length > 100 ||
                duzenlenmisSoyad.length > 100
            ) {
                return res.status(400).json({
                    mesaj:
                        "Ad ve soyad en fazla 100 karakter olabilir"
                });
            }

            if (
                duzenlenmisSicilNo.length > 50
            ) {
                return res.status(400).json({
                    mesaj:
                        "Sicil numarası en fazla 50 karakter olabilir"
                });
            }

            if (
                duzenlenmisBrans.length > 100
            ) {
                return res.status(400).json({
                    mesaj:
                        "Branş en fazla 100 karakter olabilir"
                });
            }

            const mevcutOgretmen =
                await prisma.ogretmenler.findUnique({
                    where: {
                        id: ogretmenId
                    }
                });

            if (!mevcutOgretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen bulunamadı"
                });
            }

            const ayniSicilNumarasi =
                await prisma.ogretmenler.findFirst({
                    where: {
                        sicil_no:
                            duzenlenmisSicilNo,

                        NOT: {
                            id: ogretmenId
                        }
                    }
                });

            if (ayniSicilNumarasi) {
                return res.status(409).json({
                    mesaj:
                        "Bu sicil numarası başka bir öğretmen tarafından kullanılıyor"
                });
            }

            const guncellenenOgretmen =
                await prisma.ogretmenler.update({
                    where: {
                        id: ogretmenId
                    },

                    data: {
                        ad:
                            duzenlenmisAd,

                        soyad:
                            duzenlenmisSoyad,

                        sicil_no:
                            duzenlenmisSicilNo,

                        brans:
                            duzenlenmisBrans
                    }
                });

            return res.json({
                mesaj:
                    "Öğretmen başarıyla güncellendi",

                ogretmen:
                    guncellenenOgretmen
            });

        } catch (hata) {
            console.error(
                "Öğretmen güncelleme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğretmen güncellenemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: ÖĞRETMENİ PASİFE ALMA
===================================================== */

app.delete(
    "/api/ogretmenler/:id",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const ogretmenId =
                Number(req.params.id);

            if (
                !Number.isInteger(ogretmenId) ||
                ogretmenId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir öğretmen ID değeri gönderilmelidir"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findUnique({
                    where: {
                        id: ogretmenId
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen bulunamadı"
                });
            }

            if (!ogretmen.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Öğretmen zaten pasif durumda"
                });
            }

            const islemler = [
                prisma.ogretmenler.update({
                    where: {
                        id: ogretmenId
                    },

                    data: {
                        aktif: false
                    }
                })
            ];

            if (ogretmen.kullanici_id) {
                islemler.push(
                    prisma.kullanicilar.update({
                        where: {
                            id:
                                ogretmen.kullanici_id
                        },

                        data: {
                            aktif: false
                        }
                    })
                );
            }

            await prisma.$transaction(
                islemler
            );

            return res.json({
                mesaj:
                    "Öğretmen pasif duruma getirildi"
            });

        } catch (hata) {
            console.error(
                "Öğretmen pasife alma hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğretmen pasif duruma getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: ÖĞRETMENİ YENİDEN AKTİFLEŞTİRME
===================================================== */

app.patch(
    "/api/ogretmenler/:id/aktiflestir",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const ogretmenId =
                Number(req.params.id);

            if (
                !Number.isInteger(ogretmenId) ||
                ogretmenId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir öğretmen ID değeri gönderilmelidir"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findUnique({
                    where: {
                        id: ogretmenId
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen bulunamadı"
                });
            }

            if (ogretmen.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Öğretmen zaten aktif durumda"
                });
            }

            const islemler = [
                prisma.ogretmenler.update({
                    where: {
                        id: ogretmenId
                    },

                    data: {
                        aktif: true
                    }
                })
            ];

            if (ogretmen.kullanici_id) {
                islemler.push(
                    prisma.kullanicilar.update({
                        where: {
                            id:
                                ogretmen.kullanici_id
                        },

                        data: {
                            aktif: true
                        }
                    })
                );
            }

            await prisma.$transaction(
                islemler
            );

            return res.json({
                mesaj:
                    "Öğretmen yeniden aktifleştirildi"
            });

        } catch (hata) {
            console.error(
                "Öğretmen aktifleştirme hatası:",
                hata
            );S

            return res.status(500).json({
                mesaj:
                    "Öğretmen aktifleştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);

/* =====================================================
   ADMIN: AKTİF VE PASİF TÜM DERSLERİ GETİR
===================================================== */

app.get(
    "/api/dersler/tumu",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const dersler =
                await prisma.dersler.findMany({
                    orderBy: [
                        {
                            aktif: "desc"
                        },
                        {
                            ders_kodu: "asc"
                        }
                    ]
                });

            return res.json({
                dersler
            });

        } catch (hata) {
            console.error(
                "Tüm dersleri getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Dersler getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: DERS GÜNCELLEME
===================================================== */

app.put(
    "/api/dersler/:id",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const dersId =
                Number(req.params.id);

            const {
                ders_kodu,
                ders_adi,
                aciklama
            } = req.body;

            const duzenlenmisDersKodu =
                String(ders_kodu || "")
                    .trim()
                    .toUpperCase();

            const duzenlenmisDersAdi =
                String(ders_adi || "")
                    .trim();

            const duzenlenmisAciklama =
                String(aciklama || "")
                    .trim();

            if (
                !Number.isInteger(dersId) ||
                dersId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir ders ID değeri gönderilmelidir"
                });
            }

            if (
                !duzenlenmisDersKodu ||
                !duzenlenmisDersAdi
            ) {
                return res.status(400).json({
                    mesaj:
                        "Ders kodu ve ders adı zorunludur"
                });
            }

            if (
                duzenlenmisDersKodu.length > 30
            ) {
                return res.status(400).json({
                    mesaj:
                        "Ders kodu en fazla 30 karakter olabilir"
                });
            }

            if (
                duzenlenmisDersAdi.length > 150
            ) {
                return res.status(400).json({
                    mesaj:
                        "Ders adı en fazla 150 karakter olabilir"
                });
            }

            if (
                duzenlenmisAciklama.length > 500
            ) {
                return res.status(400).json({
                    mesaj:
                        "Ders açıklaması en fazla 500 karakter olabilir"
                });
            }

            const mevcutDers =
                await prisma.dersler.findUnique({
                    where: {
                        id: dersId
                    }
                });

            if (!mevcutDers) {
                return res.status(404).json({
                    mesaj:
                        "Ders bulunamadı"
                });
            }

            const ayniKodluDers =
                await prisma.dersler.findFirst({
                    where: {
                        ders_kodu:
                            duzenlenmisDersKodu,

                        NOT: {
                            id: dersId
                        }
                    }
                });

            if (ayniKodluDers) {
                return res.status(409).json({
                    mesaj:
                        "Bu ders kodu başka bir ders tarafından kullanılıyor"
                });
            }

            const guncellenenDers =
                await prisma.dersler.update({
                    where: {
                        id: dersId
                    },

                    data: {
                        ders_kodu:
                            duzenlenmisDersKodu,

                        ders_adi:
                            duzenlenmisDersAdi,

                        aciklama:
                            duzenlenmisAciklama ||
                            null
                    }
                });

            return res.json({
                mesaj:
                    "Ders başarıyla güncellendi",

                ders:
                    guncellenenDers
            });

        } catch (hata) {
            console.error(
                "Ders güncelleme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Ders güncellenemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: DERSİ PASİFE ALMA
===================================================== */

app.delete(
    "/api/dersler/:id",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const dersId =
                Number(req.params.id);

            if (
                !Number.isInteger(dersId) ||
                dersId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir ders ID değeri gönderilmelidir"
                });
            }

            const ders =
                await prisma.dersler.findUnique({
                    where: {
                        id: dersId
                    }
                });

            if (!ders) {
                return res.status(404).json({
                    mesaj:
                        "Ders bulunamadı"
                });
            }

            if (!ders.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Ders zaten pasif durumda"
                });
            }

            const pasifDers =
                await prisma.dersler.update({
                    where: {
                        id: dersId
                    },

                    data: {
                        aktif: false
                    }
                });

            return res.json({
                mesaj:
                    "Ders pasif duruma getirildi",

                ders:
                    pasifDers
            });

        } catch (hata) {
            console.error(
                "Ders pasife alma hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Ders pasif duruma getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: DERSİ YENİDEN AKTİFLEŞTİRME
===================================================== */

app.patch(
    "/api/dersler/:id/aktiflestir",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const dersId =
                Number(req.params.id);

            if (
                !Number.isInteger(dersId) ||
                dersId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir ders ID değeri gönderilmelidir"
                });
            }

            const ders =
                await prisma.dersler.findUnique({
                    where: {
                        id: dersId
                    }
                });

            if (!ders) {
                return res.status(404).json({
                    mesaj:
                        "Ders bulunamadı"
                });
            }

            if (ders.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Ders zaten aktif durumda"
                });
            }

            const aktifDers =
                await prisma.dersler.update({
                    where: {
                        id: dersId
                    },

                    data: {
                        aktif: true
                    }
                });

            return res.json({
                mesaj:
                    "Ders yeniden aktifleştirildi",

                ders:
                    aktifDers
            });

        } catch (hata) {
            console.error(
                "Ders aktifleştirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Ders aktifleştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);

/* =====================================================
   ADMIN: TÜM ÖĞRETMEN-DERS ATAMALARINI GETİR
===================================================== */

app.get(
    "/api/ogretmen-ders-atamalari/tumu",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const atamalar =
                await prisma.ogretmen_dersleri.findMany({
                    include: {
                        ogretmen: true,
                        ders: true
                    },

                    orderBy: [
                        {
                            aktif: "desc"
                        },
                        {
                            id: "desc"
                        }
                    ]
                });

            return res.json({
                atamalar
            });
        } catch (hata) {
            console.error(
                "Öğretmen ders atamaları getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğretmen ders atamaları getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: ÖĞRETMEN-DERS ATAMASINI PASİFLEŞTİR
===================================================== */

app.patch(
    "/api/ogretmen-ders-atamalari/:id/pasiflestir",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const atamaId =
                Number(req.params.id);

            if (
                !Number.isInteger(atamaId) ||
                atamaId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir atama ID değeri gönderilmelidir"
                });
            }

            const atama =
                await prisma.ogretmen_dersleri.findUnique({
                    where: {
                        id: atamaId
                    },

                    include: {
                        ogretmen: true,
                        ders: true
                    }
                });

            if (!atama) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen ders ataması bulunamadı"
                });
            }

            if (!atama.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Bu öğretmen ders ataması zaten pasif durumda"
                });
            }

            const guncellenenAtama =
                await prisma.ogretmen_dersleri.update({
                    where: {
                        id: atamaId
                    },

                    data: {
                        aktif: false
                    },

                    include: {
                        ogretmen: true,
                        ders: true
                    }
                });

            return res.json({
                mesaj:
                    "Öğretmen ders ataması pasif duruma getirildi",

                atama:
                    guncellenenAtama
            });
        } catch (hata) {
            console.error(
                "Öğretmen ders ataması pasifleştirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğretmen ders ataması pasifleştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: ÖĞRETMEN-DERS ATAMASINI AKTİFLEŞTİR
===================================================== */

app.patch(
    "/api/ogretmen-ders-atamalari/:id/aktiflestir",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const atamaId =
                Number(req.params.id);

            if (
                !Number.isInteger(atamaId) ||
                atamaId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir atama ID değeri gönderilmelidir"
                });
            }

            const atama =
                await prisma.ogretmen_dersleri.findUnique({
                    where: {
                        id: atamaId
                    },

                    include: {
                        ogretmen: true,
                        ders: true
                    }
                });

            if (!atama) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen ders ataması bulunamadı"
                });
            }

            if (atama.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Bu öğretmen ders ataması zaten aktif durumda"
                });
            }

            if (!atama.ogretmen.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Pasif öğretmene ders atanamaz"
                });
            }

            if (!atama.ders.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Pasif ders için atama aktifleştirilemez"
                });
            }

            const guncellenenAtama =
                await prisma.ogretmen_dersleri.update({
                    where: {
                        id: atamaId
                    },

                    data: {
                        aktif: true
                    },

                    include: {
                        ogretmen: true,
                        ders: true
                    }
                });

            return res.json({
                mesaj:
                    "Öğretmen ders ataması yeniden aktifleştirildi",

                atama:
                    guncellenenAtama
            });
        } catch (hata) {
            console.error(
                "Öğretmen ders ataması aktifleştirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğretmen ders ataması aktifleştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);

/* =====================================================
   ADMIN: TÜM ÖĞRENCİ-DERS KAYITLARINI GETİR
===================================================== */

app.get(
    "/api/ogrenci-ders-kayitlari/tumu",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const kayitlar =
                await prisma.ogrenci_dersleri.findMany({
                    include: {
                        ogrenci: true,
                        ders: true
                    },

                    orderBy: [
                        {
                            aktif: "desc"
                        },
                        {
                            id: "desc"
                        }
                    ]
                });

            return res.json({
                kayitlar
            });

        } catch (hata) {
            console.error(
                "Öğrenci ders kayıtları getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğrenci ders kayıtları getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: ÖĞRENCİYİ DERSTEN ÇIKAR
===================================================== */

app.patch(
    "/api/ogrenci-ders-kayitlari/:id/pasiflestir",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const kayitId =
                Number(req.params.id);

            if (
                !Number.isInteger(kayitId) ||
                kayitId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir öğrenci ders kayıt ID değeri gönderilmelidir"
                });
            }

            const kayit =
                await prisma.ogrenci_dersleri.findUnique({
                    where: {
                        id: kayitId
                    },

                    include: {
                        ogrenci: true,
                        ders: true
                    }
                });

            if (!kayit) {
                return res.status(404).json({
                    mesaj:
                        "Öğrenci ders kaydı bulunamadı"
                });
            }

            if (!kayit.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Öğrenci zaten bu dersten çıkarılmış"
                });
            }

            const guncellenenKayit =
                await prisma.ogrenci_dersleri.update({
                    where: {
                        id: kayitId
                    },

                    data: {
                        aktif: false
                    },

                    include: {
                        ogrenci: true,
                        ders: true
                    }
                });

            return res.json({
                mesaj:
                    "Öğrenci dersten başarıyla çıkarıldı",

                kayit:
                    guncellenenKayit
            });

        } catch (hata) {
            console.error(
                "Öğrenciyi dersten çıkarma hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğrenci dersten çıkarılamadı",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: ÖĞRENCİNİN DERS KAYDINI AKTİFLEŞTİR
===================================================== */

app.patch(
    "/api/ogrenci-ders-kayitlari/:id/aktiflestir",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const kayitId =
                Number(req.params.id);

            if (
                !Number.isInteger(kayitId) ||
                kayitId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir öğrenci ders kayıt ID değeri gönderilmelidir"
                });
            }

            const kayit =
                await prisma.ogrenci_dersleri.findUnique({
                    where: {
                        id: kayitId
                    },

                    include: {
                        ogrenci: true,
                        ders: true
                    }
                });

            if (!kayit) {
                return res.status(404).json({
                    mesaj:
                        "Öğrenci ders kaydı bulunamadı"
                });
            }

            if (kayit.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Öğrenci zaten bu derse aktif olarak kayıtlı"
                });
            }

            if (!kayit.ogrenci.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Pasif öğrenci derse yeniden kaydedilemez"
                });
            }

            if (!kayit.ders.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Pasif derse öğrenci kaydedilemez"
                });
            }

            const guncellenenKayit =
                await prisma.ogrenci_dersleri.update({
                    where: {
                        id: kayitId
                    },

                    data: {
                        aktif: true
                    },

                    include: {
                        ogrenci: true,
                        ders: true
                    }
                });

            return res.json({
                mesaj:
                    "Öğrencinin ders kaydı yeniden aktifleştirildi",

                kayit:
                    guncellenenKayit
            });

        } catch (hata) {
            console.error(
                "Öğrenci ders kaydını aktifleştirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğrencinin ders kaydı aktifleştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);

/* =====================================================
   ADMIN: TÜM SINAVLARI VE ÖZETİ GETİR
===================================================== */

app.get(
    "/api/admin/sinavlar",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const sinavlar =
                await prisma.sinavlar.findMany({
                    include: {
                        ders: {
                            select: {
                                id: true,
                                ders_kodu: true,
                                ders_adi: true,
                                aktif: true
                            }
                        },

                        ogretmen: {
                            select: {
                                id: true,
                                ad: true,
                                soyad: true,
                                sicil_no: true,
                                aktif: true
                            }
                        }
                    },

                    orderBy: [
                        {
                            sinav_tarihi: "desc"
                        },
                        {
                            id: "desc"
                        }
                    ]
                });

            const ozet = {
                toplam:
                    sinavlar.length,

                aktif:
                    sinavlar.filter(
                        (sinav) => sinav.aktif
                    ).length,

                pasif:
                    sinavlar.filter(
                        (sinav) => !sinav.aktif
                    ).length
            };

            return res.json({
                ozet,
                sinavlar
            });

        } catch (hata) {
            console.error(
                "Admin sınav raporu getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Sınav raporu getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: TÜM NOTLARI VE ÖZETİ GETİR
===================================================== */

app.get(
    "/api/admin/notlar",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const notlar =
                await prisma.notlar.findMany({
                    include: {
                        ogrenci: {
                            select: {
                                id: true,
                                ad: true,
                                soyad: true,
                                ogrenci_no: true,
                                aktif: true
                            }
                        },

                        sinav: {
                            include: {
                                ders: {
                                    select: {
                                        id: true,
                                        ders_kodu: true,
                                        ders_adi: true,
                                        aktif: true
                                    }
                                },

                                ogretmen: {
                                    select: {
                                        id: true,
                                        ad: true,
                                        soyad: true,
                                        sicil_no: true,
                                        aktif: true
                                    }
                                }
                            }
                        }
                    },

                    orderBy: [
                        {
                            olusturma_tarihi: "desc"
                        },
                        {
                            id: "desc"
                        }
                    ]
                });

            const aktifNotlar =
                notlar.filter(
                    (notKaydi) =>
                        notKaydi.aktif
                );

            const toplamPuan =
                aktifNotlar.reduce(
                    (toplam, notKaydi) =>
                        toplam +
                        Number(notKaydi.puan),
                    0
                );

            const ortalamaPuan =
                aktifNotlar.length > 0
                    ? Number(
                        (
                            toplamPuan /
                            aktifNotlar.length
                        ).toFixed(2)
                    )
                    : 0;

            const notListesi =
                notlar.map((notKaydi) => ({
                    id:
                        notKaydi.id,

                    puan:
                        Number(notKaydi.puan),

                    aciklama:
                        notKaydi.aciklama,

                    aktif:
                        notKaydi.aktif,

                    olusturma_tarihi:
                        notKaydi.olusturma_tarihi,

                    guncelleme_tarihi:
                        notKaydi.guncelleme_tarihi,

                    ogrenci:
                        notKaydi.ogrenci,

                    sinav: {
                        id:
                            notKaydi.sinav.id,

                        sinav_adi:
                            notKaydi
                                .sinav
                                .sinav_adi,

                        sinav_tarihi:
                            notKaydi
                                .sinav
                                .sinav_tarihi,

                        maksimum_puan:
                            notKaydi
                                .sinav
                                .maksimum_puan,

                        aktif:
                            notKaydi.sinav.aktif
                    },

                    ders:
                        notKaydi.sinav.ders,

                    ogretmen:
                        notKaydi.sinav.ogretmen
                }));

            return res.json({
                ozet: {
                    toplam:
                        notlar.length,

                    aktif:
                        aktifNotlar.length,

                    pasif:
                        notlar.length -
                        aktifNotlar.length,

                    ortalama_puan:
                        ortalamaPuan
                },

                notlar:
                    notListesi
            });

        } catch (hata) {
            console.error(
                "Admin not raporu getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Not raporu getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: TÜM DEVAMSIZLIKLARI VE ÖZETİ GETİR
===================================================== */

app.get(
    "/api/admin/devamsizliklar",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const devamsizliklar =
                await prisma
                    .devamsizliklar
                    .findMany({
                        include: {
                            ogrenci: {
                                select: {
                                    id: true,
                                    ad: true,
                                    soyad: true,
                                    ogrenci_no: true,
                                    aktif: true
                                }
                            },

                            ders: {
                                select: {
                                    id: true,
                                    ders_kodu: true,
                                    ders_adi: true,
                                    aktif: true
                                }
                            },

                            ogretmen: {
                                select: {
                                    id: true,
                                    ad: true,
                                    soyad: true,
                                    sicil_no: true,
                                    aktif: true
                                }
                            }
                        },

                        orderBy: [
                            {
                                devamsizlik_tarihi:
                                    "desc"
                            },
                            {
                                id: "desc"
                            }
                        ]
                    });

            const aktifKayitlar =
                devamsizliklar.filter(
                    (kayit) => kayit.aktif
                );

            const ozet = {
                toplam:
                    devamsizliklar.length,

                aktif:
                    aktifKayitlar.length,

                pasif:
                    devamsizliklar.length -
                    aktifKayitlar.length,

                geldi: 0,
                gelmedi: 0,
                gec_kaldi: 0,
                izinli: 0
            };

            for (
                const kayit of aktifKayitlar
            ) {
                if (
                    Object.hasOwn(
                        ozet,
                        kayit.durum
                    )
                ) {
                    ozet[kayit.durum] += 1;
                }
            }

            return res.json({
                ozet,
                devamsizliklar
            });

        } catch (hata) {
            console.error(
                "Admin devamsızlık raporu getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Devamsızlık raporu getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);

/* =====================================================
   ÖĞRENCİ: KENDİ PROFİLİNİ GÖR
===================================================== */

app.get(
    "/api/ogrenci/profilim",
    kimlikDogrula,
    rolDogrula("ogrenci"),
    async (req, res) => {
        try {
            const kullaniciId =
                Number(req.kullanici.id);

            const kullanici =
                await prisma.kullanicilar.findUnique({
                    where: {
                        id: kullaniciId
                    },

                    select: {
                        id: true,
                        kullanici_adi: true,
                        email: true,
                        rol: true,
                        aktif: true,
                        olusturma_tarihi: true,
                        guncelleme_tarihi: true
                    }
                });

            if (!kullanici) {
                return res.status(404).json({
                    mesaj:
                        "Kullanıcı hesabı bulunamadı"
                });
            }

            const ogrenci =
                await prisma.ogrenciler.findFirst({
                    where: {
                        kullanici_id:
                            kullaniciId
                    },

                    select: {
                        id: true,
                        ad: true,
                        soyad: true,
                        ogrenci_no: true,
                        aktif: true,
                        olusturma_tarihi: true,
                        guncelleme_tarihi: true
                    }
                });

            if (!ogrenci) {
                return res.status(404).json({
                    mesaj:
                        "Öğrenci profili bulunamadı"
                });
            }

            const [
                aktifDersSayisi,
                aktifNotSayisi,
                devamsizlikGruplari
            ] = await Promise.all([
                prisma.ogrenci_dersleri.count({
                    where: {
                        ogrenci_id:
                            ogrenci.id,

                        aktif: true,

                        ders: {
                            aktif: true
                        }
                    }
                }),

                prisma.notlar.count({
                    where: {
                        ogrenci_id:
                            ogrenci.id,

                        aktif: true,

                        sinav: {
                            aktif: true
                        }
                    }
                }),

                prisma.devamsizliklar.groupBy({
                    by: [
                        "durum"
                    ],

                    where: {
                        ogrenci_id:
                            ogrenci.id,

                        aktif: true
                    },

                    _count: {
                        _all: true
                    }
                })
            ]);

            const devamsizlikOzeti = {
                toplam: 0,
                geldi: 0,
                gelmedi: 0,
                gec_kaldi: 0,
                izinli: 0
            };

            for (
                const grup of devamsizlikGruplari
            ) {
                const sayi =
                    grup._count._all;

                devamsizlikOzeti.toplam +=
                    sayi;

                if (
                    Object.hasOwn(
                        devamsizlikOzeti,
                        grup.durum
                    )
                ) {
                    devamsizlikOzeti[
                        grup.durum
                    ] = sayi;
                }
            }

            return res.json({
                kullanici,
                ogrenci,

                ozet: {
                    aktif_ders_sayisi:
                        aktifDersSayisi,

                    aktif_not_sayisi:
                        aktifNotSayisi,

                    devamsizlik:
                        devamsizlikOzeti
                }
            });

        } catch (hata) {
            console.error(
                "Öğrenci profili getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğrenci profili getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ÖĞRETMEN: KENDİ PROFİLİNİ GÖR
===================================================== */

app.get(
    "/api/ogretmen/profilim",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const kullaniciId =
                Number(req.kullanici.id);

            const kullanici =
                await prisma.kullanicilar.findUnique({
                    where: {
                        id: kullaniciId
                    },

                    select: {
                        id: true,
                        kullanici_adi: true,
                        email: true,
                        rol: true,
                        aktif: true,
                        olusturma_tarihi: true,
                        guncelleme_tarihi: true
                    }
                });

            if (!kullanici) {
                return res.status(404).json({
                    mesaj:
                        "Kullanıcı hesabı bulunamadı"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id:
                            kullaniciId
                    },

                    select: {
                        id: true,
                        ad: true,
                        soyad: true,
                        sicil_no: true,
                        brans: true,
                        aktif: true,
                        olusturma_tarihi: true,
                        guncelleme_tarihi: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen profili bulunamadı"
                });
            }

            const [
                aktifDersSayisi,
                aktifSinavSayisi,
                notSayisi,
                devamsizlikKaydiSayisi
            ] = await Promise.all([
                prisma.ogretmen_dersleri.count({
                    where: {
                        ogretmen_id:
                            ogretmen.id,

                        aktif: true,

                        ders: {
                            aktif: true
                        }
                    }
                }),

                prisma.sinavlar.count({
                    where: {
                        ogretmen_id:
                            ogretmen.id,

                        aktif: true
                    }
                }),

                prisma.notlar.count({
                    where: {
                        aktif: true,

                        sinav: {
                            ogretmen_id:
                                ogretmen.id,

                            aktif: true
                        }
                    }
                }),

                prisma.devamsizliklar.count({
                    where: {
                        ogretmen_id:
                            ogretmen.id,

                        aktif: true
                    }
                })
            ]);

            return res.json({
                kullanici,
                ogretmen,

                ozet: {
                    aktif_ders_sayisi:
                        aktifDersSayisi,

                    aktif_sinav_sayisi:
                        aktifSinavSayisi,

                    girilen_not_sayisi:
                        notSayisi,

                    devamsizlik_kaydi_sayisi:
                        devamsizlikKaydiSayisi
                }
            });

        } catch (hata) {
            console.error(
                "Öğretmen profili getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğretmen profili getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: KENDİ PROFİLİNİ VE SİSTEM ÖZETİNİ GÖR
===================================================== */

app.get(
    "/api/admin/profilim",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const kullaniciId =
                Number(req.kullanici.id);

            const kullanici =
                await prisma.kullanicilar.findUnique({
                    where: {
                        id: kullaniciId
                    },

                    select: {
                        id: true,
                        kullanici_adi: true,
                        email: true,
                        rol: true,
                        aktif: true,
                        olusturma_tarihi: true,
                        guncelleme_tarihi: true
                    }
                });

            if (!kullanici) {
                return res.status(404).json({
                    mesaj:
                        "Admin hesabı bulunamadı"
                });
            }

            const [
                aktifOgrenciSayisi,
                aktifOgretmenSayisi,
                aktifDersSayisi,
                aktifSinavSayisi,
                aktifNotSayisi,
                aktifDevamsizlikSayisi
            ] = await Promise.all([
                prisma.ogrenciler.count({
                    where: {
                        aktif: true
                    }
                }),

                prisma.ogretmenler.count({
                    where: {
                        aktif: true
                    }
                }),

                prisma.dersler.count({
                    where: {
                        aktif: true
                    }
                }),

                prisma.sinavlar.count({
                    where: {
                        aktif: true
                    }
                }),

                prisma.notlar.count({
                    where: {
                        aktif: true
                    }
                }),

                prisma.devamsizliklar.count({
                    where: {
                        aktif: true
                    }
                })
            ]);

            return res.json({
                kullanici,

                sistem_ozeti: {
                    aktif_ogrenci_sayisi:
                        aktifOgrenciSayisi,

                    aktif_ogretmen_sayisi:
                        aktifOgretmenSayisi,

                    aktif_ders_sayisi:
                        aktifDersSayisi,

                    aktif_sinav_sayisi:
                        aktifSinavSayisi,

                    aktif_not_sayisi:
                        aktifNotSayisi,

                    aktif_devamsizlik_kaydi_sayisi:
                        aktifDevamsizlikSayisi
                }
            });

        } catch (hata) {
            console.error(
                "Admin profili getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Admin profili getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);

/* =====================================================
   KULLANICI: KENDİ ŞİFRESİNİ DEĞİŞTİR
===================================================== */

app.patch(
    "/api/kullanici/sifre-degistir",
    kimlikDogrula,
    async (req, res) => {
        try {
            const kullaniciId =
                Number(req.kullanici.id);

            const {
                mevcut_sifre,
                yeni_sifre,
                yeni_sifre_tekrar
            } = req.body;

            const mevcutSifre =
                String(mevcut_sifre || "");

            const yeniSifre =
                String(yeni_sifre || "");

            const yeniSifreTekrar =
                String(yeni_sifre_tekrar || "");

            if (
                !Number.isInteger(kullaniciId) ||
                kullaniciId <= 0
            ) {
                return res.status(401).json({
                    mesaj:
                        "Geçerli kullanıcı bilgisi bulunamadı"
                });
            }

            if (
                !mevcutSifre ||
                !yeniSifre ||
                !yeniSifreTekrar
            ) {
                return res.status(400).json({
                    mesaj:
                        "Mevcut şifre, yeni şifre ve yeni şifre tekrarı zorunludur"
                });
            }

            if (
                yeniSifre !==
                yeniSifreTekrar
            ) {
                return res.status(400).json({
                    mesaj:
                        "Yeni şifreler birbiriyle eşleşmiyor"
                });
            }

            if (
                yeniSifre.length < 8 ||
                yeniSifre.length > 100
            ) {
                return res.status(400).json({
                    mesaj:
                        "Yeni şifre 8 ile 100 karakter arasında olmalıdır"
                });
            }

            if (
                !/[a-z]/.test(yeniSifre) ||
                !/[A-Z]/.test(yeniSifre) ||
                !/[0-9]/.test(yeniSifre) ||
                !/[^A-Za-z0-9]/.test(yeniSifre)
            ) {
                return res.status(400).json({
                    mesaj:
                        "Yeni şifre en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir"
                });
            }

            if (
                mevcutSifre === yeniSifre
            ) {
                return res.status(400).json({
                    mesaj:
                        "Yeni şifre mevcut şifreyle aynı olamaz"
                });
            }

            const kullanici =
                await prisma.kullanicilar.findUnique({
                    where: {
                        id: kullaniciId
                    }
                });

            if (
                !kullanici ||
                !kullanici.aktif
            ) {
                return res.status(404).json({
                    mesaj:
                        "Aktif kullanıcı hesabı bulunamadı"
                });
            }

            const mevcutSifreDogruMu =
                await bcrypt.compare(
                    mevcutSifre,
                    kullanici.sifre_hash
                );

            if (!mevcutSifreDogruMu) {
                return res.status(400).json({
                    mesaj:
                        "Mevcut şifre hatalı"
                });
            }

            const yeniSifreHash =
                await bcrypt.hash(
                    yeniSifre,
                    10
                );

            await prisma.kullanicilar.update({
                where: {
                    id: kullaniciId
                },

                data: {
                    sifre_hash:
                        yeniSifreHash
                }
            });

            return res.json({
                mesaj:
                    "Şifre başarıyla değiştirildi"
            });

        } catch (hata) {
            console.error(
                "Şifre değiştirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Şifre değiştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);

/* =====================================================
   ADMIN: AKTİF VE PASİF TÜM ÖĞRENCİLERİ GETİR
===================================================== */

app.get(
    "/api/admin/ogrenciler",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const ogrenciler =
                await prisma.ogrenciler.findMany({
                    orderBy: [
                        {
                            aktif: "desc"
                        },
                        {
                            id: "desc"
                        }
                    ]
                });

            const kullaniciIdleri =
                ogrenciler
                    .map(
                        (ogrenci) =>
                            ogrenci.kullanici_id
                    )
                    .filter(
                        (kullaniciId) =>
                            Number.isInteger(
                                kullaniciId
                            )
                    );

            const kullanicilar =
                kullaniciIdleri.length > 0
                    ? await prisma
                        .kullanicilar
                        .findMany({
                            where: {
                                id: {
                                    in:
                                        kullaniciIdleri
                                }
                            },

                            select: {
                                id: true,
                                kullanici_adi: true,
                                email: true,
                                rol: true,
                                aktif: true
                            }
                        })
                    : [];

            const kullaniciHaritasi =
                new Map(
                    kullanicilar.map(
                        (kullanici) => [
                            kullanici.id,
                            kullanici
                        ]
                    )
                );

            const ogrenciListesi =
                ogrenciler.map(
                    (ogrenci) => ({
                        ...ogrenci,

                        kullanici:
                            ogrenci.kullanici_id
                                ? kullaniciHaritasi.get(
                                    ogrenci.kullanici_id
                                ) || null
                                : null
                    })
                );

            const aktifSayisi =
                ogrenciler.filter(
                    (ogrenci) =>
                        ogrenci.aktif
                ).length;

            return res.json({
                ozet: {
                    toplam:
                        ogrenciler.length,

                    aktif:
                        aktifSayisi,

                    pasif:
                        ogrenciler.length -
                        aktifSayisi
                },

                ogrenciler:
                    ogrenciListesi
            });

        } catch (hata) {
            console.error(
                "Tüm öğrencileri getirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğrenciler getirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ADMIN: PASİF ÖĞRENCİYİ YENİDEN AKTİFLEŞTİR
===================================================== */

app.patch(
    "/api/admin/ogrenciler/:id/aktiflestir",
    kimlikDogrula,
    rolDogrula("admin"),
    async (req, res) => {
        try {
            const ogrenciId =
                Number(req.params.id);

            if (
                !Number.isInteger(ogrenciId) ||
                ogrenciId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir öğrenci ID değeri gönderilmelidir"
                });
            }

            const ogrenci =
                await prisma.ogrenciler.findUnique({
                    where: {
                        id: ogrenciId
                    }
                });

            if (!ogrenci) {
                return res.status(404).json({
                    mesaj:
                        "Öğrenci bulunamadı"
                });
            }

            if (ogrenci.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Öğrenci zaten aktif durumda"
                });
            }

            const islemler = [
                prisma.ogrenciler.update({
                    where: {
                        id: ogrenciId
                    },

                    data: {
                        aktif: true,
                        silinme_tarihi: null
                    }
                })
            ];

            if (ogrenci.kullanici_id) {
                islemler.push(
                    prisma.kullanicilar.updateMany({
                        where: {
                            id:
                                ogrenci.kullanici_id
                        },

                        data: {
                            aktif: true
                        }
                    })
                );
            }

            const sonuc =
                await prisma.$transaction(
                    islemler
                );

            return res.json({
                mesaj:
                    "Öğrenci yeniden aktifleştirildi",

                ogrenci:
                    sonuc[0]
            });

        } catch (hata) {
            console.error(
                "Öğrenci aktifleştirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Öğrenci aktifleştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);

/* =====================================================
   ÖĞRETMEN: KENDİ SINAVINI GÜNCELLE
===================================================== */

app.put(
    "/api/ogretmen/sinavlar/:id",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const sinavId =
                Number(req.params.id);

            const {
                sinav_adi,
                sinav_tarihi,
                maksimum_puan,
                aciklama
            } = req.body;

            const duzenlenmisSinavAdi =
                String(sinav_adi || "")
                    .trim();

            const duzenlenmisAciklama =
                String(aciklama || "")
                    .trim();

            const maksimumPuan =
                Number(maksimum_puan);

            const sinavTarihi =
                new Date(sinav_tarihi);

            if (
                !Number.isInteger(sinavId) ||
                sinavId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir sınav ID değeri gönderilmelidir"
                });
            }

            if (!duzenlenmisSinavAdi) {
                return res.status(400).json({
                    mesaj:
                        "Sınav adı zorunludur"
                });
            }

            if (
                duzenlenmisSinavAdi.length >
                150
            ) {
                return res.status(400).json({
                    mesaj:
                        "Sınav adı en fazla 150 karakter olabilir"
                });
            }

            if (
                !sinav_tarihi ||
                Number.isNaN(
                    sinavTarihi.getTime()
                )
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir sınav tarihi gönderilmelidir"
                });
            }

            if (
                !Number.isFinite(
                    maksimumPuan
                ) ||
                maksimumPuan <= 0 ||
                maksimumPuan > 1000
            ) {
                return res.status(400).json({
                    mesaj:
                        "Maksimum puan 1 ile 1000 arasında olmalıdır"
                });
            }

            if (
                duzenlenmisAciklama.length >
                500
            ) {
                return res.status(400).json({
                    mesaj:
                        "Sınav açıklaması en fazla 500 karakter olabilir"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id:
                            req.kullanici.id,

                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen profili bulunamadı"
                });
            }

            const mevcutSinav =
                await prisma.sinavlar.findUnique({
                    where: {
                        id: sinavId
                    }
                });

            if (!mevcutSinav) {
                return res.status(404).json({
                    mesaj:
                        "Sınav bulunamadı"
                });
            }

            if (
                mevcutSinav.ogretmen_id !==
                ogretmen.id
            ) {
                return res.status(403).json({
                    mesaj:
                        "Bu sınavı güncelleme yetkiniz bulunmuyor"
                });
            }

            const guncellenenSinav =
                await prisma.sinavlar.update({
                    where: {
                        id: sinavId
                    },

                    data: {
                        sinav_adi:
                            duzenlenmisSinavAdi,

                        sinav_tarihi:
                            sinavTarihi,

                        maksimum_puan:
                            maksimumPuan,

                        aciklama:
                            duzenlenmisAciklama ||
                            null
                    },

                    include: {
                        ders: true,
                        ogretmen: true
                    }
                });

            return res.json({
                mesaj:
                    "Sınav başarıyla güncellendi",

                sinav:
                    guncellenenSinav
            });

        } catch (hata) {
            console.error(
                "Sınav güncelleme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Sınav güncellenemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ÖĞRETMEN: KENDİ SINAVINI PASİFLEŞTİR
===================================================== */

app.patch(
    "/api/ogretmen/sinavlar/:id/pasiflestir",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const sinavId =
                Number(req.params.id);

            if (
                !Number.isInteger(sinavId) ||
                sinavId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir sınav ID değeri gönderilmelidir"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id:
                            req.kullanici.id,

                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen profili bulunamadı"
                });
            }

            const sinav =
                await prisma.sinavlar.findUnique({
                    where: {
                        id: sinavId
                    }
                });

            if (!sinav) {
                return res.status(404).json({
                    mesaj:
                        "Sınav bulunamadı"
                });
            }

            if (
                sinav.ogretmen_id !==
                ogretmen.id
            ) {
                return res.status(403).json({
                    mesaj:
                        "Bu sınav üzerinde işlem yapma yetkiniz bulunmuyor"
                });
            }

            if (!sinav.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Sınav zaten pasif durumda"
                });
            }

            const pasifSinav =
                await prisma.sinavlar.update({
                    where: {
                        id: sinavId
                    },

                    data: {
                        aktif: false
                    }
                });

            return res.json({
                mesaj:
                    "Sınav pasif duruma getirildi",

                sinav:
                    pasifSinav
            });

        } catch (hata) {
            console.error(
                "Sınav pasifleştirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Sınav pasifleştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ÖĞRETMEN: KENDİ SINAVINI YENİDEN AKTİFLEŞTİR
===================================================== */

app.patch(
    "/api/ogretmen/sinavlar/:id/aktiflestir",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const sinavId =
                Number(req.params.id);

            if (
                !Number.isInteger(sinavId) ||
                sinavId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir sınav ID değeri gönderilmelidir"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id:
                            req.kullanici.id,

                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen profili bulunamadı"
                });
            }

            const sinav =
                await prisma.sinavlar.findUnique({
                    where: {
                        id: sinavId
                    }
                });

            if (!sinav) {
                return res.status(404).json({
                    mesaj:
                        "Sınav bulunamadı"
                });
            }

            if (
                sinav.ogretmen_id !==
                ogretmen.id
            ) {
                return res.status(403).json({
                    mesaj:
                        "Bu sınav üzerinde işlem yapma yetkiniz bulunmuyor"
                });
            }

            if (sinav.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Sınav zaten aktif durumda"
                });
            }

            const dersAtamasi =
                await prisma
                    .ogretmen_dersleri
                    .findFirst({
                        where: {
                            ogretmen_id:
                                ogretmen.id,

                            ders_id:
                                sinav.ders_id,

                            aktif: true,

                            ders: {
                                aktif: true
                            }
                        }
                    });

            if (!dersAtamasi) {
                return res.status(400).json({
                    mesaj:
                        "Sınavın dersi aktif değil veya ders artık öğretmene atanmış değil"
                });
            }

            const aktifSinav =
                await prisma.sinavlar.update({
                    where: {
                        id: sinavId
                    },

                    data: {
                        aktif: true
                    }
                });

            return res.json({
                mesaj:
                    "Sınav yeniden aktifleştirildi",

                sinav:
                    aktifSinav
            });

        } catch (hata) {
            console.error(
                "Sınav aktifleştirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Sınav aktifleştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);
/* =====================================================
   ÖĞRETMEN: NOT KAYDINI PASİFLEŞTİR
===================================================== */

app.patch(
    "/api/ogretmen/notlar/:id/pasiflestir",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const notId =
                Number(req.params.id);

            if (
                !Number.isInteger(notId) ||
                notId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir not ID değeri gönderilmelidir"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id:
                            req.kullanici.id,

                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen profili bulunamadı"
                });
            }

            const notKaydi =
                await prisma.notlar.findUnique({
                    where: {
                        id: notId
                    }
                });

            if (!notKaydi) {
                return res.status(404).json({
                    mesaj:
                        "Not kaydı bulunamadı"
                });
            }

            const sinav =
                await prisma.sinavlar.findUnique({
                    where: {
                        id:
                            notKaydi.sinav_id
                    }
                });

            if (!sinav) {
                return res.status(404).json({
                    mesaj:
                        "Nota bağlı sınav bulunamadı"
                });
            }

            if (
                sinav.ogretmen_id !==
                ogretmen.id
            ) {
                return res.status(403).json({
                    mesaj:
                        "Bu not kaydı üzerinde işlem yapma yetkiniz bulunmuyor"
                });
            }

            if (!notKaydi.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Not kaydı zaten pasif durumda"
                });
            }

            const pasifNot =
                await prisma.notlar.update({
                    where: {
                        id: notId
                    },

                    data: {
                        aktif: false
                    }
                });

            return res.json({
                mesaj:
                    "Not kaydı pasif duruma getirildi",

                not:
                    pasifNot
            });

        } catch (hata) {
            console.error(
                "Not pasifleştirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Not kaydı pasifleştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);


/* =====================================================
   ÖĞRETMEN: NOT KAYDINI YENİDEN AKTİFLEŞTİR
===================================================== */

app.patch(
    "/api/ogretmen/notlar/:id/aktiflestir",
    kimlikDogrula,
    rolDogrula("ogretmen"),
    async (req, res) => {
        try {
            const notId =
                Number(req.params.id);

            if (
                !Number.isInteger(notId) ||
                notId <= 0
            ) {
                return res.status(400).json({
                    mesaj:
                        "Geçerli bir not ID değeri gönderilmelidir"
                });
            }

            const ogretmen =
                await prisma.ogretmenler.findFirst({
                    where: {
                        kullanici_id:
                            req.kullanici.id,

                        aktif: true
                    }
                });

            if (!ogretmen) {
                return res.status(404).json({
                    mesaj:
                        "Öğretmen profili bulunamadı"
                });
            }

            const notKaydi =
                await prisma.notlar.findUnique({
                    where: {
                        id: notId
                    }
                });

            if (!notKaydi) {
                return res.status(404).json({
                    mesaj:
                        "Not kaydı bulunamadı"
                });
            }

            if (notKaydi.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Not kaydı zaten aktif durumda"
                });
            }

            const sinav =
                await prisma.sinavlar.findUnique({
                    where: {
                        id:
                            notKaydi.sinav_id
                    }
                });

            if (!sinav) {
                return res.status(404).json({
                    mesaj:
                        "Nota bağlı sınav bulunamadı"
                });
            }

            if (
                sinav.ogretmen_id !==
                ogretmen.id
            ) {
                return res.status(403).json({
                    mesaj:
                        "Bu not kaydı üzerinde işlem yapma yetkiniz bulunmuyor"
                });
            }

            if (!sinav.aktif) {
                return res.status(400).json({
                    mesaj:
                        "Pasif sınava ait not yeniden aktifleştirilemez"
                });
            }

            const ogrenci =
                await prisma.ogrenciler.findUnique({
                    where: {
                        id:
                            notKaydi.ogrenci_id
                    }
                });

            if (
                !ogrenci ||
                !ogrenci.aktif
            ) {
                return res.status(400).json({
                    mesaj:
                        "Pasif veya bulunamayan öğrenciye ait not aktifleştirilemez"
                });
            }

            const dersKaydi =
                await prisma
                    .ogrenci_dersleri
                    .findFirst({
                        where: {
                            ogrenci_id:
                                ogrenci.id,

                            ders_id:
                                sinav.ders_id,

                            aktif: true
                        }
                    });

            if (!dersKaydi) {
                return res.status(400).json({
                    mesaj:
                        "Öğrenci sınavın dersine aktif olarak kayıtlı değil"
                });
            }

            const dersAtamasi =
                await prisma
                    .ogretmen_dersleri
                    .findFirst({
                        where: {
                            ogretmen_id:
                                ogretmen.id,

                            ders_id:
                                sinav.ders_id,

                            aktif: true
                        }
                    });

            if (!dersAtamasi) {
                return res.status(400).json({
                    mesaj:
                        "Öğretmen sınavın dersine aktif olarak atanmamış"
                });
            }

            const aktifNot =
                await prisma.notlar.update({
                    where: {
                        id: notId
                    },

                    data: {
                        aktif: true
                    }
                });

            return res.json({
                mesaj:
                    "Not kaydı yeniden aktifleştirildi",

                not:
                    aktifNot
            });

        } catch (hata) {
            console.error(
                "Not aktifleştirme hatası:",
                hata
            );

            return res.status(500).json({
                mesaj:
                    "Not kaydı aktifleştirilemedi",

                hata:
                    hata.message
            });
        }
    }
);
/* =====================================================
   SUNUCUYU BAŞLAT
===================================================== */

app.listen(PORT, () => {
    console.log(
        "Sunucu http://localhost:" +
        PORT +
        " adresinde çalışıyor"
    );
});

/* Ctrl + C yapıldığında Prisma bağlantısını kapatır */
process.on("SIGINT", async () => {
    await prisma.$disconnect();
    process.exit(0);
});



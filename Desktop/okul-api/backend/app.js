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



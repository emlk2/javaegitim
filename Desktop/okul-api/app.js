const path = require("path");
const express = require("express");
const pool = require("./db");

const bcrypt = require("bcryptjs");
const jwt = require("jsonwebtoken");
const app = express();
const PORT = 3000;

// JSON verilerini okuyabilmek için
app.use(express.json());
app.use(express.static(path.join(__dirname, "public")));
const fs = require("fs");

console.log("Proje klasörü:", __dirname);
console.log(
    "script.js var mı:",
    fs.existsSync(path.join(__dirname, "public", "script.js"))
);

// public klasöründeki HTML, CSS ve JavaScript dosyalarını açar
app.use(express.static(path.join(__dirname, "public")));

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
        const sonuc = await pool.query(
            `SELECT
                id,
                kullanici_adi,
                email,
                sifre_hash,
                rol,
                aktif
             FROM kullanicilar
             WHERE kullanici_adi = $1`,
            [kullaniciAdi]
        );

        if (sonuc.rowCount === 0) {
            return res.status(401).json({
                mesaj: "Kullanıcı adı veya şifre hatalı"
            });
        }

        const kullanici = sonuc.rows[0];

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

        if (!process.env.JWT_SECRET) {
            throw new Error(
                "JWT_SECRET ortam değişkeni bulunamadı"
            );
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

        res.json({
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
        console.log(
            "Giriş hatası:",
            hata.message
        );

        res.status(500).json({
            mesaj: "Giriş işlemi gerçekleştirilemedi"
        });
    }
});
// Tüm öğrencileri getir
app.get("/api/ogrenciler", async (req, res) => {
    try {
        const sonuc = await pool.query(
            "SELECT * FROM ogrenciler ORDER BY id"
        );

        res.json(sonuc.rows);

    } catch (hata) {
        console.log(hata.message);

        res.status(500).json({
            mesaj: "Öğrenciler getirilemedi"
        });
    }
});

// ID'ye göre tek öğrenci getir
app.get("/api/ogrenciler/:id", async (req, res) => {
    const id = Number(req.params.id);

    if (!Number.isInteger(id) || id <= 0) {
        return res.status(400).json({
            mesaj: "Geçerli bir öğrenci ID'si giriniz"
        });
    }

    try {
        const sonuc = await pool.query(
    `SELECT *
     FROM ogrenciler
     WHERE aktif = TRUE
     ORDER BY id`
);

        if (sonuc.rowCount === 0) {
            return res.status(404).json({
                mesaj: "Öğrenci bulunamadı"
            });
        }

        res.json(sonuc.rows[0]);

    } catch (hata) {
        console.log(hata.message);

        res.status(500).json({
            mesaj: "Öğrenci getirilemedi"
        });
    }
});

// Yeni öğrenci ekle
app.post("/api/ogrenciler", async (req, res) => {
    const { ad, soyad, ogrenci_no } = req.body;

    if (!ad || !soyad || !ogrenci_no) {
        return res.status(400).json({
            mesaj: "Ad, soyad ve öğrenci numarası zorunludur"
        });
    }

    try {
      const sonuc = await pool.query(
    `SELECT *
     FROM ogrenciler
     WHERE aktif = TRUE
     ORDER BY id`
);

        res.status(201).json(sonuc.rows[0]);

    } catch (hata) {
        console.log(hata.message);

        if (hata.code === "23505") {
            return res.status(409).json({
                mesaj: "Bu öğrenci numarası zaten kullanılıyor"
            });
        }

        res.status(500).json({
            mesaj: "Öğrenci eklenemedi"
        });
    }
});

// Öğrenci güncelle
app.put("/api/ogrenciler/:id", async (req, res) => {
    const id = Number(req.params.id);
    const { ad, soyad, ogrenci_no } = req.body;

    if (!Number.isInteger(id) || id <= 0) {
        return res.status(400).json({
            mesaj: "Geçerli bir öğrenci ID'si giriniz"
        });
    }

    if (!ad || !soyad || !ogrenci_no) {
        return res.status(400).json({
            mesaj: "Ad, soyad ve öğrenci numarası zorunludur"
        });
    }

    try {
        const sonuc = await pool.query(
           `UPDATE ogrenciler
 SET ad = $1,
     soyad = $2,
     ogrenci_no = $3
 WHERE id = $4
   AND aktif = TRUE
 RETURNING *`, 
            [ad, soyad, ogrenci_no, id]
        );

        if (sonuc.rowCount === 0) {
            return res.status(404).json({
                mesaj: "Güncellenecek öğrenci bulunamadı"
            });
        }

        res.json({
            mesaj: "Öğrenci başarıyla güncellendi",
            ogrenci: sonuc.rows[0]
        });

    } catch (hata) {
        console.log(hata.message);

        if (hata.code === "23505") {
            return res.status(409).json({
                mesaj: "Bu öğrenci numarası başka bir öğrenciye ait"
            });
        }

        res.status(500).json({
            mesaj: "Öğrenci güncellenemedi"
        });
    }
});

// Öğrenci sil

app.delete("/api/ogrenciler/:id", async (req, res) => {
    const id = Number(req.params.id);

    if (!Number.isInteger(id) || id <= 0) {
        return res.status(400).json({
            mesaj: "Geçerli bir öğrenci ID'si giriniz"
        });
    }

    try {
        const sonuc = await pool.query(
            `UPDATE ogrenciler
             SET aktif = FALSE,
                 silinme_tarihi = NOW()
             WHERE id = $1
               AND aktif = TRUE
             RETURNING *`,
            [id]
        );

        if (sonuc.rowCount === 0) {
            return res.status(404).json({
                mesaj: "Pasif yapılacak öğrenci bulunamadı"
            });
        }

        res.json({
            mesaj: "Öğrenci pasif duruma getirildi",
            ogrenci: sonuc.rows[0]
        });

    } catch (hata) {
        console.log(hata.message);

        res.status(500).json({
            mesaj: "Öğrenci pasif duruma getirilemedi"
        });
    }
});

// PostgreSQL bağlantı testi
app.get("/db-test", async (req, res) => {
    try {
        const sonuc = await pool.query("SELECT NOW()");

        res.json({
            mesaj: "PostgreSQL bağlantısı başarılı",
            zaman: sonuc.rows[0].now
        });

    } catch (hata) {
        console.log("GERÇEK HATA:", hata.message);

        res.status(500).json({
            mesaj: "Veritabanı bağlantısı başarısız",
            hata: hata.message
        });
    }
});

// Sunucuyu başlat
app.listen(PORT, () => {
    console.log(
        "Sunucu http://localhost:" + PORT + " adresinde çalışıyor"
    );
});






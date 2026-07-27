const bcrypt = require("bcryptjs");
const jwt = require("jsonwebtoken");
const prisma = require("../config/prisma");
const { jwtSecret, jwtExpiresIn } = require("../config/env");
const { temizMetin } = require("../utils/yardimcilar");

async function giris(req, res) {
    const kullaniciAdi = temizMetin(req.body.kullanici_adi);
    const sifre = req.body.sifre;

    if (!kullaniciAdi || !sifre) {
        return res.status(400).json({
            mesaj: "Kullanıcı adı ve şifre zorunludur"
        });
    }

    const kullanici = await prisma.kullanicilar.findUnique({
        where: { kullanici_adi: kullaniciAdi }
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

    const sifreDogruMu = await bcrypt.compare(sifre, kullanici.sifre_hash);

    if (!sifreDogruMu) {
        return res.status(401).json({
            mesaj: "Kullanıcı adı veya şifre hatalı"
        });
    }

    const rol = String(kullanici.rol).toUpperCase();

    const token = jwt.sign(
        {
            kullaniciId: kullanici.id,
            rol
        },
        jwtSecret,
        { expiresIn: jwtExpiresIn }
    );

    return res.json({
        mesaj: "Giriş başarılı",
        token,
        kullanici: {
            id: kullanici.id,
            kullanici_adi: kullanici.kullanici_adi,
            email: kullanici.email,
            rol
        }
    });
}

async function ben(req, res) {
    const kullanici = await prisma.kullanicilar.findUnique({
        where: { id: req.kullanici.id },
        select: {
            id: true,
            kullanici_adi: true,
            email: true,
            rol: true,
            aktif: true,
            ogrenciler: {
                select: {
                    id: true,
                    ad: true,
                    soyad: true,
                    ogrenci_no: true
                }
            },
            ogretmenler: {
                select: {
                    id: true,
                    ad: true,
                    soyad: true,
                    sicil_no: true,
                    brans: true
                }
            }
        }
    });

    return res.json({
        mesaj: "Token geçerli",
        kullanici: {
            ...kullanici,
            rol: String(kullanici.rol).toUpperCase()
        }
    });
}

module.exports = {
    giris,
    ben
};

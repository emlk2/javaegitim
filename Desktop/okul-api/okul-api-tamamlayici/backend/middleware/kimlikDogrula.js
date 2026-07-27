const jwt = require("jsonwebtoken");
const prisma = require("../config/prisma");
const { jwtSecret } = require("../config/env");

async function kimlikDogrula(req, res, next) {
    const authorization = req.headers.authorization;

    if (!authorization) {
        return res.status(401).json({
            mesaj: "Giriş yapmanız gerekiyor"
        });
    }

    const parcalar = authorization.trim().split(/\s+/);

    if (
        parcalar.length !== 2 ||
        parcalar[0].toLowerCase() !== "bearer"
    ) {
        return res.status(401).json({
            mesaj: "Token formatı geçersiz"
        });
    }

    let cozulmusToken;

    try {
        cozulmusToken = jwt.verify(parcalar[1], jwtSecret);
    } catch {
        return res.status(401).json({
            mesaj: "Token geçersiz veya süresi dolmuş"
        });
    }

    try {
        const kullanici = await prisma.kullanicilar.findUnique({
            where: { id: Number(cozulmusToken.kullaniciId) },
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
                mesaj: "Kullanıcı bulunamadı veya hesap pasif"
            });
        }

        req.kullanici = {
            ...kullanici,
            rol: String(kullanici.rol).toUpperCase()
        };

        next();
    } catch (hata) {
        next(hata);
    }
}

module.exports = kimlikDogrula;

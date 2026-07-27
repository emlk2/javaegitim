const jwt = require("jsonwebtoken");

function kimlikDogrula(req, res, next) {
    const authorization = req.headers.authorization;

    if (!authorization) {
        return res.status(401).json({
            mesaj: "Giriş yapmanız gerekiyor"
        });
    }

    const parcalar = authorization.split(" ");

    if (
        parcalar.length !== 2 ||
        parcalar[0] !== "Bearer"
    ) {
        return res.status(401).json({
            mesaj: "Token formatı geçersiz"
        });
    }

    const token = parcalar[1];

    try {
        const cozulmusToken = jwt.verify(
            token,
            process.env.JWT_SECRET
        );

        req.kullanici = {
            id: cozulmusToken.kullaniciId,
            rol: cozulmusToken.rol
        };

        next();

    } catch (hata) {
        return res.status(401).json({
            mesaj: "Token geçersiz veya süresi dolmuş"
        });
    }
}

module.exports = kimlikDogrula;
function rolDogrula(...izinliRoller) {
    const roller = izinliRoller.map((rol) => String(rol).toUpperCase());

    return (req, res, next) => {
        if (!req.kullanici) {
            return res.status(401).json({
                mesaj: "Bu işlem için giriş yapmalısınız"
            });
        }

        const kullaniciRolu = String(req.kullanici.rol).toUpperCase();

        if (!roller.includes(kullaniciRolu)) {
            return res.status(403).json({
                mesaj: "Bu işlem için yetkiniz bulunmuyor"
            });
        }

        next();
    };
}

module.exports = rolDogrula;

function rolDogrula(...izinliRoller) {
    return (req, res, next) => {
        if (!req.kullanici) {
            return res.status(401).json({
                mesaj: "Bu işlem için giriş yapmalısınız"
            });
        }

        if (!izinliRoller.includes(req.kullanici.rol)) {
            return res.status(403).json({
                mesaj: "Bu işlem için yetkiniz bulunmuyor"
            });
        }

        next();
    };
}

module.exports = rolDogrula;
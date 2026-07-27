function bulunamadi(req, res) {
    return res.status(404).json({
        mesaj: "İstenen API adresi bulunamadı"
    });
}

function hataYakala(hata, req, res, next) {
    console.error("Sunucu hatası:", hata);

    if (hata.code === "P2002") {
        const alanlar = Array.isArray(hata.meta?.target)
            ? hata.meta.target.join(", ")
            : "benzersiz alan";

        return res.status(409).json({
            mesaj: `${alanlar} daha önce kullanılmış`
        });
    }

    if (hata.code === "P2025") {
        return res.status(404).json({
            mesaj: "İşlem yapılacak kayıt bulunamadı"
        });
    }

    return res.status(hata.status || 500).json({
        mesaj: hata.status ? hata.message : "Sunucuda beklenmeyen bir hata oluştu"
    });
}

module.exports = {
    bulunamadi,
    hataYakala
};

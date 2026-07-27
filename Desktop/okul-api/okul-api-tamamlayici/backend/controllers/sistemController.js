const prisma = require("../config/prisma");

async function durum(req, res) {
    await prisma.$queryRaw`SELECT 1`;

    const [kullaniciSayisi, aktifOgrenciSayisi, aktifOgretmenSayisi, aktifDersSayisi] =
        await Promise.all([
            prisma.kullanicilar.count(),
            prisma.ogrenciler.count({ where: { aktif: true } }),
            prisma.ogretmenler.count({ where: { aktif: true } }),
            prisma.dersler.count({ where: { aktif: true } })
        ]);

    return res.json({
        mesaj: "Prisma ve PostgreSQL bağlantısı başarılı",
        kullaniciSayisi,
        aktifOgrenciSayisi,
        aktifOgretmenSayisi,
        aktifDersSayisi
    });
}

module.exports = { durum };

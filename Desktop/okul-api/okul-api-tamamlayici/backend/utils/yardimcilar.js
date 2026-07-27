function pozitifTamSayi(deger) {
    const sayi = Number(deger);
    return Number.isInteger(sayi) && sayi > 0 ? sayi : null;
}

function temizMetin(deger) {
    return typeof deger === "string" ? deger.trim() : "";
}

function rolBuyukHarf(rol) {
    return temizMetin(rol).toUpperCase();
}

module.exports = {
    pozitifTamSayi,
    temizMetin,
    rolBuyukHarf
};

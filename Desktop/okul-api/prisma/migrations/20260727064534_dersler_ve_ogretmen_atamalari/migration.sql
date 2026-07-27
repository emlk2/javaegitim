-- AlterTable
ALTER TABLE "ogrenciler" ADD COLUMN     "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP;

-- CreateTable
CREATE TABLE "ogretmenler" (
    "id" SERIAL NOT NULL,
    "ad" VARCHAR(100) NOT NULL,
    "soyad" VARCHAR(100) NOT NULL,
    "sicil_no" VARCHAR(30) NOT NULL,
    "brans" VARCHAR(100),
    "aktif" BOOLEAN NOT NULL DEFAULT true,
    "silinme_tarihi" TIMESTAMPTZ(6),
    "kayit_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "kullanici_id" INTEGER,

    CONSTRAINT "ogretmenler_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "dersler" (
    "id" SERIAL NOT NULL,
    "ders_kodu" VARCHAR(30) NOT NULL,
    "ders_adi" VARCHAR(150) NOT NULL,
    "aciklama" VARCHAR(500),
    "aktif" BOOLEAN NOT NULL DEFAULT true,
    "olusturma_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "dersler_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "ogretmen_dersleri" (
    "id" SERIAL NOT NULL,
    "ogretmen_id" INTEGER NOT NULL,
    "ders_id" INTEGER NOT NULL,
    "aktif" BOOLEAN NOT NULL DEFAULT true,
    "atama_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "ogretmen_dersleri_pkey" PRIMARY KEY ("id")
);

-- CreateIndex
CREATE UNIQUE INDEX "ogretmenler_sicil_no_key" ON "ogretmenler"("sicil_no");

-- CreateIndex
CREATE UNIQUE INDEX "ogretmenler_kullanici_id_key" ON "ogretmenler"("kullanici_id");

-- CreateIndex
CREATE UNIQUE INDEX "dersler_ders_kodu_key" ON "dersler"("ders_kodu");

-- CreateIndex
CREATE UNIQUE INDEX "uq_ogretmen_dersleri" ON "ogretmen_dersleri"("ogretmen_id", "ders_id");

-- RenameForeignKey
ALTER TABLE "ogrenciler" RENAME CONSTRAINT "fk_ogrenciler_kullanicilar" TO "fk_ogretmenler_kullanicilar";

-- AddForeignKey
ALTER TABLE "ogretmenler" ADD CONSTRAINT "fk_ogrenciler_kullanicilar" FOREIGN KEY ("kullanici_id") REFERENCES "kullanicilar"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "ogretmen_dersleri" ADD CONSTRAINT "fk_ogretmen_dersleri_ogretmenler" FOREIGN KEY ("ogretmen_id") REFERENCES "ogretmenler"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "ogretmen_dersleri" ADD CONSTRAINT "fk_ogretmen_dersleri_dersler" FOREIGN KEY ("ders_id") REFERENCES "dersler"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

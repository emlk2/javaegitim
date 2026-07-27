-- CreateTable
CREATE TABLE "notlar" (
    "id" SERIAL NOT NULL,
    "sinav_id" INTEGER NOT NULL,
    "ogrenci_id" INTEGER NOT NULL,
    "puan" DOUBLE PRECISION NOT NULL,
    "aciklama" VARCHAR(500),
    "aktif" BOOLEAN NOT NULL DEFAULT true,
    "olusturma_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "notlar_pkey" PRIMARY KEY ("id")
);

-- CreateIndex
CREATE INDEX "notlar_sinav_id_idx" ON "notlar"("sinav_id");

-- CreateIndex
CREATE INDEX "notlar_ogrenci_id_idx" ON "notlar"("ogrenci_id");

-- CreateIndex
CREATE UNIQUE INDEX "notlar_sinav_id_ogrenci_id_key" ON "notlar"("sinav_id", "ogrenci_id");

-- AddForeignKey
ALTER TABLE "notlar" ADD CONSTRAINT "notlar_sinav_id_fkey" FOREIGN KEY ("sinav_id") REFERENCES "sinavlar"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "notlar" ADD CONSTRAINT "notlar_ogrenci_id_fkey" FOREIGN KEY ("ogrenci_id") REFERENCES "ogrenciler"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

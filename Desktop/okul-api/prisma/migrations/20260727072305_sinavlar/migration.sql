-- CreateTable
CREATE TABLE "sinavlar" (
    "id" SERIAL NOT NULL,
    "ders_id" INTEGER NOT NULL,
    "ogretmen_id" INTEGER NOT NULL,
    "sinav_adi" VARCHAR(150) NOT NULL,
    "sinav_tarihi" TIMESTAMPTZ(6) NOT NULL,
    "maksimum_puan" DOUBLE PRECISION NOT NULL DEFAULT 100,
    "aciklama" VARCHAR(500),
    "aktif" BOOLEAN NOT NULL DEFAULT true,
    "olusturma_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "sinavlar_pkey" PRIMARY KEY ("id")
);

-- CreateIndex
CREATE INDEX "idx_sinavlar_ders_id" ON "sinavlar"("ders_id");

-- CreateIndex
CREATE INDEX "idx_sinavlar_ogretmen_id" ON "sinavlar"("ogretmen_id");

-- AddForeignKey
ALTER TABLE "sinavlar" ADD CONSTRAINT "fk_sinavlar_dersler" FOREIGN KEY ("ders_id") REFERENCES "dersler"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "sinavlar" ADD CONSTRAINT "fk_sinavlar_ogretmenler" FOREIGN KEY ("ogretmen_id") REFERENCES "ogretmenler"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

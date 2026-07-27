-- CreateTable
CREATE TABLE "devamsizliklar" (
    "id" SERIAL NOT NULL,
    "ogrenci_id" INTEGER NOT NULL,
    "ders_id" INTEGER NOT NULL,
    "ogretmen_id" INTEGER NOT NULL,
    "devamsizlik_tarihi" DATE NOT NULL,
    "durum" VARCHAR(20) NOT NULL,
    "aciklama" VARCHAR(500),
    "aktif" BOOLEAN NOT NULL DEFAULT true,
    "olusturma_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "devamsizliklar_pkey" PRIMARY KEY ("id")
);

-- CreateIndex
CREATE INDEX "devamsizliklar_ogrenci_id_idx" ON "devamsizliklar"("ogrenci_id");

-- CreateIndex
CREATE INDEX "devamsizliklar_ders_id_idx" ON "devamsizliklar"("ders_id");

-- CreateIndex
CREATE INDEX "devamsizliklar_ogretmen_id_idx" ON "devamsizliklar"("ogretmen_id");

-- CreateIndex
CREATE INDEX "devamsizliklar_devamsizlik_tarihi_idx" ON "devamsizliklar"("devamsizlik_tarihi");

-- CreateIndex
CREATE UNIQUE INDEX "devamsizliklar_ogrenci_id_ders_id_devamsizlik_tarihi_key" ON "devamsizliklar"("ogrenci_id", "ders_id", "devamsizlik_tarihi");

-- AddForeignKey
ALTER TABLE "devamsizliklar" ADD CONSTRAINT "devamsizliklar_ogrenci_id_fkey" FOREIGN KEY ("ogrenci_id") REFERENCES "ogrenciler"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "devamsizliklar" ADD CONSTRAINT "devamsizliklar_ders_id_fkey" FOREIGN KEY ("ders_id") REFERENCES "dersler"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "devamsizliklar" ADD CONSTRAINT "devamsizliklar_ogretmen_id_fkey" FOREIGN KEY ("ogretmen_id") REFERENCES "ogretmenler"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

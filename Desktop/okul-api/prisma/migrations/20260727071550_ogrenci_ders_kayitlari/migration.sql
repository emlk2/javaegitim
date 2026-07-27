-- CreateTable
CREATE TABLE "ogrenci_dersleri" (
    "id" SERIAL NOT NULL,
    "ogrenci_id" INTEGER NOT NULL,
    "ders_id" INTEGER NOT NULL,
    "aktif" BOOLEAN NOT NULL DEFAULT true,
    "kayit_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "ogrenci_dersleri_pkey" PRIMARY KEY ("id")
);

-- CreateIndex
CREATE UNIQUE INDEX "uq_ogrenci_dersleri" ON "ogrenci_dersleri"("ogrenci_id", "ders_id");

-- AddForeignKey
ALTER TABLE "ogrenci_dersleri" ADD CONSTRAINT "fk_ogrenci_dersleri_ogrenciler" FOREIGN KEY ("ogrenci_id") REFERENCES "ogrenciler"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "ogrenci_dersleri" ADD CONSTRAINT "fk_ogrenci_dersleri_dersler" FOREIGN KEY ("ders_id") REFERENCES "dersler"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- -- AlterTable
-- ALTER TABLE "ogrenciler" ADD COLUMN     "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP;

-- -- CreateTable
-- CREATE TABLE "ogretmenler" (
--     "id" SERIAL NOT NULL,
--     "ad" VARCHAR(100) NOT NULL,
--     "soyad" VARCHAR(100) NOT NULL,
--     "sicil_no" VARCHAR(30) NOT NULL,
--     "brans" VARCHAR(100),
--     "aktif" BOOLEAN NOT NULL DEFAULT true,
--     "silinme_tarihi" TIMESTAMPTZ(6),
--     "kayit_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
--     "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
--     "kullanici_id" INTEGER,

--     CONSTRAINT "ogretmenler_pkey" PRIMARY KEY ("id")
-- );

-- -- CreateIndex
-- CREATE UNIQUE INDEX "ogretmenler_sicil_no_key" ON "ogretmenler"("sicil_no");

-- -- CreateIndex
-- CREATE UNIQUE INDEX "ogretmenler_kullanici_id_key" ON "ogretmenler"("kullanici_id");

-- -- RenameForeignKey
-- ALTER TABLE "ogrenciler" RENAME CONSTRAINT "fk_ogrenciler_kullanicilar" TO "fk_ogretmenler_kullanicilar";

-- -- AddForeignKey
-- ALTER TABLE "ogretmenler" ADD CONSTRAINT "fk_ogrenciler_kullanicilar" FOREIGN KEY ("kullanici_id") REFERENCES "kullanicilar"("id") ON DELETE RESTRICT ON UPDATE CASCADE;


-- CreateSchema
CREATE SCHEMA IF NOT EXISTS "public";

-- CreateTable
CREATE TABLE "public"."kullanicilar" (
    "id" SERIAL NOT NULL,
    "kullanici_adi" VARCHAR(50) NOT NULL,
    "email" VARCHAR(150) NOT NULL,
    "sifre_hash" VARCHAR(255) NOT NULL,
    "rol" VARCHAR(20) NOT NULL,
    "aktif" BOOLEAN NOT NULL DEFAULT true,
    "olusturma_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "guncelleme_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT "kullanicilar_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "public"."ogrenciler" (
    "id" SERIAL NOT NULL,
    "ad" VARCHAR(100) NOT NULL,
    "soyad" VARCHAR(100) NOT NULL,
    "ogrenci_no" VARCHAR(20) NOT NULL,
    "aktif" BOOLEAN NOT NULL DEFAULT true,
    "silinme_tarihi" TIMESTAMPTZ(6),
    "kayit_tarihi" TIMESTAMPTZ(6) NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "kullanici_id" INTEGER,

    CONSTRAINT "ogrenciler_pkey" PRIMARY KEY ("id")
);

-- CreateIndex
CREATE UNIQUE INDEX "kullanicilar_email_key" ON "public"."kullanicilar"("email" ASC);

-- CreateIndex
CREATE UNIQUE INDEX "kullanicilar_kullanici_adi_key" ON "public"."kullanicilar"("kullanici_adi" ASC);

-- CreateIndex
CREATE UNIQUE INDEX "ogrenciler_kullanici_id_key" ON "public"."ogrenciler"("kullanici_id" ASC);

-- CreateIndex
CREATE UNIQUE INDEX "ogrenciler_ogrenci_no_key" ON "public"."ogrenciler"("ogrenci_no" ASC);

-- AddForeignKey
ALTER TABLE "public"."ogrenciler" ADD CONSTRAINT "fk_ogrenciler_kullanicilar" FOREIGN KEY ("kullanici_id") REFERENCES "public"."kullanicilar"("id") ON DELETE RESTRICT ON UPDATE CASCADE;


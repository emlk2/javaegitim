ALTER TABLE "kullanicilar"
DROP CONSTRAINT IF EXISTS "kullanicilar_rol_check";

ALTER TABLE "kullanicilar"
ADD CONSTRAINT "kullanicilar_rol_check"
CHECK ("rol" IN ('admin', 'ogretmen', 'ogrenci'));

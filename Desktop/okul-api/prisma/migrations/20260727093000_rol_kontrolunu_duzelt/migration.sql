ALTER TABLE "public"."kullanicilar"
DROP CONSTRAINT IF EXISTS "kullanicilar_rol_check";

ALTER TABLE "public"."kullanicilar"
ADD CONSTRAINT "kullanicilar_rol_check"
CHECK (rol::text = ANY (
    ARRAY[
        'admin'::character varying,
        'ogretmen'::character varying,
        'ogrenci'::character varying
    ]::text[]
));

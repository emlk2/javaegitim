import "dotenv/config";

import bcrypt from "bcryptjs";
import { PrismaPg } from "@prisma/adapter-pg";
import { PrismaClient } from "./generated/prisma/client";

const databaseUrl = process.env.DATABASE_URL;

if (!databaseUrl) {
    throw new Error("DATABASE_URL .env dosyasında bulunamadı.");
}

const adapter = new PrismaPg({
    connectionString: databaseUrl
});

const prisma = new PrismaClient({
    adapter
});

async function ayseHesabiOlustur() {
    const ogrenciNo = "2026001";

    const kullaniciAdi =
        process.env.AYSE_KULLANICI_ADI?.trim();

    const email =
        process.env.AYSE_EMAIL?.trim();

    const sifre =
        process.env.AYSE_SIFRE;

    if (!kullaniciAdi || !email || !sifre) {
        throw new Error(
            "AYSE_KULLANICI_ADI, AYSE_EMAIL ve AYSE_SIFRE eksik."
        );
    }

    if (sifre.length < 8) {
        throw new Error(
            "Ayşe'nin şifresi en az 8 karakter olmalıdır."
        );
    }

    // Ayşe'nin öğrenci kaydını buluyoruz.
    const ayse = await prisma.ogrenciler.findUnique({
        where: {
            ogrenci_no: ogrenciNo
        },
        include: {
            kullanicilar: true
        }
    });

    if (!ayse) {
        throw new Error(
            `${ogrenciNo} numaralı Ayşe kaydı bulunamadı.`
        );
    }

    if (!ayse.aktif) {
        throw new Error(
            "Ayşe'nin öğrenci kaydı pasif durumda."
        );
    }

    // Zaten bağlı hesabı varsa yeniden oluşturmayalım.
    if (ayse.kullanicilar) {
        console.log("Ayşe'nin zaten bağlı bir kullanıcı hesabı var:");

        console.table([{
            ogrenci_id: ayse.id,
            kullanici_id: ayse.kullanicilar.id,
            kullanici_adi: ayse.kullanicilar.kullanici_adi,
            email: ayse.kullanicilar.email,
            rol: ayse.kullanicilar.rol
        }]);

        return;
    }

    // Aynı kullanıcı adı veya e-posta daha önce kullanılmış mı?
    const cakisanKullanici =
        await prisma.kullanicilar.findFirst({
            where: {
                OR: [
                    {
                        kullanici_adi: kullaniciAdi
                    },
                    {
                        email: email
                    }
                ]
            }
        });

    if (cakisanKullanici) {
        throw new Error(
            "Bu kullanıcı adı veya e-posta başka bir hesapta kullanılıyor."
        );
    }

    // Düz şifreyi veritabanına yazmıyoruz.
    const sifreHash = await bcrypt.hash(sifre, 12);

    // Kullanıcı oluşturma ve Ayşe'ye bağlama tek işlemde yapılır.
    const sonuc = await prisma.$transaction(async (tx) => {
        const yeniKullanici =
            await tx.kullanicilar.create({
                data: {
                    kullanici_adi: kullaniciAdi,
                    email: email,
                    sifre_hash: sifreHash,
                    rol: "ogrenci",
                    aktif: true
                }
            });

        const guncellenenAyse =
            await tx.ogrenciler.update({
                where: {
                    id: ayse.id
                },
                data: {
                    kullanicilar: {
                        connect: {
                            id: yeniKullanici.id
                        }
                    }
                },
                include: {
                    kullanicilar: {
                        select: {
                            id: true,
                            kullanici_adi: true,
                            email: true,
                            rol: true,
                            aktif: true
                        }
                    }
                }
            });

        return guncellenenAyse;
    });

    console.log("Ayşe hesabı başarıyla oluşturuldu ve bağlandı:");

    console.table([{
        ogrenci_id: sonuc.id,
        ad: sonuc.ad,
        soyad: sonuc.soyad,
        ogrenci_no: sonuc.ogrenci_no,
        kullanici_id: sonuc.kullanici_id,
        kullanici_adi: sonuc.kullanicilar?.kullanici_adi,
        rol: sonuc.kullanicilar?.rol
    }]);
}

ayseHesabiOlustur()
    .catch((hata) => {
        console.error("HATA:", hata.message);
        process.exitCode = 1;
    })
    .finally(async () => {
        await prisma.$disconnect();
    });
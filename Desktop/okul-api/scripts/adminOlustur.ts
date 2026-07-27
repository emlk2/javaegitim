import "dotenv/config";

import bcrypt from "bcryptjs";
import { PrismaPg } from "@prisma/adapter-pg";
import { PrismaClient } from "../generated/prisma/client";

const databaseUrl = process.env.DATABASE_URL;

if (!databaseUrl) {
    throw new Error(
        "DATABASE_URL .env dosyasında bulunamadı."
    );
}

const adapter = new PrismaPg({
    connectionString: databaseUrl
});

const prisma = new PrismaClient({
    adapter
});

async function adminOlustur() {
    const kullaniciAdi =
        process.env.ADMIN_USERNAME?.trim();

    const email =
        process.env.ADMIN_EMAIL?.trim().toLowerCase();

    const sifre =
        process.env.ADMIN_PASSWORD;

    if (!kullaniciAdi || !email || !sifre) {
        throw new Error(
            "ADMIN_USERNAME, ADMIN_EMAIL veya ADMIN_PASSWORD eksik."
        );
    }

    if (sifre.length < 8) {
        throw new Error(
            "Admin şifresi en az 8 karakter olmalıdır."
        );
    }

    const mevcutKullanici =
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

    if (mevcutKullanici) {
        throw new Error(
            "Bu kullanıcı adı veya e-posta zaten kullanılıyor."
        );
    }

    const sifreHash =
        await bcrypt.hash(sifre, 12);

    const admin =
        await prisma.kullanicilar.create({
            data: {
                kullanici_adi: kullaniciAdi,
                email: email,
                sifre_hash: sifreHash,
                rol: "admin",
                aktif: true
            },
            select: {
                id: true,
                kullanici_adi: true,
                email: true,
                rol: true,
                aktif: true
            }
        });

    console.log("Admin hesabı oluşturuldu:");

    console.table([admin]);
}

adminOlustur()
    .catch((hata) => {
        console.error("HATA:", hata.message);
        process.exitCode = 1;
    })
    .finally(async () => {
        await prisma.$disconnect();
    });
require("dotenv").config();

const bcrypt = require("bcryptjs");
const pool = require("./db");

async function adminOlustur() {
    try {
        const kullaniciAdi =
            process.env.ADMIN_USERNAME?.trim();

        const email =
            process.env.ADMIN_EMAIL?.trim().toLowerCase();

        const sifre =
            process.env.ADMIN_PASSWORD;

        if (!kullaniciAdi || !email || !sifre) {
            throw new Error(
                ".env dosyasında yönetici bilgileri eksik"
            );
        }

        if (sifre.length < 8) {
            throw new Error(
                "Yönetici şifresi en az 8 karakter olmalıdır"
            );
        }

        const sifreHash = await bcrypt.hash(sifre, 10);

        const sonuc = await pool.query(
            `INSERT INTO kullanicilar
                (kullanici_adi, email, sifre_hash, rol)
             VALUES ($1, $2, $3, $4)
             RETURNING
                id,
                kullanici_adi,
                email,
                rol,
                aktif`,
            [
                kullaniciAdi,
                email,
                sifreHash,
                "admin"
            ]
        );

        console.log("Yönetici hesabı oluşturuldu:");
        console.table(sonuc.rows);

    } catch (hata) {
        if (hata.code === "23505") {
            console.log(
                "Bu kullanıcı adı veya e-posta zaten kayıtlı."
            );
        } else {
            console.log(
                "Yönetici oluşturma hatası:",
                hata.message
            );
        }

    } finally {
        await pool.end();
    }
}

adminOlustur();
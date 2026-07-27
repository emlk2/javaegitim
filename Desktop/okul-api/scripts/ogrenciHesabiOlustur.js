require("dotenv").config();

const bcrypt = require("bcryptjs");
const pool = require("./db");

async function ogrenciHesabiOlustur() {
    const client = await pool.connect();

    try {
        await client.query("BEGIN");

        const ogrenciId = 1;
        const kullaniciAdi = "ayse2026001";
        const email = "ayse@okul.com";
        const sifre = "Ayse1234!";

        const ogrenciSonucu = await client.query(
            `SELECT id, kullanici_id
             FROM ogrenciler
             WHERE id = $1
               AND aktif = TRUE`,
            [ogrenciId]
        );

        if (ogrenciSonucu.rowCount === 0) {
            throw new Error("Öğrenci bulunamadı");
        }

        if (ogrenciSonucu.rows[0].kullanici_id) {
            throw new Error("Bu öğrencinin zaten bir kullanıcı hesabı var");
        }

        const sifreHash = await bcrypt.hash(sifre, 10);

        const kullaniciSonucu = await client.query(
            `INSERT INTO kullanicilar
                (kullanici_adi, email, sifre_hash, rol)
             VALUES ($1, $2, $3, 'ogrenci')
             RETURNING id, kullanici_adi, email, rol`,
            [kullaniciAdi, email, sifreHash]
        );

        const kullaniciId = kullaniciSonucu.rows[0].id;

        await client.query(
            `UPDATE ogrenciler
             SET kullanici_id = $1
             WHERE id = $2`,
            [kullaniciId, ogrenciId]
        );

        await client.query("COMMIT");

        console.log("Öğrenci hesabı oluşturuldu:");
        console.table(kullaniciSonucu.rows);

    } catch (hata) {
        await client.query("ROLLBACK");

        if (hata.code === "23505") {
            console.log("Kullanıcı adı veya e-posta zaten kullanılıyor.");
        } else {
            console.log("Hata:", hata.message);
        }

    } finally {
        client.release();
        await pool.end();
    }
}

ogrenciHesabiOlustur();
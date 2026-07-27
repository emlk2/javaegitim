const path = require("path");

require("dotenv").config({
    path: path.join(__dirname, "../../.env")
});

const zorunluDegiskenler = ["DATABASE_URL", "JWT_SECRET"];

for (const degisken of zorunluDegiskenler) {
    if (!process.env[degisken]) {
        throw new Error(`${degisken} .env dosyasında bulunamadı.`);
    }
}

module.exports = {
    port: Number(process.env.PORT) || 3000,
    jwtSecret: process.env.JWT_SECRET,
    jwtExpiresIn: process.env.JWT_EXPIRES_IN || "2h",
    databaseUrl: process.env.DATABASE_URL
};

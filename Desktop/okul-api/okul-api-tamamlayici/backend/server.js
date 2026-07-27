const app = require("./app");
const prisma = require("./config/prisma");
const { port } = require("./config/env");

const sunucu = app.listen(port, () => {
    console.log(`Sunucu http://localhost:${port} adresinde çalışıyor`);
});

async function guvenliKapat(sinyal) {
    console.log(`\n${sinyal} alındı, sunucu kapatılıyor...`);

    sunucu.close(async () => {
        await prisma.$disconnect();
        process.exit(0);
    });

    setTimeout(() => process.exit(1), 10000).unref();
}

process.on("SIGINT", () => guvenliKapat("SIGINT"));
process.on("SIGTERM", () => guvenliKapat("SIGTERM"));

const { PrismaPg } = require("@prisma/adapter-pg");
const { PrismaClient } = require("../../generated/prisma/client");
const { databaseUrl } = require("./env");

const adapter = new PrismaPg({
    connectionString: databaseUrl
});

const prisma = new PrismaClient({ adapter });

module.exports = prisma;

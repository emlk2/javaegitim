require("dotenv").config();

const app = require("./app");
const { sequelize } = require("./models");
const PORT = Number(process.env.PORT) || 3000;

async function startServer() {
    try {
        await sequelize.authenticate();

        console.log("PostgreSQL connection established successfully.");

        app.listen(PORT, () => {
            console.log(`Server is running at http://localhost:${PORT}`);
        });
    } catch (error) {
        console.error("Server could not be started.");
        console.error("Database connection error:", error.message);

        process.exit(1);
    }
}

startServer();
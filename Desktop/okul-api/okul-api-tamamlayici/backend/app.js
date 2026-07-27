const path = require("path");
require("./config/env");

const express = require("express");
const authRoutes = require("./routes/authRoutes");
const ogrenciRoutes = require("./routes/ogrenciRoutes");
const ogretmenRoutes = require("./routes/ogretmenRoutes");
const dersRoutes = require("./routes/dersRoutes");
const sinavRoutes = require("./routes/sinavRoutes");
const notRoutes = require("./routes/notRoutes");
const devamsizlikRoutes = require("./routes/devamsizlikRoutes");
const sistemRoutes = require("./routes/sistemRoutes");
const sistemController = require("./controllers/sistemController");
const asyncHandler = require("./utils/asyncHandler");
const { bulunamadi, hataYakala } = require("./middleware/hataMiddleware");

const app = express();

app.disable("x-powered-by");
app.use(express.json({ limit: "1mb" }));
app.use(express.urlencoded({ extended: false }));

app.use(express.static(path.join(__dirname, "../frontend")));

app.use("/api/auth", authRoutes);
app.use("/api/ogrenciler", ogrenciRoutes);
app.use("/api/ogrenci", ogrenciRoutes);
app.use("/api/ogretmenler", ogretmenRoutes);
app.use("/api/ogretmen", ogretmenRoutes);
app.use("/api/dersler", dersRoutes);
app.use("/api/sinavlar", sinavRoutes);
app.use("/api/notlar", notRoutes);
app.use("/api/devamsizliklar", devamsizlikRoutes);
app.use("/api/sistem", sistemRoutes);
app.get("/db-test", asyncHandler(sistemController.durum));

app.get("/*splat", (req, res, next) => {
    if (req.path.startsWith("/api/")) {
        return next();
    }

    return res.sendFile(path.join(__dirname, "../frontend/index.html"));
});

app.use(bulunamadi);
app.use(hataYakala);

module.exports = app;

const express = require("express");
const asyncHandler = require("../utils/asyncHandler");
const kimlikDogrula = require("../middleware/kimlikDogrula");
const rolDogrula = require("../middleware/rolDogrula");
const controller = require("../controllers/dersController");

const router = express.Router();

router.use(kimlikDogrula);
router.get("/", rolDogrula("ADMIN", "OGRETMEN", "OGRENCI"), asyncHandler(controller.listele));
router.post("/", rolDogrula("ADMIN"), asyncHandler(controller.ekle));
router.put("/:id", rolDogrula("ADMIN"), asyncHandler(controller.guncelle));
router.delete("/:id", rolDogrula("ADMIN"), asyncHandler(controller.pasifYap));
router.post("/:id/ogrenciler", rolDogrula("ADMIN", "OGRETMEN"), asyncHandler(controller.ogrenciAta));
router.delete("/:id/ogrenciler/:ogrenciId", rolDogrula("ADMIN", "OGRETMEN"), asyncHandler(controller.ogrenciCikar));

module.exports = router;

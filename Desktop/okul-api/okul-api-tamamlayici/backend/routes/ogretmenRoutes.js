const express = require("express");
const asyncHandler = require("../utils/asyncHandler");
const kimlikDogrula = require("../middleware/kimlikDogrula");
const rolDogrula = require("../middleware/rolDogrula");
const controller = require("../controllers/ogretmenController");

const router = express.Router();

router.use(kimlikDogrula);
router.get("/ben", rolDogrula("OGRETMEN"), asyncHandler(controller.kendiProfilim));
router.get("/ben/dersler", rolDogrula("OGRETMEN"), asyncHandler(controller.kendiDerslerim));
router.get("/ben/ogrenciler", rolDogrula("OGRETMEN"), asyncHandler(controller.kendiOgrencilerim));

router.get("/", rolDogrula("ADMIN"), asyncHandler(controller.listele));
router.get("/:id", rolDogrula("ADMIN"), asyncHandler(controller.getir));
router.post("/", rolDogrula("ADMIN"), asyncHandler(controller.ekle));
router.put("/:id", rolDogrula("ADMIN"), asyncHandler(controller.guncelle));
router.delete("/:id", rolDogrula("ADMIN"), asyncHandler(controller.pasifYap));
router.post("/:id/hesap", rolDogrula("ADMIN"), asyncHandler(controller.hesapOlustur));

module.exports = router;

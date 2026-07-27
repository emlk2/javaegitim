const express = require("express");
const asyncHandler = require("../utils/asyncHandler");
const kimlikDogrula = require("../middleware/kimlikDogrula");
const rolDogrula = require("../middleware/rolDogrula");
const controller = require("../controllers/ogrenciController");

const router = express.Router();

router.use(kimlikDogrula);
router.get("/ben", rolDogrula("OGRENCI"), asyncHandler(controller.kendiProfilim));
router.get("/ben/notlar", rolDogrula("OGRENCI"), asyncHandler(controller.kendiNotlarim));
router.get("/ben/devamsizliklar", rolDogrula("OGRENCI"), asyncHandler(controller.kendiDevamsizliklarim));

router.get("/", rolDogrula("ADMIN"), asyncHandler(controller.listele));
router.get("/:id", rolDogrula("ADMIN"), asyncHandler(controller.getir));
router.post("/", rolDogrula("ADMIN"), asyncHandler(controller.ekle));
router.put("/:id", rolDogrula("ADMIN"), asyncHandler(controller.guncelle));
router.delete("/:id", rolDogrula("ADMIN"), asyncHandler(controller.pasifYap));
router.post("/:id/hesap", rolDogrula("ADMIN"), asyncHandler(controller.hesapOlustur));

module.exports = router;

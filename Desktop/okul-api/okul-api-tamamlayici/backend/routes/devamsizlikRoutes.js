const express = require("express");
const asyncHandler = require("../utils/asyncHandler");
const kimlikDogrula = require("../middleware/kimlikDogrula");
const rolDogrula = require("../middleware/rolDogrula");
const controller = require("../controllers/devamsizlikController");

const router = express.Router();

router.use(kimlikDogrula);
router.get("/", rolDogrula("ADMIN", "OGRETMEN", "OGRENCI"), asyncHandler(controller.listele));
router.post("/", rolDogrula("ADMIN", "OGRETMEN"), asyncHandler(controller.kaydet));
router.delete("/:id", rolDogrula("ADMIN", "OGRETMEN"), asyncHandler(controller.sil));

module.exports = router;

const express = require("express");
const asyncHandler = require("../utils/asyncHandler");
const kimlikDogrula = require("../middleware/kimlikDogrula");
const authController = require("../controllers/authController");

const router = express.Router();

router.post("/giris", asyncHandler(authController.giris));
router.get("/ben", kimlikDogrula, asyncHandler(authController.ben));

module.exports = router;

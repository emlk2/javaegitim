const express = require("express");
const asyncHandler = require("../utils/asyncHandler");
const controller = require("../controllers/sistemController");

const router = express.Router();

router.get("/durum", asyncHandler(controller.durum));

module.exports = router;

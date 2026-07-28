const jwt = require("jsonwebtoken");

const { User } = require("../models");

async function authenticate(req, res, next) {
    try {
        const authorizationHeader = req.headers.authorization;

        if (
            !authorizationHeader ||
            !authorizationHeader.startsWith("Bearer ")
        ) {
            return res.status(401).json({
                success: false,
                message: "Authentication token is required"
            });
        }

        const token = authorizationHeader.split(" ")[1];

        if (!token) {
            return res.status(401).json({
                success: false,
                message: "Authentication token is required"
            });
        }

        const decoded = jwt.verify(
            token,
            process.env.JWT_SECRET
        );

        const user = await User.findByPk(decoded.userId, {
            attributes: {
                exclude: ["passwordHash", "deletedAt"]
            }
        });

        if (!user) {
            return res.status(401).json({
                success: false,
                message: "User associated with this token was not found"
            });
        }

        req.user = user;
        next();
    } catch (error) {
        if (error.name === "TokenExpiredError") {
            return res.status(401).json({
                success: false,
                message: "Token has expired"
            });
        }

        return res.status(401).json({
            success: false,
            message: "Invalid authentication token"
        });
    }
}

module.exports = authenticate;
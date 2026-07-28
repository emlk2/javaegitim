function errorHandler(error, req, res, next) {
    let statusCode = error.statusCode || 500;
    let message = error.message || "Internal server error";

    if (error.name === "SequelizeUniqueConstraintError") {
        statusCode = 409;
        message = "A record with the same unique value already exists";
    }

    if (statusCode === 500) {
        console.error(error);
    }

    return res.status(statusCode).json({
        success: false,
        message
    });
}

module.exports = errorHandler;
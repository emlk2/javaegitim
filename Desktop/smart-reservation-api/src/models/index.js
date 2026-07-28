const sequelize = require("../config/database");

const User = require("./user");
const Business = require("./business");
const BusinessHour = require("./businessHour");
const Service = require("./service");
const Reservation = require("./reservation");

// User -> Reservation
User.hasMany(Reservation, {
    foreignKey: "userId",
    as: "reservations"
});

Reservation.belongsTo(User, {
    foreignKey: "userId",
    as: "user"
});

// Business -> BusinessHour
Business.hasMany(BusinessHour, {
    foreignKey: "businessId",
    as: "businessHours",
    onDelete: "CASCADE"
});

BusinessHour.belongsTo(Business, {
    foreignKey: "businessId",
    as: "business"
});

// Business -> Service
Business.hasMany(Service, {
    foreignKey: "businessId",
    as: "services",
    onDelete: "CASCADE"
});

Service.belongsTo(Business, {
    foreignKey: "businessId",
    as: "business"
});

// Business -> Reservation
Business.hasMany(Reservation, {
    foreignKey: "businessId",
    as: "reservations"
});

Reservation.belongsTo(Business, {
    foreignKey: "businessId",
    as: "business"
});

// Service -> Reservation
Service.hasMany(Reservation, {
    foreignKey: "serviceId",
    as: "reservations"
});

Reservation.belongsTo(Service, {
    foreignKey: "serviceId",
    as: "service"
});

module.exports = {
    sequelize,
    User,
    Business,
    BusinessHour,
    Service,
    Reservation
};
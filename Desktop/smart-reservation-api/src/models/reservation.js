const { DataTypes } = require("sequelize");
const sequelize = require("../config/database");

const Reservation = sequelize.define(
    "Reservation",
    {
        id: {
            type: DataTypes.INTEGER,
            allowNull: false,
            autoIncrement: true,
            primaryKey: true
        },

        userId: {
            type: DataTypes.INTEGER,
            allowNull: false
        },

        businessId: {
            type: DataTypes.INTEGER,
            allowNull: false
        },

        serviceId: {
            type: DataTypes.INTEGER,
            allowNull: false
        },

        startTime: {
            type: DataTypes.DATE,
            allowNull: false
        },

        endTime: {
            type: DataTypes.DATE,
            allowNull: false
        },

        status: {
            type: DataTypes.STRING(20),
            allowNull: false,
            defaultValue: "pending",
            validate: {
                isIn: [["pending", "approved", "cancelled"]]
            }
        }
    },
    {
        tableName: "reservations",
        timestamps: true,
        paranoid: true
    }
);

module.exports = Reservation;
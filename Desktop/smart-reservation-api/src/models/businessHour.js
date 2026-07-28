const { DataTypes } = require("sequelize");
const sequelize = require("../config/database");

const BusinessHour = sequelize.define(
    "BusinessHour",
    {
        id: {
            type: DataTypes.INTEGER,
            allowNull: false,
            autoIncrement: true,
            primaryKey: true
        },

        businessId: {
            type: DataTypes.INTEGER,
            allowNull: false
        },

        dayOfWeek: {
            type: DataTypes.INTEGER,
            allowNull: false,
            validate: {
                min: 1,
                max: 7
            }
        },

        openTime: {
            type: DataTypes.TIME,
            allowNull: false
        },

        closeTime: {
            type: DataTypes.TIME,
            allowNull: false
        },

        isClosed: {
            type: DataTypes.BOOLEAN,
            allowNull: false,
            defaultValue: false
        }
    },
    {
        tableName: "business_hours",
        timestamps: true,
        indexes: [
            {
                unique: true,
                fields: ["businessId", "dayOfWeek"],
                name: "business_hours_business_day_unique"
            }
        ]
    }
);

module.exports = BusinessHour;
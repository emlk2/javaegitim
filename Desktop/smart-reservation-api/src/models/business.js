const { DataTypes } = require("sequelize");
const sequelize = require("../config/database");

const Business = sequelize.define(
    "Business",
    {
        id: {
            type: DataTypes.INTEGER,
            allowNull: false,
            autoIncrement: true,
            primaryKey: true
        },

        name: {
            type: DataTypes.STRING(150),
            allowNull: false
        },

        address: {
            type: DataTypes.STRING(255),
            allowNull: false
        },

        phone: {
            type: DataTypes.STRING(30),
            allowNull: false
        },

        slotDuration: {
            type: DataTypes.INTEGER,
            allowNull: false,
            defaultValue: 30,
            validate: {
                min: 1
            }
        }
    },
    {
        tableName: "businesses",
        timestamps: true,
        paranoid: true
    }
);

module.exports = Business;
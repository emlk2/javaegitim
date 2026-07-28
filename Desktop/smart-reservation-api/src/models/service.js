const { DataTypes } = require("sequelize");
const sequelize = require("../config/database");

const Service = sequelize.define(
    "Service",
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

        name: {
            type: DataTypes.STRING(100),
            allowNull: false
        },

        description: {
            type: DataTypes.STRING(500),
            allowNull: true
        },

        duration: {
            type: DataTypes.INTEGER,
            allowNull: false,
            validate: {
                min: 1
            }
        },

        price: {
            type: DataTypes.DECIMAL(10, 2),
            allowNull: false,
            validate: {
                min: 0
            }
        },

        isActive: {
            type: DataTypes.BOOLEAN,
            allowNull: false,
            defaultValue: true
        }
    },
    {
        tableName: "services",
        timestamps: true,
        paranoid: true,
        indexes: [
            {
                unique: true,
                fields: ["businessId", "name"],
                name: "services_business_name_unique"
            }
        ]
    }
);

module.exports = Service;
"use strict";

module.exports = {
    async up(queryInterface, Sequelize) {
        await queryInterface.createTable("businesses", {
            id: {
                type: Sequelize.INTEGER,
                allowNull: false,
                autoIncrement: true,
                primaryKey: true
            },

            name: {
                type: Sequelize.STRING(150),
                allowNull: false
            },

            address: {
                type: Sequelize.STRING(255),
                allowNull: false
            },

            phone: {
                type: Sequelize.STRING(30),
                allowNull: false
            },

            slotDuration: {
                type: Sequelize.INTEGER,
                allowNull: false,
                defaultValue: 30
            },

            createdAt: {
                type: Sequelize.DATE,
                allowNull: false,
                defaultValue: Sequelize.literal("CURRENT_TIMESTAMP")
            },

            updatedAt: {
                type: Sequelize.DATE,
                allowNull: false,
                defaultValue: Sequelize.literal("CURRENT_TIMESTAMP")
            },

            deletedAt: {
                type: Sequelize.DATE,
                allowNull: true
            }
        });
    },

    async down(queryInterface) {
        await queryInterface.dropTable("businesses");
    }
};
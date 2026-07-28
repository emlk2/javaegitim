"use strict";

module.exports = {
    async up(queryInterface, Sequelize) {
        await queryInterface.createTable("services", {
            id: {
                type: Sequelize.INTEGER,
                allowNull: false,
                autoIncrement: true,
                primaryKey: true
            },

            businessId: {
                type: Sequelize.INTEGER,
                allowNull: false,
                references: {
                    model: "businesses",
                    key: "id"
                },
                onUpdate: "CASCADE",
                onDelete: "CASCADE"
            },

            name: {
                type: Sequelize.STRING(100),
                allowNull: false
            },

            description: {
                type: Sequelize.STRING(500),
                allowNull: true
            },

            duration: {
                type: Sequelize.INTEGER,
                allowNull: false
            },

            price: {
                type: Sequelize.DECIMAL(10, 2),
                allowNull: false
            },

            isActive: {
                type: Sequelize.BOOLEAN,
                allowNull: false,
                defaultValue: true
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

        await queryInterface.addConstraint("services", {
            fields: ["businessId", "name"],
            type: "unique",
            name: "services_business_name_unique"
        });

        await queryInterface.sequelize.query(`
            ALTER TABLE "services"
            ADD CONSTRAINT "services_duration_positive"
            CHECK ("duration" > 0);
        `);

        await queryInterface.sequelize.query(`
            ALTER TABLE "services"
            ADD CONSTRAINT "services_price_non_negative"
            CHECK ("price" >= 0);
        `);
    },

    async down(queryInterface) {
        await queryInterface.dropTable("services");
    }
};
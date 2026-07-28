"use strict";

module.exports = {
    async up(queryInterface, Sequelize) {
        await queryInterface.createTable("reservations", {
            id: {
                type: Sequelize.INTEGER,
                allowNull: false,
                autoIncrement: true,
                primaryKey: true
            },

            userId: {
                type: Sequelize.INTEGER,
                allowNull: false,
                references: {
                    model: "users",
                    key: "id"
                },
                onUpdate: "CASCADE",
                onDelete: "RESTRICT"
            },

            businessId: {
                type: Sequelize.INTEGER,
                allowNull: false,
                references: {
                    model: "businesses",
                    key: "id"
                },
                onUpdate: "CASCADE",
                onDelete: "RESTRICT"
            },

            serviceId: {
                type: Sequelize.INTEGER,
                allowNull: false,
                references: {
                    model: "services",
                    key: "id"
                },
                onUpdate: "CASCADE",
                onDelete: "RESTRICT"
            },

            startTime: {
                type: Sequelize.DATE,
                allowNull: false
            },

            endTime: {
                type: Sequelize.DATE,
                allowNull: false
            },

            status: {
                type: Sequelize.STRING(20),
                allowNull: false,
                defaultValue: "pending"
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

        await queryInterface.sequelize.query(`
            ALTER TABLE "reservations"
            ADD CONSTRAINT "reservations_status_check"
            CHECK ("status" IN ('pending', 'approved', 'cancelled'));
        `);

        await queryInterface.sequelize.query(`
            ALTER TABLE "reservations"
            ADD CONSTRAINT "reservations_time_range_check"
            CHECK ("endTime" > "startTime");
        `);

        await queryInterface.addIndex(
            "reservations",
            ["businessId", "startTime", "endTime"],
            {
                name: "reservations_business_time_index"
            }
        );

        await queryInterface.addIndex(
            "reservations",
            ["userId", "startTime", "endTime"],
            {
                name: "reservations_user_time_index"
            }
        );

        await queryInterface.addIndex("reservations", ["status"], {
            name: "reservations_status_index"
        });
    },

    async down(queryInterface) {
        await queryInterface.dropTable("reservations");
    }
};

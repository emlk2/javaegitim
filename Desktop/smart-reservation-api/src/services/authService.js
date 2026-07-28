const bcrypt = require("bcryptjs");
const jwt = require("jsonwebtoken");

const { User } = require("../models");

function createError(message, statusCode) {
    const error = new Error(message);
    error.statusCode = statusCode;

    return error;
}

function createToken(user) {
    return jwt.sign(
        {
            userId: user.id,
            role: user.role
        },
        process.env.JWT_SECRET,
        {
            expiresIn: process.env.JWT_EXPIRES_IN || "1d"
        }
    );
}

function getPublicUser(user) {
    return {
        id: user.id,
        name: user.name,
        email: user.email,
        role: user.role,
        createdAt: user.createdAt,
        updatedAt: user.updatedAt
    };
}

async function registerUser({ name, email, password }) {
    const normalizedEmail = email.trim().toLowerCase();

    const existingUser = await User.findOne({
        where: {
            email: normalizedEmail
        }
    });

    if (existingUser) {
        throw createError("Email is already registered", 409);
    }

    const passwordHash = await bcrypt.hash(password, 12);

    const user = await User.create({
        name: name.trim(),
        email: normalizedEmail,
        passwordHash,
        role: "user"
    });

    const token = createToken(user);

    return {
        user: getPublicUser(user),
        token
    };
}

async function loginUser({ email, password }) {
    const normalizedEmail = email.trim().toLowerCase();

    const user = await User.findOne({
        where: {
            email: normalizedEmail
        }
    });

    if (!user) {
        throw createError("Invalid email or password", 401);
    }

    const passwordMatches = await bcrypt.compare(
        password,
        user.passwordHash
    );

    if (!passwordMatches) {
        throw createError("Invalid email or password", 401);
    }

    const token = createToken(user);

    return {
        user: getPublicUser(user),
        token
    };
}

module.exports = {
    registerUser,
    loginUser
};
---
type: "THEORY"
title: "Writing Migration Files"
---

NAMING CONVENTION:
V{version}__{description}.sql

Examples:
V1__create_users_table.sql
V2__add_phone_to_users.sql
V3__create_orders_table.sql

RULES:
- Version must be unique
- Double underscore (__) separates version from description
- Use underscores in description (no spaces)
- Files are run in version order

EXAMPLE: V1__create_users_table.sql

CREATE TABLE users (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(255) NOT NULL UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_users_email ON users(email);

EXAMPLE: V2__add_phone_to_users.sql

ALTER TABLE users ADD COLUMN phone VARCHAR(20);
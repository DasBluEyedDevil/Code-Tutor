---
type: "THEORY"
title: "Flyway Migration Scripts"
---

Flyway manages database schema changes through versioned SQL scripts. Create these in src/main/resources/db/migration:

```sql
-- V1__create_users.sql
CREATE TABLE users (
    id BIGSERIAL PRIMARY KEY,
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    name VARCHAR(100),
    role VARCHAR(20) NOT NULL DEFAULT 'USER',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_users_email ON users(email);

-- V2__create_categories.sql
CREATE TABLE categories (
    id BIGSERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    description VARCHAR(255),
    color VARCHAR(7) DEFAULT '#6B7280',
    owner_id BIGINT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(name, owner_id)
);

CREATE INDEX idx_categories_owner ON categories(owner_id);

-- V3__create_tasks.sql
CREATE TABLE tasks (
    id BIGSERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    status VARCHAR(20) NOT NULL DEFAULT 'PENDING',
    priority VARCHAR(20) NOT NULL DEFAULT 'MEDIUM',
    due_date DATE,
    owner_id BIGINT NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    category_id BIGINT REFERENCES categories(id) ON DELETE SET NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_tasks_owner ON tasks(owner_id);
CREATE INDEX idx_tasks_status ON tasks(status);
CREATE INDEX idx_tasks_due_date ON tasks(due_date);
CREATE INDEX idx_tasks_category ON tasks(category_id);
```

Flyway Naming Convention:
- V1__description.sql - Version number + double underscore + description
- Scripts run in version order (V1, V2, V3...)
- Never modify a migration that has been applied!
- Add new migrations for schema changes

Key SQL Features:
- BIGSERIAL: Auto-incrementing 64-bit integer (PostgreSQL)
- REFERENCES: Foreign key constraint
- ON DELETE CASCADE: Delete children when parent is deleted
- ON DELETE SET NULL: Set to null when referenced row is deleted
- Indexes: Improve query performance on frequently filtered columns
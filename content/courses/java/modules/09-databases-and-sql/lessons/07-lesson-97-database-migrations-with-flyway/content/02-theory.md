---
type: "THEORY"
title: "Flyway - Version Control for Databases"
---

Flyway manages database migrations:

1. You write SQL migration files
2. Flyway tracks which have been applied
3. On startup, Flyway runs pending migrations
4. Every environment stays in sync!

HOW IT WORKS:
src/main/resources/db/migration/
  V1__create_users_table.sql
  V2__add_email_column.sql
  V3__create_orders_table.sql

Flyway maintains a history table:
| version | description          | installed_on        |
|---------|----------------------|---------------------|
| 1       | create users table   | 2025-01-15 10:30:00 |
| 2       | add email column     | 2025-01-16 14:20:00 |

On startup:
- Flyway checks: Which migrations are applied?
- Runs only the NEW ones
- Updates history table
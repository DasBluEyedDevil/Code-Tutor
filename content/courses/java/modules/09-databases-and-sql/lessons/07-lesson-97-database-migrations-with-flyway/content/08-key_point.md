---
type: "KEY_POINT"
title: "The Complete Database Workflow"
---

LOCAL DEVELOPMENT:
1. Create entity class with JPA annotations
2. Write Flyway migration for schema
3. Run app - Flyway creates table
4. JPA handles all CRUD operations

DEPLOYMENT:
1. Push code (includes migration files)
2. CI/CD deploys application
3. App starts, Flyway runs pending migrations
4. Database schema is now updated
5. Application uses JPA to access data

THE STACK:
- Flyway: Schema versioning and migrations
- JPA/Hibernate: Object-relational mapping
- JDBC: Low-level (JPA uses it internally)
- SQL: The language that makes it all work

Each layer builds on the previous one!
---
type: "KEY_POINT"
title: "⚠️ ddl-auto: update is NOT for Production! Use Flyway or Liquibase"
---

ddl-auto OPTIONS:
- create: DROP and recreate on startup (DESTROYS DATA!)
- create-drop: Create on start, drop on shutdown
- update: Update schema without deleting data
- validate: Only check if schema matches entities
- none: Do nothing

❌ PROBLEMS WITH 'update':
- Cannot remove columns (only adds)
- Cannot rename columns
- No rollback capability
- No version history
- Different results on different environments
- Can cause data inconsistencies over time

✓ PRODUCTION SOLUTION: Database Migration Tools

1. FLYWAY (Recommended for simplicity):

Dependency:
<dependency>
    <groupId>org.flywaydb</groupId>
    <artifactId>flyway-core</artifactId>
</dependency>

Migration file: src/main/resources/db/migration/V1__Create_users_table.sql

CREATE TABLE users (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL
);

Migration file: V2__Add_age_column.sql

ALTER TABLE users ADD COLUMN age INT;

2. LIQUIBASE (More flexible, XML/YAML/JSON):

Dependency:
<dependency>
    <groupId>org.liquibase</groupId>
    <artifactId>liquibase-core</artifactId>
</dependency>

BENEFITS OF MIGRATION TOOLS:
✓ Version-controlled schema changes
✓ Rollback support
✓ Team collaboration (everyone runs same migrations)
✓ Production-safe deployments
✓ Audit trail of all changes

PRODUCTION CONFIG:
spring.jpa.hibernate.ddl-auto=validate  # Only validate, don't modify!
spring.flyway.enabled=true  # Let Flyway handle schema changes
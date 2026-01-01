---
type: "THEORY"
title: "Handling Migration Failures"
---

WHAT IF A MIGRATION FAILS?

Flyway stops immediately. You must:

1. Fix the SQL error
2. Manually repair the database if partially applied
3. Mark migration as applied or delete from history

COMMANDS:
// Check migration status
mvn flyway:info

// Apply pending migrations
mvn flyway:migrate

// Repair checksum mismatches
mvn flyway:repair

// Delete all objects (DANGEROUS!)
mvn flyway:clean

SPRING BOOT ALTERNATIVE:
Add to application.properties:
spring.flyway.repair=true  // Auto-repair on startup
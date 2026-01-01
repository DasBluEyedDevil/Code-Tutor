---
type: "WARNING"
title: "Migration Gotchas to Avoid"
---

1. NEVER EDIT APPLIED MIGRATIONS
   - Flyway checksums will fail
   - Other environments will break
   - Create a new migration instead

2. TEST DATA MIGRATIONS
   - Adding NOT NULL column to existing data = failure
   - Add column, populate data, then add constraint

3. LARGE DATA MIGRATIONS
   - Locking tables in production = downtime
   - Consider batch updates or online schema change tools

4. COORDINATE WITH TEAM
   - Pull before creating new migrations
   - Avoid version conflicts (V3 exists twice)

5. BACKUP BEFORE PRODUCTION MIGRATIONS
   - Always. No exceptions.
   - Test rollback procedure

6. DON'T USE FLYWAY:CLEAN IN PRODUCTION
   - It deletes EVERYTHING
   - Only for local development
---
type: "KEY_POINT"
title: "Migration Best Practices"
---

1. MIGRATIONS ARE IMMUTABLE
   - Never edit an applied migration
   - Create a new migration to fix mistakes
   - Flyway checksums prevent tampering

2. EACH MIGRATION = ONE LOGICAL CHANGE
   - V1: Create users table
   - V2: Add phone column
   - Don't mix unrelated changes

3. MAKE MIGRATIONS REVERSIBLE (when possible)
   - Think: How would I undo this?
   - Document rollback steps in comments

4. TEST MIGRATIONS BEFORE PRODUCTION
   - Run on local database first
   - Test on staging environment
   - Never run untested migrations in production

5. USE MEANINGFUL VERSION NUMBERS
   - V1, V2, V3 for small projects
   - V20250115_1 (date-based) for teams
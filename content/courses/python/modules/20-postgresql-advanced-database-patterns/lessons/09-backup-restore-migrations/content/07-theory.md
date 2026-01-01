---
type: "THEORY"
title: "Zero-Downtime Migrations"
---

Production databases can't go offline. Here's how to migrate without downtime:

**Safe Migration Patterns:**

**1. Add Column (Safe)**
```sql
ALTER TABLE transactions ADD COLUMN new_field VARCHAR(100);
```
- Instant for nullable columns
- Backfill data separately

**2. Add NOT NULL Column (Two-Step)**
```sql
-- Step 1: Add nullable
ALTER TABLE transactions ADD COLUMN status VARCHAR(20);

-- Step 2: Backfill (in batches)
UPDATE transactions SET status = 'completed' WHERE status IS NULL LIMIT 1000;

-- Step 3: Add constraint
ALTER TABLE transactions ALTER COLUMN status SET NOT NULL;
```

**3. Rename Column (Expand-Contract)**
```sql
-- Step 1: Add new column
ALTER TABLE accounts ADD COLUMN account_name VARCHAR(100);

-- Step 2: Backfill
UPDATE accounts SET account_name = name;

-- Step 3: Update application to use new column
-- Step 4: Drop old column (after deployment)
ALTER TABLE accounts DROP COLUMN name;
```

**4. Add Index Concurrently**
```sql
CREATE INDEX CONCURRENTLY idx_new ON transactions(category_id);
```
- Doesn't block writes
- Takes longer but safe

**Dangerous Operations:**
- DROP COLUMN (instant but irreversible)
- ALTER COLUMN TYPE (locks table)
- Add NOT NULL with default (rewrites table)
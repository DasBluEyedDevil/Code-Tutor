---
type: "THEORY"
title: "When to Add Indexes"
---

**Add indexes for:**
- Columns in WHERE clauses (frequently filtered)
- Columns in JOIN conditions
- Columns in ORDER BY (avoid filesort)
- Foreign key columns (speeds up JOINs and ON DELETE)

**Don't add indexes for:**
- Small tables (< 1000 rows) - full scan is fine
- Columns with low cardinality (few unique values)
- Columns rarely used in queries
- Tables with heavy INSERT/UPDATE (indexes slow writes)

**Index Strategy:**
1. Start with foreign keys and common WHERE columns
2. Monitor slow queries with `pg_stat_statements`
3. Use `EXPLAIN ANALYZE` to verify index usage
4. Remove unused indexes (they still cost writes)

**Partial Indexes** - Index subset of rows:
```sql
-- Only index active users
CREATE INDEX idx_active_users ON users(email) WHERE is_active = true;

-- Only recent transactions
CREATE INDEX idx_recent_tx ON transactions(date) 
WHERE date >= CURRENT_DATE - INTERVAL '90 days';
```

**Covering Indexes** - Include extra columns:
```sql
-- Avoids table lookup for these columns
CREATE INDEX idx_covering ON transactions(user_id)
INCLUDE (amount, category_id);
```
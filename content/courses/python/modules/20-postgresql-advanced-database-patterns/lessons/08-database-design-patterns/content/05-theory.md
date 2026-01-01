---
type: "THEORY"
title: "Multi-Tenancy Patterns"
---

When multiple users/organizations share a database, you need isolation:

**Pattern 1: Shared Tables with tenant_id**
```sql
CREATE TABLE transactions (
    id SERIAL PRIMARY KEY,
    tenant_id INTEGER NOT NULL,  -- Organization/workspace
    user_id INTEGER NOT NULL,
    ...
);

-- Every query must filter by tenant
SELECT * FROM transactions WHERE tenant_id = $1 AND ...;
```

**Pros:** Simple, efficient storage
**Cons:** Must remember tenant filter, data leak risk

**Pattern 2: Row-Level Security (RLS)**
```sql
ALTER TABLE transactions ENABLE ROW LEVEL SECURITY;

CREATE POLICY tenant_isolation ON transactions
    USING (tenant_id = current_setting('app.tenant_id')::int);
```

**Pros:** Automatic filtering, can't forget
**Cons:** Slight performance overhead

**Pattern 3: Schema per Tenant**
```sql
CREATE SCHEMA tenant_123;
CREATE TABLE tenant_123.transactions (...);
```

**Pros:** Complete isolation, easy backup/restore per tenant
**Cons:** Schema management complexity, more connections

**Finance Tracker Choice:**
For a personal finance app, Pattern 2 (RLS) with user_id works well - each user sees only their data, enforced at database level.
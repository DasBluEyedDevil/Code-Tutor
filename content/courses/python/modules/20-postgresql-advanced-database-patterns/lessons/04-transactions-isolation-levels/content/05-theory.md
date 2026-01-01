---
type: "THEORY"
title: "Isolation Levels Explained"
---

Isolation levels control how transactions see each other's changes. Higher isolation = more consistency, less concurrency.

**READ UNCOMMITTED** (Not in PostgreSQL)
Can see uncommitted changes from other transactions ("dirty reads"). PostgreSQL doesn't support this - it treats it as READ COMMITTED.

**READ COMMITTED** (PostgreSQL Default)
- Only sees committed data
- Each query sees a fresh snapshot
- Same query can return different results if another transaction commits
- **Issue:** Non-repeatable reads

**REPEATABLE READ**
- Sees a snapshot from transaction start
- Same query always returns same results
- Cannot see commits from other transactions after start
- **Issue:** Serialization anomalies possible

**SERIALIZABLE**
- Strictest level - transactions appear to run one after another
- Detects all anomalies and aborts conflicting transactions
- May require retry logic for aborted transactions
- **Best for:** Financial calculations, inventory, anywhere exactness matters

**When to Use Each:**
- **READ COMMITTED:** Most web apps, dashboards, reports
- **REPEATABLE READ:** Long-running reports, analytics
- **SERIALIZABLE:** Money transfers, inventory updates, booking systems
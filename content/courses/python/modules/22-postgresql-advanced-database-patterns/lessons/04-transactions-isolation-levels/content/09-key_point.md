---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **ACID** guarantees data integrity: Atomicity, Consistency, Isolation, Durability
- Use **context managers** (`async with conn.transaction()`) for automatic commit/rollback
- **Savepoints** allow partial rollbacks within a transaction
- **READ COMMITTED** (default) is fine for most operations
- Use **SERIALIZABLE** for financial calculations and critical updates
- Prevent deadlocks by **locking in consistent order** (sort by ID)
- Always implement **retry logic** for serializable transactions
- Keep transactions **short** - do prep work outside the transaction
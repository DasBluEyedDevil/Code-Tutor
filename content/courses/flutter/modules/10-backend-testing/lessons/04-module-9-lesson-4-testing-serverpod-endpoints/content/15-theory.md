---
type: "THEORY"
title: "Database Cleanup Strategies"
---

Proper cleanup between tests is essential for reliable results. Serverpod provides several strategies:

1. **Truncate All Tables**: Fast, removes all data but preserves schema

2. **Transaction Rollback**: Wrap each test in a transaction, rollback at end

3. **Delete Specific Records**: Clean only the data your test created

4. **Database Reset**: Drop and recreate schema (slowest, use rarely)

Choose based on your test speed and isolation requirements.
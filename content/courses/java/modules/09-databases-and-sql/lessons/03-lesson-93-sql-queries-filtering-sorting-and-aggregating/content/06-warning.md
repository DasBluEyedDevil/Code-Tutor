---
type: "WARNING"
title: "Query Performance Pitfalls"
---

1. AVOID SELECT * ON LARGE TABLES:
   - Fetches all columns, wastes memory
   - Specify only needed columns

2. MISSING INDEXES:
   - Queries on non-indexed columns are SLOW
   - Add indexes on columns used in WHERE, ORDER BY, JOIN

3. LIKE WITH LEADING WILDCARD:
   - LIKE '%text' cannot use indexes = full table scan
   - LIKE 'text%' CAN use indexes = faster

4. NULL COMPARISONS:
   - WHERE column = NULL never works!
   - Use WHERE column IS NULL instead

5. AGGREGATES WITHOUT GROUP BY:
   - COUNT(*), SUM(), AVG() without GROUP BY = one result for entire table
   - Easily missed mistake in complex queries
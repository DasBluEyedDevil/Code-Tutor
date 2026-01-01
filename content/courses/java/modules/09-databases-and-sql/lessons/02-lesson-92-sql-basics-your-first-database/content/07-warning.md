---
type: "WARNING"
title: "Common SQL Mistakes to Avoid"
---

1. FORGETTING NOT NULL:
   - Fields without NOT NULL can have empty values
   - Required fields should always have NOT NULL

2. WRONG DATA TYPES:
   - Using VARCHAR for numbers = slow sorting, bad comparisons
   - Using FLOAT for money = rounding errors (use DECIMAL instead)

3. DUPLICATE DATA:
   - Storing same data in multiple tables = inconsistency
   - Use foreign keys and JOINs instead

4. NO PRIMARY KEY:
   - Every table needs a PRIMARY KEY
   - Without it, you cannot uniquely identify rows

5. SELECT * IN PRODUCTION:
   - Retrieves all columns, even unused ones
   - Wastes bandwidth and memory
   - Always specify needed columns: SELECT name, age FROM...
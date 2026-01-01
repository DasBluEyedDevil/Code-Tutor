---
type: "WARNING"
title: "JOIN Gotchas to Avoid"
---

1. CARTESIAN PRODUCT (Accidental Cross Join):
   - Forgetting ON clause = rows multiply!
   - 100 x 100 = 10,000 rows unexpectedly

2. WRONG JOIN TYPE:
   - INNER when you need LEFT = missing rows
   - LEFT when you need INNER = unexpected NULLs

3. AMBIGUOUS COLUMNS:
   - SELECT name FROM A JOIN B = Error if both have 'name'
   - Always use table prefixes: SELECT A.name, B.name

4. N+1 QUERY PROBLEM:
   - Looping through results and querying for each = SLOW
   - Use a single JOIN instead of multiple queries

5. JOINING ON WRONG COLUMNS:
   - Always join PRIMARY KEY to FOREIGN KEY
   - Verify column types match (INT to INT, not INT to VARCHAR)
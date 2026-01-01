---
type: "THEORY"
title: "Window Functions: Analytics Power"
---

Window functions perform calculations across related rows without collapsing them:

**Basic Syntax:**
```sql
function() OVER (
    PARTITION BY column  -- Group rows (optional)
    ORDER BY column      -- Order within partition
    ROWS/RANGE frame     -- Window frame (optional)
)
```

**Common Window Functions:**

**Ranking:**
- `ROW_NUMBER()` - Unique sequential number
- `RANK()` - Same rank for ties, gaps after
- `DENSE_RANK()` - Same rank for ties, no gaps

**Navigation:**
- `LAG(col, n)` - Value from n rows before
- `LEAD(col, n)` - Value from n rows after
- `FIRST_VALUE(col)` - First value in window
- `LAST_VALUE(col)` - Last value in window

**Aggregates as Windows:**
- `SUM(col) OVER (...)` - Running total
- `AVG(col) OVER (...)` - Moving average
- `COUNT(*) OVER (...)` - Running count
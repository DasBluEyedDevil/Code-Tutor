---
type: "THEORY"
title: "Normalization vs Denormalization"
---

Database design is about trade-offs. Understanding when to normalize and when to denormalize is crucial:

**Normalization** - Eliminate redundancy
- **1NF:** Atomic values, no repeating groups
- **2NF:** No partial dependencies
- **3NF:** No transitive dependencies

**Benefits:**
- Less storage space
- Easier updates (single source of truth)
- Data integrity guaranteed

**Drawbacks:**
- More JOINs for queries
- Can be slower for read-heavy workloads

**Denormalization** - Introduce controlled redundancy

**When to denormalize:**
- Read-heavy workloads (80%+ reads)
- Expensive JOINs executed frequently
- Caching query results
- Reporting/analytics tables

**Finance Tracker Example:**
```
Normalized: transactions -> categories (JOIN needed)
Denormalized: transactions.category_name (copied)
```

**Rule of Thumb:**
- Start normalized (3NF)
- Denormalize based on actual performance data
- Document every denormalization decision
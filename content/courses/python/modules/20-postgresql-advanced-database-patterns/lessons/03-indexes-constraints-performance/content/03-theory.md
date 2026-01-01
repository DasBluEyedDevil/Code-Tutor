---
type: "THEORY"
title: "Index Types in PostgreSQL"
---

Indexes speed up queries but slow down writes. Choose wisely:

**B-tree (Default)** - Best for most cases
- Equality: `WHERE id = 5`
- Range: `WHERE date > '2024-01-01'`
- Sorting: `ORDER BY date`
- Pattern prefix: `WHERE name LIKE 'John%'`

**Hash** - Only for equality
- Faster for simple `=` comparisons
- Can't do ranges or sorting
```sql
CREATE INDEX idx_hash ON users USING hash (email);
```

**GIN (Generalized Inverted Index)** - Multiple values
- Arrays: `WHERE tags @> '{python}'`
- Full-text: `WHERE document @@ 'search'`
- JSONB: `WHERE data @> '{"key": "value"}'`
```sql
CREATE INDEX idx_tags ON posts USING gin (tags);
```

**GiST (Generalized Search Tree)** - Geometric/spatial
- Range types
- PostGIS geometry
- Full-text (alternative to GIN)

**BRIN (Block Range Index)** - Huge sequential tables
- Time-series data
- Log tables
- Very small index size
---
type: "THEORY"
title: "Indexing JSONB with GIN"
---

JSONB without indexes requires scanning every row. GIN indexes make JSONB queries fast:

**Full Document Index:**
```sql
-- Index entire JSONB column (most flexible)
CREATE INDEX idx_transactions_metadata 
    ON transactions USING GIN (metadata);

-- Supports: @>, ?, ?|, ?&
```

**Path-Specific Index:**
```sql
-- Index specific path (smaller, faster for that path)
CREATE INDEX idx_merchant_category 
    ON transactions USING GIN ((metadata->'merchant'));

-- Or for equality on specific field
CREATE INDEX idx_merchant_name 
    ON transactions ((metadata->'merchant'->>'name'));
```

**jsonb_path_ops (Optimized):**
```sql
-- Smaller index, only supports @> operator
CREATE INDEX idx_metadata_pathops 
    ON transactions USING GIN (metadata jsonb_path_ops);

-- 2-3x smaller, faster for containment queries
```

**Which to Use:**
- **Default GIN:** Most flexible, supports all operators
- **jsonb_path_ops:** Smaller, only if you only use @>
- **Path-specific:** When you frequently query one path

**Performance Note:**
GIN indexes are slower to update than B-tree. For write-heavy tables, consider partial indexes or denormalizing hot paths to regular columns.
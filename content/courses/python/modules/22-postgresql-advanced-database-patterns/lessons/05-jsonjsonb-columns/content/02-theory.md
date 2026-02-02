---
type: "THEORY"
title: "JSON vs JSONB: What's the Difference?"
---

PostgreSQL offers two JSON types:

**JSON** (text storage)
- Stores exact input including whitespace
- Preserves duplicate keys
- Faster writes (no parsing)
- Slower reads (re-parses every query)
- Cannot be indexed

**JSONB** (binary storage)
- Parsed and stored in binary format
- Removes duplicates (keeps last value)
- Slower writes (parsing overhead)
- Much faster reads
- Full indexing support (GIN)
- Supports containment operators

**Always use JSONB unless you need:**
- Exact input preservation
- Audit logs where original format matters

**Storage Comparison:**
```sql
-- JSON: stored as '{"a": 1, "b": 2}' (exact input)
-- JSONB: stored as binary, may reorder keys

-- JSONB is typically 5-10% larger but 5-10x faster to query
```

**Type Declaration:**
```sql
-- Use JSONB for queryable data
metadata JSONB DEFAULT '{}'::jsonb

-- Optionally validate structure with CHECK
CHECK (jsonb_typeof(metadata) = 'object')
```
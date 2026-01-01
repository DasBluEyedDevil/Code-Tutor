---
type: "THEORY"
title: "JSONB Operators: Querying JSON Data"
---

PostgreSQL provides powerful operators for querying JSONB:

**Extraction Operators:**
```sql
-- -> returns JSON (keeps type)
metadata->'merchant'           -- {"name": "Amazon", ...}
metadata->'tags'->0            -- "shopping" (first tag as JSON)

-- ->> returns TEXT
metadata->>'merchant'          -- '{"name": "Amazon", ...}'
metadata->'merchant'->>'name'  -- 'Amazon' (as text)

-- #> path extraction (array of keys)
metadata#>'{merchant,location,city}'  -- "San Francisco" (JSON)
metadata#>>'{merchant,location,city}' -- 'San Francisco' (text)
```

**Containment Operators:**
```sql
-- @> contains (left contains right)
metadata @> '{"merchant": {"category": "grocery"}}'

-- <@ contained by
'{"category": "grocery"}' <@ metadata->'merchant'

-- ? key exists
metadata ? 'receipt_url'       -- Has this key?

-- ?| any key exists
metadata ?| array['tags', 'notes']  -- Has tags OR notes?

-- ?& all keys exist
metadata ?& array['merchant', 'tags']  -- Has BOTH?
```

**Array Operations:**
```sql
-- Check if array contains value
metadata->'tags' ? 'organic'

-- Array length
jsonb_array_length(metadata->'tags')
```
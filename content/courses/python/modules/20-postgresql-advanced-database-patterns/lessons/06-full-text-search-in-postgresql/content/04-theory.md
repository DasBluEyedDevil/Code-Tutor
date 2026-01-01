---
type: "THEORY"
title: "Creating Search Indexes"
---

Full-text search without indexes is slow. Two approaches:

**1. Expression Index (simpler)**
```sql
CREATE INDEX idx_tx_search 
    ON transactions 
    USING GIN (to_tsvector('english', description));
```
- Computed on every insert/update
- Query must match exact expression
- Good for single-column search

**2. Stored tsvector Column (faster reads)**
```sql
ALTER TABLE transactions 
    ADD COLUMN search_vector tsvector
    GENERATED ALWAYS AS (
        to_tsvector('english', 
            COALESCE(description, '') || ' ' || 
            COALESCE(category, '')
        )
    ) STORED;

CREATE INDEX idx_tx_search 
    ON transactions 
    USING GIN (search_vector);
```
- Pre-computed, stored in table
- Faster queries (no computation)
- Can combine multiple columns
- Uses more storage

**Which to choose:**
- **Expression index:** Simple cases, single column
- **Stored column:** Multiple columns, high query volume

**Language Configuration:**
- `'english'` - English stemming/stop words
- `'simple'` - No stemming, just lowercase
- `'spanish'`, `'german'`, etc. - Other languages
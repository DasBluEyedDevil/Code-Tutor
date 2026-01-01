---
type: "THEORY"
title: "tsvector and tsquery Types"
---

Full-text search uses two special types:

**tsvector** - Preprocessed document
```sql
SELECT to_tsvector('english', 'The quick brown foxes jumped');
-- Result: 'brown':3 'fox':4 'jump':5 'quick':2
```
- Removes stop words ('the')
- Stems words ('foxes' -> 'fox', 'jumped' -> 'jump')
- Records word positions

**tsquery** - Preprocessed search query
```sql
SELECT to_tsquery('english', 'running & jumping');
-- Result: 'run' & 'jump'
```

**Matching with @@:**
```sql
SELECT to_tsvector('english', 'The fox is running')
       @@ to_tsquery('english', 'run');
-- Result: true
```

**Query Operators:**
- `&` - AND (both must match)
- `|` - OR (either matches)
- `!` - NOT (must not match)
- `<->` - FOLLOWED BY (phrase)

**Examples:**
```sql
to_tsquery('coffee & shop')     -- Both words
to_tsquery('coffee | tea')      -- Either word
to_tsquery('!starbucks')        -- Not this word
to_tsquery('coffee <-> shop')   -- Exact phrase
```
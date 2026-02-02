---
type: "THEORY"
title: "Ranking Search Results"
---

Not all matches are equal. PostgreSQL provides ranking functions:

**ts_rank()** - Basic ranking
```sql
SELECT 
    description,
    ts_rank(search_vector, query) AS rank
FROM transactions,
     to_tsquery('english', 'coffee') AS query
WHERE search_vector @@ query
ORDER BY rank DESC;
```

**ts_rank_cd()** - Cover density ranking
Better for longer documents, considers proximity.

**Weighting with setweight():**
```sql
-- A (highest) > B > C > D (lowest)
setweight(to_tsvector('english', title), 'A') ||
setweight(to_tsvector('english', body), 'B')
```

Matches in weight 'A' fields score higher.

**Normalization options:**
```sql
ts_rank(vector, query, 1)  -- Divide by document length
ts_rank(vector, query, 2)  -- Divide by log(length)
ts_rank(vector, query, 32) -- Divide by rank + 1
```

**Practical tip:** Combine rank with recency:
```sql
ORDER BY 
    ts_rank(search_vector, query) * 
    (1.0 / (EXTRACT(EPOCH FROM NOW() - created_at) / 86400 + 1))
DESC
```
---
type: "THEORY"
title: "Cursor Pagination for Feeds"
---


**Why Cursor Pagination?**

Offset-based pagination has problems at scale:

```
// Offset pagination problems:
SELECT * FROM posts ORDER BY created_at OFFSET 10000 LIMIT 20;
// Database must scan 10,020 rows!

// If new post added while paginating:
Page 1: [A, B, C, D, E]  // User sees these
// New post X added
Page 2: [E, F, G, H, I]  // E appears again!
```

**Cursor Pagination Solution**

```
// Cursor pagination:
SELECT * FROM posts 
WHERE created_at < '2024-01-15T10:30:00' 
ORDER BY created_at DESC 
LIMIT 20;
// Uses index, fast regardless of page!

// New posts don't affect results:
Page 1: [A, B, C, D, E]  cursor = E.created_at
// New post X added
Page 2: [F, G, H, I, J]  // Continues from cursor
```

**Cursor Design**

| Approach | Pros | Cons |
|----------|------|------|
| **Timestamp** | Simple, readable | Ties if same timestamp |
| **ID** | Unique, simple | Sequential IDs leak info |
| **Composite** | Handles ties | More complex |
| **Encoded** | Opaque to client | Must encode/decode |

**Feed Algorithm Options**

| Algorithm | Description | Use Case |
|-----------|-------------|----------|
| **Chronological** | Newest first | Real-time updates |
| **Relevance** | Engagement-weighted | Discovery |
| **Following** | Only followed users | Personalized |
| **Trending** | Recent + popular | Viral content |

**Pagination Response Structure**

```json
{
  "items": [...],
  "cursor": {
    "next": "encoded_cursor_string",
    "previous": "encoded_cursor_string"
  },
  "hasMore": true,
  "totalCount": 1234  // Optional, can be expensive
}
```


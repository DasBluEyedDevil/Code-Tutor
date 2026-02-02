---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **tsvector** stores preprocessed documents, **tsquery** stores search queries
- Use **@@** operator to match: `search_vector @@ query`
- **to_tsvector('english', text)** applies stemming and removes stop words
- **websearch_to_tsquery()** handles user-friendly input (phrases, OR, -exclude)
- Create **GIN indexes** on tsvector columns for fast search
- Use **GENERATED ALWAYS AS ... STORED** for automatic search vector updates
- **ts_rank()** orders results by relevance, **ts_headline()** highlights matches
- **setweight()** prioritizes matches in important fields (A > B > C > D)
- Combine ranking with **recency** for time-sensitive results
---
type: "THEORY"
title: "Why Full-Text Search?"
---

Simple LIKE queries fail for real search:

```sql
-- Problems with LIKE:
WHERE description LIKE '%coffee%'
```

- **No ranking:** All matches are equal
- **No stemming:** 'running' won't match 'run'
- **No stop words:** 'the', 'a' waste index space
- **Case sensitive:** Must handle manually
- **Slow:** Can't use indexes with leading wildcard

**PostgreSQL Full-Text Search provides:**
- **Linguistic processing:** Stemming, stop words, synonyms
- **Ranking:** Order results by relevance
- **Phrase matching:** Search for exact phrases
- **Boolean operators:** AND, OR, NOT
- **Highlighting:** Show matched terms in context
- **Fast:** GIN indexes for millisecond searches

**Finance Tracker Use Cases:**
- Search transaction descriptions: "coffee", "grocery store"
- Find receipts with specific items
- Search notes and memos
- Autocomplete for categories/merchants
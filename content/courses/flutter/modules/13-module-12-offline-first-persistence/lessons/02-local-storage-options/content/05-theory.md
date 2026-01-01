---
type: "THEORY"
title: "When to Use Each"
---


**Decision Matrix:**

| Criteria | SharedPrefs | Hive | Drift | Isar |
|----------|-------------|------|-------|------|
| Key-value data | Best | Good | Overkill | Overkill |
| Simple objects | No | Best | Good | Good |
| Relational data | No | No | Best | Good |
| Complex queries | No | Limited | Best | Good |
| Performance | Fast | Very Fast | Fast | Fastest |
| Full-text search | No | No | Plugin | Built-in |
| Web support | Yes | Yes | Yes | Yes |
| Learning curve | Easy | Easy | Medium | Easy |

**Recommendations:**

1. **Settings/Preferences**: SharedPreferences
2. **Simple caching**: Hive
3. **Complex relational data**: Drift
4. **High-performance NoSQL**: Isar
5. **Full-text search needed**: Isar

**Common Pattern:**
Many apps use multiple solutions:
- SharedPreferences for app settings
- Drift or Isar for main data
- Hive for caching API responses


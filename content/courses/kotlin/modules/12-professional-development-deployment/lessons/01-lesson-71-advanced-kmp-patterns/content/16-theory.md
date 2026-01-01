---
type: "THEORY"
title: "Exercise 3: Build a Shared Repository Pattern"
---


Create a repository that manages data fetching and caching across platforms.

### Requirements

1. **ProductRepository** (commonMain):
   - Fetch products from API
   - Cache in memory
   - Return cached data if available and fresh (< 5 minutes)
   - Refresh from API if cache expired

2. Use Ktor for networking
3. Handle errors gracefully
4. Log cache hits/misses

---


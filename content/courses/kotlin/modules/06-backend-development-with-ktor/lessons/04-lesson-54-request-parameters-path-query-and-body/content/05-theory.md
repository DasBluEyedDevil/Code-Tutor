---
type: "THEORY"
title: "🔍 Query Parameters: Filtering and Options"
---


### When to Use Query Parameters

Use query parameters for:
- ✅ Filtering results (`?status=active`)
- ✅ Sorting (`?sort=date&order=desc`)
- ✅ Pagination (`?page=2&limit=20`)
- ✅ Search (`?q=kotlin`)
- ✅ Optional settings (`?format=json`)

**Examples:**

### Accessing Single Query Parameter


**Test it:**

### Accessing Multiple Query Parameters


**Test it:**

### Query Parameter with Default Values


The `?:` (Elvis operator) provides defaults:
- No `page` parameter → defaults to 1
- No `limit` parameter → defaults to 20
- No `sort` parameter → defaults to "name"

### Multiple Values for Same Parameter


**Test it:**

---



```bash
curl "http://localhost:8080/books?tag=fiction&tag=bestseller&tag=scifi"
```

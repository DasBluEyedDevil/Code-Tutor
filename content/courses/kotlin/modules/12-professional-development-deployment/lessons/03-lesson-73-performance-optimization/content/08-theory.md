---
type: "THEORY"
title: "Database Optimization"
---


### Query Optimization

❌ **Bad** (N+1 queries):

✅ **Good** (Single query with JOIN):

### Indexing

❌ **Bad** (Full table scan):

✅ **Good** (Indexed):

### Pagination

❌ **Bad** (Load all 10,000 products):

✅ **Good** (Paging):

### Batch Operations

❌ **Bad** (Individual inserts):

✅ **Good** (Batch insert):

---



```kotlin
@Insert
suspend fun insertAll(products: List<Product>)

// Single transaction - much faster
database.productDao().insertAll(products)
```

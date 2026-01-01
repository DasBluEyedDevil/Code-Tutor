---
type: "THEORY"
title: "Network Optimization"
---


### Response Caching

**HTTP Caching with OkHttp**:

**Cache Headers**:

### Compression


### Request Coalescing

❌ **Bad** (Multiple identical requests):

✅ **Good** (Share single request):

### Prefetching


---



```kotlin
class ProductRepository {
    private val cache = mutableMapOf<String, Product>()

    suspend fun prefetchProducts(ids: List<String>) {
        val uncachedIds = ids.filter { it !in cache }
        if (uncachedIds.isEmpty()) return

        val products = api.getProductsBatch(uncachedIds)
        products.forEach { cache[it.id] = it }
    }

    suspend fun getProduct(id: String): Product {
        return cache[id] ?: api.getProduct(id).also {
            cache[id] = it
        }
    }
}

// Usage
repository.prefetchProducts(listOf("1", "2", "3"))
// Later...
val product = repository.getProduct("1") // Instant! (cached)
```

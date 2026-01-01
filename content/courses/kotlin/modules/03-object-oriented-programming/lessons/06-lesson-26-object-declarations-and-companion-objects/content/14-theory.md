---
type: "THEORY"
title: "Solution: Cache System"
---



---



```kotlin
object CacheManager {
    private val cache = mutableMapOf<String, Any>()
    private var hits = 0
    private var misses = 0

    fun put(key: String, value: Any) {
        cache[key] = value
        println("✅ Cached: $key")
    }

    fun get(key: String): Any? {
        return if (cache.containsKey(key)) {
            hits++
            cache[key]
        } else {
            misses++
            null
        }
    }

    inline fun <reified T> getAs(key: String): T? {
        val value = get(key)
        return value as? T
    }

    fun remove(key: String): Boolean {
        val removed = cache.remove(key) != null
        if (removed) {
            println("🗑️  Removed: $key")
        }
        return removed
    }

    fun clear() {
        val count = cache.size
        cache.clear()
        println("🧹 Cleared $count items from cache")
    }

    fun contains(key: String): Boolean = cache.containsKey(key)

    fun getAllKeys(): Set<String> = cache.keys.toSet()

    fun size(): Int = cache.size

    fun getStatistics() {
        val totalRequests = hits + misses
        val hitRate = if (totalRequests > 0) (hits.toDouble() / totalRequests * 100) else 0.0

        println("\n=== Cache Statistics ===")
        println("Size: ${cache.size} items")
        println("Hits: $hits")
        println("Misses: $misses")
        println("Hit Rate: ${"%.2f".format(hitRate)}%")
        println("=======================\n")
    }

    fun displayContents() {
        println("\n=== Cache Contents ===")
        if (cache.isEmpty()) {
            println("(empty)")
        } else {
            cache.forEach { (key, value) ->
                println("$key = $value")
            }
        }
        println("======================\n")
    }
}

data class User(val id: Int, val name: String)

fun main() {
    // Add items to cache
    CacheManager.put("user:1", User(1, "Alice"))
    CacheManager.put("user:2", User(2, "Bob"))
    CacheManager.put("config:timeout", 30)
    CacheManager.put("config:maxUsers", 100)

    CacheManager.displayContents()

    // Retrieve items
    println("=== Retrieving items ===")
    val user1 = CacheManager.getAs<User>("user:1")
    println("Retrieved: $user1")

    val timeout = CacheManager.getAs<Int>("config:timeout")
    println("Timeout: $timeout")

    val notFound = CacheManager.get("user:999")
    println("Not found: $notFound")

    CacheManager.getStatistics()

    // Check existence
    println("Contains 'user:1': ${CacheManager.contains("user:1")}")
    println("Contains 'user:999': ${CacheManager.contains("user:999")}")

    // Get all keys
    println("\nAll keys: ${CacheManager.getAllKeys()}")

    // Remove item
    CacheManager.remove("user:2")

    CacheManager.displayContents()

    // Clear cache
    CacheManager.clear()

    CacheManager.displayContents()
    CacheManager.getStatistics()
}
```

---
type: "THEORY"
title: "Exercises"
---


### Exercise 1: Generic Stack (Medium)

Create a generic `Stack<T>` class with push, pop, and peek operations.

**Requirements**:
- `push(item: T)` - add item to top
- `pop(): T?` - remove and return top item
- `peek(): T?` - return top item without removing
- `isEmpty(): Boolean` - check if stack is empty
- `size: Int` - number of items in stack

**Solution**:


### Exercise 2: Generic Tree with Comparable (Hard)

Create a generic binary search tree that stores comparable items.

**Requirements**:
- `insert(value: T)` - add value to tree
- `contains(value: T): Boolean` - check if value exists
- `toSortedList(): List<T>` - return sorted list of all values

**Solution**:


### Exercise 3: Generic Cache with Constraints (Hard)

Create a generic cache that stores serializable items with expiration.

**Requirements**:
- Type must be serializable (toString/equals)
- `put(key: String, value: T, ttlSeconds: Int)` - store with expiration
- `get(key: String): T?` - retrieve if not expired
- `clear()` - remove all entries
- `size: Int` - number of valid entries

**Solution**:


---



```kotlin
import java.time.Instant

class Cache<T : Any> {
    private data class CacheEntry<T>(
        val value: T,
        val expiresAt: Long
    ) {
        fun isExpired(): Boolean {
            return System.currentTimeMillis() > expiresAt
        }
    }

    private val storage = mutableMapOf<String, CacheEntry<T>>()

    fun put(key: String, value: T, ttlSeconds: Int = 60) {
        val expiresAt = System.currentTimeMillis() + (ttlSeconds * 1000)
        storage[key] = CacheEntry(value, expiresAt)
        cleanupExpired()
    }

    fun get(key: String): T? {
        val entry = storage[key] ?: return null

        return if (entry.isExpired()) {
            storage.remove(key)
            null
        } else {
            entry.value
        }
    }

    fun clear() {
        storage.clear()
    }

    val size: Int
        get() {
            cleanupExpired()
            return storage.size
        }

    private fun cleanupExpired() {
        storage.entries.removeIf { it.value.isExpired() }
    }

    fun getAllKeys(): Set<String> {
        cleanupExpired()
        return storage.keys.toSet()
    }
}

fun main() {
    val cache = Cache<String>()

    cache.put("user1", "Alice", 2)
    cache.put("user2", "Bob", 5)

    println("Get user1: ${cache.get("user1")}")  // Alice
    println("Size: ${cache.size}")                // 2

    // Wait for expiration (in real code)
    Thread.sleep(2100)

    println("Get user1 after expiration: ${cache.get("user1")}")  // null
    println("Get user2: ${cache.get("user2")}")   // Bob
    println("Size: ${cache.size}")                // 1

    // Works with any type
    val numberCache = Cache<Int>()
    numberCache.put("count", 42, 10)
    println("Count: ${numberCache.get("count")}")  // 42

    cache.clear()
    println("Size after clear: ${cache.size}")  // 0
}
```

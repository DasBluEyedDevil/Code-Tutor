---
type: "WARNING"
title: "Common Mistakes"
---

### Mistake 1: Using Thread.sleep() in coroutines
```kotlin
// ❌ WRONG - blocks the thread
launch {
    Thread.sleep(1000)
    println("Done")
}

// ✅ RIGHT - suspends the coroutine
launch {
    delay(1000)
    println("Done")
}
```

### Mistake 2: Forgetting runBlocking in main()
```kotlin
// ❌ WRONG - launch needs a CoroutineScope
fun main() {
    launch { // Compiler error: Unresolved reference
        delay(1000)
    }
}

// ✅ RIGHT - use runBlocking
fun main() = runBlocking {
    launch {
        delay(1000)
    }
}
```

### Mistake 3: Calling suspend functions from regular functions
```kotlin
// ❌ WRONG - can't call suspend from regular function
fun loadData() {
    val user = fetchUserFromNetwork("123") // Compiler error!
}

// ✅ RIGHT - make it suspend or use a coroutine
suspend fun loadData() {
    val user = fetchUserFromNetwork("123")
}

// ✅ OR launch a coroutine
fun loadData() {
    scope.launch {
        val user = fetchUserFromNetwork("123")
    }
}
```

### Mistake 4: Sequential when you meant parallel
```kotlin
// ❌ Sequential execution (~2000ms)
val one = async { fetchFirst() }.await()
val two = async { fetchSecond() }.await()

// ✅ Parallel execution (~1000ms)
val one = async { fetchFirst() }
val two = async { fetchSecond() }
val results = one.await() to two.await()
```
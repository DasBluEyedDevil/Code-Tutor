---
type: "THEORY"
title: "Benefits of Pure Functions"
---


### Why Pure Functions Matter

**1. Testability**
```kotlin
// Easy to test - no mocks needed!
@Test
fun `add should sum two numbers`() {
    assertEquals(5, add(2, 3))
    assertEquals(0, add(-1, 1))
}
```

**2. Cacheability (Memoization)**
```kotlin
// Safe to cache results
val memoizedFib = mutableMapOf<Int, Long>()
fun fib(n: Int): Long = memoizedFib.getOrPut(n) {
    if (n <= 1) n.toLong() else fib(n - 1) + fib(n - 2)
}
```

**3. Parallelization**
```kotlin
// Safe to run in parallel - no shared mutable state
listOf(1, 2, 3, 4, 5)
    .parallelStream()
    .map { double(it) }  // Each call is independent
    .toList()
```

**4. Reasoning**
```kotlin
// Can substitute equals for equals
val x = add(2, 3)
val y = add(2, 3)
// x and y are guaranteed to be equal!
```

---


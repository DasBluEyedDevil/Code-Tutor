---
type: "THEORY"
title: "Understanding runBlocking, launch, and delay"
---

### runBlocking
```kotlin
fun main() = runBlocking {
    // Coroutine code here
}
```
- Creates a coroutine that **blocks** the current thread until all child coroutines complete
- Used as a bridge between regular blocking code and coroutines
- **Never use in production code** - only for `main()` or tests
- In real apps, you use proper coroutine scopes (viewModelScope, lifecycleScope, etc.)

### launch
```kotlin
launch {
    // This runs in a new coroutine
}
```
- Starts a new coroutine **concurrently** with the rest of the code
- Returns a `Job` that can be used to cancel the coroutine
- "Fire and forget" - doesn't return a result
- The code after launch continues immediately without waiting

### delay
```kotlin
delay(1000L) // Suspends for 1 second
```
- **Suspends** the coroutine (pauses without blocking the thread)
- Unlike `Thread.sleep()`, other coroutines can run during this time
- Only works inside a coroutine or suspend function
- Takes milliseconds as parameter (L suffix for Long literal)
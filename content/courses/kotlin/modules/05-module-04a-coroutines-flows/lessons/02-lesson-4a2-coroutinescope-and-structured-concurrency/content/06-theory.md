---
type: "THEORY"
title: "Parent-Child Relationships"
---

When you launch a coroutine inside another, they form a parent-child relationship:

```kotlin
fun main() = runBlocking {
    println("Parent starting")
    
    launch {  // Child 1
        delay(1000)
        println("Child 1 done")
    }
    
    launch {  // Child 2
        delay(2000)
        println("Child 2 done")
    }
    
    println("Parent waiting for children...")
    // runBlocking automatically waits for children
}

// Output:
// Parent starting
// Parent waiting for children...
// Child 1 done
// Child 2 done
```

### Key Behaviors

**1. Parent waits for children**
- A parent coroutine doesn't complete until all children complete
- This is automatic - you don't need to call `join()`

**2. Cancellation propagates down**
```kotlin
val parentJob = launch {
    launch { // Child 1
        repeat(1000) { i ->
            println("Child 1: $i")
            delay(500)
        }
    }
    
    launch { // Child 2
        repeat(1000) { i ->
            println("Child 2: $i")
            delay(500)
        }
    }
}

delay(1100)
parentJob.cancel() // Cancel parent → cancels both children
```

**3. Exceptions propagate up**
- If a child fails, it cancels the parent (and siblings)
- Unless using `SupervisorJob` (covered later)
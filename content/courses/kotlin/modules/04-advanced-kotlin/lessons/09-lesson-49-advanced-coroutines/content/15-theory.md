---
type: "THEORY"
title: "Checkpoint Quiz"
---


### Question 1: Structured Concurrency

What happens in `coroutineScope` if one child fails?

**A)** Only that child is cancelled
**B)** All children are cancelled and exception is propagated
**C)** The exception is ignored
**D)** Other children continue running

**Answer**: **B** - In `coroutineScope`, if one child fails, all siblings are cancelled and the exception is propagated to the parent.

---

### Question 2: Flow vs Channel

What's the main difference between Flow and Channel?

**A)** Flow is hot, Channel is cold
**B)** Flow is cold (lazy), Channel is hot (active)
**C)** They are the same
**D)** Channel can't be cancelled

**Answer**: **B** - Flow is cold (starts on collection), while Channel is hot (actively sends/receives regardless of consumers).

---

### Question 3: StateFlow

What makes StateFlow special?

**A)** It's the fastest flow type
**B)** It always has a current value and conflates duplicates
**C)** It can only emit once
**D)** It doesn't support multiple collectors

**Answer**: **B** - StateFlow always has a current value (accessible via `.value`) and automatically conflates duplicate consecutive values.

---

### Question 4: Exception Handling

Why doesn't this catch the exception?


**A)** launch is not a suspend function
**B)** launch is fire-and-forget, exception happens async
**C)** Exception handling doesn't work in coroutines
**D)** Missing await()

**Answer**: **B** - `launch` returns immediately (fire-and-forget), so the exception happens asynchronously after the try-catch block.

---

### Question 5: flowOn

What does `flowOn` do?

**A)** Changes the dispatcher for downstream operators
**B)** Changes the dispatcher for upstream operators
**C)** Stops the flow
**D)** Buffers the flow

**Answer**: **B** - `flowOn` changes the dispatcher for upstream operators (everything before it in the chain).

---



```kotlin
try {
    launch {
        throw Exception("Error")
    }
} catch (e: Exception) {
    println("Caught")
}
```

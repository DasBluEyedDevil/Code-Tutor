---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) `apply` uses `this` context; `also` uses `it` context**


Both return the object, but context differs.

---

**Question 2: C) `let`**


`let` is perfect for nullable chains.

---

**Question 3: C) The object itself**


Returning the object enables chaining.

---

**Question 4: B) `with` when you have an object; `run` for chaining or inline creation**


Functionally similar, but usage context differs.

---

**Question 5: C) Side effects (logging, validation) without breaking the chain**


Perfect for debugging and logging in chains.

---



```kotlin
val result = processData()
    .also { println("Step 1: $it") }
    .transform()
    .also { println("Step 2: $it") }
    .finalize()

// 'also' logs without changing the return value
```

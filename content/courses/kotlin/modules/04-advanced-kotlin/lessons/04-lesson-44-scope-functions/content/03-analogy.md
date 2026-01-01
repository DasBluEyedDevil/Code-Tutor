---
type: "ANALOGY"
title: "The Concept: What Are Scope Functions?"
---


Scope functions execute a block of code within the context of an object. They temporarily change the scope to work on that object.

### The Problem They Solve

**Without scope functions**:


**With scope functions**:


Even better:


**Benefits**:
- Less repetition (no `person.` everywhere)
- Clearer intent
- Chainable operations
- Scoped changes (visible what's being modified)

---



```kotlin
Person("Alice", 25)
    .apply {
        name = name.uppercase()
        age += 1
    }
    .also { println(it) }
    .name
    .length
```

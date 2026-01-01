---
type: "WARNING"
title: "Common Mistakes"
---


### Mistake 1: Forgetting Return Type
If your function returns something other than `Unit`, you MUST specify the return type after the parentheses.
- `fun add(a: Int, b: Int) { return a + b }` ❌
- `fun add(a: Int, b: Int): Int { return a + b }` ✅

### Mistake 2: Not Returning a Value
If you specify a return type, every possible path through your function must actually return a value.
- `fun getMessage(isValid: Boolean): String { if (isValid) return "OK" }` ❌ (What if isValid is false?)

### Mistake 3: Wrong Argument Order
...



```kotlin
fun createProfile(name: String, age: Int, city: String) { /* ... */ }

// ❌ Error - wrong order
createProfile(25, "Alice", "NYC")  // Type mismatch!

// ✅ Correct
createProfile("Alice", 25, "NYC")

// ✅ Better - use named arguments
createProfile(name = "Alice", age = 25, city = "NYC")
```

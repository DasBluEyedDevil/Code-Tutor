---
type: "WARNING"
title: "Common Mistakes"
---


### Mistake 1: Overusing !!
The `!!` operator should be your last resort. If you find yourself using it often, your code probably has design flaws. Always prefer `?.` and `?:`.

### Mistake 2: Forgetting to Handle Null
Just because you used `?.` doesn't mean you're finished. A safe call returns `null`, and you often need to handle that null result using `?:` to provide a fallback.

### Mistake 3: Unnecessary Null Checks
...



```kotlin
val name: String = "Alice"  // Non-nullable

// ❌ Unnecessary
if (name != null) {
    println(name.length)
}

// ✅ Just use it directly
println(name.length)
```

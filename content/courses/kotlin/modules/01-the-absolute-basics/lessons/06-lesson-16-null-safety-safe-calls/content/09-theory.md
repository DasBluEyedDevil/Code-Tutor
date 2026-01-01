---
type: "THEORY"
title: "⚠️ Advanced: Not-Null Assertion (!!) - Use With Extreme Caution"
---


**The `!!` operator is a LAST RESORT, not a first choice.**

It tells the compiler: "Trust me, this is definitely not null."
If you're wrong, your app CRASHES with NullPointerException.

### Why !! Exists (But Rarely Needed)

1. **Interop with Java code** - Java doesn't have null safety
2. **Test code** - Where crashes are expected behavior
3. **After external validation** - When you've already checked null elsewhere

### The Problem: !! Defeats Kotlin's Safety

Kotlin was designed to ELIMINATE NullPointerException.
Using `!!` brings that danger back!

### Real Code Statistics
In well-written Kotlin codebases:
- `?.` appears 100+ times
- `?:` appears 50+ times
- `let` appears 30+ times
- `!!` appears 0-5 times (often 0!)

### When !! MIGHT Be Acceptable

```kotlin
// After containsKey check (still prefer ?. though)
if (map.containsKey("key")) {
    val value = map["key"]!!  // You know it exists
}

// lateinit alternative in tests
val result = service.findUser(id)!!
assertEquals("Alice", result.name)
```

### Better Alternatives to !!

---



```kotlin
// ❌ AVOID: Using !! as a shortcut
val length = name!!.length  // CRASHES if null!

// ✅ PREFER: Safe call with default
val length = name?.length ?: 0

// ✅ PREFER: let for operations
name?.let { println(it.length) }

// ✅ PREFER: Explicit check with smart cast
if (name != null) {
    println(name.length)  // Smart cast to non-null
}

// ✅ PREFER: require() for validation
fun processName(name: String?) {
    requireNotNull(name) { "Name cannot be null" }
    // name is now smart-cast to String
    println(name.length)
}
```

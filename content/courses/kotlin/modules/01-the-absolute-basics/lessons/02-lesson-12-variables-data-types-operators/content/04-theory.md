---
type: "THEORY"
title: "Variables: val vs var"
---


In Kotlin, you can create two kinds of variables:

### `val` - Immutable (Read-Only)


`val` stands for **value**. Once you put something in the box, you **cannot** change it.

**When to use**: Use `val` by default for values that won't change.

**Real-World Examples**:
- `val birthYear = 1995` (Your birth year never changes)
- `val pi = 3.14159` (Mathematical constants don't change)
- `val appName = "MyCoolApp"` (The app name is usually constant)

### `var` - Mutable (Can Change)

`var` stands for **variable**. You can change what's in the box anytime.

**When to use**: Use `var` only when the value needs to change.

**Real-World Examples**:
- `var score = 0` (A player's score increases during a game)
- `var currentCity = "London"` (A user might move to a different city)
- `var healthPoints = 100` (Health goes down when taking damage)

### Best Practice: Prefer `val` Over `var`


**Why prefer `val`?**
- Prevents accidental changes
- Makes code easier to understand (you know it won't change)
- Safer for multi-threaded programs (advanced topic)

---



```kotlin
// ✅ Good - Using val by default
val name = "Bob"
val age = 30
var score = 0  // var only when needed

// ❌ Bad - Using var unnecessarily
var name = "Bob"  // Name won't change, should be val
var age = 30      // Age won't change (in one program), should be val
```

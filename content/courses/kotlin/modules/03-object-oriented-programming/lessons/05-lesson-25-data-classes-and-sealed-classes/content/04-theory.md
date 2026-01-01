---
type: "THEORY"
title: "Data Classes"
---


### Creating Data Classes

Use the `data` keyword before `class`:


**What Kotlin generates automatically**:
1. **`equals()`** - Compares data, not references
2. **`hashCode()`** - Consistent with `equals()`
3. **`toString()`** - Readable string representation
4. **`copy()`** - Creates copies with modified properties
5. **`componentN()`** - Destructuring declarations

### Requirements for Data Classes

1. Primary constructor must have at least one parameter
2. All primary constructor parameters must be `val` or `var`
3. Cannot be `abstract`, `open`, `sealed`, or `inner`
4. May extend other classes or implement interfaces

### Auto-Generated Functions

**1. `toString()`** - Readable representation


**2. `equals()` and `hashCode()`** - Structural equality


**3. `copy()`** - Create modified copies


**Why `copy()` matters**:
- Immutability: Don't modify original, create new versions
- Thread safety: Immutable data is inherently thread-safe
- Functional programming: Transform data without side effects

---



```kotlin
data class User(val name: String, val age: Int, val email: String)

val user = User("Alice", 25, "alice@example.com")

// Create a copy with modified age
val olderUser = user.copy(age = 26)

println(user)       // User(name=Alice, age=25, email=alice@example.com)
println(olderUser)  // User(name=Alice, age=26, email=alice@example.com)

// Copy with multiple changes
val differentUser = user.copy(name = "Bob", age = 30)
println(differentUser)  // User(name=Bob, age=30, email=alice@example.com)
```

---
type: "THEORY"
title: "Late Initialization (`lateinit`)"
---


Sometimes you can't initialize a property immediately (e.g., in Android, views are initialized after the object is created). **`lateinit`** lets you declare a non-null property without initializing it right away.

### When to Use `lateinit`

Use `lateinit` when:
- The property will be initialized before use (but not in the constructor)
- The property is non-null
- The property type is non-primitive (not Int, Double, Boolean, etc.)

**Example: Setup Method**


**Checking if `lateinit` is Initialized**:


**Warning**: Accessing an uninitialized `lateinit` property throws `UninitializedPropertyAccessException`!

**Example: Dependency Injection**


**Output**:

---



```kotlin
[LOG] Fetching user 42
Result of: SELECT * FROM users WHERE id = 42
```

---
type: "THEORY"
title: "Abstract Classes vs Interfaces"
---


### When to Use Abstract Classes

Use **abstract classes** when:
- You have shared **state** (properties with backing fields)
- You want to provide **common implementation** for subclasses
- You have a clear "is-a" relationship
- You need **constructors with parameters**


### When to Use Interfaces

Use **interfaces** when:
- You want to define **capabilities** or **behaviors**
- You need **multiple inheritance** of type
- You don't need shared state
- You want loose coupling


### Comparison Table

| Feature | Abstract Class | Interface |
|---------|---------------|-----------|
| State (backing fields) | ✅ Yes | ❌ No |
| Constructor | ✅ Yes | ❌ No |
| Multiple inheritance | ❌ No (single only) | ✅ Yes (multiple) |
| Default implementations | ✅ Yes | ✅ Yes (since Kotlin 1.0) |
| Access modifiers | ✅ Yes (public, protected, private) | ✅ Limited (public only) |
| When to use | "is-a" relationship | "can-do" capability |

---



```kotlin
interface Flyable {
    fun fly()
}

interface Swimmable {
    fun swim()
}

// A class can implement multiple interfaces
class Duck : Flyable, Swimmable {
    override fun fly() = println("Duck flying")
    override fun swim() = println("Duck swimming")
}
```

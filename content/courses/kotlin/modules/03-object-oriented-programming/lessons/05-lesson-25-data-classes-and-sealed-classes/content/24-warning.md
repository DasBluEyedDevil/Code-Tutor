---
type: "WARNING"
title: "Sealed Class Exhaustive When Pitfalls"
---

**Exhaustive `when` expressions break when adding new sealed subclasses** if you don't use `when` as an expression (assigned to a value).

**When used as a statement** (not assigned), Kotlin doesn't require exhaustive checking:
```kotlin
when (state) {
    is Loading -> showLoading()
    is Success -> showData()
    // Forgot Error case - compiles fine!
}
```

**When used as an expression** (assigned to a variable), Kotlin enforces exhaustiveness:
```kotlin
val message = when (state) {
    is Loading -> "Loading..."
    is Success -> "Done"
    // Compiler error if Error case missing!
}
```

**Best practice:** Always use `when` as an expression with sealed classes, even if you immediately discard the result: `val _ = when(state) { ... }`. This forces the compiler to check all cases.

Adding a new subclass to a sealed class is a breaking change—the compiler will catch missing cases in expression-when, but not statement-when.

---
type: "WARNING"
title: "Context Parameters: Beta Feature"
---


### Enable Context Parameters

Context parameters require a compiler flag (Beta since Kotlin 2.2):

```kotlin
// build.gradle.kts
kotlin {
    compilerOptions {
        freeCompilerArgs.add("-Xcontext-parameters")
    }
}
```

### Alternative: Use either { } Blocks

If you prefer not to use context parameters:

```kotlin
// Instead of:
context(raise: Raise<E>)
fun doWork(): A

// Use:
fun doWork(): Either<E, A> = either {
    // Your logic here
}
```

### Deprecated: Context Receivers

The older `-Xcontext-receivers` flag and unnamed `context(Raise<E>)` syntax were experimental and deprecated in Kotlin 2.0.20. Always use the named context parameters form: `context(raise: Raise<E>)`.

### IDE Support

IntelliJ IDEA 2024.2+ and Kotlin plugin 2.2+ have good support for context parameters. The `either { }` builder approach remains a reliable alternative.

---


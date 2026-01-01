---
type: "WARNING"
title: "Context Receiver Limitations"
---


### Experimental Feature

Context receivers require a compiler flag:

```kotlin
// build.gradle.kts
kotlin {
    compilerOptions {
        freeCompilerArgs.add("-Xcontext-receivers")
    }
}
```

### Alternative: Use either { } Blocks

If you can't use context receivers:

```kotlin
// Instead of:
context(Raise<E>)
fun doWork(): A

// Use:
fun doWork(): Either<E, A> = either {
    // Your logic here
}
```

### IDE Support

IDE support for context receivers is improving but may have issues. The `either { }` builder approach works reliably.

---


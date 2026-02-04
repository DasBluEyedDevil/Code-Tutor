---
type: "THEORY"
title: "Topic Introduction"
---


Arrow is the functional programming library for Kotlin. It provides powerful types for error handling that go beyond what `Result<T>` offers, including typed errors and error accumulation.

In this lesson, you'll learn:
- `Either<L, R>` for typed error handling
- `Option<A>` for explicit nullability
- `EitherNel` and `zipOrAccumulate` for accumulating multiple errors
- When to use each type

**Prerequisites**: Add Arrow to your project:
```kotlin
// build.gradle.kts
dependencies {
    implementation("io.arrow-kt:arrow-core:2.2.1")
}
```

---


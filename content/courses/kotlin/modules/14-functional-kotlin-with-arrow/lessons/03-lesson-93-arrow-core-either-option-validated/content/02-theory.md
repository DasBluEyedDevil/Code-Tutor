---
type: "THEORY"
title: "Topic Introduction"
---


Arrow is the functional programming library for Kotlin. It provides powerful types for error handling that go beyond what `Result<T>` offers, including typed errors and error accumulation.

In this lesson, you'll learn:
- `Either<L, R>` for typed error handling
- `Option<A>` for explicit nullability
- `Validated` for accumulating multiple errors
- When to use each type

**Prerequisites**: Add Arrow to your project:
```kotlin
// build.gradle.kts
dependencies {
    implementation("io.arrow-kt:arrow-core:2.2.1")
}
```

> **Note**: Arrow 2.x replaced `Validated` with `EitherNel` and `zipOrAccumulate`. The concepts taught here (error accumulation vs. short-circuit) remain the same — only the types changed. See the Arrow migration guide for details.

---


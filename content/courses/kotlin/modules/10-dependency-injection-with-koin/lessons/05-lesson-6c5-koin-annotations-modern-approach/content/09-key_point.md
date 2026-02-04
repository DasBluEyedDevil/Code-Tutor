---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Koin Annotations generate module definitions at compile-time** from `@Single` and `@Factory` annotations on classes. This provides type-safety and faster startup than runtime reflection.

**Annotate classes with `@Single` or `@Factory`**, then generate modules with KSP during compilation. The generated modules are regular Koin modules you include in `startKoin`.

**Annotations reduce boilerplate for large dependency graphs** but add build complexity via KSP. Choose runtime DSL for small projects, annotations for large teams needing compile-time validation.

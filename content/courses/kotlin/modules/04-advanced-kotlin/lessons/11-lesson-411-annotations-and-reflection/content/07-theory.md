---
type: "THEORY"
title: "Annotation Retention"
---


Control when annotations are available:


**Retention Policies**:
- `SOURCE` - discarded after compilation (e.g., `@Suppress`)
- `BINARY` - stored in binary but not available via reflection
- `RUNTIME` - available at runtime via reflection (default)

---



```kotlin
@Retention(AnnotationRetention.SOURCE)
annotation class CompileTimeOnly

@Retention(AnnotationRetention.BINARY)
annotation class InBinary

@Retention(AnnotationRetention.RUNTIME)
annotation class InRuntime
```

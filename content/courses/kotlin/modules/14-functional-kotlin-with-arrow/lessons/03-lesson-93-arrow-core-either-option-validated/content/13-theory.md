---
type: "THEORY"
title: "When to Use Each Type"
---


### Quick Reference

| Scenario | Type | Reason |
|----------|------|--------|
| Operation may fail with typed error | `Either<E, A>` | Error type in signature |
| Need to accumulate all errors | `Validated<E, A>` | Doesn't short-circuit |
| Value may be absent | `Option<A>` | Explicit optionality |
| Interop with exception-throwing code | `Result<T>` | Built into Kotlin |

### Decision Tree

```
Can it fail?
|-- No -> Use plain value
+-- Yes -> What kind of failure?
    |-- Typed domain error -> Either
    |   +-- Need all errors at once? -> Validated first, then Either
    |-- Exception-based -> Result
    +-- Value absent -> Option (or nullable)
```

---


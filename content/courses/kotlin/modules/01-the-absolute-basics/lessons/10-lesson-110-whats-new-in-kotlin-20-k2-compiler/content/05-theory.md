---
type: "THEORY"
title: "Improved Type Inference"
---


### K2's Better Type Understanding

The K2 compiler can infer types more accurately, reducing the need for explicit type declarations.

**Before (might need explicit types)**:
```kotlin
val items: List<String> = buildList {
    add("one")
    add("two")
}
```

**With K2 (infers correctly)**:
```kotlin
val items = buildList {
    add("one")
    add("two")
}  // Correctly inferred as List<String>
```

### Benefits of Better Type Inference

1. **Less Boilerplate**: Write less explicit type annotations
2. **Cleaner Code**: Focus on logic, not types
3. **Fewer Errors**: Compiler catches type mismatches earlier

---


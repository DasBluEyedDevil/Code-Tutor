---
type: "THEORY"
title: "Why KSP Over kapt?"
---


### The Problem with kapt

kapt (Kotlin Annotation Processing Tool) works by:
1. Generating Java stubs from Kotlin code
2. Running Java annotation processors on those stubs
3. Processing the generated code

This has significant drawbacks:

- **Slow**: Stub generation is expensive
- **Memory intensive**: Maintains two representations
- **Loses Kotlin information**: Processors see Java, not Kotlin
- **Limited incremental processing**: Often requires full rebuild

### KSP Advantages

| Feature | kapt | KSP |
|---------|------|-----|
| Speed | Baseline | **2x faster** |
| Memory | High | **Lower** |
| Kotlin-aware | No | **Yes** |
| Incremental | Limited | **Full** |
| K2 Compatible | Works but slow | **Native** |

---


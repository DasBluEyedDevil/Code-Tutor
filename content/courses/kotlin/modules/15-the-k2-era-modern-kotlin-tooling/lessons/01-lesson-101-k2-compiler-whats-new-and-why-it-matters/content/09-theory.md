---
type: "THEORY"
title: "K2 Compatibility"
---


### What Works with K2

K2 is designed to be backward compatible with existing Kotlin code:

**Fully Compatible**
- All standard Kotlin language features
- Coroutines and Flow
- Kotlin Multiplatform
- Most annotation processors (via KSP)
- Compose Compiler

**Migration Required**
- kapt (migrate to KSP where possible)
- Some compiler plugins need updates
- Custom compiler plugins need rewrite

**IDE Support**
- IntelliJ IDEA 2024.1+ has full K2 IDE support
- Android Studio Koala+ supports K2
- Fleet uses K2 by default

---


---
type: "KEY_POINT"
title: "Key Takeaways"
---

**KSP (Kotlin Symbol Processing) replaces KAPT** for annotation processing with 2x faster builds. KAPT generates Java stubs and runs Java annotation processors; KSP processes Kotlin AST directly.

**KSP is the future**—KAPT is in maintenance mode. Major libraries (Room, Moshi, Hilt) support KSP. Migrate by replacing `kapt` with `ksp` in Gradle and verifying library KSP support.

**KSP APIs are Kotlin-native**, understanding Kotlin-specific features (nullability, coroutines, multiplatform) that KAPT can't see. This enables better code generation for Kotlin codebases.

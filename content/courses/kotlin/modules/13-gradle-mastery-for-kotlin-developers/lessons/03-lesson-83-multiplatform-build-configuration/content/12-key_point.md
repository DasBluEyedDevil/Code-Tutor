---
type: "KEY_POINT"
title: "Key Takeaways"
---

**KMP projects use source sets for shared and platform-specific code**: `commonMain`, `androidMain`, `iosMain`. Dependencies declared in `commonMain` apply to all platforms; platform source sets add platform-specific deps.

**Hierarchical source sets enable code sharing patterns**—`iosMain` can depend on `appleMain` for shared iOS/macOS code. Use `dependsOn` to create custom hierarchies beyond default configurations.

**Configure targets with specific options**: Android min/target SDK, iOS deployment target, JVM toolchain version. These platform-specific settings live in the respective target blocks in `build.gradle.kts`.

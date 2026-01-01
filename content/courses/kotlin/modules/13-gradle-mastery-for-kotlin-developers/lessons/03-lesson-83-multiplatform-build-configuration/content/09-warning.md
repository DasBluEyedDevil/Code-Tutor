---
type: "WARNING"
title: "Common Multiplatform Issues"
---


### Missing Intermediate Source Sets

```kotlin
// WRONG - Duplicating dependencies
val iosX64Main by getting {
    dependencies { implementation(libs.ktor.client.darwin) }
}
val iosArm64Main by getting {
    dependencies { implementation(libs.ktor.client.darwin) }  // Duplicate!
}

// RIGHT - Use intermediate source set
val iosMain by creating {
    dependsOn(commonMain)
    dependencies { implementation(libs.ktor.client.darwin) }  // Once!
}
```

### Framework Name Conflicts

Ensure your framework name doesn't conflict with system frameworks:
- Avoid: `Foundation`, `UIKit`, `Core`
- Use: `SharedKit`, `AppShared`, `MyAppCore`

---


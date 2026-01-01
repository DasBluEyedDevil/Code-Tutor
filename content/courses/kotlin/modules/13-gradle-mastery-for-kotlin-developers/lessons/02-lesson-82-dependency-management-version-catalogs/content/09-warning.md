---
type: "WARNING"
title: "Common Pitfalls"
---


### Mixing Catalog and Inline Versions

Avoid mixing styles in the same project:

```kotlin
// BAD - inconsistent
dependencies {
    implementation(libs.ktor.server.core)  // From catalog
    implementation("io.ktor:ktor-server-netty:2.3.0")  // Hardcoded - might be different version!
}

// GOOD - all from catalog
dependencies {
    implementation(libs.ktor.server.core)
    implementation(libs.ktor.server.netty)
}
```

### Forgetting to Sync

After editing `libs.versions.toml`, sync Gradle to see changes in IDE.

---


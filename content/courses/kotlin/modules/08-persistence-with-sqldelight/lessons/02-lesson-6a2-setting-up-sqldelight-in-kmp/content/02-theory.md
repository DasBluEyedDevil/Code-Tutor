---
type: "THEORY"
title: "Gradle Plugin Setup"
---

### Step 1: Add the Plugin

**In gradle/libs.versions.toml:**
```toml
[versions]
sqldelight = "2.0.2"

[libraries]
sqldelight-coroutines = { module = "app.cash.sqldelight:coroutines-extensions", version.ref = "sqldelight" }
sqldelight-android = { module = "app.cash.sqldelight:android-driver", version.ref = "sqldelight" }
sqldelight-native = { module = "app.cash.sqldelight:native-driver", version.ref = "sqldelight" }
sqldelight-jvm = { module = "app.cash.sqldelight:sqlite-driver", version.ref = "sqldelight" }

[plugins]
sqldelight = { id = "app.cash.sqldelight", version.ref = "sqldelight" }
```

**In build.gradle.kts (project level):**
```kotlin
plugins {
    alias(libs.plugins.sqldelight) apply false
}
```

**In shared/build.gradle.kts:**
```kotlin
plugins {
    alias(libs.plugins.sqldelight)
}
```
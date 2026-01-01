---
type: "THEORY"
title: "Platform-Specific Drivers"
---

### Step 3: Add Driver Dependencies

Each platform needs its own SQLite driver:

**In shared/build.gradle.kts:**
```kotlin
kotlin {
    sourceSets {
        commonMain.dependencies {
            implementation(libs.sqldelight.coroutines)
        }
        
        androidMain.dependencies {
            implementation(libs.sqldelight.android)
        }
        
        iosMain.dependencies {
            implementation(libs.sqldelight.native)
        }
        
        // For desktop (optional)
        jvmMain.dependencies {
            implementation(libs.sqldelight.jvm)
        }
    }
}
```

### Driver Breakdown

| Platform | Driver | Underlying Implementation |
|----------|--------|---------------------------|
| Android | `android-driver` | Android SQLite Framework |
| iOS/macOS | `native-driver` | SQLite C library |
| JVM/Desktop | `sqlite-driver` | SQLite JDBC |
| Web | `web-worker-driver` | SQLite WASM |
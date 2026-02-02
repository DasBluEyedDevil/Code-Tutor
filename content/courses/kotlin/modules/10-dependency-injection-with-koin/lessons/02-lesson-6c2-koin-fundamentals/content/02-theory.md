---
type: "THEORY"
title: "Adding Koin to Your Project"
---

### Gradle Setup (Version Catalog)

```toml
# gradle/libs.versions.toml
[versions]
koin = "4.0.0"

[libraries]
koin-core = { module = "io.insert-koin:koin-core", version.ref = "koin" }
koin-compose = { module = "io.insert-koin:koin-compose", version.ref = "koin" }
koin-compose-viewmodel = { module = "io.insert-koin:koin-compose-viewmodel", version.ref = "koin" }
koin-android = { module = "io.insert-koin:koin-android", version.ref = "koin" }
koin-test = { module = "io.insert-koin:koin-test", version.ref = "koin" }
```

### Dependencies by Source Set

```kotlin
// shared/build.gradle.kts
kotlin {
    sourceSets {
        commonMain.dependencies {
            implementation(libs.koin.core)
            implementation(libs.koin.compose)
            implementation(libs.koin.compose.viewmodel)
        }
        commonTest.dependencies {
            implementation(libs.koin.test)
        }
        androidMain.dependencies {
            implementation(libs.koin.android)
        }
    }
}
```
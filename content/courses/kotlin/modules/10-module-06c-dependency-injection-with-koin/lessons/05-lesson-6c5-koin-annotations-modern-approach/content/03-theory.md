---
type: "THEORY"
title: "Setup for Koin Annotations"
---

### Add KSP and Annotations

```toml
# gradle/libs.versions.toml
[versions]
koin = "4.0.0"
ksp = "2.0.21-1.0.28"

[libraries]
koin-annotations = { module = "io.insert-koin:koin-annotations", version.ref = "koin" }
koin-ksp-compiler = { module = "io.insert-koin:koin-ksp-compiler", version.ref = "koin" }

[plugins]
ksp = { id = "com.google.devtools.ksp", version.ref = "ksp" }
```

```kotlin
// build.gradle.kts (project level)
plugins {
    alias(libs.plugins.ksp) apply false
}

// shared/build.gradle.kts
plugins {
    alias(libs.plugins.ksp)
}

kotlin {
    sourceSets {
        commonMain.dependencies {
            implementation(libs.koin.core)
            implementation(libs.koin.annotations)
        }
    }
}

dependencies {
    add("kspCommonMainMetadata", libs.koin.ksp.compiler)
    add("kspAndroid", libs.koin.ksp.compiler)
    add("kspIosArm64", libs.koin.ksp.compiler)
    add("kspIosSimulatorArm64", libs.koin.ksp.compiler)
}
```
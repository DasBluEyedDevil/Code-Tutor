---
type: "THEORY"
title: "Build Configuration"
---

### Root build.gradle.kts

```kotlin
// build.gradle.kts (root)
plugins {
    alias(libs.plugins.kotlin.multiplatform) apply false
    alias(libs.plugins.kotlin.jvm) apply false
    alias(libs.plugins.kotlin.serialization) apply false
    alias(libs.plugins.compose.multiplatform) apply false
    alias(libs.plugins.compose.compiler) apply false
    alias(libs.plugins.sqldelight) apply false
}
```

### settings.gradle.kts

```kotlin
// settings.gradle.kts
pluginManagement {
    repositories {
        google()
        mavenCentral()
        gradlePluginPortal()
    }
}

dependencyResolution {
    repositories {
        google()
        mavenCentral()
    }
}

rootProject.name = "taskflow"
include(":shared")
include(":server")
include(":composeApp")
```

### server/build.gradle.kts

```kotlin
// server/build.gradle.kts
plugins {
    alias(libs.plugins.kotlin.jvm)
    alias(libs.plugins.kotlin.serialization)
    application
}

application {
    mainClass.set("com.taskflow.server.ApplicationKt")
}

dependencies {
    implementation(project(":shared"))

    // Ktor Server
    implementation(libs.ktor.server.core)
    implementation(libs.ktor.server.cio)
    implementation(libs.ktor.server.content.negotiation)
    implementation(libs.ktor.server.auth.jwt)
    implementation(libs.ktor.server.cors)
    implementation(libs.ktor.server.status.pages)
    implementation(libs.ktor.serialization.json)

    // Database
    implementation(libs.exposed.core)
    implementation(libs.exposed.dao)
    implementation(libs.exposed.jdbc)
    implementation(libs.exposed.kotlin.datetime)
    implementation(libs.h2.database)

    // Auth
    implementation(libs.bcrypt)

    // DI
    implementation(libs.koin.ktor)

    // Logging
    implementation(libs.logback.classic)

    // Testing
    testImplementation(libs.ktor.server.test.host)
    testImplementation(libs.kotlin.test)
}
```

### shared/build.gradle.kts

```kotlin
// shared/build.gradle.kts
plugins {
    alias(libs.plugins.kotlin.multiplatform)
    alias(libs.plugins.kotlin.serialization)
}

kotlin {
    jvm()
    // Add Android, iOS targets as needed:
    // androidTarget()
    // iosX64(); iosArm64(); iosSimulatorArm64()

    sourceSets {
        commonMain.dependencies {
            implementation(libs.kotlinx.coroutines.core)
            implementation(libs.kotlinx.serialization.json)
        }
        commonTest.dependencies {
            implementation(libs.kotlin.test)
        }
    }
}
```

### composeApp/build.gradle.kts

```kotlin
// composeApp/build.gradle.kts
plugins {
    alias(libs.plugins.kotlin.multiplatform)
    alias(libs.plugins.kotlin.serialization)
    alias(libs.plugins.compose.multiplatform)
    alias(libs.plugins.compose.compiler)
    alias(libs.plugins.sqldelight)
}

kotlin {
    jvm("desktop")
    // androidTarget()

    sourceSets {
        val commonMain by getting {
            dependencies {
                implementation(project(":shared"))

                // Compose Multiplatform
                implementation(compose.runtime)
                implementation(compose.foundation)
                implementation(compose.material3)
                implementation(compose.ui)
                implementation(compose.components.resources)

                // Ktor Client
                implementation(libs.ktor.client.core)
                implementation(libs.ktor.client.content.negotiation)
                implementation(libs.ktor.client.auth)
                implementation(libs.ktor.serialization.json)

                // SQLDelight
                implementation(libs.sqldelight.coroutines)

                // Koin
                implementation(libs.koin.core)
                implementation(libs.koin.compose)

                // Coroutines
                implementation(libs.kotlinx.coroutines.core)
                implementation(libs.kotlinx.serialization.json)
            }
        }

        val desktopMain by getting {
            dependencies {
                implementation(compose.desktop.currentOs)
                implementation(libs.ktor.client.cio)
                implementation(libs.sqldelight.jvm.driver)
                implementation(libs.kotlinx.coroutines.core) // Dispatchers.Main for desktop
            }
        }

        // val androidMain by getting {
        //     dependencies {
        //         implementation(libs.ktor.client.cio)
        //         implementation(libs.sqldelight.android.driver)
        //     }
        // }
    }
}

compose.desktop {
    application {
        mainClass = "com.taskflow.app.MainKt"
    }
}

sqldelight {
    databases {
        create("TaskFlowDatabase") {
            packageName.set("com.taskflow.app.db")
        }
    }
}
```

The `shared/` module depends on nothing platform-specific. The `server/` module depends on `shared/` for DTOs. The `composeApp/` module depends on `shared/` for domain models and on platform-specific drivers via source sets.

---


---
type: "THEORY"
title: "Build Automation with Gradle"
---

### Multi-Module Setup

Organize your KMP project with proper Gradle configuration:

```kotlin
// settings.gradle.kts
rootProject.name = "MyKmpApp"

include(":shared")
include(":androidApp")
include(":iosApp")
include(":server")

pluginManagement {
    repositories {
        google()
        gradlePluginPortal()
        mavenCentral()
    }
}

dependencyResolutionManagement {
    repositories {
        google()
        mavenCentral()
    }
}
```

```kotlin
// Root build.gradle.kts
plugins {
    kotlin("multiplatform") version "2.3.0" apply false
    kotlin("android") version "2.3.0" apply false
    id("com.android.application") version "8.12.0" apply false
    id("com.android.library") version "8.12.0" apply false
}

allprojects {
    group = "com.example.myapp"
    version = "1.0.0"
}
```

### Custom Gradle Tasks

Create reusable automation tasks:

```kotlin
tasks.register("deployToStaging") {
    group = "deployment"
    description = "Deploy application to staging environment"

    dependsOn("test", "shadowJar")

    doLast {
        exec {
            commandLine(
                "scp",
                "build/libs/app-all.jar",
                "user@staging-server:/opt/app/"
            )
        }

        exec {
            commandLine(
                "ssh",
                "user@staging-server",
                "systemctl restart app"
            )
        }
    }
}

tasks.register("generateReleaseNotes") {
    group = "documentation"
    description = "Generate release notes from git commits"

    doLast {
        val output = ByteArrayOutputStream()
        exec {
            commandLine("git", "log", "--pretty=format:%s", "HEAD~10..HEAD")
            standardOutput = output
        }

        val releaseNotes = output.toString()
        file("RELEASE_NOTES.md").writeText("# Release Notes\n\n$releaseNotes")
        println("Generated RELEASE_NOTES.md")
    }
}

tasks.register("checkDependencyUpdates") {
    group = "verification"
    description = "Check for dependency updates"

    doLast {
        exec {
            commandLine("./gradlew", "dependencyUpdates")
        }
    }
}
```

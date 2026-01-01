---
type: "EXAMPLE"
title: "Creating Custom Tasks"
---


Custom tasks automate project-specific work:



```kotlin
// Custom task in build.gradle.kts
tasks.register("generateBuildInfo") {
    group = "build"
    description = "Generates build information file"

    val outputFile = layout.buildDirectory.file("generated/BuildInfo.kt")
    outputs.file(outputFile)

    doLast {
        outputFile.get().asFile.apply {
            parentFile.mkdirs()
            writeText("""
                package com.example

                object BuildInfo {
                    const val VERSION = "${project.version}"
                    const val BUILD_TIME = "${java.time.Instant.now()}"
                    const val GIT_HASH = "${getGitHash()}"
                }
            """.trimIndent())
        }
    }
}

fun getGitHash(): String =
    providers.exec {
        commandLine("git", "rev-parse", "--short", "HEAD")
    }.standardOutput.asText.get().trim()
```

---
type: "THEORY"
title: "Setup"
---


Add navigation dependency in `build.gradle.kts`:


In `gradle/libs.versions.toml`:


---



```toml
[versions]
navigation = "2.9.1"

[libraries]
androidx-navigation-compose = { group = "org.jetbrains.androidx.navigation", name = "navigation-compose", version.ref = "navigation" }
```

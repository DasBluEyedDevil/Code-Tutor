---
type: "THEORY"
title: "Setup"
---


Add navigation dependency in `build.gradle.kts`:


In `gradle/libs.versions.toml`:


---



```toml
[versions]
navigation = "2.8.4"

[libraries]
androidx-navigation-compose = { group = "androidx.navigation", name = "navigation-compose", version.ref = "navigation" }
```

---
type: "THEORY"
title: "Setup Dependencies"
---


Add in `build.gradle.kts`:


In `gradle/libs.versions.toml`:


---



```toml
[versions]
room = "2.8.4"
sqlite = "2.6.2"
ksp = "2.3.4"
datastore = "1.2.0"

[libraries]
androidx-room-runtime = { group = "androidx.room", name = "room-runtime", version.ref = "room" }
androidx-room-compiler = { group = "androidx.room", name = "room-compiler", version.ref = "room" }
androidx-sqlite-bundled = { group = "androidx.sqlite", name = "sqlite-bundled", version.ref = "sqlite" }
androidx-datastore-preferences = { group = "androidx.datastore", name = "datastore-preferences", version.ref = "datastore" }

[plugins]
ksp = { id = "com.google.devtools.ksp", version.ref = "ksp" }
```

---
type: "THEORY"
title: "Setup Dependencies"
---


Add in `build.gradle.kts`:


In `gradle/libs.versions.toml`:


---



```toml
[versions]
room = "2.6.1"
ksp = "2.0.21-1.0.27"
datastore = "1.1.1"

[libraries]
androidx-room-runtime = { group = "androidx.room", name = "room-runtime", version.ref = "room" }
androidx-room-ktx = { group = "androidx.room", name = "room-ktx", version.ref = "room" }
androidx-room-compiler = { group = "androidx.room", name = "room-compiler", version.ref = "room" }
androidx-datastore-preferences = { group = "androidx.datastore", name = "datastore-preferences", version.ref = "datastore" }

[plugins]
ksp = { id = "com.google.devtools.ksp", version.ref = "ksp" }
```

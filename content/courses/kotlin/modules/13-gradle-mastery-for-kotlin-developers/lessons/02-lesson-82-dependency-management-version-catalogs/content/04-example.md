---
type: "EXAMPLE"
title: "Version Catalogs"
---


Version catalogs centralize dependency versions in a single file:



```toml
# gradle/libs.versions.toml
[versions]
kotlin = "2.0.21"
kotlinx-coroutines = "1.9.0"
kotlinx-serialization = "1.7.3"
ktor = "3.0.2"
koin = "4.0.0"
compose-multiplatform = "1.7.1"

[libraries]
kotlinx-coroutines-core = { module = "org.jetbrains.kotlinx:kotlinx-coroutines-core", version.ref = "kotlinx-coroutines" }
kotlinx-serialization-json = { module = "org.jetbrains.kotlinx:kotlinx-serialization-json", version.ref = "kotlinx-serialization" }
ktor-server-core = { module = "io.ktor:ktor-server-core", version.ref = "ktor" }
ktor-server-netty = { module = "io.ktor:ktor-server-netty", version.ref = "ktor" }
koin-core = { module = "io.insert-koin:koin-core", version.ref = "koin" }

[bundles]
ktor-server = ["ktor-server-core", "ktor-server-netty"]

[plugins]
kotlin-jvm = { id = "org.jetbrains.kotlin.jvm", version.ref = "kotlin" }
kotlin-multiplatform = { id = "org.jetbrains.kotlin.multiplatform", version.ref = "kotlin" }
kotlinx-serialization = { id = "org.jetbrains.kotlin.plugin.serialization", version.ref = "kotlin" }
```

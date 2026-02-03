---
type: "EXAMPLE"
title: "Solution: Version Catalog"
---




```toml
# gradle/libs.versions.toml
[versions]
kotlin = "2.3.0"
ktor = "3.4.0"
kotlinx-serialization = "1.10.0"
logback = "1.5.12"

[libraries]
ktor-server-core = { module = "io.ktor:ktor-server-core", version.ref = "ktor" }
ktor-server-netty = { module = "io.ktor:ktor-server-netty", version.ref = "ktor" }
ktor-server-content-negotiation = { module = "io.ktor:ktor-server-content-negotiation", version.ref = "ktor" }
ktor-serialization-json = { module = "io.ktor:ktor-serialization-kotlinx-json", version.ref = "ktor" }
logback-classic = { module = "ch.qos.logback:logback-classic", version.ref = "logback" }

[bundles]
ktor-server = [
    "ktor-server-core",
    "ktor-server-netty",
    "ktor-server-content-negotiation",
    "ktor-serialization-json"
]

[plugins]
kotlin-jvm = { id = "org.jetbrains.kotlin.jvm", version.ref = "kotlin" }
ktor = { id = "io.ktor.plugin", version.ref = "ktor" }
kotlinx-serialization = { id = "org.jetbrains.kotlin.plugin.serialization", version.ref = "kotlin" }
```

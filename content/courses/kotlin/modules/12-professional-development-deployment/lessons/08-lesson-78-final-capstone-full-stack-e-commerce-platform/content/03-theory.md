---
type: "THEORY"
title: "Technology Choices"
---

### Version Catalog (libs.versions.toml)

All dependency versions are declared in a single Gradle version catalog. This is the recommended approach for Kotlin Multiplatform projects and prevents version conflicts across modules.

```toml
# gradle/libs.versions.toml
[versions]
kotlin = "2.3.0"
ktor = "3.4.0"
exposed = "1.0.0"
koin = "4.1.0"
sqldelight = "2.2.0"
compose-multiplatform = "1.10.0"
coroutines = "1.10.0"
serialization = "1.10.0"
h2 = "2.3.232"
logback = "1.5.13"
bcrypt = "0.10.2"

[libraries]
# Shared
kotlinx-coroutines-core = { module = "org.jetbrains.kotlinx:kotlinx-coroutines-core", version.ref = "coroutines" }
kotlinx-serialization-json = { module = "org.jetbrains.kotlinx:kotlinx-serialization-json", version.ref = "serialization" }

# Server - Ktor
ktor-server-core = { module = "io.ktor:ktor-server-core-jvm", version.ref = "ktor" }
ktor-server-cio = { module = "io.ktor:ktor-server-cio-jvm", version.ref = "ktor" }
ktor-server-content-negotiation = { module = "io.ktor:ktor-server-content-negotiation-jvm", version.ref = "ktor" }
ktor-server-auth-jwt = { module = "io.ktor:ktor-server-auth-jwt-jvm", version.ref = "ktor" }
ktor-server-cors = { module = "io.ktor:ktor-server-cors-jvm", version.ref = "ktor" }
ktor-server-status-pages = { module = "io.ktor:ktor-server-status-pages-jvm", version.ref = "ktor" }
ktor-serialization-json = { module = "io.ktor:ktor-serialization-kotlinx-json-jvm", version.ref = "ktor" }

# Server - Database
exposed-core = { module = "org.jetbrains.exposed:exposed-core", version.ref = "exposed" }
exposed-dao = { module = "org.jetbrains.exposed:exposed-dao", version.ref = "exposed" }
exposed-jdbc = { module = "org.jetbrains.exposed:exposed-jdbc", version.ref = "exposed" }
exposed-kotlin-datetime = { module = "org.jetbrains.exposed:exposed-kotlin-datetime", version.ref = "exposed" }
h2-database = { module = "com.h2database:h2", version.ref = "h2" }
logback-classic = { module = "ch.qos.logback:logback-classic", version.ref = "logback" }
bcrypt = { module = "at.favre.lib:bcrypt", version.ref = "bcrypt" }

# Server - Koin
koin-ktor = { module = "io.insert-koin:koin-ktor", version.ref = "koin" }

# Client - Ktor HttpClient
ktor-client-core = { module = "io.ktor:ktor-client-core", version.ref = "ktor" }
ktor-client-cio = { module = "io.ktor:ktor-client-cio", version.ref = "ktor" }
ktor-client-content-negotiation = { module = "io.ktor:ktor-client-content-negotiation", version.ref = "ktor" }
ktor-client-auth = { module = "io.ktor:ktor-client-auth", version.ref = "ktor" }

# Client - SQLDelight
sqldelight-coroutines = { module = "app.cash.sqldelight:coroutines-extensions", version.ref = "sqldelight" }
sqldelight-android-driver = { module = "app.cash.sqldelight:android-driver", version.ref = "sqldelight" }
sqldelight-jvm-driver = { module = "app.cash.sqldelight:sqlite-driver", version.ref = "sqldelight" }

# Client - Koin
koin-core = { module = "io.insert-koin:koin-core", version.ref = "koin" }
koin-compose = { module = "io.insert-koin:koin-compose", version.ref = "koin" }

# Testing
ktor-server-test-host = { module = "io.ktor:ktor-server-test-host-jvm", version.ref = "ktor" }
kotlin-test = { module = "org.jetbrains.kotlin:kotlin-test", version.ref = "kotlin" }

[plugins]
kotlin-multiplatform = { id = "org.jetbrains.kotlin.multiplatform", version.ref = "kotlin" }
kotlin-jvm = { id = "org.jetbrains.kotlin.jvm", version.ref = "kotlin" }
kotlin-serialization = { id = "org.jetbrains.kotlin.plugin.serialization", version.ref = "kotlin" }
compose-multiplatform = { id = "org.jetbrains.compose", version.ref = "compose-multiplatform" }
compose-compiler = { id = "org.jetbrains.kotlin.plugin.compose", version.ref = "kotlin" }
sqldelight = { id = "app.cash.sqldelight", version.ref = "sqldelight" }
```

### Why These Technologies?

| Technology | Why |
|-----------|-----|
| **Ktor 3.4** | Official JetBrains server framework. Lightweight, coroutine-native, plugin-based. |
| **Exposed 1.0** | Official JetBrains ORM. DSL and DAO patterns. 1.0 GA is production-ready. |
| **H2** | Embedded JVM database. Zero setup. In-memory or file-based. Ideal for capstones. |
| **CMP 1.10** | Share UI code across Android and Desktop. Hot reload support. |
| **SQLDelight 2.2** | Type-safe SQL queries from `.sq` files. Multiplatform drivers. |
| **Koin 4.1** | Service locator / DI. Works in commonMain and server. No annotation processing. |
| **kotlinx-serialization 1.10** | Multiplatform JSON. Shared DTOs between server and client. |
| **kotlinx-coroutines 1.10** | Structured concurrency everywhere. Flows for reactive data. |

---


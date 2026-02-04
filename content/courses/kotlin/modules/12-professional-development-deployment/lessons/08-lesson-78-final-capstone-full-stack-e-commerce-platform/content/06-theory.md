---
type: "THEORY"
title: "Server Implementation: Database and Auth"
---

### Exposed Tables

The server uses Exposed 1.0 DSL tables with H2 as the embedded database. Tables are created automatically on startup -- no migration tool needed for development.

```kotlin
// server/src/main/kotlin/com/taskflow/server/db/tables/Users.kt
package com.taskflow.server.db.tables

import org.jetbrains.exposed.sql.Table
import org.jetbrains.exposed.sql.kotlin.datetime.timestamp

object Users : Table("users") {
    val id = varchar("id", 36)
    val email = varchar("email", 255).uniqueIndex()
    val passwordHash = varchar("password_hash", 255)
    val displayName = varchar("display_name", 255)
    val createdAt = timestamp("created_at")

    override val primaryKey = PrimaryKey(id)
}
```

```kotlin
// server/src/main/kotlin/com/taskflow/server/db/tables/Tasks.kt
package com.taskflow.server.db.tables

import org.jetbrains.exposed.sql.Table
import org.jetbrains.exposed.sql.kotlin.datetime.timestamp

object Tasks : Table("tasks") {
    val id = varchar("id", 36)
    val title = varchar("title", 500)
    val description = text("description").default("")
    val priority = varchar("priority", 20).default("MEDIUM")
    val status = varchar("status", 20).default("TODO")
    val category = varchar("category", 100).default("")
    val dueDate = varchar("due_date", 30).nullable()
    val userId = varchar("user_id", 36).references(Users.id)
    val createdAt = timestamp("created_at")
    val updatedAt = timestamp("updated_at")

    override val primaryKey = PrimaryKey(id)
}
```

### Database Plugin

```kotlin
// server/src/main/kotlin/com/taskflow/server/plugins/Database.kt
package com.taskflow.server.plugins

import com.taskflow.server.db.tables.Tasks
import com.taskflow.server.db.tables.Users
import io.ktor.server.application.Application
import org.jetbrains.exposed.sql.Database
import org.jetbrains.exposed.sql.SchemaUtils
import org.jetbrains.exposed.sql.transactions.transaction

fun Application.configureDatabase() {
    Database.connect(
        url = "jdbc:h2:mem:taskflow;DB_CLOSE_DELAY=-1",
        driver = "org.h2.Driver"
    )

    transaction {
        SchemaUtils.create(Users, Tasks)
    }
}
```

The `DB_CLOSE_DELAY=-1` flag keeps the in-memory database alive for the entire JVM lifetime. For persistent storage between restarts, change the URL to `jdbc:h2:file:./data/taskflow`.

### JWT Security Plugin

```kotlin
// server/src/main/kotlin/com/taskflow/server/plugins/Security.kt
package com.taskflow.server.plugins

import com.auth0.jwt.JWT
import com.auth0.jwt.algorithms.Algorithm
import io.ktor.http.HttpStatusCode
import io.ktor.server.application.Application
import io.ktor.server.auth.authentication
import io.ktor.server.auth.jwt.JWTPrincipal
import io.ktor.server.auth.jwt.jwt
import io.ktor.server.response.respond

fun Application.configureSecurity() {
    val jwtSecret = environment.config.property("jwt.secret").getString()
    val jwtIssuer = environment.config.property("jwt.issuer").getString()
    val jwtAudience = environment.config.property("jwt.audience").getString()

    authentication {
        jwt("auth-jwt") {
            verifier(
                JWT.require(Algorithm.HMAC256(jwtSecret))
                    .withIssuer(jwtIssuer)
                    .withAudience(jwtAudience)
                    .build()
            )
            validate { credential ->
                if (credential.payload.getClaim("userId").asString() != null) {
                    JWTPrincipal(credential.payload)
                } else {
                    null
                }
            }
            challenge { _, _ ->
                call.respond(HttpStatusCode.Unauthorized, mapOf("message" to "Token invalid or expired"))
            }
        }
    }
}
```

### application.conf

```hocon
# server/src/main/resources/application.conf
ktor {
    deployment {
        port = 8080
    }
    application {
        modules = [ com.taskflow.server.ApplicationKt.module ]
    }
}

jwt {
    secret = "taskflow-dev-secret-change-in-production"
    issuer = "taskflow"
    audience = "taskflow-users"
    realm = "TaskFlow"
}
```

---


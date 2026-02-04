---
type: "THEORY"
title: "Server Implementation: Application Entry Point and Koin DI"
---

### Koin Module

```kotlin
// server/src/main/kotlin/com/taskflow/server/di/ServerModule.kt
package com.taskflow.server.di

import com.taskflow.server.db.dao.TaskDao
import com.taskflow.server.db.dao.UserDao
import io.ktor.server.application.Application
import org.koin.dsl.module

fun serverModule(application: Application) = module {
    single {
        UserDao(
            jwtSecret = application.environment.config.property("jwt.secret").getString(),
            jwtIssuer = application.environment.config.property("jwt.issuer").getString(),
            jwtAudience = application.environment.config.property("jwt.audience").getString()
        )
    }
    single { TaskDao() }
}
```

### Serialization Plugin

```kotlin
// server/src/main/kotlin/com/taskflow/server/plugins/Serialization.kt
package com.taskflow.server.plugins

import io.ktor.serialization.kotlinx.json.json
import io.ktor.server.application.Application
import io.ktor.server.application.install
import io.ktor.server.plugins.contentnegotiation.ContentNegotiation
import kotlinx.serialization.json.Json

fun Application.configureSerialization() {
    install(ContentNegotiation) {
        json(Json {
            prettyPrint = true
            isLenient = true
            ignoreUnknownKeys = true
        })
    }
}
```

### Routing Plugin

```kotlin
// server/src/main/kotlin/com/taskflow/server/plugins/Routing.kt
package com.taskflow.server.plugins

import com.taskflow.server.db.dao.TaskDao
import com.taskflow.server.db.dao.UserDao
import com.taskflow.server.routes.authRoutes
import com.taskflow.server.routes.taskRoutes
import com.taskflow.shared.dto.ApiResponse
import io.ktor.http.HttpStatusCode
import io.ktor.server.application.Application
import io.ktor.server.application.install
import io.ktor.server.plugins.statuspages.StatusPages
import io.ktor.server.response.respond
import io.ktor.server.routing.routing
import org.koin.ktor.ext.inject

fun Application.configureRouting() {
    val userDao by inject<UserDao>()
    val taskDao by inject<TaskDao>()

    install(StatusPages) {
        exception<Throwable> { call, cause ->
            call.respond(
                HttpStatusCode.InternalServerError,
                ApiResponse<Unit>(success = false, message = cause.message ?: "Internal server error")
            )
        }
    }

    routing {
        authRoutes(userDao)
        taskRoutes(taskDao)
    }
}
```

### Application Entry Point

```kotlin
// server/src/main/kotlin/com/taskflow/server/Application.kt
package com.taskflow.server

import com.taskflow.server.di.serverModule
import com.taskflow.server.plugins.configureDatabase
import com.taskflow.server.plugins.configureRouting
import com.taskflow.server.plugins.configureSecurity
import com.taskflow.server.plugins.configureSerialization
import io.ktor.server.application.Application
import io.ktor.server.application.install
import io.ktor.server.plugins.cors.routing.CORS
import org.koin.ktor.plugin.Koin

fun main(args: Array<String>) {
    io.ktor.server.cio.EngineMain.main(args)
}

fun Application.module() {
    install(Koin) {
        modules(serverModule(this@module))
    }

    install(CORS) {
        anyHost() // Development only; restrict in production
        allowHeader("Content-Type")
        allowHeader("Authorization")
    }

    configureDatabase()
    configureSerialization()
    configureSecurity()
    configureRouting()
}
```

### Running the Server

```bash
# From the project root
./gradlew :server:run
```

The server starts on `http://localhost:8080`. The H2 database creates the `users` and `tasks` tables automatically. No manual setup required.

### API Endpoints Summary

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| POST | `/api/auth/register` | No | Register a new user |
| POST | `/api/auth/login` | No | Login and receive JWT |
| GET | `/api/tasks` | JWT | List user's tasks |
| POST | `/api/tasks` | JWT | Create a new task |
| GET | `/api/tasks/{id}` | JWT | Get task by ID |
| PUT | `/api/tasks/{id}` | JWT | Update task |
| DELETE | `/api/tasks/{id}` | JWT | Delete task |

---


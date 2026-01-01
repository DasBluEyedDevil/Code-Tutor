---
type: "THEORY"
title: "Implementing Login with JWT"
---


### Step 1: Add JWT Dependencies

Update your `build.gradle.kts`:


### Step 2: Create JWT Configuration


**Security Note**: The secret should be:
- At least 256 bits (32 characters) long
- Randomly generated
- Loaded from environment variables, not hardcoded
- Different for each environment (dev, staging, production)

### Step 3: Create Login Models


### Step 4: Create Authentication Service


### Step 5: Create Login Route


### Step 6: Wire Everything Together

Update your Application.kt:


---



```kotlin
// src/main/kotlin/com/example/Application.kt
package com.example

import com.example.database.DatabaseFactory
import com.example.plugins.configureErrorHandling
import com.example.repositories.UserRepositoryImpl
import com.example.routes.authRoutes
import com.example.services.AuthService
import com.example.services.UserService
import io.ktor.serialization.kotlinx.json.*
import io.ktor.server.application.*
import io.ktor.server.cio.*
import io.ktor.server.engine.*
import io.ktor.server.plugins.contentnegotiation.*
import io.ktor.server.routing.*
import kotlinx.serialization.json.Json

fun main() {
    embeddedServer(CIO, port = 8080, module = Application::module).start(wait = true)
}

fun Application.module() {
    // Install plugins
    install(ContentNegotiation) {
        json(Json {
            prettyPrint = true
            ignoreUnknownKeys = true
        })
    }

    // Install error handling
    configureErrorHandling()

    // Initialize database
    DatabaseFactory.init()

    // Create dependencies
    val userRepository = UserRepositoryImpl()
    val userService = UserService(userRepository)
    val authService = AuthService(userRepository)

    // Configure routes
    routing {
        authRoutes(userService, authService)
    }
}
```

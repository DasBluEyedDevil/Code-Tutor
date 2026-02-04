---
type: "THEORY"
title: "Configuring JWT Authentication"
---


### Step 1: Update User Model with Roles

First, add role support to your user system:


Update UserRepository to include role:


### Step 2: Update JWT to Include Role


### Step 3: Install Ktor Authentication Plugin

Create a configuration file for authentication:


### Step 4: Apply Authentication to Routes

Now protect your routes with the `authenticate` function:


### Step 5: Create Admin-Only Routes


### Step 6: Update Application Configuration


---



```kotlin
// src/main/kotlin/com/example/Application.kt
package com.example

import com.example.database.DatabaseFactory
import com.example.plugins.configureAuthentication
import com.example.plugins.configureErrorHandling
import com.example.repositories.UserRepositoryImpl
import com.example.routes.adminRoutes
import com.example.routes.authRoutes
import com.example.routes.userRoutes
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

    // Install authentication
    configureAuthentication()

    // Initialize database
    DatabaseFactory.init()

    // Create dependencies
    val userRepository = UserRepositoryImpl()
    val userService = UserService(userRepository)
    val authService = AuthService(userRepository)

    // Configure routes
    routing {
        // Public routes (no authentication required)
        authRoutes(userService, authService)

        // Protected routes (authentication required)
        userRoutes(userService)

        // Admin routes (admin role required)
        adminRoutes(userService)
    }
}
```

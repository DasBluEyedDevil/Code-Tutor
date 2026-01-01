---
type: "EXAMPLE"
title: "Complete Example: Refactoring to Koin"
---


Let's refactor our entire application to use Koin:

### Module Definitions


### Application Setup


### Routes with Injection


---



```kotlin
// src/main/kotlin/com/example/routes/AuthRoutes.kt
package com.example.routes

import com.example.models.ApiResponse
import com.example.models.LoginRequest
import com.example.models.RegisterRequest
import com.example.models.RegisterResponse
import com.example.services.AuthService
import com.example.services.UserService
import io.ktor.http.*
import io.ktor.server.application.*
import io.ktor.server.request.*
import io.ktor.server.response.*
import io.ktor.server.routing.*
import org.koin.ktor.ext.inject

fun Route.authRoutes() {
    // Inject dependencies
    val userService by inject<UserService>()
    val authService by inject<AuthService>()

    route("/api/auth") {
        post("/register") {
            val request = call.receive<RegisterRequest>()

            userService.register(request)
                .onSuccess { user ->
                    call.respond(
                        HttpStatusCode.Created,
                        ApiResponse(
                            data = RegisterResponse(
                                user = user,
                                message = "Registration successful"
                            )
                        )
                    )
                }
                .onFailure { error ->
                    throw error
                }
        }

        post("/login") {
            val request = call.receive<LoginRequest>()

            authService.login(request)
                .onSuccess { loginResponse ->
                    call.respond(ApiResponse(data = loginResponse))
                }
                .onFailure { error ->
                    throw error
                }
        }
    }
}
```

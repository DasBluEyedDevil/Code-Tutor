---
type: "THEORY"
title: "Solution 1"
---


**Common Models (commonMain)**:

**Validation Utilities (commonMain)**:

**Storage Interface (commonMain)**:

**Android Storage (androidMain)**:

**iOS Storage (iosMain)**:

**Auth Service (commonMain)**:

---



```kotlin
// shared/src/commonMain/kotlin/com/example/shared/service/AuthService.kt
package com.example.shared.service

import com.example.shared.models.*
import com.example.shared.storage.TokenStorage
import com.example.shared.utils.Validation
import io.ktor.client.*
import io.ktor.client.call.*
import io.ktor.client.request.*
import io.ktor.client.plugins.contentnegotiation.*
import io.ktor.serialization.kotlinx.json.*
import kotlinx.serialization.json.Json

class AuthService(
    private val baseUrl: String,
    private val tokenStorage: TokenStorage
) {
    private var currentUser: User? = null

    private val client = HttpClient {
        install(ContentNegotiation) {
            json(Json { ignoreUnknownKeys = true })
        }
    }

    suspend fun login(email: String, password: String): Result<User> {
        val request = LoginRequest(email, password)

        // Validate
        val validationError = Validation.validateLoginRequest(request)
        if (validationError != null) {
            return Result.failure(IllegalArgumentException(validationError))
        }

        return try {
            val response: AuthResponse = client.post("$baseUrl/auth/login") {
                setBody(request)
            }.body()

            if (response.success && response.user != null && response.token != null) {
                currentUser = response.user
                tokenStorage.saveToken(response.token)
                Result.success(response.user)
            } else {
                Result.failure(Exception(response.message ?: "Login failed"))
            }
        } catch (e: Exception) {
            Result.failure(e)
        }
    }

    suspend fun register(request: RegisterRequest): Result<User> {
        // Validate
        val validationError = Validation.validateRegisterRequest(request)
        if (validationError != null) {
            return Result.failure(IllegalArgumentException(validationError))
        }

        return try {
            val response: AuthResponse = client.post("$baseUrl/auth/register") {
                setBody(request)
            }.body()

            if (response.success && response.user != null && response.token != null) {
                currentUser = response.user
                tokenStorage.saveToken(response.token)
                Result.success(response.user)
            } else {
                Result.failure(Exception(response.message ?: "Registration failed"))
            }
        } catch (e: Exception) {
            Result.failure(e)
        }
    }

    fun logout() {
        currentUser = null
        tokenStorage.clearToken()
    }

    fun isLoggedIn(): Boolean {
        return tokenStorage.getToken() != null
    }

    fun getCurrentUser(): User? {
        return currentUser
    }
}
```

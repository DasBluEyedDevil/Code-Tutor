---
type: "THEORY"
title: "Testing Protected Endpoints"
---



---



```kotlin
// src/test/kotlin/com/example/routes/UserRoutesTest.kt
package com.example.routes

import com.example.database.DatabaseFactory
import com.example.models.*
import com.example.module
import io.ktor.client.call.*
import io.ktor.client.plugins.contentnegotiation.*
import io.ktor.client.request.*
import io.ktor.http.*
import io.ktor.serialization.kotlinx.json.*
import io.ktor.server.testing.*
import kotlinx.serialization.json.Json
import org.junit.jupiter.api.AfterAll
import org.junit.jupiter.api.BeforeAll
import org.junit.jupiter.api.Test
import org.koin.core.context.stopKoin
import kotlin.test.assertEquals
import kotlin.test.assertNotNull

class UserRoutesTest {

    companion object {
        @BeforeAll
        @JvmStatic
        fun setup() {
            DatabaseFactory.init()
        }

        @AfterAll
        @JvmStatic
        fun teardown() {
            stopKoin()
        }
    }

    /**
     * Helper function to register and login, returning the JWT token
     */
    private suspend fun ApplicationTestBuilder.registerAndLogin(
        client: io.ktor.client.HttpClient,
        email: String = "test@example.com",
        password: String = "SecurePass123!",
        fullName: String = "Test User"
    ): String {
        // Register
        client.post("/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(RegisterRequest(email, password, fullName))
        }

        // Login
        val loginResponse = client.post("/api/auth/login") {
            contentType(ContentType.Application.Json)
            setBody(LoginRequest(email, password))
        }

        val apiResponse = loginResponse.body<ApiResponse<LoginResponse>>()
        return apiResponse.data?.token ?: throw Exception("No token received")
    }

    @Test
    fun `test get current user profile`() = testApplication {
        application {
            module()
        }

        val client = createClient {
            install(ContentNegotiation) {
                json(Json {
                    ignoreUnknownKeys = true
                })
            }
        }

        // Get token
        val token = registerAndLogin(client, email = "profile@example.com")

        // Get profile
        val response = client.get("/api/users/me") {
            header(HttpHeaders.Authorization, "Bearer $token")
        }

        // Assert
        assertEquals(HttpStatusCode.OK, response.status)

        val apiResponse = response.body<ApiResponse<User>>()
        assertNotNull(apiResponse.data)
        assertEquals("profile@example.com", apiResponse.data?.email)
    }

    @Test
    fun `test access protected route without token`() = testApplication {
        application {
            module()
        }

        val client = createClient {
            install(ContentNegotiation) {
                json(Json {
                    ignoreUnknownKeys = true
                })
            }
        }

        // Try to access without token
        val response = client.get("/api/users/me")

        // Assert unauthorized
        assertEquals(HttpStatusCode.Unauthorized, response.status)
    }

    @Test
    fun `test access protected route with invalid token`() = testApplication {
        application {
            module()
        }

        val client = createClient {
            install(ContentNegotiation) {
                json(Json {
                    ignoreUnknownKeys = true
                })
            }
        }

        // Try with invalid token
        val response = client.get("/api/users/me") {
            header(HttpHeaders.Authorization, "Bearer invalid-token")
        }

        // Assert unauthorized
        assertEquals(HttpStatusCode.Unauthorized, response.status)
    }

    @Test
    fun `test update user profile`() = testApplication {
        application {
            module()
        }

        val client = createClient {
            install(ContentNegotiation) {
                json(Json {
                    ignoreUnknownKeys = true
                })
            }
        }

        val token = registerAndLogin(
            client,
            email = "update@example.com",
            fullName = "Original Name"
        )

        // Update profile
        val response = client.put("/api/users/me") {
            header(HttpHeaders.Authorization, "Bearer $token")
            contentType(ContentType.Application.Json)
            setBody(UpdateProfileRequest(fullName = "Updated Name"))
        }

        // Assert
        assertEquals(HttpStatusCode.OK, response.status)

        val apiResponse = response.body<ApiResponse<User>>()
        assertEquals("Updated Name", apiResponse.data?.fullName)
    }
}
```

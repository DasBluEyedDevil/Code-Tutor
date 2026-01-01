---
type: "THEORY"
title: "Integration Testing Endpoints"
---


### Example: Testing Auth Endpoints


---



```kotlin
// src/test/kotlin/com/example/routes/AuthRoutesTest.kt
package com.example.routes

import com.example.database.DatabaseFactory
import com.example.di.appModules
import com.example.models.ApiResponse
import com.example.models.LoginRequest
import com.example.models.LoginResponse
import com.example.models.RegisterRequest
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
import kotlin.test.assertTrue

class AuthRoutesTest {

    companion object {
        @BeforeAll
        @JvmStatic
        fun setup() {
            // Initialize test database
            DatabaseFactory.init()
        }

        @AfterAll
        @JvmStatic
        fun teardown() {
            stopKoin()
        }
    }

    @Test
    fun `test user registration`() = testApplication {
        application {
            module()  // Load your application module
        }

        // Create HTTP client with JSON support
        val client = createClient {
            install(ContentNegotiation) {
                json(Json {
                    ignoreUnknownKeys = true
                })
            }
        }

        // Send registration request
        val response = client.post("/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(
                RegisterRequest(
                    email = "test@example.com",
                    password = "SecurePass123!",
                    fullName = "Test User"
                )
            )
        }

        // Assert response
        assertEquals(HttpStatusCode.Created, response.status)

        val apiResponse = response.body<ApiResponse<RegisterResponse>>()
        assertTrue(apiResponse.success)
        assertNotNull(apiResponse.data)
        assertEquals("test@example.com", apiResponse.data?.user?.email)
        assertEquals("Test User", apiResponse.data?.user?.fullName)
    }

    @Test
    fun `test user registration with weak password`() = testApplication {
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

        val response = client.post("/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(
                RegisterRequest(
                    email = "test2@example.com",
                    password = "weak",  // Weak password
                    fullName = "Test User 2"
                )
            )
        }

        // Assert validation error
        assertEquals(HttpStatusCode.BadRequest, response.status)

        val apiResponse = response.body<ErrorResponse>()
        assertEquals(false, apiResponse.success)
        assertNotNull(apiResponse.errors)
        assertTrue(apiResponse.errors!!.containsKey("password"))
    }

    @Test
    fun `test user login`() = testApplication {
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

        // First, register a user
        client.post("/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(
                RegisterRequest(
                    email = "login@example.com",
                    password = "SecurePass123!",
                    fullName = "Login User"
                )
            )
        }

        // Now, login with credentials
        val loginResponse = client.post("/api/auth/login") {
            contentType(ContentType.Application.Json)
            setBody(
                LoginRequest(
                    email = "login@example.com",
                    password = "SecurePass123!"
                )
            )
        }

        // Assert successful login
        assertEquals(HttpStatusCode.OK, loginResponse.status)

        val apiResponse = loginResponse.body<ApiResponse<LoginResponse>>()
        assertTrue(apiResponse.success)
        assertNotNull(apiResponse.data)
        assertNotNull(apiResponse.data?.token)
        assertEquals("login@example.com", apiResponse.data?.user?.email)
    }

    @Test
    fun `test login with wrong password`() = testApplication {
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

        // Register user
        client.post("/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(
                RegisterRequest(
                    email = "wrong@example.com",
                    password = "SecurePass123!",
                    fullName = "Wrong User"
                )
            )
        }

        // Try to login with wrong password
        val loginResponse = client.post("/api/auth/login") {
            contentType(ContentType.Application.Json)
            setBody(
                LoginRequest(
                    email = "wrong@example.com",
                    password = "WrongPassword!"
                )
            )
        }

        // Assert unauthorized
        assertEquals(HttpStatusCode.Unauthorized, loginResponse.status)

        val apiResponse = loginResponse.body<ErrorResponse>()
        assertEquals(false, apiResponse.success)
        assertEquals("Invalid email or password", apiResponse.message)
    }

    @Test
    fun `test duplicate email registration`() = testApplication {
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

        // Register first user
        client.post("/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(
                RegisterRequest(
                    email = "duplicate@example.com",
                    password = "SecurePass123!",
                    fullName = "First User"
                )
            )
        }

        // Try to register second user with same email
        val response = client.post("/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(
                RegisterRequest(
                    email = "duplicate@example.com",
                    password = "DifferentPass456!",
                    fullName = "Second User"
                )
            )
        }

        // Assert conflict error
        assertEquals(HttpStatusCode.Conflict, response.status)

        val apiResponse = response.body<ErrorResponse>()
        assertEquals(false, apiResponse.success)
        assertTrue(apiResponse.message.contains("already exists"))
    }
}
```

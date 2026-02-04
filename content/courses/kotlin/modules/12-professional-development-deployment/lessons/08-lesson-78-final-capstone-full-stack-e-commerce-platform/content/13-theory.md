---
type: "THEORY"
title: "Testing and Running the Application"
---

### Server Integration Tests

Use Ktor's `testApplication` to test API endpoints without starting a real server.

```kotlin
// server/src/test/kotlin/com/taskflow/server/AuthRoutesTest.kt
package com.taskflow.server

import com.taskflow.server.di.serverModule
import com.taskflow.server.plugins.configureDatabase
import com.taskflow.server.plugins.configureRouting
import com.taskflow.server.plugins.configureSecurity
import com.taskflow.server.plugins.configureSerialization
import com.taskflow.shared.dto.ApiResponse
import com.taskflow.shared.dto.AuthResponse
import com.taskflow.shared.dto.LoginRequest
import com.taskflow.shared.dto.RegisterRequest
import io.ktor.client.call.body
import io.ktor.client.plugins.contentnegotiation.ContentNegotiation
import io.ktor.client.request.post
import io.ktor.client.request.setBody
import io.ktor.http.ContentType
import io.ktor.http.HttpStatusCode
import io.ktor.http.contentType
import io.ktor.serialization.kotlinx.json.json
import io.ktor.server.testing.testApplication
import org.koin.ktor.plugin.Koin
import kotlin.test.Test
import kotlin.test.assertEquals
import kotlin.test.assertNotNull
import kotlin.test.assertTrue

class AuthRoutesTest {

    @Test
    fun `register should create user and return token`() = testApplication {
        application {
            install(Koin) { modules(serverModule(this@application)) }
            configureDatabase()
            configureSerialization()
            configureSecurity()
            configureRouting()
        }

        val client = createClient {
            install(ContentNegotiation) { json() }
        }

        val response = client.post("/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(RegisterRequest("test@example.com", "password123", "Test User"))
        }

        assertEquals(HttpStatusCode.Created, response.status)

        val body: ApiResponse<AuthResponse> = response.body()
        assertTrue(body.success)
        assertNotNull(body.data?.token)
        assertEquals("test@example.com", body.data?.user?.email)
    }

    @Test
    fun `login should return token for valid credentials`() = testApplication {
        application {
            install(Koin) { modules(serverModule(this@application)) }
            configureDatabase()
            configureSerialization()
            configureSecurity()
            configureRouting()
        }

        val client = createClient {
            install(ContentNegotiation) { json() }
        }

        // Register first
        client.post("/api/auth/register") {
            contentType(ContentType.Application.Json)
            setBody(RegisterRequest("login@example.com", "password123", "Login User"))
        }

        // Then login
        val response = client.post("/api/auth/login") {
            contentType(ContentType.Application.Json)
            setBody(LoginRequest("login@example.com", "password123"))
        }

        assertEquals(HttpStatusCode.OK, response.status)

        val body: ApiResponse<AuthResponse> = response.body()
        assertTrue(body.success)
        assertNotNull(body.data?.token)
    }

    @Test
    fun `login should reject invalid credentials`() = testApplication {
        application {
            install(Koin) { modules(serverModule(this@application)) }
            configureDatabase()
            configureSerialization()
            configureSecurity()
            configureRouting()
        }

        val client = createClient {
            install(ContentNegotiation) { json() }
        }

        val response = client.post("/api/auth/login") {
            contentType(ContentType.Application.Json)
            setBody(LoginRequest("nobody@example.com", "wrongpassword"))
        }

        assertEquals(HttpStatusCode.Unauthorized, response.status)
    }
}
```

### Running the Full Stack

**Step 1: Start the server**

```bash
./gradlew :server:run
```

The server starts on `http://localhost:8080` with H2 in-memory database.

**Step 2: Run the desktop client**

```bash
./gradlew :composeApp:run
```

The Compose Multiplatform desktop window opens. Register a user, then create and manage tasks.

**Step 3: Run on Android** (requires Android SDK)

```bash
./gradlew :composeApp:installDebug
```

**Step 4: Run server tests**

```bash
./gradlew :server:test
```

### What You Have Built

At this point, you have a working full-stack Kotlin application with:

- A Ktor REST API with JWT authentication and H2 database
- Shared domain models compiled to both server JVM and client targets
- A Compose Multiplatform client running on Desktop and Android
- SQLDelight offline cache with sync logic
- Koin dependency injection on both server and client
- Integration tests using Ktor test host
- Zero external service dependencies -- everything runs locally

---


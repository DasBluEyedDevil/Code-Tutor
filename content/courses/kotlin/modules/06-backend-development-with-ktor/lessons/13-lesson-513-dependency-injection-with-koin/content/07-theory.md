---
type: "THEORY"
title: "Testing with Koin"
---


Koin makes testing incredibly easy by allowing you to swap implementations:


**Benefits**:
- No database setup needed
- Fast tests (in-memory mock data)
- Easy to simulate different scenarios
- Complete isolation between tests

---



```kotlin
// src/test/kotlin/com/example/UserServiceTest.kt
package com.example

import com.example.di.appModule
import com.example.models.User
import com.example.repositories.UserRepository
import com.example.services.UserService
import org.junit.jupiter.api.AfterEach
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import org.koin.core.context.startKoin
import org.koin.core.context.stopKoin
import org.koin.dsl.module
import org.koin.test.KoinTest
import org.koin.test.inject
import kotlin.test.assertEquals
import kotlin.test.assertNotNull

/**
 * Mock repository for testing
 */
class MockUserRepository : UserRepository {
    private val users = mutableMapOf<Int, User>()
    private var nextId = 1

    override fun insert(
        email: String,
        passwordHash: String,
        fullName: String,
        role: String
    ): Int {
        val id = nextId++
        users[id] = User(
            id = id,
            email = email,
            fullName = fullName,
            role = role,
            createdAt = "2025-01-01T00:00:00"
        )
        return id
    }

    override fun getById(id: Int): User? = users[id]

    override fun getByEmail(email: String): User? =
        users.values.find { it.email == email }

    override fun getPasswordHash(email: String): String? = null
    override fun emailExists(email: String): Boolean = getByEmail(email) != null
}

/**
 * Test module with mock dependencies
 */
val testModule = module {
    single<UserRepository> { MockUserRepository() }  // Mock instead of real
    single { UserService(get()) }
}

class UserServiceTest : KoinTest {

    // Inject UserService (using mock repository)
    private val userService: UserService by inject()

    @BeforeEach
    fun setup() {
        startKoin {
            modules(testModule)  // Load test module instead of app module
        }
    }

    @AfterEach
    fun teardown() {
        stopKoin()
    }

    @Test
    fun `test user creation`() {
        // This uses MockUserRepository, no real database needed!
        val user = userService.createUser(
            email = "test@example.com",
            passwordHash = "hash",
            fullName = "Test User",
            role = "USER"
        ).getOrNull()

        assertNotNull(user)
        assertEquals("test@example.com", user.email)
        assertEquals("Test User", user.fullName)
    }
}
```

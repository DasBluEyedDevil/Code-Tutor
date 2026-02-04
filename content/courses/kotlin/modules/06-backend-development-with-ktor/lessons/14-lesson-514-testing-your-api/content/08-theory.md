---
type: "THEORY"
title: "Testing with Koin"
---


### Test Module Setup


---



```kotlin
// src/test/kotlin/com/example/TestModule.kt
package com.example

import com.example.repositories.UserRepository
import com.example.services.AuthService
import com.example.services.UserService
import org.koin.dsl.module

class MockUserRepository : UserRepository {
    // ... implementation
}

val testModule = module {
    single<UserRepository> { MockUserRepository() }
    single { UserService(get()) }
    single { AuthService(get()) }
}

// Usage in tests
@ExtendWith(KoinExtension::class)
@KoinTest
class MyServiceTest {

    @BeforeEach
    fun setup() {
        startKoin {
            modules(testModule)
        }
    }

    @AfterEach
    fun teardown() {
        stopKoin()
    }

    @Test
    fun `test with Koin`() {
        val userService by inject<UserService>()
        // Test using injected service
    }
}
```

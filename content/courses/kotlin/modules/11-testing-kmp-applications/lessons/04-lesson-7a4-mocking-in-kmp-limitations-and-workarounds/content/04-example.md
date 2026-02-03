---
type: "EXAMPLE"
title: "Using Mokkery for KMP Mocking"
---

Mokkery is a KMP-compatible mocking library using KSP:

```kotlin
// build.gradle.kts
plugins {
    id("dev.mokkery") version "3.1.1"
}

kotlin {
    sourceSets {
        val commonTest by getting {
            dependencies {
                implementation("dev.mokkery:mokkery-runtime:3.1.1")
            }
        }
    }
}

// Test using Mokkery
import dev.mokkery.*
import dev.mokkery.answering.returns
import dev.mokkery.matcher.any

class UserViewModelTest {
    @Test
    fun `loads user on init`() = runTest {
        // Create mock
        val mockRepo = mock<UserRepository> {
            everySuspend { getUser(any()) } returns User("1", "John")
        }
        
        val viewModel = UserViewModel(mockRepo)
        advanceUntilIdle()
        
        assertEquals("John", viewModel.state.value.user?.name)
        
        // Verify interaction
        verifySuspend { mockRepo.getUser("1") }
    }
    
    @Test
    fun `handles error gracefully`() = runTest {
        val mockRepo = mock<UserRepository> {
            everySuspend { getUser(any()) } throws IOException("Network error")
        }
        
        val viewModel = UserViewModel(mockRepo)
        advanceUntilIdle()
        
        assertNotNull(viewModel.state.value.error)
    }
}

// Mokkery features
mock<T> { }              // Create mock with stubbing
everySuspend { }         // Stub suspend functions  
every { }                // Stub regular functions
returns value            // Return a value
throws exception         // Throw an exception
verifySuspend { }        // Verify suspend call happened
verify { }               // Verify regular call
any()                    // Match any argument
eq(value)                // Match specific value
```

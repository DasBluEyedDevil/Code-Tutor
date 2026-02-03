---
type: "THEORY"
title: "Testing Flows with Turbine"
---

**Turbine** is a library for testing flows:

### Setup
```kotlin
// In build.gradle.kts
commonTest {
    dependencies {
        implementation("app.cash.turbine:turbine:1.2.1")
    }
}
```

### Testing StateFlow
```kotlin
import app.cash.turbine.test
import kotlin.test.Test
import kotlin.test.assertEquals

class ProfileViewModelTest {
    @Test
    fun `loading then success states`() = runTest {
        val viewModel = ProfileViewModel(FakeRepository())
        
        viewModel.uiState.test {
            // Initial state
            assertEquals(ProfileUiState.Loading, awaitItem())
            
            viewModel.loadProfile("123")
            
            // Success state after loading
            val success = awaitItem()
            assertTrue(success is ProfileUiState.Success)
            assertEquals("John", (success as ProfileUiState.Success).user.name)
            
            cancelAndIgnoreRemainingEvents()
        }
    }
}
```

### Testing Flow Emissions
```kotlin
@Test
fun `emits updated values`() = runTest {
    val repository = FakeUserRepository()
    
    repository.users.test {
        assertEquals(emptyList(), awaitItem()) // Initial
        
        repository.addUser(User("1", "Alice"))
        assertEquals(listOf(User("1", "Alice")), awaitItem())
        
        repository.addUser(User("2", "Bob"))
        val users = awaitItem()
        assertEquals(2, users.size)
        
        cancelAndIgnoreRemainingEvents()
    }
}
```

### Testing Error Cases
```kotlin
@Test
fun `handles errors gracefully`() = runTest {
    val errorRepository = FailingRepository()
    val viewModel = ProfileViewModel(errorRepository)
    
    viewModel.uiState.test {
        assertEquals(ProfileUiState.Loading, awaitItem())
        
        viewModel.loadProfile("123")
        
        val error = awaitItem()
        assertTrue(error is ProfileUiState.Error)
        assertEquals("Network error", (error as ProfileUiState.Error).message)
    }
}
```
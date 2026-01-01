---
type: "THEORY"
title: "Testing Coroutines"
---


### runTest - The Testing Coroutine


**Basic Coroutine Test**:

### Testing Flows


### Testing ViewModels with Coroutines


---



```kotlin
import kotlinx.coroutines.*
import kotlinx.coroutines.test.*
import org.junit.jupiter.api.*

class UserViewModelTest {

    private val testDispatcher = StandardTestDispatcher()

    @BeforeEach
    fun setup() {
        Dispatchers.setMain(testDispatcher)
    }

    @AfterEach
    fun cleanup() {
        Dispatchers.resetMain()
    }

    @Test
    fun `loading users should update state`() = runTest {
        val mockRepo = mockk<UserRepository>()
        every { mockRepo.getUsers() } returns flowOf(
            listOf(User(1, "John"), User(2, "Jane"))
        )

        val viewModel = UserViewModel(mockRepo)

        // Trigger action
        viewModel.loadUsers()

        // Advance until idle
        advanceUntilIdle()

        // Verify state
        assertEquals(2, viewModel.users.value.size)
        assertEquals(false, viewModel.isLoading.value)
    }
}
```

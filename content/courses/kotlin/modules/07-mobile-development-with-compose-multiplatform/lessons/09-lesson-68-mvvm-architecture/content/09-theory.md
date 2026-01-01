---
type: "THEORY"
title: "Testing ViewModels"
---


### Unit Test Setup


### Test ViewModel


---



```kotlin
import kotlinx.coroutines.ExperimentalCoroutinesApi
import kotlinx.coroutines.test.*
import org.junit.After
import org.junit.Before
import org.junit.Test
import kotlin.test.assertEquals

@ExperimentalCoroutinesApi
class TasksViewModelTest {

    private val testDispatcher = StandardTestDispatcher()
    private lateinit var repository: FakeTaskRepository
    private lateinit var viewModel: TasksViewModel

    @Before
    fun setup() {
        Dispatchers.setMain(testDispatcher)
        repository = FakeTaskRepository()
        viewModel = TasksViewModel(repository)
    }

    @After
    fun tearDown() {
        Dispatchers.resetMain()
    }

    @Test
    fun `addTask should add task to repository`() = runTest {
        // Given
        val title = "Test Task"
        val description = "Test Description"

        // When
        viewModel.addTask(title, description)
        advanceUntilIdle()

        // Then
        val tasks = repository.tasks.value
        assertEquals(1, tasks.size)
        assertEquals(title, tasks[0].title)
    }

    @Test
    fun `deleteTask should remove task from repository`() = runTest {
        // Given
        val task = Task(id = 1, title = "Task")
        repository.insertTask(task)

        // When
        viewModel.deleteTask(task)
        advanceUntilIdle()

        // Then
        val tasks = repository.tasks.value
        assertEquals(0, tasks.size)
    }
}

// Fake repository for testing
class FakeTaskRepository : TaskRepository {
    private val _tasks = MutableStateFlow<List<Task>>(emptyList())
    override val tasks: Flow<List<Task>> = _tasks

    override suspend fun insertTask(task: Task) {
        _tasks.value = _tasks.value + task
    }

    override suspend fun deleteTask(task: Task) {
        _tasks.value = _tasks.value - task
    }
}
```

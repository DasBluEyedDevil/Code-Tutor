---
type: "THEORY"
title: "Clean Architecture Layers"
---


### Domain Layer (Business Logic)


### ViewModel with Use Cases


---



```kotlin
// Note: @HiltViewModel and @Inject are Android-only (Hilt DI).
// For cross-platform, inject dependencies via constructor and use Koin.

// Android with Hilt:
@HiltViewModel
class TasksViewModel @Inject constructor(
    private val getTasksUseCase: GetTasksUseCase,
    private val addTaskUseCase: AddTaskUseCase,
    private val deleteTaskUseCase: DeleteTaskUseCase
) : ViewModel() {

    val tasks: StateFlow<List<Task>> = getTasksUseCase()
        .stateIn(viewModelScope, SharingStarted.WhileSubscribed(5000), emptyList())

    fun addTask(title: String, description: String) {
        viewModelScope.launch {
            addTaskUseCase(title, description)
        }
    }

    fun deleteTask(task: Task) {
        viewModelScope.launch {
            deleteTaskUseCase(task)
        }
    }
}

// Cross-platform alternative (Koin):
class TasksViewModel(
    private val getTasksUseCase: GetTasksUseCase,
    private val addTaskUseCase: AddTaskUseCase,
    private val deleteTaskUseCase: DeleteTaskUseCase
) : ViewModel() {
    // Same logic -- DI framework differs, not the ViewModel
}
```

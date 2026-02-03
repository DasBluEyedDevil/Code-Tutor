---
type: "EXAMPLE"
title: "Complete Example: Todo List with StateFlow"
---

A complete ViewModel pattern using StateFlow:

```kotlin
import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

data class Todo(val id: Int, val text: String, val completed: Boolean)

data class TodoState(
    val todos: List<Todo> = emptyList(),
    val isLoading: Boolean = false,
    val error: String? = null
)

sealed class TodoEvent {
    data class ShowSnackbar(val message: String) : TodoEvent()
    data object ScrollToTop : TodoEvent()
}

class TodoViewModel {
    private val scope = CoroutineScope(Dispatchers.Main + SupervisorJob())
    
    private val _state = MutableStateFlow(TodoState())
    val state: StateFlow<TodoState> = _state.asStateFlow()
    
    private val _events = MutableSharedFlow<TodoEvent>()
    val events: SharedFlow<TodoEvent> = _events.asSharedFlow()
    
    private var nextId = 1
    
    fun addTodo(text: String) {
        val newTodo = Todo(nextId++, text, false)
        _state.update { currentState ->
            currentState.copy(
                todos = currentState.todos + newTodo
            )
        }
        scope.launch {
            _events.emit(TodoEvent.ShowSnackbar("Todo added"))
            _events.emit(TodoEvent.ScrollToTop)
        }
    }
    
    fun toggleTodo(id: Int) {
        _state.update { currentState ->
            currentState.copy(
                todos = currentState.todos.map { todo ->
                    if (todo.id == id) todo.copy(completed = !todo.completed)
                    else todo
                }
            )
        }
    }
    
    fun deleteTodo(id: Int) {
        val deletedTodo = _state.value.todos.find { it.id == id }
        _state.update { currentState ->
            currentState.copy(
                todos = currentState.todos.filter { it.id != id }
            )
        }
        scope.launch {
            _events.emit(TodoEvent.ShowSnackbar("Deleted: ${deletedTodo?.text}"))
        }
    }
    
    fun onCleared() = scope.cancel()
}

fun main() = runBlocking {
    val viewModel = TodoViewModel()
    
    // Collect state
    launch {
        viewModel.state.collect { state ->
            println("Todos: ${state.todos.map { "${it.text}(${if (it.completed) "✓" else " "})" }}")
        }
    }
    
    // Collect events
    launch {
        viewModel.events.collect { event ->
            when (event) {
                is TodoEvent.ShowSnackbar -> println("Snackbar: ${event.message}")
                TodoEvent.ScrollToTop -> println("Scrolling to top")
            }
        }
    }
    
    delay(100)
    viewModel.addTodo("Learn StateFlow")
    delay(100)
    viewModel.addTodo("Build an app")
    delay(100)
    viewModel.toggleTodo(1)
    delay(100)
    viewModel.deleteTodo(2)
    delay(200)
    
    viewModel.onCleared()
}
```

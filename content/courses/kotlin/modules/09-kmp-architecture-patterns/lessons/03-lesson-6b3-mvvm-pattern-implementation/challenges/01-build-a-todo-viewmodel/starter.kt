data class Todo(
    val id: String,
    val title: String,
    val isCompleted: Boolean
)

data class TodoUiState(
    val todos: List<Todo> = emptyList(),
    val newTodoText: String = "",
    val filter: TodoFilter = TodoFilter.ALL
)

enum class TodoFilter { ALL, ACTIVE, COMPLETED }

// TODO: Implement the ViewModel
class TodoViewModel {
    // 1. Create state flow
    // 2. Implement onNewTodoTextChanged(text: String)
    // 3. Implement onAddTodo()
    // 4. Implement onToggleTodo(id: String)
    // 5. Implement onDeleteTodo(id: String)
    // 6. Implement onFilterChanged(filter: TodoFilter)
}
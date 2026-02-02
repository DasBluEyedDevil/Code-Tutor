class TodoViewModel {
    private val _state = MutableStateFlow(TodoUiState())
    val state: StateFlow<TodoUiState> = _state.asStateFlow()
    
    private val allTodos = mutableListOf<Todo>()
    
    fun onNewTodoTextChanged(text: String) {
        _state.update { it.copy(newTodoText = text) }
    }
    
    fun onAddTodo() {
        val text = _state.value.newTodoText.trim()
        if (text.isBlank()) return
        
        val newTodo = Todo(
            id = UUID.randomUUID().toString(),
            title = text,
            isCompleted = false
        )
        allTodos.add(newTodo)
        
        _state.update {
            it.copy(
                todos = filterTodos(allTodos, it.filter),
                newTodoText = ""
            )
        }
    }
    
    fun onToggleTodo(id: String) {
        val index = allTodos.indexOfFirst { it.id == id }
        if (index >= 0) {
            allTodos[index] = allTodos[index].copy(
                isCompleted = !allTodos[index].isCompleted
            )
            _state.update {
                it.copy(todos = filterTodos(allTodos, it.filter))
            }
        }
    }
    
    fun onDeleteTodo(id: String) {
        allTodos.removeAll { it.id == id }
        _state.update {
            it.copy(todos = filterTodos(allTodos, it.filter))
        }
    }
    
    fun onFilterChanged(filter: TodoFilter) {
        _state.update {
            it.copy(
                filter = filter,
                todos = filterTodos(allTodos, filter)
            )
        }
    }
    
    private fun filterTodos(todos: List<Todo>, filter: TodoFilter): List<Todo> {
        return when (filter) {
            TodoFilter.ALL -> todos
            TodoFilter.ACTIVE -> todos.filter { !it.isCompleted }
            TodoFilter.COMPLETED -> todos.filter { it.isCompleted }
        }
    }
}
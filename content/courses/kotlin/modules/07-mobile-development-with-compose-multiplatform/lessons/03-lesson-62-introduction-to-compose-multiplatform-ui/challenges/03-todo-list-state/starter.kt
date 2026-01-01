data class Todo(val id: Int, val text: String, var completed: Boolean = false)

class TodoListState {
    private val todos = mutableListOf<Todo>()
    private var nextId = 1
    
    fun addTodo(text: String) {
        // Add new todo
    }
    
    fun toggleTodo(id: Int) {
        // Toggle completed status
    }
    
    fun getTodos(): List<Todo> = todos.toList()
}

fun main() {
    val state = TodoListState()
    state.addTodo("Learn Kotlin")
    state.addTodo("Build app")
    println("Todos: ${state.getTodos()}")
    state.toggleTodo(1)
    println("After toggle: ${state.getTodos()}")
}
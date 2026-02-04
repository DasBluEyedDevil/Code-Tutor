data class TodoItem(val id: Int, val title: String, var isComplete: Boolean = false)

class TodoViewModel {
    private val _todos = mutableListOf<TodoItem>()
    private var nextId = 1
    
    val todos: List<TodoItem> get() = _todos.toList()
    
    fun addTodo(title: String) {
        _todos.add(TodoItem(nextId++, title))
    }
    
    fun toggleComplete(id: Int) {
        _todos.find { it.id == id }?.let { it.isComplete = !it.isComplete }
    }
    
    fun display() {
        _todos.forEach { todo ->
            val check = if (todo.isComplete) "x" else " "
            println("[$check] ${todo.title}")
        }
    }
}

fun main() {
    val vm = TodoViewModel()
    vm.addTodo("Buy groceries")
    vm.addTodo("Walk the dog")
    vm.display()
    vm.toggleComplete(1)
    vm.display()
}

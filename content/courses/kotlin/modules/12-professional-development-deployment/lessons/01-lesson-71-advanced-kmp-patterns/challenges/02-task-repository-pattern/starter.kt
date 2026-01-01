data class Task(
    val id: Int,
    val title: String,
    val description: String,
    var completed: Boolean,
    val userId: Int
)

class TaskRepository {
    private val tasks = mutableListOf<Task>()
    private var nextId = 1
    
    fun create(title: String, description: String, userId: Int): Task {
        // Create and add task
    }
    
    fun findAll(userId: Int): List<Task> {
        // Find all tasks for user
    }
    
    fun findById(id: Int): Task? {
        // Find task by ID
    }
    
    fun update(id: Int, title: String?, description: String?, completed: Boolean?): Boolean {
        // Update task fields if not null
    }
    
    fun delete(id: Int): Boolean {
        // Delete task
    }
}

fun main() {
    val repo = TaskRepository()
    val task1 = repo.create("Task 1", "Description 1", 1)
    val task2 = repo.create("Task 2", "Description 2", 1)
    
    println("All tasks: ${repo.findAll(1)}")
    repo.update(1, null, null, true)
    println("After update: ${repo.findById(1)}")
    repo.delete(2)
    println("After delete: ${repo.findAll(1).size}")
}
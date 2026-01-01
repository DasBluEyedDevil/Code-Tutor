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
        val task = Task(nextId++, title, description, false, userId)
        tasks.add(task)
        return task
    }
    
    fun findAll(userId: Int): List<Task> {
        return tasks.filter { it.userId == userId }
    }
    
    fun findById(id: Int): Task? {
        return tasks.find { it.id == id }
    }
    
    fun update(id: Int, title: String?, description: String?, completed: Boolean?): Boolean {
        val task = findById(id) ?: return false
        val index = tasks.indexOf(task)
        tasks[index] = task.copy(
            title = title ?: task.title,
            description = description ?: task.description,
            completed = completed ?: task.completed
        )
        return true
    }
    
    fun delete(id: Int): Boolean {
        return tasks.removeIf { it.id == id }
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
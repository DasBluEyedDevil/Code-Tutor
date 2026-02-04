data class Task(val name: String, val priority: Int)

class TaskQueue {
    private val tasks = mutableListOf<Task>()
    
    fun add(task: Task) {
        TODO("Add task to queue")
    }
    
    fun processAll() {
        // Process tasks in priority order (lower number = higher priority)
        TODO("Process all tasks")
    }
}

fun main() {
    val queue = TaskQueue()
    queue.add(Task("Test", 3))
    queue.add(Task("Build", 2))
    queue.add(Task("Deploy", 1))
    queue.processAll()
}

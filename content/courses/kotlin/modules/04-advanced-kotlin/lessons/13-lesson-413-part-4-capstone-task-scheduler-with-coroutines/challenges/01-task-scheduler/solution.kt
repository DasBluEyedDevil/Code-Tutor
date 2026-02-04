data class Task(val name: String, val priority: Int)

class TaskQueue {
    private val tasks = mutableListOf<Task>()
    
    fun add(task: Task) {
        tasks.add(task)
    }
    
    fun processAll() {
        tasks.sortedBy { it.priority }.forEach { task ->
            println("Processing: ${task.name} (priority ${task.priority})")
        }
    }
}

fun main() {
    val queue = TaskQueue()
    queue.add(Task("Test", 3))
    queue.add(Task("Build", 2))
    queue.add(Task("Deploy", 1))
    queue.processAll()
}

data class Task(
    val id: Int,
    val title: String,
    val description: String,
    val completed: Boolean,
    val priority: String,
    val userId: Int
)

data class CreateTaskRequest(
    val title: String,
    val description: String,
    val priority: String
)

data class UpdateTaskRequest(
    val title: String?,
    val description: String?,
    val completed: Boolean?
)

fun main() {
    val task = Task(1, "Learn Kotlin", "Complete course", false, "high", 1)
    val createRequest = CreateTaskRequest("New Task", "Description", "medium")
    val updateRequest = UpdateTaskRequest(null, null, true)
    
    println(task)
    println(createRequest)
    println(updateRequest)
}
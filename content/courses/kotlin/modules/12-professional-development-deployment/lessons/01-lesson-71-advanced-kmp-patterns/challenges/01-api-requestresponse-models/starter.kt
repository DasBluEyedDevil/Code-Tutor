// Create Task data class with: id, title, description, completed, priority, userId

// Create CreateTaskRequest with: title, description, priority

// Create UpdateTaskRequest with nullable fields: title?, description?, completed?

fun main() {
    val task = Task(1, "Learn Kotlin", "Complete course", false, "high", 1)
    val createRequest = CreateTaskRequest("New Task", "Description", "medium")
    val updateRequest = UpdateTaskRequest(null, null, true)
    
    println(task)
    println(createRequest)
    println(updateRequest)
}
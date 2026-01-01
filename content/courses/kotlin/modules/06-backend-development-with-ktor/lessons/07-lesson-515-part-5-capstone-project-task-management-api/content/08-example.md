---
type: "EXAMPLE"
title: "Example Solution Structure"
---



---



```kotlin
// models/Task.kt
@Serializable
data class Task(
    val id: Int,
    val title: String,
    val description: String?,
    val status: TaskStatus,
    val priority: TaskPriority,
    val dueDate: String?,
    val ownerId: Int,
    val assignedToId: Int?,
    val createdAt: String,
    val updatedAt: String
)

@Serializable
enum class TaskStatus {
    TODO, IN_PROGRESS, DONE
}

@Serializable
enum class TaskPriority {
    LOW, MEDIUM, HIGH
}

@Serializable
data class CreateTaskRequest(
    val title: String,
    val description: String? = null,
    val status: String = "TODO",
    val priority: String = "MEDIUM",
    val dueDate: String? = null,
    val assignedToId: Int? = null
)

@Serializable
data class UpdateTaskRequest(
    val title: String,
    val description: String?,
    val status: String,
    val priority: String,
    val dueDate: String?
)

@Serializable
data class UpdateTaskStatusRequest(
    val status: String
)

@Serializable
data class AssignTaskRequest(
    val assignedToId: Int
)

// repositories/TaskRepository.kt
interface TaskRepository {
    fun insert(task: Task): Int
    fun update(id: Int, task: Task): Boolean
    fun delete(id: Int): Boolean
    fun getById(id: Int): Task?
    fun getAllForUser(userId: Int): List<Task>
    fun getAssignedToUser(userId: Int): List<Task>
    fun search(userId: Int, filters: TaskFilters): List<Task>
}

data class TaskFilters(
    val status: TaskStatus? = null,
    val priority: TaskPriority? = null,
    val assignedToMe: Boolean = false,
    val search: String? = null,
    val sortBy: String = "createdAt",
    val order: String = "desc"
)

// services/TaskService.kt
class TaskService(
    private val taskRepository: TaskRepository,
    private val userRepository: UserRepository
) {
    fun createTask(request: CreateTaskRequest, principal: UserPrincipal): Result<Task>
    fun updateTask(id: Int, request: UpdateTaskRequest, principal: UserPrincipal): Result<Task>
    fun deleteTask(id: Int, principal: UserPrincipal): Result<Unit>
    fun getTaskById(id: Int, principal: UserPrincipal): Result<Task>
    fun getUserTasks(principal: UserPrincipal, filters: TaskFilters): Result<List<Task>>
    fun assignTask(id: Int, request: AssignTaskRequest, principal: UserPrincipal): Result<Task>
    fun updateTaskStatus(id: Int, request: UpdateTaskStatusRequest, principal: UserPrincipal): Result<Task>

    private fun canViewTask(task: Task, principal: UserPrincipal): Boolean {
        return task.ownerId == principal.userId ||
               task.assignedToId == principal.userId ||
               principal.role == "ADMIN"
    }

    private fun canModifyTask(task: Task, principal: UserPrincipal): Boolean {
        return task.ownerId == principal.userId || principal.role == "ADMIN"
    }

    private fun canUpdateStatus(task: Task, principal: UserPrincipal): Boolean {
        return task.ownerId == principal.userId ||
               task.assignedToId == principal.userId ||
               principal.role == "ADMIN"
    }
}

// routes/TaskRoutes.kt
fun Route.taskRoutes() {
    val taskService by inject<TaskService>()

    authenticate("jwt-auth") {
        route("/api/tasks") {
            post {
                val principal = call.principal<UserPrincipal>()!!
                val request = call.receive<CreateTaskRequest>()

                taskService.createTask(request, principal)
                    .onSuccess { task ->
                        call.respond(
                            HttpStatusCode.Created,
                            ApiResponse(data = task, message = "Task created")
                        )
                    }
                    .onFailure { error -> throw error }
            }

            get {
                val principal = call.principal<UserPrincipal>()!!
                val filters = TaskFilters(
                    status = call.request.queryParameters["status"]?.let { TaskStatus.valueOf(it) },
                    priority = call.request.queryParameters["priority"]?.let { TaskPriority.valueOf(it) },
                    assignedToMe = call.request.queryParameters["assignedToMe"]?.toBoolean() ?: false,
                    search = call.request.queryParameters["search"],
                    sortBy = call.request.queryParameters["sortBy"] ?: "createdAt",
                    order = call.request.queryParameters["order"] ?: "desc"
                )

                taskService.getUserTasks(principal, filters)
                    .onSuccess { tasks ->
                        call.respond(ApiResponse(data = tasks))
                    }
                    .onFailure { error -> throw error }
            }

            // ... other routes
        }
    }
}
```

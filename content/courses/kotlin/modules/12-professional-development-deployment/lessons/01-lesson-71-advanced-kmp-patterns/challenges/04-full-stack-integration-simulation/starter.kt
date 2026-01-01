data class User(val id: Int, val username: String, val email: String)
data class Task(val id: Int, val title: String, val completed: Boolean, val userId: Int)

class Application {
    private val users = mutableMapOf<Int, User>()
    private val tasks = mutableListOf<Task>()
    private val sessions = mutableMapOf<String, Int>() // token to userId
    private var nextUserId = 1
    private var nextTaskId = 1
    
    fun register(username: String, email: String): User {
        // Create and store user
    }
    
    fun login(userId: Int): String {
        // Create session and return token
    }
    
    fun createTask(token: String, title: String): Task? {
        // Validate token, create task
    }
    
    fun getTasks(token: String): List<Task>? {
        // Validate token, return user's tasks
    }
}

fun main() {
    val app = Application()
    val user = app.register("alice", "alice@example.com")
    println("User registered: $user")
    
    val token = app.login(user.id)
    println("Logged in with token")
    
    app.createTask(token, "Learn Kotlin")
    app.createTask(token, "Build App")
    
    val tasks = app.getTasks(token)
    println("Tasks: $tasks")
}
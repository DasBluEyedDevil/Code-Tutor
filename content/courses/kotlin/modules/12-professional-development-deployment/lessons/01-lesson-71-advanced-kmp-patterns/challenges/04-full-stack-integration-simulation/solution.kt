import java.util.UUID

data class User(val id: Int, val username: String, val email: String)
data class Task(val id: Int, val title: String, val completed: Boolean, val userId: Int)

class Application {
    private val users = mutableMapOf<Int, User>()
    private val tasks = mutableListOf<Task>()
    private val sessions = mutableMapOf<String, Int>() // token to userId
    private var nextUserId = 1
    private var nextTaskId = 1
    
    fun register(username: String, email: String): User {
        val user = User(nextUserId++, username, email)
        users[user.id] = user
        return user
    }
    
    fun login(userId: Int): String {
        val token = UUID.randomUUID().toString()
        sessions[token] = userId
        return token
    }
    
    fun createTask(token: String, title: String): Task? {
        val userId = sessions[token] ?: return null
        val task = Task(nextTaskId++, title, false, userId)
        tasks.add(task)
        return task
    }
    
    fun getTasks(token: String): List<Task>? {
        val userId = sessions[token] ?: return null
        return tasks.filter { it.userId == userId }
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
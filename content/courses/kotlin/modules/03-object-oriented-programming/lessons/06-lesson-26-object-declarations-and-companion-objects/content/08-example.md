---
type: "EXAMPLE"
title: "Real-World Example: Database Manager"
---



---



```kotlin
data class User(val id: Int, val name: String, val email: String)

object DatabaseManager {
    private val users = mutableMapOf<Int, User>()
    private var nextId = 1
    private var isInitialized = false

    init {
        println("Initializing Database Manager...")
    }

    fun initialize() {
        if (isInitialized) {
            println("Database already initialized")
            return
        }
        println("Setting up database connection...")
        isInitialized = true
    }

    fun insertUser(name: String, email: String): User {
        require(isInitialized) { "Database not initialized" }
        val user = User(nextId++, name, email)
        users[user.id] = user
        println("Inserted user: ${user.name}")
        return user
    }

    fun getUserById(id: Int): User? {
        require(isInitialized) { "Database not initialized" }
        return users[id]
    }

    fun getAllUsers(): List<User> {
        require(isInitialized) { "Database not initialized" }
        return users.values.toList()
    }

    fun deleteUser(id: Int): Boolean {
        require(isInitialized) { "Database not initialized" }
        return users.remove(id) != null
    }

    fun getUserCount() = users.size
}

fun main() {
    DatabaseManager.initialize()

    DatabaseManager.insertUser("Alice", "alice@example.com")
    DatabaseManager.insertUser("Bob", "bob@example.com")
    DatabaseManager.insertUser("Carol", "carol@example.com")

    println("\nAll users:")
    DatabaseManager.getAllUsers().forEach { user ->
        println("${user.id}: ${user.name} (${user.email})")
    }

    println("\nGet user by ID:")
    val user = DatabaseManager.getUserById(2)
    println(user)

    println("\nDelete user 2:")
    DatabaseManager.deleteUser(2)

    println("\nRemaining users: ${DatabaseManager.getUserCount()}")
    DatabaseManager.getAllUsers().forEach { user ->
        println("${user.id}: ${user.name}")
    }
}
```

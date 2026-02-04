data class User(val id: Int, val name: String, val email: String)

class UserDatabase {
    private val users = mutableListOf<User>()
    
    fun addUser(user: User) {
        users.add(user)
    }
    
    fun findById(id: Int): User? {
        return users.find { it.id == id }
    }
}

fun main() {
    val db = UserDatabase()
    db.addUser(User(1, "Alice", "alice@example.com"))
    db.addUser(User(2, "Bob", "bob@example.com"))
    
    println(db.findById(1))
    println(db.findById(99))
}
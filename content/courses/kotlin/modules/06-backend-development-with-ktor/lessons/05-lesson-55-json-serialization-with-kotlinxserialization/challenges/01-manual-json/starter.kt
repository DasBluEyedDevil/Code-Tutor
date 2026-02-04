data class User(val name: String, val email: String, val age: Int)

fun User.toJson(): String {
    // Build a JSON string representation
    TODO("Implement JSON serialization")
}

fun main() {
    val user = User("Alice", "alice@example.com", 25)
    println(user.toJson())
}

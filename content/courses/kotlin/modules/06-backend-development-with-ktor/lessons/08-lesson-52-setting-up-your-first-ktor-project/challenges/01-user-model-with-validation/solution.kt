data class User(val name: String, val email: String) {
    fun isValid(): Boolean {
        return email.contains("@") && name.isNotBlank()
    }
}

fun main() {
    val user1 = User("Alice", "alice@example.com")
    val user2 = User("Bob", "invalid-email")
    println("User1 valid: ${user1.isValid()}")
    println("User2 valid: ${user2.isValid()}")
}
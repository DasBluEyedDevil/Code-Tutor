data class User(val name: String, val email: String, val age: Int)

fun User.toJson(): String {
    return """{"name":"$name","email":"$email","age":$age}"""
}

fun main() {
    val user = User("Alice", "alice@example.com", 25)
    println(user.toJson())
}

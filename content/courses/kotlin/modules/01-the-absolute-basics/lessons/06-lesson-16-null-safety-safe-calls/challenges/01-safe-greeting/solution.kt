fun greet(name: String?): String {
    val displayName = name ?: "Guest"
    return "Hello, $displayName!"
}

fun main() {
    println(greet("Alice"))
    println(greet(null))
}

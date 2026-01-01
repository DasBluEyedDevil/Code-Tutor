fun findUserById(id: Int): String? {
    return when (id) {
        1 -> "Alice"
        2 -> "Bob"
        3 -> "Charlie"
        4 -> "Diana"
        5 -> "Eve"
        else -> null
    }
}

fun main() {
    println(findUserById(3))
    println(findUserById(10))
}
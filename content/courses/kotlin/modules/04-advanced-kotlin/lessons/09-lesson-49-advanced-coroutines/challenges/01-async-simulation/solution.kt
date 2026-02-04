fun fetchUserName(): String {
    return "Alice"
}

fun fetchUserAge(): Int {
    return 25
}

fun main() {
    val name = fetchUserName()
    val age = fetchUserAge()
    println("User: $name, Age: $age")
}

fun validate(name: String, email: String, age: Int): List<String> {
    val errors = mutableListOf<String>()
    if (name.isBlank()) errors.add("Name must not be empty")
    if (!email.contains("@")) errors.add("Email must contain @")
    if (age !in 1..150) errors.add("Age must be between 1 and 150")
    return errors
}

fun main() {
    val errors = validate("", "invalid-email", -5)
    errors.forEach { println(it) }
}

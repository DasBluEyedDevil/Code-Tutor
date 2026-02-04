fun validate(name: String, email: String, age: Int): List<String> {
    val errors = mutableListOf<String>()
    // Validate name is not empty
    // Validate email contains @
    // Validate age is between 1 and 150
    TODO("Implement validation")
}

fun main() {
    val errors = validate("", "invalid-email", -5)
    errors.forEach { println(it) }
}

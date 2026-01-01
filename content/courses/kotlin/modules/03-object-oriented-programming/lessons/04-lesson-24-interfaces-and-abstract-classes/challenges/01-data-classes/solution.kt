data class Person(val name: String, val age: Int, val email: String)

fun main() {
    val person1 = Person("Alice", 30, "alice@example.com")
    val person2 = Person("Alice", 30, "alice@example.com")
    println("Are they equal? ${person1 == person2}")
    println(person1)
}
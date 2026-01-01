data class GreetingProps(val name: String)

fun main() {
    val props = GreetingProps("Alice")
    println("Hello, ${props.name}!")
}
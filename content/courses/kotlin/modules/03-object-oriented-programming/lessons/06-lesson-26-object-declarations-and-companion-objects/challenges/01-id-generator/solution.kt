object IdGenerator {
    private var counter = 0
    fun nextId(): Int = ++counter
}

fun main() {
    println(IdGenerator.nextId())
    println(IdGenerator.nextId())
    println(IdGenerator.nextId())
}

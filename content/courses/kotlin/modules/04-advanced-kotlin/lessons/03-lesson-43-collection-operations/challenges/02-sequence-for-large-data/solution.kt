fun main() {
    val squares = generateSequence(1) { it + 1 }
        .map { it * it }
    
    println(squares.take(5).toList())
}
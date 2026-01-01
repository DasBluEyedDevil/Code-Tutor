fun main() {
    val numbers = listOf(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)
    
    val (even, odd) = numbers.partition { it % 2 == 0 }
    
    println("Even: $even")
    println("Odd: $odd")
}
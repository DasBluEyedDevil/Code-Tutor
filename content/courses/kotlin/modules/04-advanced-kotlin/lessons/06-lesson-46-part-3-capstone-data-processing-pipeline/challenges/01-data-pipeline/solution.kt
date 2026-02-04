fun main() {
    val scores = listOf(85, 42, 73, 91, 55, 68)
    val passing = scores.filter { it >= 60 }
    println("Passing: ${passing.size} students")
    println("Average: ${"%.1f".format(passing.average())}")
}

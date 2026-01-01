fun List<Int>.secondLargest(): Int? {
    if (this.size < 2) return null
    val sorted = this.sortedDescending()
    return sorted[1]
}

fun main() {
    println(listOf(5, 2, 8, 1, 9).secondLargest())  // Should be 8
    println(listOf(10).secondLargest())              // Should be null
}
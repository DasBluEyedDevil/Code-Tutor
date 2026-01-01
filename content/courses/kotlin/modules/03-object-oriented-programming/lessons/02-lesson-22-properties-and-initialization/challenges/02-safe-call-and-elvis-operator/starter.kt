fun getLength(text: String?): Int {
    // Use safe call and Elvis operator
}

fun main() {
    println(getLength("Hello"))  // Should print 5
    println(getLength(null))      // Should print 0
}
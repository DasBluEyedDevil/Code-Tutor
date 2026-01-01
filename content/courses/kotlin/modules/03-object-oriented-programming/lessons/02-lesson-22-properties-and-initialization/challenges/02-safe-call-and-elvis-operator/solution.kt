fun getLength(text: String?): Int {
    return text?.length ?: 0
}

fun main() {
    println(getLength("Hello"))  // Should print 5
    println(getLength(null))      // Should print 0
}
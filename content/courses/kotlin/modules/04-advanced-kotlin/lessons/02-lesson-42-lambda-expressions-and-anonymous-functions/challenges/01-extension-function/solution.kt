fun String.isPalindrome(): Boolean {
    return this == this.reversed()
}

fun main() {
    println("racecar".isPalindrome())  // Should be true
    println("hello".isPalindrome())    // Should be false
    println("madam".isPalindrome())    // Should be true
}
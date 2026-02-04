fun <T> compose(f: (T) -> T, g: (T) -> T): (T) -> T {
    // Return a function that applies g first, then f
    TODO("Implement compose")
}

fun main() {
    val trimAndUppercase = compose(String::uppercase, String::trim)
    println(trimAndUppercase("  hello world  "))
}

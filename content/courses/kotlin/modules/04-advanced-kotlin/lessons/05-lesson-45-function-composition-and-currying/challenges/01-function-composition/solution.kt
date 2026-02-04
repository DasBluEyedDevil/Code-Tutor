fun <T> compose(f: (T) -> T, g: (T) -> T): (T) -> T {
    return { x -> f(g(x)) }
}

fun main() {
    val trimAndUppercase = compose(String::uppercase, String::trim)
    println(trimAndUppercase("  hello world  "))
}

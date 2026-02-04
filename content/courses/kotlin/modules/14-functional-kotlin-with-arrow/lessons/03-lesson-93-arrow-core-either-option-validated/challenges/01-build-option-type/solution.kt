sealed class Option<out A> {
    data class Some<out A>(val value: A) : Option<A>()
    data object None : Option<Nothing>()

    fun <B> map(transform: (A) -> B): Option<B> = when (this) {
        is Some -> Some(transform(value))
        is None -> None
    }

    fun <B> flatMap(transform: (A) -> Option<B>): Option<B> = when (this) {
        is Some -> transform(value)
        is None -> None
    }

    fun getOrElse(default: () -> @UnsafeVariance A): A = when (this) {
        is Some -> value
        is None -> default()
    }

    fun filter(predicate: (A) -> Boolean): Option<A> = when (this) {
        is Some -> if (predicate(value)) this else None
        is None -> None
    }
}

fun <A> A?.toOption(): Option<A> = if (this != null) Option.Some(this) else Option.None

fun main() {
    val some: Option<Int> = Option.Some(5)
    val none: Option<Int> = Option.None

    // map
    println(some.map { it * 2 })  // Some(10)
    println(none.map { it * 2 })  // None

    // getOrElse
    println("${some.getOrElse { 0 }}, ${none.getOrElse { 0 }}") // 5, 0

    // filter
    println(some.filter { it > 10 }) // None

    // flatMap
    fun safeDivide(a: Int, b: Int): Option<Int> =
        if (b != 0) Option.Some(a / b) else Option.None

    println(some.flatMap { safeDivide(10, it) }) // Some(2)
    println(some.flatMap { safeDivide(10, 0) })  // None

    // Nullable conversion
    val fromNull: Option<String> = null.toOption()
    val fromValue: Option<String> = "hello".toOption()
    println("$fromNull, $fromValue") // None, Some(hello)
}

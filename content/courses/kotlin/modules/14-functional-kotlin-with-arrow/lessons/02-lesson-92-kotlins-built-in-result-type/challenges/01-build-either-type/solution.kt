sealed class Either<out L, out R> {
    data class Left<out L>(val value: L) : Either<L, Nothing>()
    data class Right<out R>(val value: R) : Either<Nothing, R>()

    fun <C> map(transform: (R) -> C): Either<L, C> = when (this) {
        is Left -> this
        is Right -> Right(transform(value))
    }

    fun <C> flatMap(transform: (R) -> Either<@UnsafeVariance L, C>): Either<L, C> = when (this) {
        is Left -> this
        is Right -> transform(value)
    }

    fun <C> fold(onLeft: (L) -> C, onRight: (R) -> C): C = when (this) {
        is Left -> onLeft(value)
        is Right -> onRight(value)
    }
}

fun <L> L.left(): Either<L, Nothing> = Either.Left(this)
fun <R> R.right(): Either<Nothing, R> = Either.Right(this)

fun main() {
    // map on Right
    val right: Either<String, Int> = 3.right()
    println(right.map { it * 2 }) // Right(6)

    // map on Left
    val left: Either<String, Int> = "not found".left()
    println(left.map { it * 2 }) // Left(not found)

    // flatMap chaining
    fun parseInt(s: String): Either<String, Int> =
        s.toIntOrNull()?.right() ?: "not a number".left()

    fun doubleIfPositive(n: Int): Either<String, Int> =
        if (n > 0) (n * 2).right() else "must be positive".left()

    val result = parseInt("21").flatMap { doubleIfPositive(it) }
    println(result) // Right(42)

    // fold
    val message = left.fold(
        onLeft = { "Error: $it" },
        onRight = { "Value: $it" }
    )
    println(message) // Error: not found
}

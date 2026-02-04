// Simplified Either (reuse from previous challenge or copy here)
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
}

fun <L> L.left(): Either<L, Nothing> = Either.Left(this)
fun <R> R.right(): Either<Nothing, R> = Either.Right(this)

// Domain types
sealed interface ValidationError {
    data class InvalidName(val name: String) : ValidationError
    data class InvalidEmail(val email: String) : ValidationError
    data class InvalidAge(val age: Int) : ValidationError
}

data class User(val name: String, val email: String, val age: Int)

// TODO: Implement validation functions
// fun validateName(name: String): Either<ValidationError, String>
//   - Invalid if blank or length < 2
// fun validateEmail(email: String): Either<ValidationError, String>
//   - Invalid if doesn't contain '@'
// fun validateAge(age: Int): Either<ValidationError, Int>
//   - Invalid if < 0 or > 150

// TODO: Implement registerUser that chains all validations
// fun registerUser(name: String, email: String, age: Int): Either<ValidationError, User>

fun main() {
    // Valid input
    println(registerUser("Alice", "alice@example.com", 25))
    // Right(User(name=Alice, email=alice@example.com, age=25))

    // Invalid email -- short-circuits
    println(registerUser("Alice", "not-an-email", 25))
    // Left(InvalidEmail(email=not-an-email))

    // Invalid name
    println(registerUser("", "alice@example.com", 25))
    // Left(InvalidName(name=))

    // Invalid age
    println(registerUser("Alice", "alice@example.com", -5))
    // Left(InvalidAge(age=-5))
}

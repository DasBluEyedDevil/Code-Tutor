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

sealed interface ValidationError {
    data class InvalidName(val name: String) : ValidationError
    data class InvalidEmail(val email: String) : ValidationError
    data class InvalidAge(val age: Int) : ValidationError
}

data class User(val name: String, val email: String, val age: Int)

fun validateName(name: String): Either<ValidationError, String> =
    if (name.isNotBlank() && name.length >= 2) name.right()
    else ValidationError.InvalidName(name).left()

fun validateEmail(email: String): Either<ValidationError, String> =
    if ("@" in email) email.right()
    else ValidationError.InvalidEmail(email).left()

fun validateAge(age: Int): Either<ValidationError, Int> =
    if (age in 0..150) age.right()
    else ValidationError.InvalidAge(age).left()

fun registerUser(name: String, email: String, age: Int): Either<ValidationError, User> =
    validateName(name).flatMap { validName ->
        validateEmail(email).flatMap { validEmail ->
            validateAge(age).map { validAge ->
                User(validName, validEmail, validAge)
            }
        }
    }

fun main() {
    println(registerUser("Alice", "alice@example.com", 25))
    println(registerUser("Alice", "not-an-email", 25))
    println(registerUser("", "alice@example.com", 25))
    println(registerUser("Alice", "alice@example.com", -5))
}

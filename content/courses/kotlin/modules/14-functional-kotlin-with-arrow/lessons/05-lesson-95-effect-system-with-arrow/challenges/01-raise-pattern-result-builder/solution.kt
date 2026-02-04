sealed class Either<out L, out R> {
    data class Left<out L>(val value: L) : Either<L, Nothing>()
    data class Right<out R>(val value: R) : Either<Nothing, R>()
}

interface Raise<E> {
    fun raise(error: E): Nothing
}

private class RaiseException(val error: Any?) : Exception()

fun <E, A> either(block: Raise<E>.() -> A): Either<E, A> {
    val raise = object : Raise<E> {
        override fun raise(error: E): Nothing {
            throw RaiseException(error)
        }
    }
    return try {
        Either.Right(block(raise))
    } catch (e: RaiseException) {
        @Suppress("UNCHECKED_CAST")
        Either.Left(e.error as E)
    }
}

// Domain
sealed interface UserError {
    data class InvalidName(val value: String) : UserError
    data class InvalidEmail(val value: String) : UserError
}

data class User(val name: String, val email: String)

fun Raise<UserError>.validateName(name: String): String {
    if (name.isBlank()) raise(UserError.InvalidName(name))
    return name
}

fun Raise<UserError>.validateEmail(email: String): String {
    if ("@" !in email) raise(UserError.InvalidEmail(email))
    return email
}

fun Raise<UserError>.createUser(name: String, email: String): User {
    val validName = validateName(name)
    val validEmail = validateEmail(email)
    return User(validName, validEmail)
}

fun main() {
    val result1 = either<UserError, User> {
        createUser("Alice", "alice@example.com")
    }
    println(result1)

    val result2 = either<UserError, User> {
        createUser("Alice", "bad-email")
    }
    println(result2)
}

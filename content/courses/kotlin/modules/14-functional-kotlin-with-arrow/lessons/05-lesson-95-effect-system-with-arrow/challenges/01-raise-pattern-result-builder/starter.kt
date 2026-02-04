// Simplified Either (same as previous challenges)
sealed class Either<out L, out R> {
    data class Left<out L>(val value: L) : Either<L, Nothing>()
    data class Right<out R>(val value: R) : Either<Nothing, R>()
}

// TODO: Define Raise<E> interface
// interface Raise<E> {
//     fun raise(error: E): Nothing
// }

// TODO: Implement the 'either' builder function
// fun <E, A> either(block: Raise<E>.() -> A): Either<E, A>
//   - Create a Raise<E> implementation that throws a private exception
//   - Wrap block in try/catch
//   - Catch the special exception -> return Left(error)
//   - Normal return -> return Right(result)

// Domain
sealed interface UserError {
    data class InvalidName(val value: String) : UserError
    data class InvalidEmail(val value: String) : UserError
}

data class User(val name: String, val email: String)

// TODO: Implement validation functions that use Raise
// fun Raise<UserError>.validateName(name: String): String
//   - if blank, raise(InvalidName)
//   - otherwise return name
//
// fun Raise<UserError>.validateEmail(email: String): String
//   - if no '@', raise(InvalidEmail)
//   - otherwise return email
//
// fun Raise<UserError>.createUser(name: String, email: String): User
//   - Call validateName and validateEmail (imperative style!)
//   - Return User(validName, validEmail)

fun main() {
    // Success
    val result1 = either<UserError, User> {
        createUser("Alice", "alice@example.com")
    }
    println(result1) // Right(User(name=Alice, email=alice@example.com))

    // Failure
    val result2 = either<UserError, User> {
        createUser("Alice", "bad-email")
    }
    println(result2) // Left(InvalidEmail(value=bad-email))
}

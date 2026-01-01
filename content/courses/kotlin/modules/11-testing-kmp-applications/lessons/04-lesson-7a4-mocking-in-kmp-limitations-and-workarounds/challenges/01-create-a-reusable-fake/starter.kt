interface AuthRepository {
    suspend fun login(email: String, password: String): Result<User>
    suspend fun logout()
    suspend fun getCurrentUser(): User?
    fun observeAuthState(): Flow<AuthState>
}

sealed class AuthState {
    object LoggedOut : AuthState()
    data class LoggedIn(val user: User) : AuthState()
    object Loading : AuthState()
}

data class User(val id: String, val email: String, val name: String)

// TODO: Implement FakeAuthRepository that:
// 1. Stores current user in memory
// 2. Has simulateLoginError property to trigger failures
// 3. Has loginDelay property to simulate network delay
// 4. Emits AuthState changes through observeAuthState()
// 5. Has helper methods for test setup

class FakeAuthRepository : AuthRepository {
    // Implement here
}